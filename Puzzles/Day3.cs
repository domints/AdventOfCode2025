using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2025.Puzzles
{
    [PuzzleDay(3)]
    public class Day3 : IPuzzle
    {
        public long SolvePartOne(IEnumerable<string> input)
        {
            var sum = 0L;
            foreach (var line in input.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                /*char biggestTensDigit = '0';
                int biggestTensDigitPos = -1;
                for (int i = 0; i < line.Length - 1; i++)
                {
                    if (line[i] > biggestTensDigit)
                    {
                        biggestTensDigit = line[i];
                        biggestTensDigitPos = i;
                    }
                }
                if (biggestTensDigitPos == -1)
                    continue;
                
                char biggestUnitsDigit = '0';
                int biggestUnitsDigitPos = -1;
                for (int i = biggestTensDigitPos + 1; i < line.Length; i++)
                {
                    if (line[i] > biggestUnitsDigit)
                    {
                        biggestUnitsDigit = line[i];
                        biggestUnitsDigitPos = i;
                    }
                }

                var value = new String([biggestTensDigit, biggestUnitsDigit]);
                sum += int.Parse(value);*/
                sum += FindBiggestNumber(line, 2);
            }

            return sum;
        }

        public long? SolvePartTwo(IEnumerable<string> input)
        {
            var sum = 0L;
            foreach (var line in input.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                sum += FindBiggestNumber(line, 12);
            }

            return sum;
        }

        private long FindBiggestNumber(string line, int length)
        {
            List<char> digits = new List<char>();
            int lastIndex = -1;
            for (int l = 0; l < length; l++)
            {
                char biggestDigit = '0';
                var limit = length - (l + 1);
                for (int i = lastIndex + 1; i < line.Length - limit; i++)
                {
                    if (line[i] > biggestDigit)
                    {
                        biggestDigit = line[i];
                        lastIndex = i;
                    }
                }
                digits.Add(biggestDigit);
            }

            return long.Parse(new string(digits.ToArray()));
        }
    }
}