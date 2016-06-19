namespace _03_CubicMessages
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    public class CubicMessages
    {
        public static void Main()
        {
            string message = Console.ReadLine();
            int messageLength = int.Parse(Console.ReadLine());
            StringBuilder output = new StringBuilder();

            while (true)
            {//(?:^[0-9]+)[a-zA-z]{3}(?![a-zA-Z]+)
                Regex messageRegex = new Regex($"(?<=^[0-9]+)[a-zA-z]{{{messageLength}}}(?!.*[a-zA-Z]+.*)");
                if (messageRegex.IsMatch(message))
                {
                    string messageText = messageRegex.Match(message).Groups[0].Value;

                    Regex digitsRegex = new Regex(@"[0-9]");
                    MatchCollection digitMatches = digitsRegex.Matches(message);
                    List<int> digitIndexes = new List<int>();

                    foreach (Match digitMatch in digitMatches)
                    {
                        digitIndexes.Add(int.Parse(digitMatch.Groups[0].Value));
                    }

                    StringBuilder validationKey = new StringBuilder();
                    for (int i = 0; i < digitIndexes.Count; i++)
                    {
                        if (digitIndexes[i] < messageText.Length)
                        {
                            validationKey.Append(messageText[digitIndexes[i]]);
                        }
                        else
                        {
                            validationKey.Append(" ");
                        }
                    }

                    output.AppendLine($"{messageText} == {validationKey}");
                }

                message = Console.ReadLine();
                if (message == "Over!")
                {
                    break;
                }
                messageLength = int.Parse(Console.ReadLine());
            }

            Console.Write(output.ToString());
        }
    }
}
