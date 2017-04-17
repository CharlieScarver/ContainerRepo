namespace _01_SystemSplit
{
    using System;
    using System.Text.RegularExpressions;

    public static class CommandParser
    {
        public static void ParseCommand(string input, TheSystem system)
        {
            Regex regex = new Regex(@"(\w+)\(([\w\s,]+)\)");
            
            if (regex.IsMatch(input))
            {
                MatchCollection commandMatchCollection = regex.Matches(input);
                string commandName = commandMatchCollection[0].Groups[1].Value;
                string[] parameters = commandMatchCollection[0].Groups[2].Value.Split(new char[] { ',', ' ' },
                        StringSplitOptions.RemoveEmptyEntries);

                if (commandName == "RegisterPowerHardware")
                {
                    system.RegisterPowerHardware(parameters[0], int.Parse(parameters[1]), int.Parse(parameters[2]));
                }
                else if (commandName == "RegisterHeavyHardware")
                {
                    system.RegisterHeavyHardware(parameters[0], int.Parse(parameters[1]), int.Parse(parameters[2]));
                }
                else if (commandName == "RegisterExpressSoftware")
                {
                    system.RegisterExpressSoftware(parameters[0], parameters[1], int.Parse(parameters[2]), int.Parse(parameters[3]));
                }
                else if (commandName == "RegisterLightSoftware")
                {
                    system.RegisterLightSoftware(parameters[0], parameters[1], int.Parse(parameters[2]), int.Parse(parameters[3]));
                }
                else if (commandName == "ReleaseSoftwareComponent")
                {
                    system.ReleaseSoftwareComponent(parameters[0], parameters[1]);
                }
                else if (commandName == "Dump")
                {
                    system.Dump(parameters[0]);
                }
                else if (commandName == "Restore")
                {
                    system.Restore(parameters[0]);
                }
                else if (commandName == "Destroy")
                {
                    system.Destroy(parameters[0]);
                }
            }
            else if (input == "Analyze()")
            {
                system.Analyze();
            }
            else if (input == "DumpAnalyze()")
            {
                system.DumpAnalyze();
            }
        }
    }
}
