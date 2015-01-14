using System;
using System.Collections.Generic;

namespace AStarPathFinder {
    internal class Program {
        private static void Main(string[] args) {

            // 1 = wall
            // 2 = start
            // 3 = end

            int[,] grid = TestGrids.GetGrid(6);

            DrawGrid(grid);

            Point startPoint = FindPoint(grid, 2);
            Point endPoint = FindPoint(grid, 3);

            PathFinder pathFinder = new PathFinder();
            List<Point> path = pathFinder.FindPath(grid, startPoint, endPoint, true);

            Console.WriteLine();

            GridWriter.DrawPathOnGrid(grid, path);

            Console.Read();
        }

        private static Point FindPoint(int[,] grid, int val) {
            for (int y = 0; y < grid.GetLength(0); y++) {
                for (int x = 0; x < grid.GetLength(1); x++) {
                    if (grid[y, x] == val)
                        return new Point {X = x, Y = y};
                }
            }
            throw new Exception("Point with value not found!");
        }

        public static void DrawGrid(int[,] grid) {
            for (int y = 0; y < grid.GetLength(0); y++) {
                for (int x = 0; x < grid.GetLength(1); x++) {
                    Console.Write(grid[y, x]);
                }
                Console.Write("\n");
            }
        }
    }
}
