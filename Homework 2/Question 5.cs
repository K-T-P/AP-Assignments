using System;

namespace tamrin_seri_2_soal_5
{
    class Program
    {
        static void Main()
        {
            sodoku game;
            game = new sodoku();
            Console.WriteLine("Welcome to our Sudoku Game!\nI'm sure you know the rules!\n");
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("This is the menu. You can choose one of these" +
                    " options. According to their name, their function is " +
                    "clear.\nAdd number\nDelete\nHint\nShow Table\nExit\n");
                string receiver = Console.ReadLine();
                if (receiver == "Add number")
                {
                    game.Add();
                }
                else if (receiver == "Delete")
                {
                    game.Delete();
                }
                else if (receiver == "Hint")
                {
                    game.Hint();
                }
                else if (receiver == "Show Table")
                {
                    game.ShowTable();
                }
                else if (receiver == "Exit")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Operator!");
                }
                if(game.checkWhetherTableIsCompletedOrNot())
                {
                    break;
                }
            }
            game.situationAnalyzer();
        }
    }
    class sodoku
    {
        private int[,] table =
        {
            {0,18,14,0,0,0,0,11,13 },
            {12,0,0,0,13,0,16,0,0 },
            {16,0,0,15,0,19,0,0,12 },
            {0,0,12,0,0,0,14,16,19 },
            {17,0,0,0,0,0,0,0,0 },
            {0,0,0,12,18,0,0,0,0 },
            {0,12,0,17,0,0,0,0,0 },
            {0,0,18,0,0,15,19,0,16 },
            {15,0,0,0,12,0,13,0,17 }
        };
        private int[,] answerTable =
        {
            {0,18,14,0,0,0,0,11,13 },
            {12,0,0,0,13,0,16,0,0 },
            {16,0,0,15,0,19,0,0,12 },
            {0,0,12,0,0,0,14,16,19 },
            {17,0,0,0,0,0,0,0,0 },
            {0,0,0,12,18,0,0,0,0 },
            {0,12,0,17,0,0,0,0,0 },
            {0,0,18,0,0,15,19,0,16 },
            {15,0,0,0,12,0,13,0,17 }
        };
        int helpRandomMaker = 3;
        int hint = 3;
        int row;
        int column;
        int count;
        public sodoku()
        {
            tableSolver(false, 0);
        }
        public void Hint()
        {
            if (hint == 0)
            {
                Console.WriteLine("You've used all your hints!\n");
            }
            else
            {
                Random rand = new Random();
                int randomNum;
                do
                {
                    int cheatsheet = (rand.Next() * helpRandomMaker / 7) % 81;
                    if (cheatsheet < 0)
                    {
                        cheatsheet *= (-1);
                    }
                    randomNum = cheatsheet;
                    helpRandomMaker++;
                } while (table[randomNum / 9, randomNum % 9] != 0);
                answerTable[randomNum / 9, randomNum % 9] += 20;
                table[randomNum / 9, randomNum % 9] = answerTable[randomNum / 9, randomNum % 9];
                hint--;
                Console.WriteLine("{0} numbers have been added to the table sucessfully.\nAdded numbers are shown with blue color!", 3 - hint);
                Console.WriteLine();
            }
        }
        public void Delete()
        {
            Console.WriteLine("Please enter the row and column of the " +
                "square which you want to remove its value! Proper Format :" +
                "\n[row] [column]   : For Example:\n3 6\nNote that the row" +
                "is greater than or equal to 1 or less than or equal to 9.\n" +
                "This goes same for column!\n");
            string[] deleteNumberInfo = Console.ReadLine().Split();
            row = int.Parse(deleteNumberInfo[0]) - 1;
            column = int.Parse(deleteNumberInfo[1]) - 1;
            if (row > 8 || row < 0 || column > 8 || column < 0)
            {
                Console.WriteLine("Out of Range!\n");
            }
            else if (table[row, column] > 10)
            {
                Console.WriteLine("You can't remove the number of this square!\n");
            }
            else
            {
                table[row, column] = 0;
                Console.WriteLine("Number removed sucessfully!\n");
            }
        }
        public void Add()
        {
            Console.WriteLine("Please enter the row, the column and the number" +
                " you want to enter.\nNote that : row is greater than or equal" +
                " to 1 and is less than or equal to 9. This goes same for column." +
                "\nEnterinf Format:\n[row] [column] [number]  For Example :\n" +
                "4 7 3\n");
            string[] addNumberInfo = Console.ReadLine().Split();
            row = int.Parse(addNumberInfo[0]) - 1;
            column = int.Parse(addNumberInfo[1]) - 1;
            count = int.Parse(addNumberInfo[2]);
            if (row < 0 || row > 8 || column > 8 || column < 0)
            {
                Console.WriteLine("Out of Range!\n");
            }
            else if (count > 9 || count < 1)
            {
                Console.WriteLine("Invalid Input!\n");
            }
            else if (table[row, column] > 10)
            {
                Console.WriteLine("The number of this square cannot be changed!\n");
            }
            else if (table[row, column] != 0)
            {
                Console.WriteLine("This square is already filled!\n" +
                    "Please remove the number of the square before adding " +
                    "another number!\n");
            }
            else
            {
                table[row, column] = count;
                Console.WriteLine("Adding number completed sucessfully!\n");
            }
        }
        public void ShowTable()
        {
            Console.WriteLine();
            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0)
                {
                    Console.WriteLine();
                }
                for (int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0)
                    {
                        Console.Write(' ');
                    }
                    if (table[i, j] > 10 && table[i, j] < 20)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write((table[i, j] % 10).ToString() + ' ');
                    }
                    else if (table[i, j] > 20)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write((table[i, j] % 10).ToString() + ' ');
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        if (table[i, j] == 0)
                        {
                            Console.Write("- ");
                        }
                        else
                        {
                            Console.Write(table[i, j].ToString() + ' ');
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void situationAnalyzer()
        {
            int correctlyFilledHomes = 0;
            int emptyHomes = 0;
            int wronglyFilledHomes = 0;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (table[i, j] == 0)
                    {
                        emptyHomes++;
                    }
                    if (table[i, j] >= 1 && table[i, j] <= 9 && table[i, j] == answerTable[i, j])
                    {
                        correctlyFilledHomes++;
                    }
                    if (table[i, j] >= 1 && table[i, j] <= 9 && table[i, j] != answerTable[i, j])
                    {
                        wronglyFilledHomes++;
                    }
                }
            }
            Console.WriteLine("{0} homes are filled correctly,\n{1} homes are filled wrongly," +
                "\n{2} homes are remained empty.", correctlyFilledHomes, wronglyFilledHomes, emptyHomes);
            Console.WriteLine("Thanks for your play!\nHope to see you soon!");
        }
        public bool checkWhetherTableIsCompletedOrNot()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (table[i, j] != answerTable[i, j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        private bool tableSolver(bool isSolvingTableFinishedOrNot,
            int numberOfTheSquare)
        {
            if (numberOfTheSquare == 81)
            {
                return true;
            }
            while (table[numberOfTheSquare / 9, numberOfTheSquare % 9] != 0)
            {
                numberOfTheSquare++;
                if (numberOfTheSquare == 81)
                {
                    return true;
                }
            }

            for (int i = 1; i <= 9; i++)
            {
                if (isItProperOrNot(numberOfTheSquare, i))
                {
                    answerTable[numberOfTheSquare / 9, numberOfTheSquare % 9] = i;
                    isSolvingTableFinishedOrNot = tableSolver(false, numberOfTheSquare + 1);
                }
                if (isSolvingTableFinishedOrNot)
                {
                    return true;
                }
            }
            answerTable[numberOfTheSquare / 9, numberOfTheSquare % 9] = 0;
            return false;
        }
        private bool isItProperOrNot(int whichSquareAreWeIn, int enteredNumber)
        {
            int current_column = whichSquareAreWeIn % 9;
            int current_row = whichSquareAreWeIn / 9;
            for (int i = 0; i < 9; i++)
            {
                if (answerTable[i, current_column] % 10 == enteredNumber)
                {
                    return false;
                }
            }
            for (int j = 0; j < 9; j++)
            {
                if (answerTable[current_row, j] % 10 == enteredNumber)
                {
                    return false;
                }
            }
            for (int i = (current_row / 3) * 3; i < (current_row / 3) * 3 + 3; i++)
            {
                for (int j = (current_column / 3) * 3; j < (current_column / 3) * 3 + 3; j++)
                {
                    if (answerTable[i, j] % 10 == enteredNumber)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
