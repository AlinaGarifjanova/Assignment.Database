using Assignment_04.Contexts;
using Assignment_04.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Repositories
{
    public class AddressRepository : Repo<AddressEntity>
    {
        private readonly DataContext _context;
        public AddressRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
