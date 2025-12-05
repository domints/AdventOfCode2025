using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2025.Puzzles
{
    [PuzzleDay(5)]
    public class Day5 : IPuzzle
    {
        public long SolvePartOne(IEnumerable<string> input)
        {
            var inputEnumerator = input.GetEnumerator();
            var ranges = LoadRanges(inputEnumerator);
            var ids = LoadIds(inputEnumerator);
            var count = 0L;
            foreach (var id in ids)
            {
                foreach (var range in ranges)
                {
                    if (id >= range.start && id <= range.end)
                    {
                        count++;
                        break;
                    }
                }
            }

            return count;
        }

        public long? SolvePartTwo(IEnumerable<string> input)
        {
            var inputEnumerator = input.GetEnumerator();
            var ranges = LoadRanges(inputEnumerator);
            var set = new HashSet<long>();
            foreach (var range in ranges)
            {
                for (long i = range.start; i <= range.end; i++)
                    set.Add(i);
            }

            return set.Count;
        }

        private List<(long start, long end)> LoadRanges(IEnumerator<string> input)
        {
            List<(long start, long end)> ranges = [];
            while (input.MoveNext() && !string.IsNullOrWhiteSpace(input.Current))
            {
                var line = input.Current;
                var values = line.Split('-');
                ranges.Add((long.Parse(values[0]), long.Parse(values[1])));
            }

            return ranges;
        }

        private List<long> LoadIds(IEnumerator<string> input)
        {
            List<long> ids = [];
            while (input.MoveNext() && !string.IsNullOrWhiteSpace(input.Current))
            {
                ids.Add(long.Parse(input.Current));
            }

            return ids;
        }
    }
}