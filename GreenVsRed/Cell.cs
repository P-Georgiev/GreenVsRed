using System.Collections.Generic;

namespace GreenVsRed
{
    public class Cell
    {

        public Cell(int xCoord, int yCoord, int value)
        {
            this.XCoord = xCoord;
            this.YCoord = yCoord;
            this.Value = value;
            this.Neighbours = new Dictionary<int, int>();
            this.Neighbours.Add(0, 0);
            this.Neighbours.Add(1, 0);
        }

        public int XCoord { get; }

        public int YCoord { get; }

        public int Value { get; set; }

        public Dictionary<int, int> Neighbours { get; }

        public void CalculateNeighbours(GameState currentGen)
        {
            int startPosX = (this.XCoord - 1 < 0) ? this.XCoord : this.XCoord - 1;
            int startPosY = (this.YCoord - 1 < 0) ? this.YCoord : this.YCoord - 1;
            int endPosX = (this.XCoord + 1 > currentGen.Height - 1) ? this.XCoord : this.XCoord + 1;
            int endPosY = (this.YCoord + 1 > currentGen.Width - 1) ? this.YCoord : this.YCoord + 1;

            for (int row = startPosX; row <= endPosX; row++)
            {
                for (int col = startPosY; col <= endPosY; col++)
                {
                    if (row == this.XCoord && col == this.YCoord)
                    {
                        continue;
                    }
                    if (currentGen.Grid[row, col].Value == 0)
                    {
                        this.Neighbours[0] += 1;
                    }
                    else
                    {
                        this.Neighbours[1] += 1;
                    }
                }
            }
        }
    }
}
