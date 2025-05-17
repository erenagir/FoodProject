using ProductProject.Data;
using ProductProject.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProductProject.Controllers
{
    [AllowAnonymous]
    [Authorize(Roles = "Admin,Uye")]
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly Context _context;

        public LoginController(SignInManager<AppUser> signInManager, Context context)
        {
            _signInManager = signInManager;
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Index(UserSignInViewModel p)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(p.username, p.password, false, true);
                if (result.Succeeded)
                {
                    var name = _context.Users
                        .Where(x => x.UserName == p.username)
                        .Select(y => y.NameSurname)
                        .FirstOrDefault();

                    var userId = _context.Users
                        .Where(x => x.UserName == p.username)
                        .Select(y => y.Id)
                        .FirstOrDefault();

                    var userRoleId = _context.UserRoles
                        .Where(x => x.UserId == userId)
                        .Select(y => y.RoleId)
                        .FirstOrDefault();

                    var roleName = _context.Roles
                        .Where(x => x.Id == userRoleId)
                        .Select(y => y.Name)
                        .FirstOrDefault();

                    if (roleName == "Admin")
                    {
                        return RedirectToAction("Statistics", "Chart");
                    }
                    else if (roleName == "Uye")
                    {
                        return RedirectToAction("Index", "Default");
                    }
                }
                else
                {
                    return RedirectToAction("Index", "Login");
                }
            }

            return RedirectToAction("Index", "Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Category");
        }
    }
}
