using System.Collections.Generic;
using XMEN.Application.Interfaces;
using XMEN.Entities;
using XMEN.Entities.DTOs;
using XMEN.Services.Services;
using Xunit;

namespace XMEN.Tests
{
    public class MutantValidator
    {
        [Fact]
        public void ValidateValidSequenceDna()
        {
            //Arrange
            var mutant = new Mutant();
            string[] dnaSequence = new[] { "ATGC", "CAGT", "ttag", "aGTc" };

            //Act
            bool isValid = mutant.CheckDna(dnaSequence, out string msgError);

            //Assert
            Assert.True(isValid, msgError);
        }

        [Fact]
        public void ValidateInvalidDimentionSequenceDna()
        {
            //Arrange
            var mutant = new Mutant();
            string[] dnaSequence = new[] { "ATGCCCCG", "CAGT", "ttag", "aGTc" };

            //Act
            bool isValid = mutant.CheckDna(dnaSequence, out string msgError);

            //Assert
            Assert.False(isValid, msgError);
        }

        [Fact]
        public void ValidateInvalidNitrogenousBaseDna()
        {
            //Arrange
            var mutant = new Mutant();
            string[] dnaSequence = new[] { "ATGC", "CAGV", "ttrg", "aGTc" };

            //Act
            bool isValid = mutant.CheckDna(dnaSequence, out string msgError);

            //Assert
            Assert.False(isValid, msgError);
        }

        [Theory]
        [InlineData(new[] { "CAGTG", "ttagC", "AAACG", "aGTcC", "GGCTA" }, true)]
        [InlineData(new[] { "CAGTG", "ttagC", "AAACG", "aGTcC", "GGCTAS" }, false)]
        [InlineData(new[] { "CAGTG", "ttagC", "AAACG", "aGTcC", "GGCTV" }, false)]
        [InlineData(new[] { "CAG", "tta", "AAA" }, false)]
        public void ValidateSequenceDnas(string[] dna, bool expected)
        {
            //Arrange
            var mutant = new Mutant();
            //Act
            bool isValid = mutant.CheckDna(dna, out _);
            //Assert
            Assert.Equal(isValid, expected);
        }

        [Theory]
        [InlineData(new[] { "CAGTG", "ttagC", "AAACG", "aGTcC", "GGCTA" }, false)]
        [InlineData(new[] { "CATT", "ccat", "CCAT", "ttcg" }, false)]
        [InlineData(new[] { "GATTT", "gattC", "GTTTT", "gtccg", "ttttc" }, true)]
        [InlineData(new[] { "CAGTGT", "ccctgg", "CtcGgT", "CCGCGG", "TTTTTG", "gagatc" }, true)]
        [InlineData(new[] { "AGTTGACCCA", "GTTGAACTTA", "GCCTTGGATG", "CGAAACGTGC", "AGCTAGCAAA",
                            "TTAGAATGTG", "TAATAGTCGG", "GCCTCGCGGT", "AGGAAAGCAG", "ATGTTTTCGA" }, true)]
        public void ValidateIsMutant(string[] dna, bool expected)
        {
            //Arrange
            var mutant = new Mutant();
            //Act
            bool isValid = mutant.IsMutant(dna);
            //Assert
            Assert.Equal(isValid, expected);
        }

        [Fact]
        public void ValidateGetStats()
        {
            //Arrange
            var mutant = new Mutant();
            List<ResultXmen> result = new()
            {
                new ResultXmen { Id = 1, Dna = "GATTTgattCGTTTTgtccgttttc", IsMutant = true },
                new ResultXmen { Id = 2, Dna = "CATTccatCCATttcg", IsMutant = false },
                new ResultXmen { Id = 3, Dna = "CAGTGttagCAAACGaGTcCGGCTA", IsMutant = false }
            };

            //Act
            StatsDto stats = mutant.GetStats(result);

            //Assert
            Assert.True(stats != null);
        }
    }
}