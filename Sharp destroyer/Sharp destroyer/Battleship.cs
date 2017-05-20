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

        Random r = new Random();

        public Battleship()
        {
            for (int i=1; i<=10; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    EnemyField[i, j] = CellType.Empty;
                    //Console.WriteLine($"Setted {i}, {j} as Empty");
                }
            }
        }

        public string SetUpShips()
        {

            return "1,1; 1,1";
        }
        
        public Point GetPointToFire(string status = "")
        {
            //обработка строки
            //var splitted = status.Split(new char[] { ':' });

            //рандом
            var point = new Point(r.Next(1,10), r.Next(1, 10));

            return point;
            //Массив начинающийся с индекса 1
            
        }
        


    }
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point (int x, int y)
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
