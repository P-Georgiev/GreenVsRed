using System.Collections.Generic;

namespace GreenVsRed
{
    public class Game
    {
        public Game(GameState currentGen)
        {
            this.CurrentGen = currentGen;
        }

        public GameState CurrentGen { get; set; }

        /// <summary>
        /// Calculates and returns the state of the next generation.
        /// </summary>
        /// <param name="nextGen">The GameState instance that represents the next generation.</param>
        /// <returns></returns>
        public GameState CalculateNextGen(GameState nextGen)
        {
            for (int i = 0; i < this.CurrentGen.Height; i++)
            {
                for (int j = 0; j < this.CurrentGen.Width; j++)
                {
                    nextGen.Grid[i, j].CalculateNeighbours(this.CurrentGen);
                    nextGen.Grid[i, j].Value = GetCellValueInNextGen(nextGen.Grid, i, j, nextGen.Grid[i, j].Neighbours);
                }
            }

            return nextGen;
        }

        /// <summary>
        /// Calculates and returns the value the current cell will have in the next generation.
        /// </summary>
        /// <param name="nextGen">The Cell[,] instance that represents the next generation.</param>
        /// <param name="x">The int that represents the x coodrinate of the current cell.</param>
        /// <param name="y">The int that represents the y coodrinate of the current cell.</param>
        /// <param name="neighbours">The Dictionary<int, int> instance that represents the neighbours of the current cell.</param>
        /// <returns></returns>
        private int GetCellValueInNextGen(Cell[,] nextGen, int x, int y, Dictionary<int, int> neighbours)
        {
            if (this.CurrentGen.Grid[x, y].Value == 0 && (neighbours[1] == 3 || neighbours[1] == 6))
            {
                nextGen[x, y].Value = 1;
            }
            else if (this.CurrentGen.Grid[x, y].Value == 1 && (neighbours[1] != 2 && neighbours[1] != 3 && neighbours[1] != 6))
            {
                nextGen[x, y].Value = 0;
            }
            else if (this.CurrentGen.Grid[x, y].Value == 1 && (neighbours[1] == 2 || neighbours[1] == 3 || neighbours[1] != 6))
            {
                nextGen[x, y].Value = 1;
            }

            return nextGen[x, y].Value;
        }
    }
}
