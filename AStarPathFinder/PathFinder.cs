using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AStarPathFinder {
    public class PathFinder {
        private List<Point> open = new List<Point>();
        private List<Point> closed = new List<Point>();
        private List<Point> path = new List<Point>();

        private int[,] grid;
        private Point end;

        private bool isDiagonalMovesEnabled;

        public List<Point> FindPath(int[,] grid, Point start, Point end, bool enableDiagonalMoves) {
            open = new List<Point>();
            closed = new List<Point>();
            path = new List<Point>();

            isDiagonalMovesEnabled = enableDiagonalMoves;

            this.grid = grid;
            this.end = end;
            open.Add(start);
            FindPath(GetLowestCostPoint());
            return path;
        }

        private void FindPath(Point start) {
            open.Remove(start);
            closed.Add(start);
            if (end.IsEqual(start.X, start.Y)) {
                SavePath(start);
                return;
            }

            open.AddRange(GetAllAdjacent(start));

            if (!open.Any())
                return;

            FindPath(GetLowestCostPoint());
        }

        private void SavePath(Point end) {
            Point parent = end;
            while (parent != null) {
                path.Add(parent);
                parent = parent.Parent;
            }
        }

        private Point GetLowestCostPoint() {
            Point currentPoint = open.First();
            foreach (Point p in open) {
                if (p.FScore < currentPoint.FScore) {
                    currentPoint = p;
                }
            }
            return currentPoint;
        }

        private List<Point> GetAllAdjacent(Point point) {
            List<Point> adjacent = new List<Point>();

            int maxY = grid.GetLength(0) - 1, maxX = grid.GetLength(1) - 1;

            // Left
            ValidateAndAdd(maxY, maxX, point.X - 1, point.Y, point, adjacent, false);
            // Up one
            ValidateAndAdd(maxY, maxX, point.X, point.Y - 1, point, adjacent, false);
            // Right
            ValidateAndAdd(maxY, maxX, point.X + 1, point.Y, point, adjacent, false);
            // Under
            ValidateAndAdd(maxY, maxX, point.X, point.Y + 1, point, adjacent, false);


            // Diagonal moves
            if (isDiagonalMovesEnabled) {
                // Left, up one
                ValidateAndAdd(maxY, maxX, point.X - 1, point.Y - 1, point, adjacent, true);
                // Right, up one
                ValidateAndAdd(maxY, maxX, point.X + 1, point.Y - 1, point, adjacent, true);
                //Right down one
                ValidateAndAdd(maxY, maxX, point.X + 1, point.Y + 1, point, adjacent, true);
                // Left down one
                ValidateAndAdd(maxY, maxX, point.X - 1, point.Y + 1, point, adjacent, true);
            }

            return adjacent;
        }

        private void ValidateAndAdd(int maxY, int maxX, int x, int y, Point parent, List<Point> adjacent, bool isDiagonal) {
            if (x < 0 || x > maxX || y < 0 || y > maxY) return;
            if (closed.Any(p => p.IsEqual(x, y))) return;

            if (grid[y, x] != 1) // grid[y, x] == 0 || grid[y, x] == 3
            {
                // 1
                // Calculate the scores , F, G, H
                Point p = new Point();
                p.X = x;
                p.Y = y;
                p.Parent = parent;
                p.GScore = CalculateGScore(parent, isDiagonal);
                p.HScore = CalculateHScore(x, y);

                // 2
                // If it is on the open list already, check to see if this path to that square is better, '
                // using G cost as the measure. A lower G cost means that this is a better path. 
                // If so, change the parent of the square to the current square, and recalculate the G and F scores of the square. 
                // If you are keeping your open list sorted by F score, you may need to resort the list to account for the change.
                Point inOpen = open.FirstOrDefault(o => o.Equals(p));
                if (inOpen != null) {
                    if (p.GScore < inOpen.GScore) {
                        open.Remove(inOpen);
                        open.Add(p);
                    }
                } else // 3. Add it to the list
                    adjacent.Add(p);
            }
        }

        // the way to figure out the G cost of that square is to take the G cost of its parent, 
        // and then add 10 or 14 depending on whether it is diagonal or orthogonal (non-diagonal) from that parent square. 
        private int CalculateGScore(Point parent, bool isDiagonal) {
            return parent.GScore + (isDiagonal ? 14 : 10);
        }

        // Use Manhattan method
        // where you calculate the total number of squares moved horizontally and vertically to reach the target square from 
        // the current square, ignoring diagonal movement, and ignoring any obstacles that may be in the way.
        //  We then multiply the total by 10, our cost for moving one square horizontally or vertically.
        private int CalculateHScore(int x, int y) {
            return end.X - x + end.Y - y;
        }
    }
}
