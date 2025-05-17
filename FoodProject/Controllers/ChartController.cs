using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductProject.Data;
using ProductProject.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace ProductProject.Controllers
{
	 
    [Authorize(Roles = "Admin")]
    [AllowAnonymous]// Bu controller'in yetkilendirilmeden muaf tutulması için yazdık
    public class ChartController : Controller
    {
        private readonly Context _context;

        public ChartController(Context context)
        {
            _context = context;
        }
        // Static Google Chart
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index2()
        {
            return View();
        }
        public IActionResult VisualizeProductResult()
        {
            return Json(ProList());
        }
        public List<Class1> ProList()
        {
            List<Class1> cs=new List<Class1>();
            cs.Add(new Class1()
            {
                productName="Computer",
                stock=150
            });
            cs.Add(new Class1()
            {
                productName = "LCD",
                stock = 75
            });
            cs.Add(new Class1()
            {
                productName = "USB Disk",
                stock = 220
            });
            return cs;
        }


        // Dynamic Google Chart (Products)
        public IActionResult Index3()
        {
            return View();
        }
        public IActionResult VisualizeFoodResult()
        {
            return Json(ProductList());
        }
        public List<Class2> ProductList()
        {
            List<Class2> cs2=new List<Class2>();
           
                cs2 = _context.Products.Select(x => new Class2
                {
                    ProductName = x.Name,
                    stock = x.Stock
                }).ToList();
            
            return cs2;
        }



        // Statistics
        public IActionResult Statistics()
        {

            var ProductCount = _context.Products.Count();
            ViewBag.fCount = ProductCount;

            var categoryCount = _context.Categories.Count();
            ViewBag.categoriCount = categoryCount;

            //var fruitCount = _context.Products.Where(x => x.Category.CategoryName == "Meyveler" || x.Category.CategoryName == "meyveler").Count();
            //ViewBag.meyveCount = fruitCount;

            var vID = _context.Categories.Where(x => x.CategoryName.ToLower() == "sebzeler").Select(y => y.CategoryID).FirstOrDefault();
            var vegetableCount = _context.Products.Where(x => x.CategoryID == vID).Count();
            ViewBag.sebzeCount = vegetableCount;

            var orderCount = _context.Payments.Count();
            ViewBag.ordercount = orderCount;

            var ProductSum = _context.Products.Sum(x => x.Stock);
            ViewBag.fSum = ProductSum;

            var userCount = _context.Users.Count();
            ViewBag.userCount = userCount;

            var maxStockProduct = _context.Products.OrderByDescending(x => x.Stock).Select(y => y.Name).FirstOrDefault(); // FirstOrDefault ile sadece ilk sıradakinin Name'ini çekeceğiz
            ViewBag.maxfStock=maxStockProduct;

            var minStockProduct = _context.Products.OrderBy(x => x.Stock).Select(y => y.Name).FirstOrDefault(); // OrderBy ile varsayılan olarak ascending sıralayacağı için yine ilk Product'u seçtik.
            ViewBag.minfStock = minStockProduct;

            var ProductPriceAverage=_context.Products.Average(x=>x.Price).ToString("0.00");
            ViewBag.ProductPriceAvg = ProductPriceAverage;

            var fruitID = _context.Categories.Where(x => x.CategoryName.ToLower() == "meyveler").Select(y => y.CategoryID).FirstOrDefault();
            var fruitSum = _context.Products.Where(y=>y.CategoryID==fruitID).Sum(x => x.Stock);
            ViewBag.toplamFruit = fruitSum;

            var vegetableID = _context.Categories.Where(x => x.CategoryName.ToLower() == "sebzeler").Select(y => y.CategoryID).FirstOrDefault();
            var vegetableSum = _context.Products.Where(y => y.CategoryID == vegetableID).Sum(x => x.Stock);
            ViewBag.toplamVegetable = vegetableSum;

            var maxPriceProduct = _context.Products.OrderByDescending(x => x.Price).Select(y => y.Name).FirstOrDefault();
            ViewBag.maxFiyatProduct= maxPriceProduct;

            return View();
        }
    }
}
