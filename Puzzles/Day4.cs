using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode2025.Puzzles
{
    [PuzzleDay(4)]
    public class Day4 : IPuzzle
    {
        public long SolvePartOne(IEnumerable<string> input)
        {
            var (map, coords) = LoadMap(input);

            return FindAccessible(map, coords).Count;
        }

        public long? SolvePartTwo(IEnumerable<string> input)
        {
            var (map, coords) = LoadMap(input);

            var removed = 0;
            List<int> accessible;
            do
            {
                accessible = FindAccessible(map, coords);
                removed += accessible.Count;
                var itemsToRemove = new List<(int X, int Y)>();
                foreach (var roll in accessible)
                {
                    var c = coords[roll];
                    map[c.Y][c.X] = false;
                    itemsToRemove.Add(c);
                }

                foreach(var itr in itemsToRemove)
                {
                    coords.Remove(itr);
                }
                
            }
            while (accessible.Count > 0);

            return removed;
        }

        private (List<List<bool>> map, List<(int X, int Y)> coords) LoadMap(IEnumerable<string> input)
        {
            var map = new List<List<bool>>();
            var coords = new List<(int X, int Y)>();
            int loadY = 0;
            foreach (var line in input.Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                int loadX = 0;
                foreach (var ch in line)
                {
                    if (ch == '@')
                    {
                        coords.Add((loadX, loadY));
                        if (loadY > map.Count - 1)
                        {
                            map.Add(new List<bool>());
                        }

                        map[loadY].Add(true);
                    }
                    else if (ch == '.')
                    {
                        if (loadY > map.Count - 1)
                        {
                            map.Add(new List<bool>());
                        }

                        map[loadY].Add(false);
                    }
                    else
                    {
                        throw new InvalidDataException();
                    }

                    loadX++;
                }

                loadY++;
            }

            return (map, coords);
        }

        private List<int> FindAccessible(List<List<bool>> map, List<(int X, int Y)> coords)
        {
            var accessible = new List<int>();

            for (int index = 0; index < coords.Count; index++)
            {
                var (X, Y) = coords[index];
                var neighbours = 0;
                for (int y = Y - 1; y <= Y + 1; y++)
                {
                    for (int x = X - 1; x <= X + 1; x++)
                    {
                        if (x == X && y == Y)
                            continue;
                        if (x < 0 || y < 0)
                            continue;
                        if (y >= map.Count)
                            continue;
                        if (x >= map[y].Count)
                            continue;

                        if (map[y][x])
                            neighbours++;
                    }
                }

                if (neighbours < 4)
                    accessible.Add(index);
            }

            return accessible;
        }
    }
}