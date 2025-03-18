using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using S18L1.Models;
using S18L1.ViewModels;
using S18L1.Services;

namespace S18L1.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeService _homeService;

        public HomeController(HomeService homeService)
        {
            _homeService = homeService;
        }

        public IActionResult Index() 
        { 
        return View();
        }

        [HttpGet("students/get-all")]
        public async Task<IActionResult> ListStudents()
        {
            var studentsList = await _homeService.GetAllStudents();
            return PartialView("_StudentsList", studentsList);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await _homeService.GetStudentById(id);

            EditViewModel studentEdit = new()
            {
                Id = id,
                Name = student.Name,
                Surname = student.Surname,
                BirthDate = student.BirthDate,
                Email= student.Email,
            };

            return PartialView("_StudentEdit", studentEdit);
        }

        [HttpPost("Home/Edit/SaveEdit")]
        public async Task<IActionResult> SaveEdit(EditViewModel editViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    message = "Errore nel modello del form",
                    success = false
                });

            }

            var result = await _homeService.UpdateStudent(editViewModel);

            return Json(new
            {
                success = result,
                message = result ? "Update success" : "Error in the entity update on Db"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _homeService.DeleteStudent(id);

            return Json(new
            {
                success = result,
                message = result ? "Delete success" : "Error in the entity delete on Db"
            });
        }
    }
}
