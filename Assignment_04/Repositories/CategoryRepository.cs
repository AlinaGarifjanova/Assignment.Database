using Assignment_04.Contexts;
using Assignment_04.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Repositories
{
    public class CategoryRepository : Repo<CategoryEntity>
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        /*public override async Task<CategoryEntity> UpdateAsync(CategoryEntity entity)
        {
            try
            {
                _context.Set<CategoryEntity>().Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null!;
            }
        }*/

    }
}
