using GreenVsRed.IO;

namespace GreenVsRed
{
    class Program
    {

        static void Main(string[] args)
        {
            ConsoleIOHandler iOHandler = new ConsoleIOHandler();
            CellFactory cellFactory= new CellFactory();
            Engine engine = new Engine(iOHandler, cellFactory);
            engine.Run();
        }
    }
}
