namespace RecyclingStation
{
    using Core;
    using IO.Readers;
    using IO.Writers;

    public class RecyclingStationMain
    {
        private static void Main(string[] args)
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IEngine engine = new Engine(reader, writer);
            engine.Run();
        }
    }
}
