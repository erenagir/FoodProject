using ProductProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ProductProject.ViewComponents
{
	public class ProductListByCategory : ViewComponent
	{
		public IViewComponentResult Invoke(int id)
		{
			
			ProductRepository ProductRepository = new ProductRepository();
			var ProductList = ProductRepository.List(x => x.CategoryID == id); // gönderilen id'ye göre ilgili kategorideki ürünler listelenecek.
			return View(ProductList);
		}
	}
}
