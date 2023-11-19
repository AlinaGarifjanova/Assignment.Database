
using Assignment_04.Contexts;
using Assignment_04.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Assignment_04.Repositories
{
    public class OrderRepository : Repo<OrderEntity>
    {
        private readonly DataContext _context;
        public OrderRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<OrderEntity>> GetAllAsync()
        {
            var orders = await _context.Orders
                 .Include(o => o.Employee)
                 .Include(x => x.OrderDetails)
                 .ThenInclude(p => p.Product)
                 .ThenInclude(c => c.Category)
                 .ToListAsync();
            return orders;
        }

        public override async Task<OrderEntity> GetAsync(Expression<Func<OrderEntity, bool>> expression)
        {
            var order = await _context.Orders
                .Include(o => o.Employee)
                .Include(x => x.OrderDetails)
                .ThenInclude(p => p.Product)
                .ThenInclude(c => c.Category)
                .FirstOrDefaultAsync(expression);

            if (order != null)
            {
                return order;
            }
            return null!;
        }

    }

}
