// Koroush Taghi Pour Pasdar
// Student ID : 400521207

using System;

namespace tamrin_seri_1_soal_1
{
    class Program
    {
        static void Main()
        {
            int numberOfStrings = int.Parse(Console.ReadLine());
            string[,] groupOfString = new string[numberOfStrings, 2];
            for (int i = 0; i < numberOfStrings; i++)
            {
                groupOfString[i, 0] = Console.ReadLine();
                groupOfString[i, 1] = Console.ReadLine();
            }
            for (int i = 0; i < numberOfStrings; i++)
            {
                subStringCounter(groupOfString[i, 0], groupOfString[i, 1]);
            }
        }
        static void subStringCounter(string numberOfString, string groupOfStrings)
        {
            int theCountOfTheSubs = -1;
            int countOfLettera = 0;
            int countOfLetterb = 0;
            int countOfLetterc = 0;
            for (int i = 0; i < int.Parse(numberOfString) - 1; i++)
            {
                countOfLettera = 0;
                countOfLetterb = 0;
                countOfLetterc = 0;
                for (int j = i; j < int.Parse(numberOfString); j++)
                {
                    if (groupOfStrings[j] == 'a')
                    {
                        countOfLettera++;
                    }
                    else if (groupOfStrings[j] == 'b')
                    {
                        countOfLetterb++;
                    }
                    else
                    {
                        countOfLetterc++;
                    }
                    if (i == j)
                    {
                        continue;
                    }
                    if (countOfLettera > countOfLetterb && countOfLettera > countOfLetterc)
                    {
                        if (theCountOfTheSubs == -1)
                        {
                            theCountOfTheSubs = j - i + 1;
                        }
                        else if (theCountOfTheSubs > j - i)
                        {
                            theCountOfTheSubs = j - i + 1;
                        }
                        else
                        {
                        }
                    }
                }
            }
            Console.WriteLine(theCountOfTheSubs);
        }
    }
}
