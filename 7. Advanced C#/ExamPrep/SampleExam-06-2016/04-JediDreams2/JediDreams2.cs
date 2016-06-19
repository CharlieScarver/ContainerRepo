// 100/100
namespace _04_JediDreams2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class JediDreams2
    {
        public static void Main()
        {
            int linesCount = int.Parse(Console.ReadLine());

            StringBuilder text = new StringBuilder();

            Dictionary<string, List<string>> methods = new Dictionary<string, List<string>>();

            Regex methodDeclarationPattern = new Regex(@"static\s+.*?\s+([a-zA-Z]*[A-Z]{1}[a-zA-Z]*)\s*\(");
            Regex methodCallPattern = new Regex(@"([a-zA-Z]*[A-Z]+[a-zA-Z]*)\s*\(");

            string currentMethod = string.Empty;

            for (int i = 0; i < linesCount; i++)
            {
                string line = Console.ReadLine();

                if (methodDeclarationPattern.IsMatch(line))
                {
                    Match methodDeclarationMatch = methodDeclarationPattern.Match(line);

                    currentMethod = methodDeclarationMatch.Groups[1].Value;

                    if (!methods.ContainsKey(currentMethod))
                    {
                        methods.Add(currentMethod, new List<string>());
                    }
                }
                else if (methodCallPattern.IsMatch(line) && currentMethod != string.Empty)
                {
                    MatchCollection currentMethodCallMatches = methodCallPattern.Matches(line);

                    foreach (Match currentMethodCallMatch in currentMethodCallMatches)
                    {
                        methods[currentMethod].Add(currentMethodCallMatch.Groups[1].Value);
                    }
                }
                    
            }

            var sortedMethods = methods
                .OrderByDescending(kvp => kvp.Value.Count)
                .ThenBy(kvp => kvp.Key);

            StringBuilder output = new StringBuilder();
            foreach (var kvp in sortedMethods)
            {
                if (kvp.Value.Count > 0)
                {
                    output.AppendLine($"{kvp.Key} -> {kvp.Value.Count} -> {string.Join(", ", kvp.Value.OrderBy(val => val))}");
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
