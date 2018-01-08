using MyWebApp.Core.Entities;
using MyWebApp.Core.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MyWebApp.Infrastructure.Data
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        private static ConcurrentDictionary<long, T> _objectList = 
            new ConcurrentDictionary<long, T>();

        public void Add(T entity)
        {
            _objectList.TryAdd(entity.Key(), entity);            
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            _objectList[entity.Key()] = entity;            
        }

        public IEnumerable<T> List(ISpecification<T> spec)
        {
            return List().Where(spec.Criteria).AsEnumerable();
        }

        public IEnumerable<T> ListAll()
        {
            return List();
        }

        public void Update(T entity)
        {            
            _objectList[entity.Key()] = entity;
        }

        private IEnumerable<T> List()
        {
            return _objectList.Select(o => o.Value).Where(e => e.IsDeleted == false).AsEnumerable();
        }
    }
}
