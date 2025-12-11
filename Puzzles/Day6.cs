using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2025.Puzzles
{
    [PuzzleDay(6)]
    public class Day6 : IPuzzle
    {
        public long SolvePartOne(IEnumerable<string> input)
        {
            List<List<long>> values = new();
            long sum = 0;
            foreach (var line in input.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                var purified = Regex.Replace(line, @"\s+", " ");
                var split = purified.Trim().Split(' ');
                for (int i = 0; i < split.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(split[i]))
                        continue;

                    if (split[i] == "+")
                    {
                        sum += values[i].Sum();
                    }
                    else if (split[i] == "*")
                    {
                        sum += values[i].Aggregate(1L, (acc, v) => acc * v);
                    }
                    else
                    {
                        if (i >= values.Count)
                        {
                            values.Add(new());
                        }

                        values[i].Add(long.Parse(split[i]));
                    }
                }
            }

            return sum;
        }

        public long? SolvePartTwo(IEnumerable<string> input)
        {
            List<StringBuilder> lines = new();

            // Rotate the table
            foreach (var line in input.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (lines.Count <= i)
                        lines.Add(new StringBuilder());

                    lines[i].Append(line[i]);
                }
            }

            string? operation = null;
            List<long> currentNumbers = new();
            var sum = 0L;
            var compute = () => { return operation == "+" ? currentNumbers.Sum() : currentNumbers.Aggregate(1L, (acc, v) => acc * v); };
            foreach (var line in lines.Select(l => l.ToString()).Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                var s = line;
                if (line.EndsWith('+') || line.EndsWith('*'))
                {
                    if (operation != null)
                    {
                        sum += compute();
                        operation = null;
                        currentNumbers.Clear();
                    }

                    operation = line[^1..];
                    s = line[..^1];
                }

                currentNumbers.Add(long.Parse(s.Trim()));
            }

            if (operation != null)
            {
                sum += compute();
            }

            return sum;
        }
    }
}