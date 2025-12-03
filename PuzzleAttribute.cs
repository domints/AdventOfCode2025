using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PuzzleAttribute : Attribute
    {
        public int Day { get; set; }
        public PuzzleAttribute(int day)
        {
            this.Day = day;
        }
    }
}