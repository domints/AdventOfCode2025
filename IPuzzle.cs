using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    public interface IPuzzle
    {
        long SolvePartOne(IEnumerable<string> input);
        long? SolvePartTwo(IEnumerable<string> input);
    }
}