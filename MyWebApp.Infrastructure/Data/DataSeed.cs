using MyWebApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyWebApp.Infrastructure.Data
{
    public static class DataSeed
    {
        public static void Seed()
        {
            var repository = new InMemoryRepository<GamingMachine>();
            var gamingMachines = GetData();

            foreach (var gamingMachine in gamingMachines)
            {
                repository.Add(gamingMachine);
            }
        }

        static IEnumerable<GamingMachine> GetData()
        {
            var result = new List<GamingMachine>();
            int nameIndex = 1;

            for (int i = 1; i <= 100; i++)
            {
                if (i % 25 == 0)
                    nameIndex = i;

                result.Add(new GamingMachine()
                {
                    GamingSerialNumber = i,
                    GamingMachinePosition = i,
                    GameName = "GameName_" + nameIndex.ToString()
                });
            }

            return result;
        }
    }
}
