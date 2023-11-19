
using Assignment_04.Contexts;
using Assignment_04.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment_04.Repositories;

public class ProductRepository : Repo<ProductEntity>
{
    private readonly DataContext _context;

    public ProductRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        return await _context.Products
            .Include(x => x.Category)
            .Include(x => x.Supplier)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProductEntity>> GetProductsByCategoryAsync(string categoryName)
    {
        return await _context.Products
            .Where(p => p.Category.CategoryName == categoryName)
            .ToListAsync();
    }

    public ProductRepository() : base(null)
    {
        // Den här konstruktorn är till för Testet 

    }


}