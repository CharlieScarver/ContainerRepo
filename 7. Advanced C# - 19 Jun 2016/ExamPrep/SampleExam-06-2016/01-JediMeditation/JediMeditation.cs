namespace _01_JediMeditation
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class JediMeditation
    {
        public static void Main()
        {
            Queue<string> masters = new Queue<string>();
            Queue<string> knigths = new Queue<string>();
            Queue<string> padawans = new Queue<string>();
            Queue<string> specials = new Queue<string>();
            bool yodaIsHere = false;

            // process input
            int linesCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < linesCount; i++)
            {
                string[] line = Console.ReadLine().Split(' ');
                // process line
                for (int j = 0; j < line.Length; j++)
                {
                    char jediType = line[j][0];
                    switch (jediType)
                    {
                        case 'm':
                            masters.Enqueue(line[j]);
                            break;
                        case 'k':
                            knigths.Enqueue(line[j]);
                            break;
                        case 'p':
                            padawans.Enqueue(line[j]);
                            break;
                        case 't':
                        case 's':
                            specials.Enqueue(line[j]);
                            break;
                        case 'y':
                            yodaIsHere = true;
                            break;
                    }
                }
            }

            // prepare output
            string output = "";
            StringBuilder builder = new StringBuilder();
            if (yodaIsHere)
            {
                builder.Append($"{string.Join(" ", masters)} ");
                builder.Append($"{string.Join(" ", knigths)} ");
                builder.Append($"{string.Join(" ", specials)} ");
                builder.Append($"{string.Join(" ", padawans)} ");
            }
            else
            {
                builder.Append($"{string.Join(" ", specials)} ");
                builder.Append($"{string.Join(" ", masters)} ");
                builder.Append($"{string.Join(" ", knigths)} ");
                builder.Append($"{string.Join(" ", padawans)} ");
            }
            
            output = output.TrimEnd();
            Console.WriteLine(builder.ToString());
        }
    }
}
