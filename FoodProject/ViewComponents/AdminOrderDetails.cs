using ProductProject.Data;
using ProductProject.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ProductProject.ViewComponents
{
    public class AdminOrderDetails : ViewComponent
    {
        private readonly Context _context;

        public AdminOrderDetails(Context context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke(int id)
        {
            var userOrders= _context.OrderDetails.Where(x => x.AppUserID == id).ToList();
            return View(userOrders);
        }
    }
}
