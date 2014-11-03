using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathFinder
{
    public static class GridWriter
    {
        public static void DrawPathOnGrid(int[,] grid, List<Point> path)
        {
            for (int y = 0; y < grid.GetLength(0); y++)
            {
                for (int x = 0; x < grid.GetLength(1); x++)
                {
                    if (path.Any(p => p.IsEqual(x, y)))
                        Console.ForegroundColor = ConsoleColor.Green;
                    else
                        Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(grid[y, x]);
                }
                Console.Write("\n");
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("----------------------------------------------");
        }
    }
}
