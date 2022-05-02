namespace XMEN.WebApi.DTOs
{
    public class StatsResponseDto
    {
        public int count_mutant_dna { get; set; }
        public int count_human_dna { get; set; }
        public double ratio { get; set; }
    }
}