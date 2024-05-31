using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment11
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var data = File.ReadAllLines(@"..\..\IMDB-Movie-Data.csv")
                    .Skip(1)
                    .Select(line => new IMDBData(line));
                Console.WriteLine($"The film with highest metascore : {data.GetHighestMetascore().Title}");

                // If necessary, you can use more than one extension method to calculate these answers.
                Console.WriteLine($"Question 1: {data.Question1()}\n");
                Console.WriteLine($"Question 2: {data.Question2()}\n");
                Console.WriteLine($"Question 3: {data.Question3()}\n");
                Console.WriteLine($"Question 4: {data.Question4Method()} \n");
                Console.WriteLine($"Question 5: {data.Question5()} \n");
                Console.WriteLine($"Question 6: {data.Question6()} \n");
                Console.WriteLine($"Question 7: {data.Question7()}\n");
                Console.WriteLine($"Question 8: {data.Question8()}\n");
                Console.WriteLine($"Question 9: {data.Question9()}\n");
                Console.WriteLine($"Question 10: {data.Question10()}\n");
                Console.WriteLine($"Question 11: {data.ExtensionMethodPlaceHolder()}\n");
                Console.WriteLine($"Question 12: {data.Question12()}\n");
                Console.WriteLine($"Question 13: {data.Question13()}\n");
                Console.WriteLine($"Question 14: {data.Question14()}\n");
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
    }

    public static class Extensions
    {
        public static Nullable<int> ParseIntOrNull(this string str)
            => !string.IsNullOrEmpty(str) ? int.Parse(str) as Nullable<int> : null;
        public static string ParseStringOrNull(this string str)
            => !string.IsNullOrEmpty(str) ? str : null;

        //For example
        public static IMDBData GetHighestMetascore(this IEnumerable<IMDBData> data)
            => data.OrderByDescending(x => x.Metascore).First();

        /// <summary>
        /// you must modify the name of this method and its 
        /// implementation to fit your need and create more methods like this
        public static IMDBData ExtensionMethodPlaceHolder(this IEnumerable<IMDBData> data)
            => data.First();

        public static IMDBData HighestVote(this IEnumerable<IMDBData> data)
            => data.Where(x => x.Year == 2006).OrderByDescending(x => x.Year).First();

        //Question 1
        public static string Question1(this IEnumerable<IMDBData> data)
        {
            string Genres = "\n";
            var films = data.Where(x => x.Runtime <= 100);
            foreach (var film in films) { Genres += (film.Genre + ' '); }
            return Genres;
        }

        //Question 2
        public static string Question2(this IEnumerable<IMDBData> data)
        {
            string directors = "\n";
            var films =
                data
                .Where(x =>
                (x.Actor1.Contains("Vin Diesel")) ||
                (x.Actor2.Contains("Vin Diesel")) ||
                (x.Actor3.Contains("Vin Diesel")) ||
                (x.Actor4.Contains("Vin Diesel")));
            foreach (var film in films)
            {
                directors += (film.Director + '\n');
            }
            return directors;
        }

        static public string Question3(this IEnumerable<IMDBData> data)
        {
            var film=data.OrderByDescending(x => x.Votes).First();
            return "Title : " + film.Title +
                "\nRank : " + film.Rank +
                "\nRating : " + film.Rating +
                "\nDirector : " + film.Director +
                "\nActor 1 : " + film.Actor1 +
                "\nActor 2 : " + film.Actor2 +
                "\nActor 3 : " + film.Actor3 +
                "\nActor 4 : " + film.Actor4;
                }
        //Question 4
        public static string Question4Method(this IEnumerable<IMDBData> data)
        {
            string returnFilms = "";
            var films = data
                .Where(x => x.Director == "Bryan Singer")
                .OrderByDescending(x => x.Revenue);
            foreach (var film in films)
            {
                returnFilms += (film.Title.ToString() + ' ');
            }
            return returnFilms;
        }

        //Question 5
        static public double Question5(this IEnumerable<IMDBData> data)
        {
            double totalRevenue = 0;
            var films = data.Where(x => x.Year == 2011);
            foreach (var film in films)
            {
                totalRevenue += double.Parse(film.Revenue);
            }
            return totalRevenue;
        }

        //Question 6
        static public double Question6(this IEnumerable<IMDBData> data)
        {
            double totalRevenue = 0;
            var films = data.Where(x => x.Year.ToString().Contains("2014"));
            foreach (var film in data)
            {
                try
                {
                    totalRevenue += double.Parse(film.Revenue);
                }
                catch { }
            }
            return totalRevenue / films.Count();
        }

        //Question 7
        static public string Question7(this IEnumerable<IMDBData> data)
        {
            string filmsString = "";
            var films = data
                .Where(x => x.Genre.Contains("Action"))
                .OrderByDescending(x => x.Revenue)
                .Take(10);
            foreach (var film in films)
            {
                filmsString += (film.Title + '\n');
            }
            return filmsString;
        }

        //Question 8
        static public string Question8(this IEnumerable<IMDBData> data)
        {
            string filmString = "";
            var films = data
                 .Where(x =>
                     x.Title.Contains('1') || x.Title.Contains('2') ||
                     x.Title.Contains('3') || x.Title.Contains('4') ||
                     x.Title.Contains('5') || x.Title.Contains('6') ||
                     x.Title.Contains('7') || x.Title.Contains('8') ||
                     x.Title.Contains('9') || x.Title.Contains('0'));
            foreach (var film in films)
            {
                filmString += (film.Title + '\n');
            }
            return filmString;
        }

        //Question 9
        static public string Question9(this IEnumerable<IMDBData> data)
        {
            string jenniferFilmString = "\nJennifer Lawrence Films :\n";
            string HathawayFilmString = "Anne Hathaway Films:\n";
            var hathawayFilms = data.Where(x =>
                         x.Actor1.Contains("Anne Hathaway") ||
                         x.Actor2.Contains("Anne Hathaway") ||
                         x.Actor3.Contains("Anne Hathaway") ||
                         x.Actor4.Contains("Anne Hathaway"))
                .OrderByDescending(x => x.Rating);
            var jenniferFilms = data.Where(x =>
                          (x.Actor1.Contains("Jennifer Lawrence")) ||
                          (x.Actor2.Contains("Jennifer Lawrence")) ||
                          (x.Actor3.Contains("Jennifer Lawrence")) ||
                          (x.Actor4.Contains("Jennifer Lawrence")))
                .OrderBy(x => x.Year);
            foreach (var film in hathawayFilms)
            {
                HathawayFilmString += (film.Title + '\n');
            }

            foreach (var film in jenniferFilms)
            {
                jenniferFilmString += (film.Title + '\n');
            }
            return HathawayFilmString + jenniferFilmString;
        }

        //Question 10
        static public string Question10(this IEnumerable<IMDBData> data)
        {
            string numberOfComedyFilms = "\nNumber of Comedy films which rating is more" +
                " than 8 : ";

            numberOfComedyFilms +=
                data
                 .Where(x => x.Genre.Contains("Comedy"))
                 .Where(x => double.Parse(x.Rating) > 8)
                 .Count()
                 .ToString();

            string numberOFDramaFilms = "Number of Drama films which rating is more " +
                "than 8 : ";
            numberOFDramaFilms +=
                data
                .Where(x => x.Genre.Contains("Drama"))
                .Where(x => double.Parse(x.Rating) > 8)
                .Count()
                .ToString();
            return
                numberOfComedyFilms + '\n' + numberOFDramaFilms;
        }

        //Question 12
        static public string Question12(this IEnumerable<IMDBData> data)
        {
            string filmString = "\n";
            var films = data
                .Where(x => x.Title.Length > "Prometheus".Length);
            foreach (var film in films)
            {
                filmString += (film.Title + '\n');
            }
            return filmString;
        }

        //Question 13
        static public string Question13(this IEnumerable<IMDBData> data)
        {
            string filmString = "\n";
            var films = data
                .Where(x => !(!(x.Genre.Contains("Action")) && !(x.Year.ToString().Contains("2014"))));
            foreach (var film in films)
            {
                filmString += (film.Title + '\n');
            }
            return filmString;
        }

        //Question 14
        static public string Question14(this IEnumerable<IMDBData> data)
        {
            string filmString = "\n";
            var films = data
                .Where(x => x.Genre == "Comedy")
                .OrderByDescending(x => x.Rank)
                .Skip(3)
                ;
            foreach (var film in films)
            {
                filmString += (film.Title + '\n');
            }
            return filmString;
        }
    }



    public class IMDBData
    {
        public IMDBData(string line)
        {
            var toks = line.Split(',');
            Rank = int.Parse(toks[0]);
            Title = toks[1];
            Genre = toks[2];
            Director = toks[3];
            Actor1 = toks[4];
            Actor2 = toks[5];
            Actor3 = toks[6];
            Actor4 = toks[7];
            Year = int.Parse(toks[8]);
            Runtime = int.Parse(toks[9]);
            Rating = (toks[10]);
            Votes = int.Parse(toks[11]);
            Revenue = toks[12].ParseStringOrNull();
            Metascore = toks[13].ParseIntOrNull();
        }
        public int Rank;
        public string Title;
        public string Genre;
        public string Director;
        public string Actor1;
        public string Actor2;
        public string Actor3;
        public string Actor4;
        public int Year;
        public int Runtime;
        public string Rating;
        public int Votes;
        public string Revenue;
        public Nullable<int> Metascore;
    }
}

//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Assignment11
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            var data = File.ReadAllLines(@"..\..\IMDB-Movie-Data.csv")
//                .Skip(1)
//                .Select(line => new IMDBData(line));
//            Console.WriteLine($"The film with highest metascore : {data.GetHighestMetascore().Title}");

//            // If necessary, you can use more than one extension method to calculate these answers.
//            Console.WriteLine($"Question 1: {data.ExtensionMethodPlaceHolder()}");
//            Console.WriteLine($"Question 2: {data.ExtensionMethodPlaceHolder()}");
//            Console.WriteLine($"Question 3: {data.ExtensionMethodPlaceHolder()}");
//            Console.WriteLine($"Question 4: {data.ExtensionMethodPlaceHolder()}");
//            Console.WriteLine($"Question 5: {data.ExtensionMethodPlaceHolder()}");
//            Console.WriteLine($"Question 6: {data.ExtensionMethodPlaceHolder()}");
//            Console.WriteLine($"Question 7: {data.ExtensionMethodPlaceHolder()}");
//            Console.WriteLine($"Question 8: {data.ExtensionMethodPlaceHolder()}");
//            Console.WriteLine($"Question 9: {data.ExtensionMethodPlaceHolder()}");
//            Console.WriteLine($"Question 10: {data.ExtensionMethodPlaceHolder()}");

//        }
//    }

//    public static class Extensions
//    {
//        public static Nullable<int> ParseIntOrNull(this string str)
//            => !string.IsNullOrEmpty(str) ? int.Parse(str) as Nullable<int> : null;
//        public static string ParseStringOrNull(this string str)
//            => !string.IsNullOrEmpty(str) ? str : null;

//        //For example
//        public static IMDBData GetHighestMetascore(this IEnumerable<IMDBData> data)
//            => data.OrderByDescending(x => x.Metascore).First();

//        /// <summary>
//        /// you must modify the name of this method and its 
//        /// implementation to fit your need and create more methods like this
//        public static IMDBData ExtensionMethodPlaceHolder(this IEnumerable<IMDBData> data)
//            => data.First();

//    }



//    public class IMDBData
//    {
//        public IMDBData(string line)
//        {
//            var toks = line.Split(',');
//            Rank = int.Parse(toks[0]);
//            Title = toks[1];
//            Genre = toks[2];
//            Director = toks[3];
//            Actor1 = toks[4];
//            Actor2 = toks[5];
//            Actor3 = toks[6];
//            Actor4 = toks[7];
//            Year = int.Parse(toks[8]);
//            Runtime = int.Parse(toks[9]);
//            Rating = (toks[10]);
//            Votes = int.Parse(toks[11]);
//            Revenue = toks[12].ParseStringOrNull();
//            Metascore = toks[13].ParseIntOrNull();
//        }
//        public int Rank;
//        public string Title;
//        public string Genre;
//        public string Director;
//        public string Actor1;
//        public string Actor2;
//        public string Actor3;
//        public string Actor4;
//        public int Year;
//        public int Runtime;
//        public string Rating;
//        public int Votes;
//        public string Revenue;
//        public Nullable<int> Metascore;
//    }
//}
