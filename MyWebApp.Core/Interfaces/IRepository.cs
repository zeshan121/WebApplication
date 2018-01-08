using MyWebApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebApp.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Lists all records
        /// </summary>       
        /// <returns>IEnumerable Objects of type BaseEntity</returns>
        IEnumerable<T> ListAll();

        /// <summary>
        /// Lists all records according to given criteria(specification)
        /// </summary>
        /// <param name="spec">ISpecification with criteria intialized</param>
        /// <returns>IEnumerable Objects of type BaseEntity</returns>
        IEnumerable<T> List(ISpecification<T> spec);

        /// <summary>
        /// Insert Object of type BaseEntity
        /// </summary>
        /// <param name="entity">Object of type BaseEntity</param>
        void Add(T entity);

        /// <summary>
        /// Update entity
        /// </summary>
        /// <param name="entity">Object of type BaseEntity</param>
        void Update(T entity);

        /// <summary>
        /// Delete Object of type BaseEntity
        /// </summary>
        /// <param name="entity">Object of type BaseEntity</param>
        void Delete(T entity);
    }
}
