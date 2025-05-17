using ProductProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ProductProject.ViewComponents
{
    public class ProductGetList : ViewComponent
    {
        private readonly ProductRepository _productRepository;

        public ProductGetList(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IViewComponentResult Invoke()
        {
            var productList = _productRepository.TList().Take(8);
            return View(productList);
        }
    }
}
