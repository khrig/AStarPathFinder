using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathFinder
{
    public class Point {
        public int X { get; set; }
        public int Y { get; set; }
        public Point Parent { get; set; }
        public int FScore { get { return GScore + HScore; } }
        public int GScore { get; set; }
        public int HScore { get; set; }

        public override string ToString() {
            return X + "," + Y + ", " + FScore;
        }

        public bool IsEqual(int x, int y) {
            return X == x && Y == y;
        }

        public bool Equals(Point p) {
            return p.X == X && p.Y == Y;
        }
    }
}
