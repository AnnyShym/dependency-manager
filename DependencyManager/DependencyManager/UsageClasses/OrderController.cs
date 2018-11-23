using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyManager.Classes
{
    public class OrderController : Controller
    {

        private readonly IOrderService orderService;


        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }


        public ActionResult Create(int productId)
        {
            int orderId = orderService.Create(new Order(productId));


            ViewData["OrderId"] = orderId;


            return View();
        }

    }
}
