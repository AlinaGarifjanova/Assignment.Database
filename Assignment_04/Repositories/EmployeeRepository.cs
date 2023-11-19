using Assignment_04.Contexts;
using Assignment_04.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Repositories
{
    public class EmployeeRepository : Repo<EmployeeEntity>
    {
        private readonly DataContext _context;
        public EmployeeRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EmployeeEntity> GetEmployeeWithDetailsAsync(Expression<Func<EmployeeEntity, bool>> expression)
        {
            var employee = await _context.Employees
                .Include(e => e.Region)
                .Include(e => e.Orders)
                .ThenInclude(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(expression);

            return employee ?? throw new InvalidOperationException("Den anställde kunde inte hittas.");
        }
    }
}
