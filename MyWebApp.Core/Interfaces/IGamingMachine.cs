using MyWebApp.Core.Entities;
using MyWebApp.Core.Entities.Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebApp.Core.Interfaces
{
    public interface IGamingMachine
    {
        IEnumerable<GamingMachine> Get(out int totalRecords, int page = 0, int skip = 10, string filter = "");
        GamingMachine Get(long gamingSerialNumber);
        Result CreateGamingMachine(GamingMachine gamingMachine);
        Result UpdateGamingMachine(GamingMachine gamingMachine);
        Result DeleteGamingMachine(GamingMachine gamingMachine);
    }
}
