using ProductProject.Data;
using ProductProject.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProductProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SettingsController : Controller
    {
        private readonly Context _context;

        private readonly UserManager<AppUser> _userManager;

        public SettingsController(UserManager<AppUser> userManager, Context context)
        {
            _userManager = userManager;
            _context = context;

        }

        [HttpGet]
        public IActionResult Index()
        {
            // var values = await _userManager.FindByNameAsync(User.Identity.Name);
            var userName = User.Identity.Name;
            var userID= _context.Users.Where(x=>x.UserName== userName).Select(y=>y.Id).FirstOrDefault();
            var eMail= _context.Users.Where(x => x.Id == userID).Select(y => y.Email).FirstOrDefault();
            var nameSurname= _context.Users.Where(x => x.Id == userID).Select(y => y.NameSurname).FirstOrDefault();

            UserUpdateModel model = new UserUpdateModel();
            model.userid = userID;
            model.email = eMail;
            model.namesurname = nameSurname;
            model.username = userName;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserUpdateModel model)
        {
            var userName = User.Identity.Name;
            var userID = _context.Users.Where(x => x.UserName == userName).Select(y => y.Id).FirstOrDefault();
            model.userid=userID;
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    Id = model.userid,
                    PasswordHash = model.password
                };
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                user.NameSurname = model.namesurname;
                user.UserName = model.username;
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.password);
                user.Email = model.email;
                IdentityResult result = await _userManager.UpdateAsync(user);
            }
            //var values = await _userManager.FindByNameAsync(User.Identity.Name);
            //values.NameSurname = model.namesurname;
            //values.UserName = model.username;
            //values.Email = model.email;
            //var result = await _userManager.UpdateAsync(values);
            return RedirectToAction("Statistics", "Chart");

        }

    }
}
