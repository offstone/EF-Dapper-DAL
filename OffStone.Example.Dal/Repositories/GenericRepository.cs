using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using OffStone.Example.Dal.Entities;

namespace OffStone.Example.DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly NorthwindContext _context;
        public GenericRepository(NorthwindContext context)
        {
            _context = context ??
                throw new ArgumentNullException(nameof(context));
        }

        public void Add(T entity)
        {
            var ent =_context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression);
        }

        public bool Exists(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).Any();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }
        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
