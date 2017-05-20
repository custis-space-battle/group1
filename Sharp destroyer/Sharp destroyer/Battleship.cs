using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp_destroyer
{
    public class Battleship
    {
        public CellType[,] OurField = new CellType[10, 10];
        public bool SetUpShips()
        {
            return true;
        }


    }
    public enum CellType
    {
        Empty,
        Ship,
        Wreck,
        Mine
    }
}
