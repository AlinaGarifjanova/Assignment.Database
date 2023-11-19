using Assignment_04.Contexts;
using Assignment_04.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Repositories
{
    public class OrderDetailsRepository : Repo<OrderDetailsEntity>
    {
        private readonly DataContext _context;
        public OrderDetailsRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<OrderDetailsEntity>> GetAllAsync()
        {
            return await _context.OrderDetails
                .Include(x => x.Order)
                .Include(x => x.Product)
                .ToListAsync();
        }


        public OrderDetailsEntity GetOrderByIdWithDetails(int orderId)
        {
            return _context.OrderDetails
                .Include(od => od.Order)
                .Include(od => od.Product)
                .FirstOrDefault(od => od.OrderId == orderId)
                ?? throw new InvalidOperationException("Beställningsdetailjerna kunde inte hittas");
        }
    }
}
