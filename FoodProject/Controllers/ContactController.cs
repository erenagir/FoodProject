using ProductProject.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Linq;

namespace ProductProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        private readonly Context _context;

        public ContactController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var contact = _context.Contacts.ToList();
            return View(contact);
        }
        public IActionResult ContactDelete(int id)
        {
            var contactID = _context.Contacts.Find(id);
            _context.Contacts.Remove(contactID);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult ContactDetails(int id)
        {
            var contactID = _context.Contacts.Find(id);
            return View(contactID);
        }

    }
}
