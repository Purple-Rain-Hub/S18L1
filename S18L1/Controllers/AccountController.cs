using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using S18L1.Models;
using S18L1.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

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

        private bool VerifyAuth()
        {
            return User.Identity.IsAuthenticated;
        }

        public IActionResult RegisterPage()
        {
            if (VerifyAuth())
            {
                return RedirectToAction("Index", "Home");
            }

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

            if (!result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByEmailAsync(newUser.Email);

            if(user == null)
            {
                return RedirectToAction("Index", "Home");
            }

            await _userManager.AddToRoleAsync(user, "Student");

            return RedirectToAction("Index", "Home");
        }


        public IActionResult LoginPage()
        {
            if (VerifyAuth())
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("LoginPage");
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("LoginPage");
            }

            var signinResult = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            if (!signinResult.Succeeded)
            {
                return RedirectToAction("LoginPage");
            }

            var claims = new List<Claim>();

            var nameClaim = new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}");

            var emailClaim = new Claim(ClaimTypes.Email, user.Email);

            claims.Add(nameClaim);
            claims.Add(emailClaim);

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
