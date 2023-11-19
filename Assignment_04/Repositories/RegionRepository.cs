using Assignment_04.Contexts;
using Assignment_04.Entities;

namespace Assignment_04.Repositories
{
    public class RegionRepository : Repo<RegionEntity>
    {
        private readonly DataContext _context;
        public RegionRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}
