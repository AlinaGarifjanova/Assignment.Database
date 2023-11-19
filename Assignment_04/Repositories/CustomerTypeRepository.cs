using Assignment_04.Contexts;
using Assignment_04.Entities;

namespace Assignment_04.Repositories;

public class CustomerTypeRepository : Repo<CustomerTypeEntity>
{
    private readonly DataContext _context;
    public CustomerTypeRepository(DataContext context) : base(context)
    {
        _context = context;
    }
}


