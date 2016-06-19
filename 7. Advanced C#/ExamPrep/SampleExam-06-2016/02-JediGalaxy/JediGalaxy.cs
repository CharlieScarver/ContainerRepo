namespace _02_JediGalaxy
{
    using System;
    using System.Linq;

    public class JediMeditation
    {
        public static void Main()
        {
            // read dimensions
            int[] matrixDimensions = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            // fill matrix
            int[,] galaxyMatrix = new int[ matrixDimensions[0], matrixDimensions[1] ];
            fillMatrix(galaxyMatrix);

            // ivo's star value
            long sum = 0;

            // read paths
            string ivoLine = Console.ReadLine();
            string evilLine = Console.ReadLine();
            while (ivoLine != "Let the Force be with you")
            {
                // ivo coordinates
                int[] startCoordinates = ivoLine.Split(' ').Select(int.Parse).ToArray();
                int currRow = startCoordinates[0];
                int currCol = startCoordinates[1];

                // evil coordinates
                int[] evilStartCoordinates = evilLine.Split(' ').Select(int.Parse).ToArray();
                int evilCurrRow = evilStartCoordinates[0];
                int evilCurrCol = evilStartCoordinates[1];

                // traverse the galaxy as Evil
                while (evilCurrRow > -1)
                {
                    if (IsInMatrix(galaxyMatrix, evilCurrRow, evilCurrCol))
                    {
                        // destroy star
                        galaxyMatrix[evilCurrRow, evilCurrCol] = 0;
                    }

                    --evilCurrRow;
                    --evilCurrCol;
                }

                // traverse the galaxy as Ivo
                while (currRow > -1)
                {
                    if (IsInMatrix(galaxyMatrix, currRow, currCol))
                    {
                        // get start value
                        sum += galaxyMatrix[currRow, currCol];
                    }

                    --currRow;
                    ++currCol;
                }
                
                ivoLine = Console.ReadLine();
                evilLine = Console.ReadLine();
            }


            Console.WriteLine(sum);
        }

        private static void fillMatrix(int[,] matrix)
        {
            int counter = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i,j] = counter++;
                }
            }
        }

        private static bool IsInMatrix(int[,] matrix, int row, int col)
        {
            bool result = row >= 0
                          && row < matrix.GetLength(0)
                          && col >= 0
                          && col < matrix.GetLength(1);
            return result;
        }
    }
}
