using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using S18L1.Models;
using S18L1.ViewModels;

namespace S18L1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult RegisterPage()
        {
            return View();
        }

        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var newUser = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email,
                BirthDate = model.BirthDate
            };

            var result = await _userManager.CreateAsync(newUser, model.Password);

            return RedirectToAction("Index", "Home");
        }
    }
}
