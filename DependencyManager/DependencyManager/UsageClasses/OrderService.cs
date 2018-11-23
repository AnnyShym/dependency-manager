using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyManager.Classes
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepository orderRepository;


        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }


        public int Create(Order order)
        {
            int orderId = orderRepository.Insert(order);
            return orderId;
        }

    }
}
