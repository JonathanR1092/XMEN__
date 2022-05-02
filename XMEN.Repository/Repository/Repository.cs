using System.Collections.Generic;
using XMEN.Abstractions.Interfaces;
using XMEN.Repository.Interfaces;

namespace XMEN.Repository.Repository
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        private readonly IDbContext<T> _context;

        public Repository(IDbContext<T> context)
        {
            _context = context;
        }

        public void Delete(int id)
        {
            _context.Delete(id);
        }

        public IList<T> List()
        {
            return _context.List();
        }

        public T ListById(int id)
        {
            return _context.ListById(id);
        }

        public T Save(T entity)
        {
            return _context.Save(entity);
        }
    }
}