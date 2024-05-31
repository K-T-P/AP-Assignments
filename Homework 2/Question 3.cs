// Koroush Taghi Pour
// Student ID: 400521207
using System;

namespace tamrin_seri_2_soal_3
{
    public class Program
    {
        public static void Main()
        {
            //prints a text to guide the user
            Console.WriteLine("Please enter how many row(s) your matrix will have. For Example : \n2");

            //receives row
            int row = int.Parse(Console.ReadLine());

            //prints a text to guide user
            Console.WriteLine("Now Please enter how many column(s) your matrix will have: For Example\n3");

            //receives column
            int column = int.Parse(Console.ReadLine());

            //prints a text to guide user
            Console.WriteLine("Now Please enter your matrix. First, enter the first row of your matrix. For Example " +
                ":\n1 2 3\nThen press enter. Next, Enter the second row :\n4 5 6\nThen press enter. And keep doing this until you enter all matrix. After the" +
                " last enter, program will show you the answer.");

            //defines an array to store the path 
            int[,] matrix = new int[row, column];

            //stores the path
            for (int i = 0; i < row; i++)
            {
                string[] cheatSheetToReceiveValues = Console.ReadLine().Split();
                for (int j = 0; j < column; j++)
                {
                    matrix[i, j] = int.Parse(cheatSheetToReceiveValues[j]);
                }
            }

            //calculates how many squares it has to pass which will be used as stop condition
            int howManySquaresProgramWillMove = row * column;

            //prints the returned string from method
            Console.WriteLine(movingSnail(0, 0, matrix, 0, row - 1, column - 1, 0, 0, howManySquaresProgramWillMove));
        }


        public static string movingSnail(int current_row, int current_column, int[,] matrix, int determinesWhichSideProgramWilMove,
            int downsideLimiter, int rightsideLimiter, int leftsideLimiter, int upsideLimiter, int countOfTheSquares)
        {
            //determinesWhichSideProgramWilMove : if its value be 0, it means moving to the right. 1:moving down, 2:moving to the left, 3:moving to the up

            countOfTheSquares--;

            //if countOfSquares==0, it means that it passed all squares and has to be stopped
            if (countOfTheSquares == 0)
            {
                return matrix[current_row, current_column].ToString();
            }

            //it executes when it still can move forward. Note that the previous process of method determined it
            if (determinesWhichSideProgramWilMove == 0)
            {
                if (current_column == rightsideLimiter)                      //it means that it can't move ahead any more. so it will change the direction
                {
                    return matrix[current_row, current_column].ToString() + ',' +
                        movingSnail(current_row + 1, current_column, matrix, 1, downsideLimiter, rightsideLimiter, leftsideLimiter, upsideLimiter + 1, countOfTheSquares);
                }
                else             //it still can move forward
                {
                    return matrix[current_row, current_column].ToString() + ',' +
                        movingSnail(current_row, current_column + 1, matrix, 0, downsideLimiter, rightsideLimiter, leftsideLimiter, upsideLimiter, countOfTheSquares);
                }
            }

            else if (determinesWhichSideProgramWilMove == 1)
            {
                if (current_row == downsideLimiter)
                {
                    return matrix[current_row, current_column].ToString() + ',' +
                        movingSnail(current_row, current_column - 1, matrix, 2, downsideLimiter, rightsideLimiter - 1, leftsideLimiter, upsideLimiter, countOfTheSquares);
                }
                else
                {
                    return matrix[current_row, current_column].ToString() + ',' +
                        movingSnail(current_row + 1, current_column, matrix, 1, downsideLimiter, rightsideLimiter, leftsideLimiter, upsideLimiter, countOfTheSquares);
                }
            }

            else if (determinesWhichSideProgramWilMove == 2)
            {
                if (current_column == leftsideLimiter)
                {
                    return matrix[current_row, current_column].ToString() + ',' +
                        movingSnail(current_row - 1, current_column, matrix, 3, downsideLimiter - 1, rightsideLimiter, leftsideLimiter, upsideLimiter, countOfTheSquares);
                }
                else
                {
                    return matrix[current_row, current_column].ToString() + ',' +
                        movingSnail(current_row, current_column - 1, matrix, 2, downsideLimiter, rightsideLimiter, leftsideLimiter, upsideLimiter, countOfTheSquares);
                }
            }

            else
            {
                if (current_row == upsideLimiter)
                {
                    return matrix[current_row, current_column].ToString() + ',' +
                        movingSnail(current_row, current_column + 1, matrix, 0, downsideLimiter, rightsideLimiter, leftsideLimiter + 1, upsideLimiter, countOfTheSquares);
                }
                else
                {
                    return matrix[current_row, current_column].ToString() + ',' +
                        movingSnail(current_row - 1, current_column, matrix, 3, downsideLimiter, rightsideLimiter, leftsideLimiter, upsideLimiter, countOfTheSquares);
                }
            }
        }
    }
}
