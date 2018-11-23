using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyManager.Classes
{
    public static class BootStrapper
    {
        public static void Configure(IContainer container)
        {
            container.Register<OrderController, OrderController>();
            container.Register<IOrderService, OrderService>();
            container.Register<IOrderRepository, OrderRepository>();
        }
    }
}
