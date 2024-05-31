// Koroush Taghi Pour Pasdar
// Student ID : 400521207
using System;

namespace tamrin_seri_1_soal_4
{
    class Program
    {
        static void Main()
        {
            Random rnd = new Random();
            int number = rnd.Next(1, 100000000);
            
            Console.WriteLine(colatz(number));
        }

        static int colatz(int number)
        {
            int count = 0;
            bool flag;
            
            do
            {
                flag = primePrimeOrNot(number);
                
                if (flag)
                {
                    Console.WriteLine(number + " Y");
                }
                else
                {
                    Console.WriteLine(number + " N");
                }
                
                if (number % 2 == 0) 
                {
                    number /= 2;
                }
                else
                {
                    number += 1;
                }
                count++;
            } while (number != 1);

            Console.WriteLine("1 N");
            
            return count;
        }

        //check mikonad ke ada avale aval ast ya na
        static bool primePrimeOrNot(int number)
        {
            bool flag = IsPrimeOrNot(number);
            
            if (flag)
            {
                if (number < 10)
                {
                    return true;
                }
                number = sumOfLetters(number);
                return primePrimeOrNot(number);
            }
            else
            {
                return false;
            }
        }

        //jame ragam hara hesab mikonad
        static int sumOfLetters(int number)
        {
            int sum = 0;
            
            while (number > 0)
            {
                sum += number % 10;
                number /= 10;
            }
            return sum;
        }

        //check mikonad ke adad aval ast ya na
        static bool IsPrimeOrNot(int number)
        {
            if (number == 1)
            {
                return false;
            }
            for (int i = 2; i < number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
