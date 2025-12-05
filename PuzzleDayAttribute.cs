using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PuzzleDayAttribute : Attribute
    {
        public int Day { get; set; }
        public PuzzleDayAttribute(int day)
        {
            this.Day = day;
        }
    }
}