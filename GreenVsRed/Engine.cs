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
