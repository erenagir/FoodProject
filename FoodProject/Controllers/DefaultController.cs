using ProductProject.Data;
using ProductProject.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Linq;

namespace ProductProject.Controllers
{
    [AllowAnonymous]
    [Authorize(Roles = "Admin, Uye")]
    public class DefaultController : Controller
    {
        private readonly Context _context;

        public DefaultController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CategoryDetails(int id)
        {
            ViewBag.ID = id;
            return View();
        }
        [HttpGet]
        public PartialViewResult Subscribe()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Subscribe(Subscribe subscribe)
        {
            _context.Subscribes.Add(subscribe);
            _context.SaveChanges();
            Response.Redirect("/Default/Index", true); // Abone olduktan sonra başka sayfaya gitmemesi için
            return PartialView();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Contact(Contact contact)
        {
            _context.Contacts.Add(contact);
            _context.SaveChanges();
            return RedirectToAction("Index", "Default");
        }
        public PartialViewResult Footer()
        {
            return PartialView();
        }
        public IActionResult About()
        {
            var aboutList = _context.Abouts.ToList();
            return View(aboutList);
        }
        public IActionResult Products()
        {
            var productList = _context.Products.ToList();
            return View(productList);
        }
        public PartialViewResult Slider()
        {
            return PartialView();
        }
        public IActionResult Arama(string p)
        {
            var viewModel = new AramaModel();
            viewModel.AramaKey = p;

            if (!string.IsNullOrEmpty(p))
            {
                var about = _context.Abouts.Where(x => x.AboutTitle!.Contains(p)).ToList();
                var Product = _context.Products.Where(x => x.Name!.Contains(p)).ToList();

                if (Product.Count != 0) // Eğer ürün adı yazılmışsa o ürünün id'sini ViewBag ile taşıyalım
                {
                    var ProductID = _context.Products.Where(x => x.Name!.Contains(p)).FirstOrDefault();
                    ViewBag.fID = ProductID.ProductID;
                }
                viewModel.Abouts = about;
                viewModel.Products = Product;

            }
            return View(viewModel);
        }
        public IActionResult ProductDetails(int id)
        {
            var userName = User.Identity.Name;
            var ProductID = _context.Products.Find(id);
            return View(ProductID);

        }
    }
}
