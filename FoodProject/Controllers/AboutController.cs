using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductProject.Data;
using ProductProject.Data.Models;
using System;
using System.IO;
using System.Linq;

namespace ProductProject.Controllers
{
    [AllowAnonymous]
    [Authorize(Roles = "Admin")]
    public class AboutController : Controller
    {
        private readonly Context _context;

        public AboutController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var about= _context.Abouts.ToList();
            return View(about);
        }
        [HttpGet]
        public IActionResult AboutUpdate(int id)
        {
            var aboutID = _context.Abouts.Find(id);
            return View(aboutID);
        }
        [HttpPost]
        public IActionResult AboutUpdate(AboutImage p)
        {
            About about = new About();
            if (p.AboutImageURL != null)
            {
                var extension = Path.GetExtension(p.AboutImageURL.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Resimler/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                p.AboutImageURL.CopyTo(stream);
                about.AboutImageURL = newImageName;
            }
            about.AboutID = p.AboutID;
            about.AboutTitle = p.AboutTitle;
            about.AboutText = p.AboutText;
            _context.Abouts.Update(about);
            _context.SaveChanges();
            return RedirectToAction("Index", "About");
        }
    }
}
