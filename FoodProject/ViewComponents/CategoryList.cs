using ProductProject.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ProductProject.ViewComponents
{
    public class CategoryList : ViewComponent
    {
        private readonly Context _context;

        public CategoryList(Context context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var categoryList = _context.Categories.Where(x => x.Status == true).ToList();
            return View(categoryList);
        }
    }
}
