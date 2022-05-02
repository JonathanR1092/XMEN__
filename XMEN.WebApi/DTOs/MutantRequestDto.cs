using System.ComponentModel.DataAnnotations;

namespace XMEN.WebApi.DTOs
{
    public class MutantRequestDto
    {
        [Required]
        public string[] Dna { get; set; }
    }
}