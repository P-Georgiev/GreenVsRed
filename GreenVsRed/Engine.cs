using GreenVsRed.IO;

namespace GreenVsRed
{
    public class Engine
    {
        private readonly IIOHandler iOHandler;
        private readonly ICellFactory cellFactory;

        public Engine(IIOHandler iOHandler, ICellFactory cellFactory)
        {
            this.iOHandler = iOHandler;
            this.cellFactory = cellFactory;
        }

        public void Run()
        {
            int width;
            int height;

            // read height and width input
            int[] dimensions = ReadDimensions();
            height = dimensions[0];
            width = dimensions[1];

            // create "Generation Zero"
            Game game = new Game(new GameState(new Cell[height, width]));
            PopulateInitialState(game);

            // read coordinates of target cell and number of generations.
            string[] targetCellAndNAsString = this.iOHandler.Read().Split(", ");
            int targetCellX = int.Parse(targetCellAndNAsString[0]);
            int targetCellY = int.Parse(targetCellAndNAsString[1]);
            int generationsNumber = int.Parse(targetCellAndNAsString[2]);

            // calculate N generations and print result         
            this.iOHandler.Write(GetTargetCellGreenStateCount(game, targetCellX, targetCellY, generationsNumber).ToString());
        }

        public int[] ReadDimensions()
        {
            int width;
            int height;
            do
            {
                string[] dimensions = this.iOHandler.Read().Split(", ");
                width = int.Parse(dimensions[0]);
                height = int.Parse(dimensions[1]);
            } while (width > height || height >= 1000);

            return new int[] { height, width };
        }

        /// <summary>
        /// Sets the values of each cell in "Generation Zero".
        /// </summary>
        /// <param name="game">The Game instance that represents the game GreenVsRed</param>
        public void PopulateInitialState(Game game)
        {
            for (int row = 0; row < game.CurrentGen.Height; row++)
            {
                string rowAsString = this.iOHandler.Read();
                for (int col = 0; col < game.CurrentGen.Width; col++)
                {
                    int cellValue = int.Parse(rowAsString[col].ToString());
                    game.CurrentGen.Grid[row, col] = this.cellFactory.Create(row, col, cellValue);
                }
            }
        }

        /// <summary>
        /// Calculates and returns in how many generations the target cell value was green.
        /// </summary>
        /// <param name="game">The Game instance that represents the game GreenVsRed</param>
        /// <param name="x">The int that represents the x coodrinate of the target cell.</param>
        /// <param name="y">The int that represents the y coodrinate of the target cell.</param>
        /// <param name="n">The int that represents the number of generations that are calculated.</param>
        /// <returns></returns>
        public int GetTargetCellGreenStateCount(Game game, int x, int y, int n)
        {
            int count = 0;
            for (int genCount = 0; genCount <= n; genCount++)
            {
                count = game.CurrentGen.Grid[y, x].Value == 1 ? count + 1 : count;

                GameState nextGen = new GameState(new Cell[game.CurrentGen.Height, game.CurrentGen.Width]);
                for (int row = 0; row < game.CurrentGen.Height; row++)
                {
                    for (int col = 0; col < game.CurrentGen.Width; col++)
                    {
                        nextGen.Grid[row, col] = this.cellFactory.Create(row, col);
                    }
                }

                game.CurrentGen = game.CalculateNextGen(nextGen);
            }

            return count;
        }
    }
}
