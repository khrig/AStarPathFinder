using System;
using System.Collections.Generic;
using System.Linq;

namespace AStarPathFinder {
    public static class GridWriter {
        public static void DrawPathOnGrid(int[,] grid, List<Point> path) {
            for (int y = 0; y < grid.GetLength(0); y++) {
                for (int x = 0; x < grid.GetLength(1); x++) {
                    if (path.Any(p => p.IsEqual(x, y)))
                        Console.ForegroundColor = ConsoleColor.Green;

                    if (grid[y, x] == 1)
                        Console.ForegroundColor = ConsoleColor.Blue;

                    Console.Write(grid[y, x]);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                Console.Write("\n");
            }
        }
    }
}
