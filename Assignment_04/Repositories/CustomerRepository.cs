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
    public class CustomerRepository : Repo<CustomerEntity>
    {
        private readonly DataContext _context;

        public CustomerRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<CustomerEntity>> GetAllAsync()
        {
            return await _context.Customers
                .Include(x => x.Address)
                .Include(x => x.CustomerType)
                .ToListAsync();
        }
    }
}
