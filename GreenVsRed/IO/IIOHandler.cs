namespace GreenVsRed.IO
{
    public interface IIOHandler
    {
        string Read();

        void Write(string str);
    }
}
