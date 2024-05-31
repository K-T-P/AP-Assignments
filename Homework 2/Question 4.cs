using System;

namespace tamrin_seri_2_soal_4
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Please enter row and column in this format:\n[row] [column]   For Example:\n2 2");
            string[] coordinates = Console.ReadLine().Split();
            int row = int.Parse(coordinates[0]);
            int column = int.Parse(coordinates[1]);
            int[,] matrix = new int[row + 2, column + 2];
            for (int i = 0; i <= row + 1; i++)
            {
                for (int j = 0; j <= column + 1; j++)
                {
                    matrix[i, j] = 4;
                }
            }
            int first_row = 0, first_column = 0, allFreeSquares = row * column - 1;
            Console.WriteLine("Now Please enter your path.\nRemember that to show the beginning point with 1 and the finish point with 2.\nObstacles are show with -1." +
                "\nFirst, enter the first row, then press enter.\nSecond,enter the second row, then press enter and so on. For Example :\n1 0(press enter)\n0 2(press enter)" +
                "\nAfter pressing the last enter, program will calculate all possible\nways from 1 to 2 avoiding obstacles and passing all free squares.\n" +
                "Remember to avoid entering extra whitespaces to prevent crashing program.");
            for (int i = 1; i <= row; i++)
            {
                string[] cheatsheet = Console.ReadLine().Split();
                for (int j = 1; j <= column; j++)
                {
                    matrix[i, j] = int.Parse(cheatsheet[j - 1]);
                    if (matrix[i, j] == 1)
                    {
                        first_row = i;
                        first_column = j;
                    }
                    if (matrix[i, j] == -1)
                    {
                        allFreeSquares--;
                    }
                }
            }
            Console.WriteLine(masirekhas(first_row, first_column, allFreeSquares, matrix));
        }
        public static int masirekhas(int current_row, int current_column,
            int allFreeWays, int[,] matrix)
        {
            int numberOfThePossibleWaysAccordingToTheConditions = 0;
            bool showsThatItIsTrappedInADeadendOrNot = true;
            if (matrix[current_row, current_column] == 2)
            {
                if (allFreeWays==0)
                {
                    return 1;
                }
                return 0;
            }
            if (matrix[current_row + 1, current_column] == 0 ||
                matrix[current_row + 1, current_column] == 2)
            {
                showsThatItIsTrappedInADeadendOrNot = false;
                if (matrix[current_row + 1, current_column] == 0)
                {
                    matrix[current_row + 1, current_column] = 30 - allFreeWays;
                }
                numberOfThePossibleWaysAccordingToTheConditions += masirekhas(current_row + 1, current_column,
                    allFreeWays - 1, matrix);
                if (matrix[current_row + 1, current_column] == 30 - allFreeWays)
                {
                    matrix[current_row + 1, current_column] = 0;
                }
            }
            if (matrix[current_row - 1, current_column] == 0 ||
                matrix[current_row - 1, current_column] == 2)
            {
                showsThatItIsTrappedInADeadendOrNot = false;
                if (matrix[current_row - 1, current_column] == 0)
                {
                    matrix[current_row - 1, current_column] = 30 - allFreeWays;
                }
                numberOfThePossibleWaysAccordingToTheConditions += masirekhas(current_row - 1, current_column,
                    allFreeWays - 1, matrix);
                if (matrix[current_row - 1, current_column] == 30 - allFreeWays)
                {
                    matrix[current_row - 1, current_column] = 0;
                }
            }
            if (matrix[current_row, current_column + 1] == 0 ||
                matrix[current_row, current_column + 1] == 2)
            {
                showsThatItIsTrappedInADeadendOrNot = false;
                if (matrix[current_row, current_column + 1] == 0)
                {
                    matrix[current_row, current_column + 1] = 30 - allFreeWays;
                }
                numberOfThePossibleWaysAccordingToTheConditions += masirekhas(current_row, current_column + 1,
                    allFreeWays - 1, matrix);
                if (matrix[current_row, current_column + 1] == 30 - allFreeWays)
                {
                    matrix[current_row, current_column + 1] = 0;
                }
            }
            if (matrix[current_row, current_column - 1] == 0 ||
                matrix[current_row, current_column - 1] == 2)
            {
                showsThatItIsTrappedInADeadendOrNot = false;
                if (matrix[current_row, current_column - 1] == 0)
                {
                    matrix[current_row, current_column - 1] = 30 - allFreeWays;
                }
                numberOfThePossibleWaysAccordingToTheConditions += masirekhas(current_row, current_column - 1,
                    allFreeWays - 1, matrix);
                if (matrix[current_row, current_column - 1] == 30 - allFreeWays)
                {
                    matrix[current_row, current_column - 1] = 0;
                }
            }
            if (showsThatItIsTrappedInADeadendOrNot)
            {
                return 0;
            }
            return numberOfThePossibleWaysAccordingToTheConditions;
        }
    }
}
