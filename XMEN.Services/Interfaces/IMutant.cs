using System.Collections.Generic;
using XMEN.Entities;
using XMEN.Entities.DTOs;

namespace XMEN.Services.Interfaces
{
    public interface IMutant
    {
        bool IsMutant(string[] dnaSequence);

        bool CheckDna(string[] dna, out string messageError);

        StatsDto GetStats(IList<ResultXmen> listResult);
    }
}