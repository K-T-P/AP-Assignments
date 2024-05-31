using System;
using System.Collections.Generic;

namespace tamrin_seri_3_soal_4
{
    enum newsType { Economic = 12, Social = 23, Crisis = 34, Technology = 45, Sport = 56, Climate = 67 }
    class Program
    {
        static void Main()
        {
            string processFailed = "\nProcess failed";
            bool flagForLogIn = true;
            int indexOfUser = 0;
            while (true)
            {

                try
                {
                    if (flagForLogIn)
                    {
                        Console.WriteLine("Menu\nlog in\tsign up\tAdd contact\t" +
                            "remove contact\nsend NEWS\tedit NEWS\tshow NEWS\tsort NEWS" +
                            "\nsearch NEWS\tdelete NEWS\tchange Password\texit");
                    }
                    else
                    {
                        Console.WriteLine("Menu\n\tsign up\tAdd contact\t" +
                            "remove contact\nsend NEWS\tedit NEWS\tshow NEWS\tsort NEWS" +
                            "\nsearch NEWS\tdelete NEWS\tchange Password\texit");
                    }
                    string order = "";
                    order = Console.ReadLine();
                    if (order == "log in" && flagForLogIn)
                    {
                        try
                        {
                            bool flagToFindUser = true;
                            Console.Write("Please enter user name: ");
                            string userName = Console.ReadLine();
                            for (int i = 0; i < User.userList.Count; i++)
                            {
                                if (userName == User.userList[i].userName)
                                {
                                    indexOfUser = i;
                                    flagToFindUser = false;
                                    break;
                                }
                            }
                            if (flagToFindUser)
                            {
                                throw new Exception("UserNotFound");
                            }
                            Console.Write("Please enter password");
                            string password = Console.ReadLine();
                            if (User.userList[indexOfUser].password == password)
                            {
                                Console.WriteLine("You logged in successfully!");
                                flagForLogIn = false;
                            }
                            else
                            {
                                throw new Exception("WrongPassword");
                            }
                            Console.WriteLine("You logged in seccessfully!");
                        }
                        catch (Exception error) when (error.Message == "WrongPassword")
                        {
                            Console.WriteLine("Wrong password!" + processFailed);
                        }
                        catch (OutOfMemoryException)
                        {
                            Console.WriteLine("There are not enough memory on " +
                                "the device!" + processFailed);
                        }
                        catch (Exception error) when (error.Message == "UserNotFound")
                        {
                            Console.WriteLine("User was not found" + processFailed);
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Input is too big!" + processFailed);
                        }
                    }
                    else if (order == "Add contact")
                    {
                        try
                        {
                            bool flagFoundUserOrNot = true;
                            if (flagForLogIn)
                            {
                                throw new Exception("YouAreNotLoggedIn");
                            }
                            Console.Write("Please enter user's name: ");
                            string addUser = Console.ReadLine();
                            for (int i = 0; i < User.userList.Count; i++)
                            {
                                if (User.userList[i].userName == addUser)
                                {
                                    User.userList[indexOfUser].userContacts.Add(
                                        addUser);
                                    flagFoundUserOrNot = false;
                                    break;
                                }
                            }
                            if (flagFoundUserOrNot)
                            {
                                throw new Exception("UserNotFound");
                            }
                            Console.WriteLine("User added!");
                        }
                        catch (OutOfMemoryException)
                        {
                            Console.WriteLine("There are not enough memory " +
                                "nt the device!" + processFailed);
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Input is too big!" + processFailed);
                        }
                        catch (Exception error) when (error.Message == "UserNotFound")
                        {
                            Console.WriteLine("User was not found!" + processFailed);
                        }
                    }
                    else if (order == "edit NEWS")
                    {
                        try
                        {
                            bool flagIDNotFound = true;
                            int newsIndex = 0;
                            Console.Write("Please enter the ID of the News: ");
                            int ID = int.Parse(Console.ReadLine());
                            for (int i = 0; i < News.newsList.Count; i++)
                            {
                                if (News.newsList[i].newsID == ID)
                                {
                                    newsIndex = i;
                                    flagIDNotFound = false;
                                    break;
                                }
                            }
                            if (flagIDNotFound)
                            {
                                throw new Exception("NewsNotFound");
                            }
                            Console.Write("Please enter your news: ");
                            string newsTitle = Console.ReadLine();
                            News.newsList[newsIndex].newsTitle = newsTitle;
                            Console.WriteLine("News edited successfully!");
                        }
                        catch (Exception error) when (error.Message == "NewsNotFound")
                        {
                            Console.WriteLine("News with this ID was not found!" +
                                processFailed);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Input is invalid!" + processFailed);
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Input is too big!" + processFailed);
                        }
                    }
                    else if (order == "show NEWS")
                    {
                        if (flagForLogIn)
                        {
                            throw new Exception("YouAreNoyLoggedIn");
                        }
                        for (int i = 0; i < User.userList[indexOfUser].receivedNews.Count; i++)
                        {
                            Console.WriteLine(User.userList[indexOfUser].receivedNews[i].newsTitle);
                        }
                        for (int i = 0; i < User.userList[indexOfUser].sendNews.Count; i++)
                        {
                            Console.WriteLine(User.userList[indexOfUser].sendNews[i].newsTitle);
                        }
                    }
                    else if(order== "sort NEWS")
                    {
                        try
                        {
                            Console.WriteLine("Please enter the number\n" +
                                "1-By time\t2-By ID");
                            int number = int.Parse(Console.ReadLine());
                            if (number == 1)
                            {
                                int index = 0;
                                for(int i = 0; i < News.newsList.Count * News.newsList.Count; i++)
                                {
                                    if(News.newsList[index].sendTime>
                                        News.newsList[index + 1].sendTime)
                                    {
                                        News news = News.newsList[index];
                                        News.newsList[index] = News.newsList[index + 1];
                                        News.newsList[index + 1] = news;
                                    }
                                    index++;
                                    if (index == News.newsList.Count - 1)
                                    {
                                        index = 0;
                                    }
                                }
                                for(int i = 0; i < News.newsList.Count; i++)
                                {
                                    Console.WriteLine(News.newsList[i].newsTitle);
                                }
                            }
                            else if (number == 2)
                            {
                                int index = 0;
                                for (int i = 0; i < News.newsList.Count * News.newsList.Count; i++)
                                {
                                    if (News.newsList[index].newsID >
                                        News.newsList[index + 1].newsID)
                                    {
                                        News news = News.newsList[index];
                                        News.newsList[index] = News.newsList[index + 1];
                                        News.newsList[index + 1] = news;
                                    }
                                    index++;
                                    if (index == News.newsList.Count - 1)
                                    {
                                        index = 0;
                                    }
                                }
                                for (int i = 0; i < News.newsList.Count; i++)
                                {
                                    Console.WriteLine(News.newsList[i].newsTitle);
                                }
                            }
                            else
                            {
                                throw new Exception("WrongInput");
                            }
                        }
                        catch(Exception error)when(error.Message== "WrongInput")
                        {
                            Console.WriteLine("Invalid input"+processFailed);
                        }
                        catch (OutOfMemoryException)
                        {
                            Console.WriteLine("There are not enough memory on the device!" +
                                ""+processFailed);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input!"+processFailed);
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Input is too big!"+processFailed);
                        }
                    }
                    else if(order== "search NEWS")
                    {
                        try
                        {
                            Console.WriteLine("Please enter the number: \n1-search by " +
                                "username\t2-search by news type");
                            int number = int.Parse(Console.ReadLine());
                            if (number == 1)
                            {
                                int userIndex = 0;
                                bool flagFindUser = true;
                                Console.Write("Enter user name: ");
                                string userName = Console.ReadLine();
                                for(int i = 0; i < User.userList.Count; i++)
                                {
                                    if (User.userList[i].userName == userName)
                                    {
                                        userIndex = i;
                                        flagFindUser = false;
                                        break;
                                    }
                                }
                                if (flagFindUser)
                                {
                                    throw new Exception("UserNotFound");
                                }
                                for(int i = 0; i < User.userList[userIndex].sendNews.Count; i++)
                                {
                                    Console.WriteLine(User.userList[userIndex].sendNews[i].newsTitle);
                                    Console.WriteLine(User.userList[userIndex].sendNews[i].newsType);
                                }
                            }
                            else if (number == 2)
                            {
                                Console.WriteLine("Please enter the news type: ");
                                newsType type = (newsType)Enum.Parse(
                                    typeof(newsType), Console.ReadLine(), true);
                                for(int i =0; i < News.newsList.Count; i++)
                                {
                                    Console.WriteLine(News.newsList[i].newsTitle);
                                }
                            }
                            else
                            {
                                throw new Exception("WrongInput");
                            }
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("Invalid input");
                        }
                        catch(Exception error)when(error.Message== "UserNotFound")
                        {
                            Console.WriteLine("Entered user was not found!"+
                                processFailed);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input");
                        }
                        catch (OutOfMemoryException)
                        {
                            Console.WriteLine("There are not enough memory on your" +
                                "device!"+processFailed);
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Input is too big!"+processFailed);
                        }
                        catch(Exception error)when(error.Message== "WrongInput")
                        {
                            Console.WriteLine("Wrong input!"+processFailed);
                        }
                    }
                    else if(order== "exit")
                    {
                        if (flagForLogIn)
                        {
                            Console.WriteLine("Program ended!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Logged out!");
                            flagForLogIn = true; ;
                        }
                    }
                }
                catch (Exception error) when (error.Message == "YouAreNoyLoggedIn")
                {
                    Console.WriteLine("You are not logged in ");
                }
                catch (OutOfMemoryException)
                {
                    Console.WriteLine("There are not enough memory on " +
                        "the device!\nProgram stopped!");
                    break;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Input is too big!" + processFailed);
                }
                catch (System.IO.IOException)
                {

                }
            }
        }
    }
    class User
    {
        static public List<User> userList = new List<User>();
        public List<string> userContacts = new List<string>();
        public List<News> sendNews = new List<News>();
        public List<News> receivedNews = new List<News>();
        public string userName;
        public string password;
    }
    class News
    {
        static public List<News> newsList = new List<News>();
        public newsType newsType;
        public int newsID;
        public string senderName;
        public string receiverName;
        public int sendTime;
        public string newsTitle;
    }
}
