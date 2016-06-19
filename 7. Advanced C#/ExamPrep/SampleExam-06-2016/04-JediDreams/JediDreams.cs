// 20/100
// 40/100 -> print None case
// 60/100 -> expect more whitespaces in regex
//100/100 -> expect more whitespaces in regex + method return type is not [a-z] only
namespace _04_JediDreams
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class JediDreams
    {
        public static void Main()
        {
            int linesCount = int.Parse(Console.ReadLine());

            StringBuilder text = new StringBuilder();

            for (int i = 0; i < linesCount; i++)
            {
                string line = Console.ReadLine();

                text.AppendLine(line);
            }

            //Console.WriteLine(text.ToString());

            SortedDictionary<string, List<string>> methods = new SortedDictionary<string, List<string>>();

            string[] codeBlocks = Regex.Split(text.ToString(), @"static\s+.*?\s+((?:[A-Z][a-z]*)+)\s*\(.*?\)");
            //Console.WriteLine(codeBlocks.Length);

            for (int i = 1; i < codeBlocks.Length; i += 2)
            {
                string methodName = codeBlocks[i];
                string methodCode = codeBlocks[i + 1];

                if (!methods.ContainsKey(methodName))
                {
                    methods.Add(methodName, new List<string>());
                }

                Regex methodCallsRegex = new Regex(@"((?:[A-Z][a-z]*)+)\s*\(");
                MatchCollection methodCalls = methodCallsRegex.Matches(methodCode);

                foreach (Match methodCall in methodCalls)
                {
                    methods[methodName].Add(methodCall.Groups[1].Value);
                }

                methods[methodName].Sort();
            }

            var sortedMethods = methods
                .OrderByDescending(kvp => kvp.Value.Count)
                .ThenBy(kvp => kvp.Key)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value); // hacking the world

            StringBuilder output = new StringBuilder();
            foreach (var kvp in sortedMethods)
            {
                if (kvp.Value.Count > 0)
                {
                    output.AppendLine($"{kvp.Key} -> {kvp.Value.Count} -> {string.Join(", ", kvp.Value)}");
                }
                else
                {
                    output.AppendLine($"{kvp.Key} -> None");
                }
            }

            Console.Write(output.ToString());
        }
    }
}
