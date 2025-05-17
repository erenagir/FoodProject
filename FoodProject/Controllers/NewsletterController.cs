using ProductProject.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;

namespace ProductProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NewsletterController : Controller
    {
        private readonly Context _context;

        public NewsletterController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var subscribeMails = _context.Subscribes.ToList();
            return View(subscribeMails);
        }

        public IActionResult NewsletterDelete(int id)
        {
            var subscribeID = _context.Subscribes.Find(id);
            _context.Subscribes.Remove(subscribeID);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
