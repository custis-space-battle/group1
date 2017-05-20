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
        public CellType[,] OurField = (CellType[,])Array.CreateInstance(typeof(CellType), new int[] { 10, 10 }, new int[] { 1, 1 });
        public CellType[,] EnemyField = (CellType[,])Array.CreateInstance(typeof(CellType), new int[] { 10, 10 }, new int[] { 1, 1 });
        public string SetUpShips()
        {

            return "1,1; 1,1";
        }
        public Point GetPointToFire()
        {
            return new Point(1,1);
            //Массив начинающийся с индекса 1
            
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
