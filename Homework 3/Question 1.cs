using System;
using System.IO;

namespace tamrin_seri_3_soal_1
{
    class Program
    {
        static void Main()
        {
            //string a = "aeiouAEIOU ";
            //for(int i = 0; i < a.Length; i++)
            //{
            //    Console.WriteLine((int)a[i]);
            //}
            Analizer analyzer = new Analizer();
            StreamWriter writer = null;
            StreamReader reader = null;
            bool flag = false;
            try
            {
                reader = new StreamReader("a1.txt");
                writer = new StreamWriter("a2.txt");
                flag =true;
                string line = "";
                for (int i = 0; reader.EndOfStream == false; i++)
                {
                    line = reader.ReadLine();
                    writer.WriteLine(analyzer.stringReceiver(line));
                }
                analyzer.DisplayResult();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File was not found!\nPlease \n1-rename the file, which will be analized, to \"a1.txt\"\n2-move it to the directory" +
                    " in which this program is.\nThen run this program again!");
            }
            catch (OverflowException)
            {
                Console.WriteLine("File is too big to be analized!");
            }
            finally
            {
                if (flag)
                {
                    reader.Close();
                    writer.Close();
                }
            }

        }
    }
    class Analizer
    {
        private string lineWhichWillBeReturned;
        private int countOf_Lines = 0;
        private int countOf_Numbers = 0;
        private int countOf_Stars = 0;
        private int countOf_VowelSounds = 0;
        private int countOf_startWith_a_endWith_e = 0;
        private int countOf_student = 0;
        public void DisplayResult()
        {
            Console.WriteLine("Number of lines: " + countOf_Lines);
            Console.WriteLine("Number of stars : " + countOf_Stars);
            Console.WriteLine("Number of digits : " + countOf_Numbers);
            Console.WriteLine("Number of vowel sounds : " + countOf_VowelSounds);
            Console.WriteLine("Number of words start " +
                "with 'a' and end with 'e' : " + countOf_startWith_a_endWith_e);
            Console.WriteLine("Number of \"student\" : " + countOf_student);
        }
        public string stringReceiver(string line)
        {
            this.lineWhichWillBeReturned = line;
            count_Increaser();
            Number_Counter(line);
            Star_Counter(line);
            Vowel_Counter(line);
            Start_a_End_e_Counter(line);
            Student_Counter(line);
            return line.Replace(' ', '*');
        }
        private void Student_Counter(string line)
        {
            string[] wordGrp = line.Split();
            for (int i = 0; i < wordGrp.Length; i++)
            {
                if (wordGrp[i].ToLower().Contains("student"))
                {
                    countOf_student++;
                }
            }
        }
        private void Start_a_End_e_Counter(string line)
        {
            string[] wordGrp = line.Split();
            for (int i = 0; i < wordGrp.Length; i++)
            {
                if (wordGrp[i][0] == 'a' && wordGrp[i][wordGrp[i].Length - 1] == 'e')
                {
                    countOf_startWith_a_endWith_e++;
                }
            }
        }
        private void Vowel_Counter(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                switch ((int)line[i])
                {
                    case 97:
                    case 101:
                    case 105:
                    case 111:
                    case 117:
                    case 65:
                    case 69:
                    case 73:
                    case 79:
                    case 85: countOf_VowelSounds++; break;
                    default: break;
                }
            }
        }
        private void Star_Counter(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if ((int)line[i] == 32 || (int)line[i] == 42)
                {
                    countOf_Stars++;
                }
            }

        }
        private void Number_Counter(string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (((int)line[i] >= 48) && ((int)line[i] <= 57))
                {
                    countOf_Numbers++;
                }
            }
        }
        private void count_Increaser()
        {
            countOf_Lines++;
        }
    }
}
