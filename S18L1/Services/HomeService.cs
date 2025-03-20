using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using S18L1.Data;
using S18L1.Models;
using S18L1.ViewModels;

namespace S18L1.Services
{
    public class HomeService
    {
        private readonly S18L1DbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeService(S18L1DbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<bool> SaveAsync()
        {
            try
            {
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<StudentsViewModel> GetAllStudents()
        {
            try
            {
                var studentsList = new StudentsViewModel();

                studentsList.Students = await _context.Students.Include(s => s.User).ToListAsync();

                return studentsList;
            }
            catch
            {
                return new StudentsViewModel() { Students = new List<Student>() };
            }
        }

        public async Task<Student> GetStudentById(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return null;
            }

            return student;
        }

        public async Task<bool> UpdateStudent(EditViewModel editViewModel)
        {
            var student = await _context.Students.FindAsync(editViewModel.Id);
            if (student == null)
            {
                return false;
            }

            student.Id = editViewModel.Id;
            student.Name = editViewModel.Name;
            student.Surname = editViewModel.Surname;
            student.BirthDate = editViewModel.BirthDate;
            student.Email = editViewModel.Email;

            return await SaveAsync();
        }

        public async Task<bool> DeleteStudent(Guid id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return false;
            }

            _context.Students.Remove(student);
            return await SaveAsync();
        }

        public async Task<bool> AddStudent(AddViewModel addViewModel, ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.FindByEmailAsync(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value);

            var student = new Student()
            {
                Id = Guid.NewGuid(),
                Name = addViewModel.Name,
                Surname = addViewModel.Surname,
                BirthDate = addViewModel.BirthDate,
                Email = addViewModel.Email,
                UserId = user.Id
            };

            _context.Students.Add(student);

            return await SaveAsync();
        }
    }
}
