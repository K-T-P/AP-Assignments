// Koroush Taghi Pour
// Student ID: 400521207
using System;

namespace tamrin_seri_2_soal_1
{
    public class Program
    {
        public static void Main()
        {
            //prints a description message to enter the count of the numbers
            Console.WriteLine("Please enter how many numbers you will" +
                " enter. For Example :\n4");

            //receives the count of the numbers
            int countOfNumbers = int.Parse(Console.ReadLine());

            //asks to enter numbers
            Console.WriteLine("Now please enter your numbers with " +
                "whitespace among them. For Example : \n12 5 7 23");

            //stores numbers as string
            string[] stringFormatOfNumbers = Console.ReadLine().Split();

            //defines a new array to store numbers as int
            int[] arrayHoldingReceivedNumbersAs_int = new int[countOfNumbers];

            //convert saved as string numbers to int
            for (int index = 0; index < countOfNumbers; index++)
            {
                arrayHoldingReceivedNumbersAs_int[index] = int.Parse(stringFormatOfNumbers[index]);
            }

            //defines a bool variable to show that it is a good array or not
            bool flagToDetermineTheFinalOutput = false;

            //because if an array be a good array, surely there will be a subarray consisting of two members which are prime to each other
            for (int firstNumberIndex = 0; firstNumberIndex < countOfNumbers; firstNumberIndex++)           //for first number
            {
                for (int secondNumberIndex = 0; secondNumberIndex < countOfNumbers; secondNumberIndex++)     //for second number
                {
                    if (firstNumberIndex == secondNumberIndex)                    //skips if it is going to examine the same number
                    {
                        continue;
                    }

                    //calls a method to examine whether numbers are prime to each other or not. If they be, changes flag to true. If not, won't change it 
                    if (isItGoodArrayOrNot(arrayHoldingReceivedNumbersAs_int[firstNumberIndex], arrayHoldingReceivedNumbersAs_int[secondNumberIndex]))
                    {
                        flagToDetermineTheFinalOutput = true;
                    }
                }
            }

            //prints the final result
            Console.WriteLine(flagToDetermineTheFinalOutput);
        }

        //determines whether the two number are prime to each other or not
        public static bool isItGoodArrayOrNot(int num_1, int num_2)
        {
            //determines the greater number and lesser one. 
            int bigger = 0, lesser = 0;
            if (num_1 >= num_2)
            {
                bigger = num_1;
                lesser = num_2;
            }
            else
            {
                bigger = num_2;
                lesser = num_1;
            }

            //determines whether they are prime to each other or not
            for (int divisor = 2; divisor <= lesser; divisor++)
            {
                if (lesser % divisor == 0 && bigger % divisor == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
