using Assignment_04.Entities;
using Assignment_04.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Services
{
    public class OrderDetailsService
    {
        private readonly OrderDetailsRepository _orderDetailsRepo;

        public OrderDetailsService(OrderDetailsRepository orderDetailsRepo)
        {
            _orderDetailsRepo = orderDetailsRepo;
        }

        public async Task<IEnumerable<OrderDetailsEntity>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            try
            {
                var allOrderDetails = await _orderDetailsRepo.GetAllAsync();
                var orderDetailsForOrderId = allOrderDetails.Where(x => x.OrderId == orderId);

                return orderDetailsForOrderId;
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return Enumerable.Empty<OrderDetailsEntity>();
        }
    }
}
