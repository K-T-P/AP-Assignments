// Koroush Taghi Pour Pasdar
// Student ID : 400521207

using System;

namespace tamrin_seri_1_soal_3
{
    class Program
    {
        static void Main()
        {
            int n = int.Parse(Console.ReadLine());
            string sb = Console.ReadLine();
            
            int homes = 0;
            int amaliyatha = 0;
            string[] h = Console.ReadLine().Split();
            int s = int.Parse(h[0]);
            int t = int.Parse(h[1]);
            
            for (int i = s - 1; i < t; i++)
            {
                if (sb[i] == 'H')
                {
                    homes++;
                }
                else if (sb[i] == 'P' && homes != 0)
                {
                    amaliyatha += howmany(homes);
                    homes = 0;
                }

            }
            Console.WriteLine(amaliyatha);
        }

        static int howmany(int number)
        {
            if (number == 1)
            {
                return 1;
            }
            
            int d = 0;
            
            while (number >= 1)
            {
                int i = 1;
                
                while (number >= i)
                {
                    i *= 2;
                }
                
                i /= 2;
                
                number -= i;
                
                d++;
            }
            return d;
        }
    }
}
