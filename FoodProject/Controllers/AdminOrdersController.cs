using ProductProject.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using X.PagedList.Extensions;

namespace ProductProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminOrdersController : Controller
    {
        private readonly Context _context;

        public AdminOrdersController(Context context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            var orders = _context.Payments.ToList();
            return View(orders.ToPagedList(page, 8)); // Her sayfada 8 sipariş olsun
        }
        public IActionResult OrderDetails(int id) 
        {
            var paymentID = _context.Payments.Find(id);
            ViewBag.ID = paymentID.AppUserID;
            return View(paymentID);
        }
        public IActionResult OrderCompleted(int id)
        {
            var paymentID = _context.Payments.Find(id);
            _context.Payments.Remove(paymentID);
            _context.SaveChanges();

            var orderDetailID=_context.OrderDetails.Where(x => x.AppUserID == paymentID.AppUserID).Select(y=>y.OrderDetailID).ToList();
            foreach(var item in orderDetailID)
            {
                var orderIDFind=_context.OrderDetails.Find(item);
                _context.OrderDetails.Remove(orderIDFind);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "AdminOrders");
        }
    }
}
