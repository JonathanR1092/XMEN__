using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using XMEN.Application.Interfaces;
using XMEN.Entities;
using XMEN.Entities.DTOs;
using XMEN.Services.Interfaces;
using XMEN.WebApi.DTOs;

namespace XMEN.WebApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class MutantController : ControllerBase
    {
        private readonly IApplication<ResultXmen> _result;
        private readonly IMutant _moduleMutant;

        public MutantController(IApplication<ResultXmen> result, IMutant mutant)
        {
            _result = result;
            _moduleMutant = mutant;
        }

        [HttpGet]
        [Route("Stats")]
        public IActionResult Get()
        {
            StatsDto stats = _moduleMutant.GetStats(_result.List());

            return Ok(new StatsResponseDto
            {
                count_mutant_dna = stats.CountMutant,
                count_human_dna = stats.CountHuman,
                ratio = stats.Ratio
            });
        }

        [HttpPost]
        [Route("IsMutant")]
        public IActionResult IsMutant(MutantRequestDto mutant)
        {
            bool result;
            bool check = _moduleMutant.CheckDna(mutant.Dna, out string error);

            if (check && string.IsNullOrEmpty(error))
            {
                IList<ResultXmen> listResult = _result.List();

                if (!listResult.Any(x => x.Dna.ToUpper().Equals(string.Concat(mutant.Dna).ToUpper())))
                {
                    result = _moduleMutant.IsMutant(mutant.Dna);
                }
                else
                {
                    error = "La sequencia de ADN ya fue evaluada";
                    return BadRequest(error);
                }
            }
            else
            {
                return BadRequest(error);
            }

            _result.Save(new ResultXmen { Dna = string.Concat(mutant.Dna), IsMutant = result });

            if (result)
            {
                return Ok();
            }
            else
            {
                return Forbid();
            }
        }
    }
}