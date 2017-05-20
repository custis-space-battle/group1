using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sharp_destroyer
{
    class Battleship
    {
        public CellType[,] OurField = new CellType[10, 10];
        public CellType[,] EnemyField = new CellType[10, 10];
        public string SetUpShips()
        {

            return "1,2";
        }
        public Point PointToFire()
        {
            return new Point(1,1);
        }


    }
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public override string ToString()
        {
            return X.ToString() + "," + Y.ToString();
        }
    }
}
