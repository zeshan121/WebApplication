using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MyWebApp.Core.Interfaces
{        
    public interface ISpecification<T>
    {
        /// <summary>
        /// Filter criteria in form of lamda exression.
        /// </summary>
        Func<T, bool> Criteria { get; }
    }
}
