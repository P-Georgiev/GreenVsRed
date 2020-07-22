using System;
using System.Collections.Generic;
using System.Text;

namespace GreenVsRed
{
    public interface ICellFactory
    {
        public Cell Create(int xCoord, int yCoord, int value = 0);
    }
}
