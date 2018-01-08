using MyWebApp.Core.Interfaces;
using System;


namespace MyWebApp.Core.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification(Func<T, bool> criteria)
        {
            Criteria = criteria;
        }

        public Func<T, bool> Criteria { get; }
    }
}
