using System.Collections.Generic;
using XMEN.Abstractions.Interfaces;
using XMEN.Application.Interfaces;
using XMEN.Repository.Interfaces;

namespace XMEN.Application.Application
{
    public class Application<T> : IApplication<T> where T : IEntity
    {
        private readonly IRepository<T> _repository;

        public Application(IRepository<T> repository)
        {
            _repository = repository;
        }

        public void Delete(int id)
        {
            _repository.Delete(id);
        }

        public IList<T> List()
        {
            return _repository.List();
        }

        public T ListById(int id)
        {
            return _repository.ListById(id);
        }

        public T Save(T entity)
        {
            return _repository.Save(entity);
        }
    }
}