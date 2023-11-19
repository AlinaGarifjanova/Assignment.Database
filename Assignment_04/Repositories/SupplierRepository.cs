using Assignment_04.Contexts;
using Assignment_04.Entities;

namespace Assignment_04.Repositories
{
    public class SupplierRepository : Repo<SupplierEntity>
    {
        private readonly DataContext _context;
        public SupplierRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
