using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMEN.DataAccess;
using XMEN.Entities;

namespace XMEN.DataAccess.Data
{
    public class InitialData
    {
        public static void Initialize(ApiDbContext dbContext)
        {
            if (dbContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
            {
                return;
            }

            dbContext.Database.EnsureCreated();

            var resultMutant = new ResultXmen[]
            {
                new ResultXmen { Id = 1, Dna = "GATTTgattCGTTTTgtccgttttc", IsMutant = true },
                new ResultXmen { Id = 2, Dna = "CATTccatCCATttcg", IsMutant = false },
                new ResultXmen { Id = 3, Dna = "CAGTGttagCAAACGaGTcCGGCTA", IsMutant = false }
            };

            foreach (ResultXmen result in resultMutant)
            {
                dbContext.resultXmen.Add(result);
            }
        }
    }
}