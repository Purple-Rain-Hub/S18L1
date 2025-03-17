using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using S18L1.Models;
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
    }
}
