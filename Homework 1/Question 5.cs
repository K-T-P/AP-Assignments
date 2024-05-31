// Koroush Taghi Pour Pasdar
// Student ID : 400521207

using System;

namespace tamrin_seri_1_soal_5
{
    class Program
    {
        static void Main()
        {
            string[] c = Console.ReadLine().Split();
            int n = int.Parse(c[0]);
            int k = int.Parse(c[1]);
            int[] basicNumbers = new int[n];
            int finalNumber = 0;

            //to calculate the factorial of n-1
            int factorial = 1;
            
            for (int i = n - 1; i >= 1; i--)
            {
                factorial *= i;
            }

            //to fill basicNumbers from 1 to n
            for (int i = 0; i < n; i++)
            {
                basicNumbers[i] = i + 1;
                //Console.WriteLine(basicNumbers[i]);
            }
            
            for (int i = 1; i < n; i++)
            {
                finalNumber = (basicNumbers[k / factorial] + finalNumber) * 10;
            
                for (int j = k / factorial; j < n - 1; j++)
                {
                    basicNumbers[j] = basicNumbers[j + 1];
                }
                
                k %= factorial;
                
                if (k == 1 || k == 0)
                {
                    k = 1 - k;
                }

                if (i != (n - 1))
                {
                    factorial /= (n - i);
                }
            }
            finalNumber += basicNumbers[0];
            
            Console.WriteLine(finalNumber);
        }
    }
}
