using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2025.Puzzles
{
    [Puzzle(1)]
    public class Day1 : IPuzzle
    {
        public long SolvePartOne(IEnumerable<string> input)
        {
            var position = 50;
            var zeroes = 0;
            foreach (var line in input.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                var mul = 0;
                if (line[0] == 'L')
                {
                    mul = -1;
                }
                else if (line[0] == 'R')
                {
                    mul = 1;
                }

                var count = int.Parse(line[1..]);
                position += count * mul;
                while (position > 99)
                    position = position - 100;
                while (position < 0)
                    position = position + 100;
                if (position == 0)
                    zeroes++;
            }
            return zeroes;
        }

        public long? SolvePartTwo(IEnumerable<string> input)
        {
            var position = 50;
            var zeroes = 0;
            foreach (var line in input.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                var mul = 0;
                if (line[0] == 'L')
                {
                    mul = -1;
                }
                else if (line[0] == 'R')
                {
                    mul = 1;
                }

                var count = int.Parse(line[1..]);

                // Do the naive approach because I am not sure what's wrong with the good one atm
                for (int i = 0; i < count; i++)
                {
                    position += mul;
                    if (position == -1)
                        position = 99;
                    if (position == 100)
                        position = 0;
                    if (position == 0)
                        zeroes++;
                }

                /*var wasZero = position == 0;
                position += count * mul;
                var wrapped = false;
                while (position > 99)
                {
                    position = position - 100;
                    if (!wasZero)
                    {
                        zeroes++;
                        wasZero = false;
                    }
                    wrapped = true;
                }
                while (position < 0)
                {
                    position = position + 100;
                    if (!wasZero)
                    {
                        zeroes++;
                        wasZero = false;
                    }
                    wrapped = true;
                }

                if (position == 0 && !wrapped)
                    zeroes++;*/
            }
            return zeroes;
        }
    }
}