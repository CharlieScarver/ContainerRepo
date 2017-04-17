using System.Linq;

namespace _02_CubicsRube
{
    using System;
    public class CubicsRube
    {
        public static void Main()
        {
            int dimensions = int.Parse(Console.ReadLine());

            int[,,] cubetrix = new int[dimensions, dimensions, dimensions];
            long sum = 0;
            int unchangedCells = (int)Math.Pow(dimensions, 3);

            string inputLine = Console.ReadLine();

            while (inputLine != "Analyze")
            {
                int[] cellData = inputLine.Split(' ').Select(int.Parse).ToArray();

                // if cell is in the cubetrix
                if (IsInCubetrix(cubetrix, cellData[0], cellData[1], cellData[2]))
                {
                    // if the particles are more than 0
                    if (cellData[3] > 0)
                    {
                        // bombard cell
                        cubetrix[cellData[0], cellData[1], cellData[2]] += cellData[3];
                        sum += cellData[3];
                        unchangedCells--;
                    }
                }

                inputLine = Console.ReadLine();
            }
            
            Console.WriteLine(sum);
            Console.WriteLine(unchangedCells);

        }

        private static bool IsInCubetrix(int[,,] cubetrix, int x, int y, int z)
        {
            bool result = x >= 0
                        && x < cubetrix.GetLength(0)
                        && y >= 0
                        && y < cubetrix.GetLength(1)
                        && z >= 0
                        && z < cubetrix.GetLength(2);
            return result;
        }
    }
}
