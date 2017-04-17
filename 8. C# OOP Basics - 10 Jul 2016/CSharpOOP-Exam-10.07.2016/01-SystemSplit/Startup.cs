namespace _01_SystemSplit
{
    using System;

    public class Startup
    {
        public static void Main(string[] args)
        {
            TheSystem system = new TheSystem();
            string input = Console.ReadLine();

            while (input != "System Split")
            {
                CommandParser.ParseCommand(input, system);

                input = Console.ReadLine();
            }

            system.SystemSplit();
        }
    }
}
