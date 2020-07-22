namespace GreenVsRed
{
    public class GameState
    {
        public GameState(Cell[,] state)
        {
            this.Grid = state;
        }

        public int Height => this.Grid.GetLength(0);

        public int Width => this.Grid.GetLength(1);

        public Cell[,] Grid { get; set; }
    }
}
