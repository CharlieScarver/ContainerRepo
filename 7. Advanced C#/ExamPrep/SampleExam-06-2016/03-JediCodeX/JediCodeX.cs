// 80/100
//100/100 -> text.Append instead of .AppendLine
namespace _03_JediCodeX
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class JediCodeX
    {
        public static void Main()
        {
            // read input
            int linesCount = int.Parse(Console.ReadLine());

            StringBuilder text = new StringBuilder();

            for (int i = 0; i < linesCount; i++)
            {
                string line = Console.ReadLine();

                text.Append(line);
            }

            string namePattern = Console.ReadLine();
            string messagePattern = Console.ReadLine();

            int[] messageIndexes = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            // create regex
            Regex nameRegex = new Regex($"{Regex.Escape(namePattern)}([a-zA-Z]{{{namePattern.Length}}})(?![a-zA-Z])");
            Regex messageRegex = new Regex($"{Regex.Escape(messagePattern)}([a-zA-Z0-9]{{{messagePattern.Length}}})(?![a-zA-Z0-9])");

            // create collections
            List<string> names = new List<string>();
            List<string> messages = new List<string>();

            MatchCollection nameMatches = nameRegex.Matches(text.ToString());
            MatchCollection messageMatches = messageRegex.Matches(text.ToString());

            // fill collections
            foreach (Match nameMatch in nameMatches)
            {
                names.Add(nameMatch.Groups[1].Value);
                //Console.WriteLine(nameMatch.Groups[1].Value);
            }

            foreach (Match messageMatch in messageMatches)
            {
                messages.Add(messageMatch.Groups[1].Value);
                //Console.WriteLine(messageMatch.Groups[1].Value);
            }

            // prepare output
            StringBuilder output = new StringBuilder();
            int currentNameIndex = 0;

            for (int i = 0; i < messageIndexes.Length; i++)
            {
                int msgIndex = messageIndexes[i] - 1;
                if (msgIndex < messages.Count)
                {
                    //Console.WriteLine($"{names[currentNameIndex]} - {messages[msgIndex]}");
                    output.Append($"{names[currentNameIndex]} - {messages[msgIndex]}{Environment.NewLine}");
                    currentNameIndex++;

                    if (currentNameIndex >= names.Count)
                    {
                        break;
                    }
                }
            }

            Console.Write(output.ToString());
        }
    }
}
