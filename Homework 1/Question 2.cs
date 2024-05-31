// Koroush Taghi Pour Pasdar
// Student ID : 400521207

using System;

namespace tamrin_seri_1_soal_2
{
    class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            drawTriangle(n);
        }
        
        static void drawTriangle(int n)
        {
            for (int i = 0; i < n * 4 - 1; i++)
            {
                Console.Write('*');
            }
            
            Console.WriteLine();
            
            for (int i = 1; i <= n * 2 - 1; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write(' ');
                }
                
                Console.Write('*');
                
                for (int j = 1; j <= n * 4 - 3 - 2 * i; j++)
                {
                    Console.Write(' ');
                }
                
                if (i != n * 2 - 1)
                {
                    Console.Write('*');
                }
                
                Console.WriteLine();
            }

            if (n == 0)
            {
                return;
            }
            drawTriangle(n - 1);
        }
    }
}
