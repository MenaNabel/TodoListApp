using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TodoList_Base.Database;

namespace TodoListApp.Repository
{
    public class DeleteRepository<T> : IDeleteRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;
        private readonly ApplicationDbContext _context;

        public DeleteRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public void Delete(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void Delete(params T[] entities)
        {
            _dbSet.RemoveRange(entities);
        }
        public void Delete(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        public bool commit()
        {

            return _context.SaveChanges() > 0 ? true : false;

        }
    }
}