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
                    if (id >= range.Start && id <= range.End)
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
            var count = 0L;
            var counted = new List<Range>();
            foreach (var range in ranges)
            {
                List<Range> leftRanges = [range];
                foreach (var ct in counted)
                {
                    List<Range> step = new();
                    bool changed = false;
                    foreach (var left in leftRanges)
                    {
                        if (!Overlap(left, ct))
                        {
                            step.Add(left);
                            continue;
                        }

                        changed = true;

                        step.AddRange(p1_excluding_p2(left, ct));
                    }

                    if (changed)
                        leftRanges = step;
                }

                foreach (var r in leftRanges)
                {
                    count += r.End - r.Start + 1;
                    counted.Add(r);
                }
            }

            return count;
        }

        private List<Range> LoadRanges(IEnumerator<string> input)
        {
            List<Range> ranges = [];
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

        private bool Overlap(Range x, Range y)
        {
            return x.Start <= y.End && y.Start <= x.End;
        }

        private List<Range> p1_excluding_p2(Range p1, Range p2)
        {
            if (p1.End < p2.Start) return [p1]; // line p1 finishes before the exclusion line p2
            if (p1.Start > p2.End) return [p1]; // line p1 starts after exclusion line p1
            List<Range> lines = [];
            // calculate p1 before p2 starts
            var line1 = new Range(p1.Start, Math.Min(p1.End, p2.Start - 1));
            if (line1.Start <= line1.End) lines.Add(line1);
            // calculate p1 after p2 ends
            var line2 = new Range(p2.End + 1, p1.End);
            if (line2.Start <= line2.End) lines.Add(line2);
            // these contain the lines we calculated above
            return lines;
        }

        record Range(long Start, long End)
        {
            public static implicit operator Range((long start, long end) v) => new Range(v.start, v.end);
        };
    }
}