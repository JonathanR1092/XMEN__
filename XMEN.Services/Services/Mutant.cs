using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using XMEN.Entities;
using XMEN.Entities.DTOs;
using XMEN.Services.Interfaces;

namespace XMEN.Services.Services
{
    public class Mutant : IMutant
    {
        public bool IsMutant(string[] dnaSequence)
        {
            bool vertical = false, horizontal = false, oblique = false;
            char[,] dna = buildArray(dnaSequence);
            int size = dna.GetLength(1);
            int sizeArray = size - 1;
            int minObliqueId = GetOblicueId(size);
            int minObliqueInvertId = GetObliqueInvertId(size);
            StringBuilder dnaHorizontal = new();
            StringBuilder dnaVertical = new();

            for (int rowId = 0; rowId < size; rowId++)
            {
                for (int columnId = 0; columnId < size; columnId++)
                {
                    if (!horizontal)
                    {
                        dnaHorizontal.Append(dna.GetValue(rowId, columnId));
                    }

                    if (!vertical)
                    {
                        dnaVertical.Append(dna.GetValue(columnId, rowId));
                    }
                }

                if (!horizontal)
                {
                    horizontal = CheckMutant(dnaHorizontal.ToString());
                }

                if (!vertical)
                {
                    vertical = CheckMutant(dnaVertical.ToString());
                }

                if (!oblique)
                {
                    oblique = SearchSequenceOblique(minObliqueId, minObliqueInvertId, rowId, dna, size, sizeArray);
                }

                if (horizontal && vertical && oblique)
                {
                    return true;
                }
                else
                {
                    dnaHorizontal.Clear();
                    dnaVertical.Clear();
                }
            }

            return false;
        }

        public bool CheckDna(string[] dna, out string messageError)
        {
            int size = dna.Length;
            int rowId = 0;
            messageError = string.Empty;

            if (size >= 4)
            {
                foreach (string row in dna)
                {
                    char[] rowArray = row.ToCharArray();

                    if (rowArray.Length != size || !CheckNitrogenousBase(row))
                    {
                        messageError = "El adn posee un base nitrogenada diferente (A,T,C,G) o no tiene una dimensión NxN ";
                        return false;
                    }
                    rowId++;
                }
            }
            else
            {
                messageError = "El adn no tiene la dimensión minima requerida, mayor o igual a 4 x 4";
                return false;
            }

            return true;
        }

        public StatsDto GetStats(IList<ResultXmen> listResult)
        {
            int countMutant = listResult.Count(x => x.IsMutant);
            int countHuman = listResult.Count(x => !x.IsMutant);

            return new StatsDto
            {
                CountMutant = countMutant,
                CountHuman = countHuman,
                Ratio = (double)countMutant / countHuman
            };
        }

        private char[,] buildArray(string[] dna)
        {
            int size = dna.Length;
            int rowId = 0, columnId = 0;
            char[,] dnaMatrix = new char[size, size];

            foreach (string row in dna)
            {
                char[] rowArray = row.ToCharArray();
                foreach (char charColumn in rowArray)
                {
                    dnaMatrix[rowId, columnId] = charColumn;
                    columnId++;
                }

                columnId = 0;
                rowId++;
            }

            return dnaMatrix;
        }

        private bool CheckMutant(string dnaSequence)
        {
            string[] nitrogrenusBase = new string[4] { "A", "T", "C", "G" };

            foreach (string data in nitrogrenusBase)
            {
                string pattern = $"{data}" + "{4}";
                if (Regex.IsMatch(dnaSequence, pattern, RegexOptions.IgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckNitrogenousBase(string dnaSequence)
        {
            string pattern = @"^[atcg]+$";
            return Regex.IsMatch(dnaSequence, pattern, RegexOptions.IgnoreCase);
        }

        private StringBuilder GetDnaOblique(char[,] dna, int rowId, int size, bool isInvert)
        {
            StringBuilder dnaOblique = new();
            int columnPosition;

            if (isInvert)
            {
                columnPosition = size - 1;
            }
            else
            {
                columnPosition = 0;
            }

            for (int rowPosition = rowId; rowPosition < size; rowPosition++)
            {
                dnaOblique.Append(dna.GetValue(rowPosition, columnPosition));

                if (isInvert)
                {
                    columnPosition--;
                }
                else
                {
                    columnPosition++;
                }
            }

            return dnaOblique;
        }

        private StringBuilder GetInitialDnaOblique(char[,] dna, int columnId, int size, ref int minSize, bool isInvert)
        {
            int columnPosition = columnId;
            StringBuilder dnaOblique = new();

            for (int rowsId = 0; rowsId < minSize; rowsId++)
            {
                dnaOblique.Append(dna.GetValue(rowsId, columnPosition));

                if (!isInvert)
                {
                    columnPosition++;
                }
                else
                {
                    columnPosition--;
                }
            }

            if (minSize < size)
            {
                minSize++;
            }

            return dnaOblique;
        }

        private int GetOblicueId(int size)
        {
            int id = 0;

            for (int columnId = 0; columnId < size; columnId++)
            {
                if (size - columnId <= 4)
                {
                    id = columnId;
                    break;
                }
            }

            return id;
        }

        private int GetObliqueInvertId(int size)
        {
            int id = 0;

            for (int columnId = size; columnId > 0; columnId--)
            {
                if (columnId - 4 < 0)
                {
                    id = columnId;
                    break;
                }
            }

            return id;
        }

        private bool SearchSequenceOblique(int minObliqueId, int minObliqueInvertId, int rowId, char[,] dna, int size, int sizeArray)
        {
            int minSize = 4;

            if (rowId == 0)
            {
                for (int columnId = minObliqueId; columnId >= 0; columnId--)
                {
                    StringBuilder dnaOblique = GetInitialDnaOblique(dna, columnId, size, ref minSize, false);

                    if (CheckMutant(dnaOblique.ToString()))
                    {
                        return true;
                    }

                    dnaOblique.Clear();
                }

                minSize = 4;

                for (int columnId = minObliqueInvertId; columnId <= sizeArray; columnId++)
                {
                    StringBuilder dnaObliqueInvert = GetInitialDnaOblique(dna, columnId, size, ref minSize, true);
                    if (CheckMutant(dnaObliqueInvert.ToString()))
                    {
                        return true;
                    }

                    dnaObliqueInvert.Clear();
                }
            }
            else
            {
                int delimiter = size - rowId;
                if (delimiter >= minSize)
                {
                    StringBuilder dnaOblique = GetDnaOblique(dna, rowId, size, false);
                    StringBuilder dnaObliqueInvert = GetDnaOblique(dna, rowId, size, true);

                    if (CheckMutant(dnaOblique.ToString()) || CheckMutant(dnaObliqueInvert.ToString()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}