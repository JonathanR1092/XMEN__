using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using XMEN.Abstractions.Interfaces;

namespace XMEN.DataAccess
{
    public class DbContext<T> : IDbContext<T> where T : class, IEntity
    {
        private readonly DbSet<T> _items;
        private readonly ApiDbContext _context;

        public DbContext(ApiDbContext context)
        {
            _context = context;
            _items = context.Set<T>();
        }

        public void Delete(int id)
        {
            var entity = ListById(id);
            if (entity != null)
            {
                _items.Remove(entity);
                _context.SaveChanges();
            }
        }

        public IList<T> List()
        {
            return _items.ToList();
        }

        public T ListById(int id)
        {
            return _items.FirstOrDefault(i => i.Id.Equals(id));
        }

        public T Save(T entity)
        {
            _items.Add(entity);
            _context.SaveChanges();
            return entity;
        }
    }
}