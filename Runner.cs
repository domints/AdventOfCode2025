using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    public static class Runner
    {
        public static void Run(int? day, string? inputFile)
        {
            if (!day.HasValue && inputFile != null)
            {
                Console.WriteLine("You can't provide single input for all puzzles!");
                return;
            }

            var puzzlesToRun = GetPuzzleClasses(Assembly.GetCallingAssembly())
                .Where(p => !day.HasValue || day.Value == GetDayOfPuzzle(p));

            foreach (var puzzleType in puzzlesToRun)
            {
                var inst = System.Activator.CreateInstance(puzzleType);
                var puzzle = inst as IPuzzle;
                if (puzzle == null)
                    continue;
                var currDay = GetDayOfPuzzle(puzzleType);
                Console.Write($"Solving puzzle for day {currDay}.");
                var inputPath = $"Input/day{currDay}.txt";
                if (!string.IsNullOrWhiteSpace(inputFile))
                {
                    inputPath = inputFile;
                }
                var input = File.ReadLines(inputPath);
                var result1 = puzzle.SolvePartOne(input);
                Console.Write($" Solution for part 1: {result1}");
                var result2 = puzzle.SolvePartTwo(input);
                if (result2.HasValue)
                {
                    Console.WriteLine($", solution for part 2: {result2}");
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }

        private static IEnumerable<Type> GetPuzzleClasses(Assembly assembly)
        {
            foreach (Type type in assembly.GetTypes())
            {
                if (type.GetCustomAttributes(typeof(PuzzleAttribute), true).Length > 0 && typeof(IPuzzle).IsAssignableFrom(type))
                {
                    yield return type;
                }
            }
        }

        private static int? GetDayOfPuzzle(Type puzzleType)
        {
            var puzzleAttribute = puzzleType.GetCustomAttribute<PuzzleAttribute>();
            if (puzzleAttribute == null)
                return null;

            return puzzleAttribute.Day;
        }
    }
}