using Microsoft.EntityFrameworkCore;
using S18L1.Data;
using S18L1.Models;

namespace S18L1.Services
{
    public class HomeService
    {
        private readonly S18L1DbContext _context;

        public HomeService(S18L1DbContext context)
        {
            _context = context;
        }

        public async Task<StudentsViewModel> GetAllStudents()
        {
            try
            {
                var studentsList = new StudentsViewModel();

                studentsList.Students = await _context.Students.ToListAsync();

                return studentsList;
            }
            catch
            {
                return new StudentsViewModel() { Students = new List<Student>() };
            }
        }
    }
}
