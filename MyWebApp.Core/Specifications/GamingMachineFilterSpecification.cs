using MyWebApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebApp.Core.Specifications
{
    public class GamingMachineFilterSpecification : BaseSpecification<GamingMachine>
    {
        public GamingMachineFilterSpecification(long gamingSerialNumber)
            :base(g => g.GamingSerialNumber == gamingSerialNumber)
        {            
        }

        public GamingMachineFilterSpecification(string gameName)
            : base(g => String.Equals(g.GameName, gameName,
                   StringComparison.OrdinalIgnoreCase))
        {
        }
    }
}
