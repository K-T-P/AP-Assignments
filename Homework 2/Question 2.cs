// Koroush Taghi Pour
// Student ID: 400521207
using System;

namespace tamrin_seri_2_soal_2
{
    public class Program
    {
        public static void Main()
        {
            //strings to keep the needing strings to print the converted number
            String[] units = { "zero", "one", "two", "three",
                               "four", "five", "six", "seven",
                               "eight",  "nine","ten", "eleven",
                               "twelve","thirteen", "fourteen",
                               "fifteen","sixteen","seventeen",
                               "eighteen", "nineteen" };
            String[] tens = { "", "", "twenty", "thirty",
                              "forty","fifty", "sixty",
                              "seventy", "eighty", "ninety" };

            //flag is used for one situation: when the enterd number is zero. It is an exception to avoid situations like :Input: 22 Output: twenty two zero
            bool flag = false;

            //receives the number
            int number = int.Parse(Console.ReadLine());

            //executes when number is equal to zero
            if (number == 0)
            {
                flag = true;
            }

            //prints returned string
            Console.WriteLine(transformer(number, units, tens, flag));

        }

        //method to return the string concerned with the input number
        public static string transformer(int number, string[] units, string[] tens, bool flag)
        {
            if (flag)          //only when main number is zero
            {
                return units[0];
            }

            //for too large or too small numbers
            if (number > 999)
            {
                return "Too Large";
            }
            if (number < 999 * (-1))
            {
                return "Too Small";
            }

            //for numbers less than zero
            if (number < 0)
            {
                return "minus " + transformer(number * (-1), units, tens, flag);
            }

            //for numbers bigger than or equal to 100
            if (number >= 100)
            {
                if (number % 100 == 0)
                {
                    return units[number / 100] + '-' + "hundred";
                }
                else
                {
                    return units[number / 100] + '-' + "hundred and " + transformer(number % 100, units, tens, flag);
                }
            }

            //for numbers less than 100
            else
            {
                if (number < 20)
                {
                    if (number == 0)
                    {
                        return "";
                    }
                    return units[number];
                }
                if (number >= 20)
                {
                    string letter = tens[number / 10];
                    while (number >= 10)
                    {
                        number -= 10;
                    }
                    return letter + ' ' + transformer(number, units, tens, flag);
                }
            }
            return "";
        }
    }
}
