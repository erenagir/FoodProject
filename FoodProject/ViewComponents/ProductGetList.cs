using ProductProject.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ProductProject.ViewComponents
{
	public class ProductGetList:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			ProductRepository ProductRepository = new ProductRepository();
			var ProductList = ProductRepository.TList().Take(8);
			return View(ProductList);
		}
	}
}
