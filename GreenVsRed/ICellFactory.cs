namespace GreenVsRed
{
    public interface ICellFactory
    {
        public Cell Create(int xCoord, int yCoord, int value = 0);
    }
}
