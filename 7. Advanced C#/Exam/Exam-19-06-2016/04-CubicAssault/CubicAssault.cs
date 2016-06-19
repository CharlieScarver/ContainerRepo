using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _04_CubicAssault
{
    using System;

    public class CubicAssault
    {
        public static void Main()
        {
            Dictionary<string, Dictionary<string, long>> areas = new Dictionary<string, Dictionary<string, long>>();
            string inputLine = Console.ReadLine();

            while (inputLine != "Count em all")
            {
                string[] areaData = inputLine.Split(new[] {" -> "}, StringSplitOptions.RemoveEmptyEntries);

                if (!areas.ContainsKey(areaData[0]))
                {
                    areas.Add(areaData[0], new Dictionary<string, long>());
                    areas[areaData[0]].Add("Green", 0);
                    areas[areaData[0]].Add("Red", 0);
                    areas[areaData[0]].Add("Black", 0);
                }
                areas[areaData[0]][areaData[1]] += long.Parse(areaData[2]);

                if (areas[areaData[0]]["Green"] >= 1000000)
                {
                    long redQuantity = areas[areaData[0]]["Green"] / 1000000;
                    areas[areaData[0]]["Green"] = areas[areaData[0]]["Green"] - (redQuantity*1000000);
                    areas[areaData[0]]["Red"] += redQuantity;
                }
                if (areas[areaData[0]]["Red"] >= 1000000)
                {
                    long blackQuantity = areas[areaData[0]]["Red"] / 1000000;
                    areas[areaData[0]]["Red"] = areas[areaData[0]]["Red"] - (blackQuantity * 1000000);
                    areas[areaData[0]]["Black"] += blackQuantity;
                }

                inputLine = Console.ReadLine();
            }

            // sort areas dictionary
            var sortedAreas = areas
                .OrderByDescending(kvp => kvp.Value["Black"])
                .ThenBy(kvp => kvp.Key.Length)
                .ThenBy(kvp => kvp.Key);

            // prepare output
            StringBuilder output = new StringBuilder();
            foreach (var areaKvp in sortedAreas)
            {
                output.AppendLine(areaKvp.Key);

                // sort types
                var sortedTypes = areaKvp.Value
                    .OrderByDescending(typeKvp => typeKvp.Value)
                    .ThenBy(typeKvp => typeKvp.Key);

                StringBuilder types = new StringBuilder();
                foreach (var typeKvp in sortedTypes)
                {
                    types.AppendLine($"-> {typeKvp.Key} : {typeKvp.Value}");
                }

                output.Append(types.ToString());
            }

            Console.WriteLine(output.ToString());
        }
    }
}
