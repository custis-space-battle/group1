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
        //Массив начинающийся с индекса 1
        public CellType[,] OurField = (CellType[,])Array.CreateInstance(typeof(CellType), new int[] { 10, 10 }, new int[] { 1, 1 });
        public CellType[,] EnemyField = (CellType[,])Array.CreateInstance(typeof(CellType), new int[] { 10, 10 }, new int[] { 1, 1 });
        public List<Point> WreckedShipPoints = new List<Point>();
        public Point LastHitPoint;

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
        
        public Point GetPointToFire()
        {
            //обработка строки
            //var splitted = status.Split(new char[] { ':' });

            //рандом
            int count = 0;
            var point = new Point(r.Next(1,10), r.Next(1, 10));

            if (EnemyField[point.X, point.Y] == CellType.Hitted)
            {
                count++;
                if (count > 101)
                {
                    return GetPointToFire();
                }
                else
                {
                    return point;
                }
            }
            else
            {
                EnemyField[point.X, point.Y] = CellType.Hitted;
                return point;
            }
            //Массив начинающийся с индекса 1
        }

        public Point PointToHitWreckedShip(Point LastHit)
        {
            if (WreckedShipPoints.Count == 0)
            {
                return null;
            }

            else if (WreckedShipPoints.Count == 1)
            {
                foreach (Point p in WreckedShipPoints)
                {
                    for (int i = -1; i < 1; i++)
                    {
                        for (int j = -1; j < 1; j++)
                        {
                            if (Math.Abs(i) != Math.Abs(j) && p.X + i <= 10 && p.X + i >= 1 && p.Y + j <= 10 && p.Y + j >= 1)
                            {
                                if (EnemyField[p.X + i, p.Y + j] == CellType.Empty)
                                {
                                    return new Point(p.X + i, p.Y + j);
                                }
                            }
                        }
                    }
                }
            }

            else
            {

                var p1 = WreckedShipPoints.Min(x => x.Y);
                var p2 = WreckedShipPoints.Max(x => x.Y);
                if (p1==p2)
                {
                    var targetX = WreckedShipPoints.Min(x => x.X);
                    Point pt = new Point(targetX - 1, p1);
                    if (EnemyField[pt.X, pt.Y] == CellType.Empty)
                    {
                        return pt;
                    }
                    else
                    {
                        targetX = WreckedShipPoints.Max(x => x.X);
                        pt = new Point(targetX + 1, p1);
                        return pt;
                    }
                }
                else
                {
                    var targetY = WreckedShipPoints.Min(x => x.Y);
                    var targetX = WreckedShipPoints.Min(x => x.X);
                    Point pt = new Point(targetX , targetY-1);
                    if (EnemyField[pt.X, pt.Y] == CellType.Empty)
                    {
                        return pt;
                    }
                    else
                    {
                        targetY = WreckedShipPoints.Max(x => x.Y);
                        pt = new Point(targetX, targetY+1);
                        return pt;
                    }
                }

            }


            return new Point(-1,-1);
        }
        public IEnumerable<Point> GetPointToFireEvgeny(string lastHitStatus)
        {
            if (SpecialEvent.Exist)
            {

            }
            else
            {
                if (lastHitStatus == "MISSED")
                {
                    //Проходим вторую линию снизу вверх. j - x координата, i - у координата
                    for (int j = 1, i = 8; i > 0; j++, i--)
                    {
                        if (EnemyField[j, i] == CellType.Empty)
                            yield return new Point(j, i);
                        else continue;
                    }
                    //Большая правая линия
                    for (int j = 3, i = 10; i > 2; j++, i--)
                    {
                        yield return new Point(j, i);
                    }
                    //левый верхний участок
                    for (int j = 1, i = 4; i > 0; j++, i--)
                    {
                        yield return new Point(j, i);
                    }
                    //Правый нижний участок
                    for (int j = 7, i = 7; i > 6; j++, i--)
                    {
                        yield return new Point(j, i);
                    }
                }
                else if (lastHitStatus == "HIT")
                {
                 //    return PointToHitWreckedPoint(lastHitPoint);
                }
            }
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
