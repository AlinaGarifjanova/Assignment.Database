using Assignment_04.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Repositories
{
    public abstract class Repo<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        protected Repo(DataContext context)
        {
            _context = context;
        }
        // Skapa
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            try
            {
                await _context.Set<TEntity>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"An ennor occured while creating entity {ex.Message} ");

                if (ex.InnerException != null)
                {
                    Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }

                throw; // rethrow the exception after logging
            }
        }

        // Visa en specifik
        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
                return entity!;

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;
        }

        //Visa alla 
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();

        }
        
        //Uppdatera
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            try
            {
                _context.Set<TEntity>().Update(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return null!;

        }

        // Ta bort
        public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
                if (entity != null)
                {
                    _context.Set<TEntity>().Remove(entity);
                    await _context.SaveChangesAsync();
                    return true;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }

            return false;
        }

        // Kollar att den finns 
        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> expression)
        {
            try
            {
                var exists = await _context.Set<TEntity>().AnyAsync(expression);
                return exists!;
            }
            catch (Exception ex) { Debug.Write(ex.Message); }

            return false!;
        }
    }

}
