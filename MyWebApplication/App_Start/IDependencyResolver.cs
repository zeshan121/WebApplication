using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApp.Web.App_Start
{
    public interface IDependencyResolver : IDependencyScope, IDisposable
    {
        IDependencyScope BeginScope();
    }
}
