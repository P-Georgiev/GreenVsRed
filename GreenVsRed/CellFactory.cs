using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVsRed
{
    public class CellFactory : ICellFactory
    {
        public Cell Create(int xCoord, int yCoord, int value = 0)
        {
            return new Cell(xCoord, yCoord, value);
        }
    }
}
