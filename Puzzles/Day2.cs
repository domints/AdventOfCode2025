using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2025.Puzzles
{
    [Puzzle(2)]
    public class Day2 : IPuzzle
    {
        public long SolvePartOne(IEnumerable<string> input)
        {
            var line = input.First();
            var ranges = line.Split(',');
            var sum = 0L;
            foreach (var range in ranges)
            {
                var borders = range.Split('-');
                var start = long.Parse(borders[0]);
                var end = long.Parse(borders[1]);
                for (long i = start; i <= end; i++)
                {
                    var str = i.ToString();
                    if (str.Length % 2 == 1)
                        continue;
                    var half = str.Length / 2;
                    if (str[0..half] == str[(half)..])
                        sum += i;
                }
            }
            return sum;
        }

        public long? SolvePartTwo(IEnumerable<string> input)
        {
            var line = input.First();
            var ranges = line.Split(',');
            var sum = 0L;
            foreach (var range in ranges)
            {
                var borders = range.Split('-');
                var start = long.Parse(borders[0]);
                var end = long.Parse(borders[1]);
                for (long i = start; i <= end; i++)
                {
                    var str = i.ToString();
                    var added = false;
                    for (int s = 2; s <= str.Length && !added; s++)
                    {
                        if (str.Length % s != 0)
                            continue;
                        var chunkL = str.Length / s;
                        var substr = str[0..chunkL];
                        var equal = true;
                        for (int x = 1; x < s; x++)
                        {
                            var nextsubstr = str[(x * chunkL)..(x * chunkL + chunkL)];
                            if (nextsubstr != substr)
                            {
                                equal = false;
                                break;
                            }
                        }

                        if (equal)
                        {
                            sum += i;
                            added = true;
                            continue;
                        }
                    }
                }
            }
            
            return sum;
        }
    }
}