using ProductProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ProductProject.ViewComponents
{
    public class ProductListByCategory : ViewComponent
    {
        private readonly ProductRepository _productRepository;

        public ProductListByCategory(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IViewComponentResult Invoke(int id)
        {
            var productList = _productRepository.List(x => x.CategoryID == id);
            return View(productList);
        }
    }
}
