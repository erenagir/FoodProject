using ProductProject.Data;
using ProductProject.Data.Models;
using ProductProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using X.PagedList;
using X.PagedList.Extensions;

namespace ProductProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        ProductRepository ProductRepository = new ProductRepository();
        Context context = new Context();
        public IActionResult Index(int page = 1)
        {
            return View(ProductRepository.TList("Category").ToPagedList(page, 4)); // Sayfalama 1. sayfadan başlayıp her sayfada 4 veri olsun. // İlgili yiyeceğin kategori adını getirebilmek için
        }
        [HttpGet]
        public IActionResult ProductAdd()
        {
            List<SelectListItem> values = (from x in context.Categories.Where(x=>x.Status==true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.categories = values;
            return View();
        }
        [HttpPost]
        public IActionResult ProductAdd(ProductImage p)
        {
            Product Product = new Product();
            if (p.ImageURL != null)
            {
                var extension = Path.GetExtension(p.ImageURL.FileName);
                var newImageName = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Resimler/", newImageName);
                var stream = new FileStream(location, FileMode.Create);
                p.ImageURL.CopyTo(stream);
                Product.ImageURL = newImageName;
            }
            Product.Name = p.Name;
            Product.Price = p.Price;
            Product.Stock = p.Stock;
            Product.Description = p.Description;
            Product.CategoryID = p.CategoryID;
            ProductRepository.TAdd(Product);
            return RedirectToAction("ProductAdd");
        }
        public IActionResult ProductDelete(int id)
        {
            ProductRepository.TDelete(new Product { ProductID = id });
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult ProductUpdate(int id)
        {
            ProductImage ProductImage= new ProductImage();
            // Verileri dropdownlist'e taşıma
            List<SelectListItem> values = (from y in context.Categories.Where(x => x.Status == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = y.CategoryName,
                                               Value = y.CategoryID.ToString()
                                           }).ToList();
            ViewBag.categories = values;



            var x = ProductRepository.TGet(id);
            return View(x);
        }
        [HttpPost]
        public IActionResult ProductUpdate(ProductImage p)
        {
            Product Product = new Product();
            if (ModelState.IsValid)
            {
                if (p.ImageURL != null)
                {
                   
                    var extension = Path.GetExtension(p.ImageURL.FileName);
                    var newImageName = Guid.NewGuid() + extension;
                    var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Resimler/", newImageName);
                    var stream = new FileStream(location, FileMode.Create);
                    p.ImageURL.CopyTo(stream);
                    p.ImageName= newImageName;
                }

                Product.ProductID = p.ProductID;
                Product.Name = p.Name;
                Product.Stock = p.Stock;
                Product.Price = p.Price;
                Product.Description = p.Description;
                Product.ImageURL = p.ImageName;
                Product.CategoryID = p.CategoryID;
                ProductRepository.TUpdate(Product);
            }
            return RedirectToAction("Index", "Product");
        }
        public IActionResult ProductDetails(int id)
        {
            var ProductID = ProductRepository.TGet(id);
            var categoryID = ProductID.CategoryID;
            var categoryName = context.Categories.Where(x => x.CategoryID == categoryID).Select(y => y.CategoryName).FirstOrDefault();
            ViewBag.categoryName = categoryName;
            return View(ProductID);
        }
    }
}
