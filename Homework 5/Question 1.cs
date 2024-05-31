using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace tamrin_seri_5_soal_1
{
    enum meeting
    {
        TradeShow,
        Ceremony,
        VIP,
        conference
    }
    public class Program
    {
        static public void Main()
        {
            try
            {
                    try
                    {
                        registerMenu();
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Input is too big!");
                    }
            }
            catch
            {
                Console.WriteLine("An Error occured! Program stopped!");
            }
        }
        //register menu
        static public void registerMenu()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("\nRegister Menu:\n" +
                        "Register\tLogin\tChange Password\t" +
                        "Remove\tShow All Usernames\tExit");
                    string order = Console.ReadLine();
                    if (order == "Register")
                    {
                        Register();
                    }
                    else if (order == "Login")
                    {
                        Login();
                    }
                    else if (order == "Change Password")
                    {
                        Change_Password();
                    }
                    else if (order == "Remove")
                    {
                        Remove();
                    }
                    else if (order == "Show All Usernames")
                    {
                        Show_All_Usernames();
                    }
                    else if (order == "Exit")
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input!");
                    }
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Input is too big!");
                }
            }
        }
        //checked
        static public void Register()
        {
            try
            {
                Console.Write("Please enter your new user name: ");
                string newUserName = Console.ReadLine();
                if (User.userList.Find(x => x.userName == newUserName) != null)
                {
                    throw new Exception("UserNameAlreadyUsed");
                }
                Console.Write("Please enter your password: ");
                string newPassword = Console.ReadLine();
                if (checkPassword(newPassword) == false)
                {
                    throw new Exception("InvalidPasswordFormat");
                }
                User newUSer = new User(newUserName, newPassword);
                User.userList.Add(newUSer);

                Calender newCalender = new Calender();
                newUSer.calendersOfUser.Add(newCalender);
                Console.WriteLine("Done!");
            }
            catch (Exception error) when (error.Message == "InvalidPasswordFormat")
            {
                Console.WriteLine("Invalid password format!");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Input is too big!");
            }
            catch (Exception error) when (error.Message == "UserNameAlreadyUsed")
            {
                Console.WriteLine("Entered user name has already used!");
            }
        }
        //checked
        static public void Login()
        {
            try
            {
                Console.Write("Please enter your user name: ");
                string userName = Console.ReadLine();
                int indexOfUser = 0;
                if (User.userList.Find(user => user.userName == userName) == null)
                {
                    throw new Exception("UserNotFound");
                }
                else
                {
                    indexOfUser = User.userList.FindIndex(user => user.userName == userName);
                }

                Console.Write("Please enter your password: ");
                string password = Console.ReadLine();
                if (User.userList[indexOfUser].password == password)
                {
                    mainMenu(userName, indexOfUser);
                }
                else
                {
                    throw new Exception("WrongPassword");
                }
            }
            catch (Exception error) when (error.Message == "WrongPassword")
            {
                Console.WriteLine("Wrong password!");
            }
            catch (Exception error) when (error.Message == "UserNotFound")
            {
                Console.WriteLine("Entered user was not found!");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Input is too big!");
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
        //checked
        static public void Change_Password()
        {
            try
            {
                Console.WriteLine("Please enter your user name: ");
                string userName = Console.ReadLine();
                if (User.userList.Find(user => user.userName == userName) == null)
                {
                    throw new Exception("Entered user was not found!");
                }

                int indexUser = User.userList.FindIndex(user => user.userName == userName);
                Console.WriteLine("Please enter your old password: ");
                string oldPassword = Console.ReadLine();
                if (User.userList[indexUser].password != oldPassword)
                {
                    throw new Exception("InvalidPassword");
                }

                Console.WriteLine("Please enter your new password: ");
                string newPassword = Console.ReadLine();
                if (checkPassword(newPassword) == false)
                {
                    throw new Exception("InvalidPasswordFormat");
                }
                User.userList[indexUser].password = newPassword;
                Console.WriteLine("Password was changed successfully!");
            }
            catch (Exception error) when (error.Message == "InvalidPasswordFormat")
            {
                Console.WriteLine("Invalid password format");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Input is too big!");
            }
            catch (Exception error) when (error.Message == "InvalidPassword")
            {
                Console.WriteLine("Invalid password!");
            }
        }
        //checked
        static public void Remove()
        {
            try
            {
                Console.Write("Please enter your user name: ");
                string userNameToDelete = Console.ReadLine();
                int indexOfUser = -1;
                if (User.userList.Find(user => user.userName == userNameToDelete) == null)
                {
                    throw new Exception("EnteredUserNotFound");
                }
                else
                {
                    indexOfUser = User.userList.FindIndex(user => user.userName == userNameToDelete);
                }

                Console.Write("Please enter your password: ");
                string password = Console.ReadLine();
                if (User.userList[indexOfUser].password != password)
                {
                    throw new Exception("WrongPassword");
                }
                User.userList.RemoveAt(indexOfUser);
                Console.WriteLine("User was removed successfully!");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Input is too big!");
            }
            catch (Exception error) when (error.Message == "WrongPassword")
            {
                Console.WriteLine("Incorrect password!");
            }
            catch (Exception error) when (error.Message == "EnteredUserNotFound")
            {
                Console.WriteLine("Entered user was not found!");
            }
        }
        //checked
        static public void Show_All_Usernames()
        {
            if (User.userList.Count == 0)
            {
                Console.WriteLine("nothing");
            }
            else
            {
                List<User> userGrp = User.userList;
                userGrp.OrderBy(user => user.userName);
                foreach (User user in userGrp)
                {
                    Console.WriteLine(user.userName);
                }
            }
            Console.WriteLine("Process finished successfully!");
        }
        //checked
        static public bool checkPassword(string password)
        {
            if (password.Length < 5)
            {
                return false;
            }
            bool contains_UPPERCASE_Alphabet = false;
            bool contains_lowercase_alphabet = false;
            bool contains_numbers = false;
            for (int i = 0; i < password.Length; i++)
            {
                //underline
                if ((int)password[i] == 95)
                {
                    continue;
                }
                //lowercase alphabet
                else if ((int)password[i] >= 97 && (int)password[i] <= 122)
                {
                    contains_lowercase_alphabet = true;
                    continue;
                }
                //uppercase alphabet
                else if ((int)password[i] >= 65 && (int)password[i] <= 90)
                {
                    contains_UPPERCASE_Alphabet = true;
                    continue;
                }
                //digits
                else if ((int)password[i] >= 48 && (int)password[i] <= 57)
                {
                    contains_numbers = true;
                    continue;
                }
                else
                {
                    return false;
                }
            }
            if (
                contains_lowercase_alphabet == false ||
                contains_UPPERCASE_Alphabet == false ||
                           contains_numbers == false)
            {
                return false;
            }
            return true;
        }

        //main menu
        static public void mainMenu(string userName, int IndexOfUser)
        {
            Calender activeCalender = null;

            while (true)
            {
                try
                {
                    Console.WriteLine("\nMain menu:\nCreate New Calender\t" +
                        "Open Calender\tEnable Calender\nDisable Calender\t" +
                        "Delete Calender\tEdit Calender\nShow\tEvents On\t" +
                        "Show Enabled Calenders\nLogout");
                    string order = Console.ReadLine();
                    if (order == "Create New Calender") { CreateNewCalender(IndexOfUser); }
                    else if (order == "Open Calender") { OpenCalender(IndexOfUser); }
                    else if (order == "Enable Calender") { EnableCalender(IndexOfUser); }
                    else if (order == "Disable Calender") { DisableCalender(IndexOfUser); }
                    else if (order == "Delete Calender") { deleteCalender(IndexOfUser); }
                    else if (order == "Edit Calender") { EditCalender(IndexOfUser); }
                    else if (order == "Show") { }
                    else if (order == "Events On") { }
                    else if (order == "Show Enabled Calenders") { ShowEnabledCalenders(IndexOfUser); }
                    else if (order == "Logout") { break; }
                    else { Console.WriteLine("Invalid input!"); }
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Input is too big!");
                }
            }
        }
        static public void CreateNewCalender(int indexOfUser)
        {
            try
            {
                Console.Write("Please enter the new title of the calender: ");
                string newCalenderTitle = Console.ReadLine();
                Calender newCalender = new Calender();
                User.userList[indexOfUser].calendersOfUser.Add(newCalender);
                Console.WriteLine("Calender added successfully!");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Input is too big!");
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
        static public void OpenCalender(int indexOfUser)
        {
            try
            {
                Console.Write("Please enter the ID of the Calender: ");
                int calenderID = int.Parse(Console.ReadLine());

                if (User.userList[indexOfUser].calendersOfUser.Find(cal => cal.ID == calenderID) == null)
                {
                    throw new Exception("CalenderNotFound");
                }
                else
                {
                    int indexOfCalender = User.userList[indexOfUser].calendersOfUser.FindIndex(cal => cal.ID == calenderID);
                    calenderMenu(indexOfUser, indexOfCalender);
                }
            }
            catch (Exception error) when (error.Message == "CalenderNotFound")
            {
                Console.WriteLine("Entered calender was not found!");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Input is too big!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input!");
            }
        }
        //checked
        static public void EnableCalender(int indexOfUser)
        {
            try
            {
                Console.Write("Please enter the ID of the calender: ");
                int CalenderID = int.Parse(Console.ReadLine());
                if (User.userList[indexOfUser].calendersOfUser.Find(cal => cal.ID == CalenderID) != null)
                {
                    User.userList[indexOfUser].calendersOfUser.Find(cal => cal.ID == CalenderID).enableOrDisable = true;
                }
                else
                {
                    throw new Exception("CalenderNotFound");
                }
                Console.WriteLine("Entered Calender enabled!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input!");
            }
            catch (Exception error) when (error.Message == "CalenderNotFound")
            {
                Console.WriteLine("Input calender was not found! Or This person does not own that calender!");
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
        //checked
        static public void DisableCalender(int indexOfUser)
        {
            try
            {
                Console.Write("Please enter the ID of the calender: ");
                int calenderID = int.Parse(Console.ReadLine());
                if (User.userList[indexOfUser].calendersOfUser.Find(cal => cal.ID == calenderID) != null)
                {
                    User.userList[indexOfUser].calendersOfUser.Find(cal => cal.ID == calenderID).enableOrDisable = false;
                }
                else
                {
                    throw new Exception("EnteredIDNotFound");
                }
                Console.WriteLine("Entered calender disabled!");
            }
            catch (Exception error) when (error.Message == "EnteredIDNotFound")
            {
                Console.WriteLine("Input calender was not found! Or This person does not own that calender!");
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
        //checked
        static public void deleteCalender(int indexOfUser)
        {
            try
            {
                Console.Write("Please enter the ID of the calender: ");
                int ID = int.Parse(Console.ReadLine());
                if (User.userList[indexOfUser].calendersOfUser.Find(cal => cal.ID == ID) != null)
                {
                    User.userList[indexOfUser].calendersOfUser.RemoveAt(User.userList[indexOfUser].calendersOfUser.FindIndex(cal => cal.ID == ID));
                    Calender.calenderGrp.RemoveAt(Calender.calenderGrp.FindIndex(cal => cal.ID == ID));
                    Console.WriteLine("Calender was removed successfully!");
                }
                else
                {
                    throw new Exception("EnteredCalenderNotFound");
                }
            }
            catch (Exception error) when (error.Message == "EnteredCalenderNotFound")
            {
                Console.WriteLine("Entered ID was not found! Or this person does not own that calender!");
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
        static public void EditCalender(int indexOfUser)
        {
            try
            {
                Console.WriteLine("Edit Calender:\nChangeTime\tChangeDay\tChangeMonth" +
                    "\tChangeYear");
                string order = Console.ReadLine();
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
        //checked
        static public void ShowEnabledCalenders(int indexOfUser)
        {
            try
            {
                if (User.userList[indexOfUser].calendersOfUser.Find(cal => cal.enableOrDisable == true) == null)
                {
                    Console.WriteLine("nothing");
                }
                else
                {
                    List<Calender> calenderList = new List<Calender>();
                    calenderList = User.userList[indexOfUser].calendersOfUser.FindAll(cal => cal.enableOrDisable == true);
                    calenderList.OrderBy(x => x.ID);
                    foreach (Calender calender in calenderList)
                    {
                        Console.WriteLine("Calender name: " + calender.calenderName + "\tCalenderID: " + calender.ID);
                    }
                }
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
        //checked
        static public bool checkCalenderTitle(string title)
        {
            for (int i = 0; i < title.Length; i++)
            {
                //underline
                if ((int)title[i] == 95)
                {
                    continue;
                }
                //lowercase alphabet
                else if ((int)title[i] >= 97 && (int)title[i] <= 122)
                {
                    continue;
                }
                //uppercase alphabet
                else if ((int)title[i] >= 65 && (int)title[i] <= 90)
                {
                    continue;
                }
                //digits
                else if ((int)title[i] >= 48 && (int)title[i] <= 57)
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        //Calender menu
        static public void calenderMenu(int indexOfUser, int indexOfCalender)
        {
            try
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("\nCalender menu:\nAdd Event\tDelete Event\tBack");
                        string order = Console.ReadLine();
                        if (order == "Add Event")
                        {
                            AddEvent(indexOfUser,indexOfCalender);
                        }
                        else if (order == "Delete Event")
                        {
                            DeleteEvent(indexOfUser,indexOfCalender);
                        }
                        else if (order == "Back")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Input is too big!");
                    }
                }
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
        //checked
        static public void AddEvent(int indexOfUser, int indexOfCalender)
        {
            try
            {
                Console.Write("Please enter the name of the event: ");
                string eventName = Console.ReadLine();

                Console.Write("Please enter the type of the event:" +
                    "(TradeShow,Ceremony,VIP,conference) ");
                meeting eventType = (meeting)Enum.Parse(typeof(meeting), Console.ReadLine(), true);

                int eventID;

                while (true)
                {
                    Console.Write("Please enter the ID of the Event: ");
                    eventID = int.Parse(Console.ReadLine());
                    foreach (Event ev in Event.eventGrp)
                    {
                        if (ev.ID == eventID)
                        {
                            continue;
                        }
                    }
                    break;
                }
                Console.WriteLine("Please enter in which calender would you like" +
                    " to save your event: 1-LunerDate 2-SolarDate(enter the number :");
                int number = int.Parse(Console.ReadLine());
                if (number == 1)
                {
                    Console.WriteLine("Please enter the date in the format written below:" +
                        "\nyyyy_mm_dd");
                    string[] date = Console.ReadLine().Split('_');
                    int day = int.Parse(date[2]);
                    int month = int.Parse(date[1]);
                    int year = int.Parse(date[0]);
                    if (day < 1 || day > 31)
                    {
                        throw new Exception("InvalidTime");
                    }
                    if (month < 1 || month > 12)
                    {
                        throw new Exception("InvalidTime");
                    }
                    if (year < 1500)
                    {
                        throw new Exception("InvalidTime");
                    }
                    LunerCalenderEvent newLunerCalenderEvent = new LunerCalenderEvent(
                        eventName, eventType, eventID, day, month, year);
                    LunerCalenderEvent.L_C_E_Grp.Add(newLunerCalenderEvent);
                    Calender.calenderGrp[indexOfCalender].L_Grp.Add(newLunerCalenderEvent);

                }
                else if (number == 2)
                {
                    Console.WriteLine("Please enter the date in the format written below:" +
                           "\ndd_mm_yyyy");
                    string[] date = Console.ReadLine().Split('_');
                    int day = int.Parse(date[0]);
                    int month = int.Parse(date[1]);
                    int year = int.Parse(date[2]);
                    if (day > 31 || day < 1)
                    {
                        throw new Exception("InvalidTime");
                    }
                    if (month < 1 || month > 12)
                    {
                        throw new Exception("InvalidTime");
                    }
                    if (year < 0)
                    {
                        throw new Exception("InvalidTime");
                    }
                    SolarCalenderEvent newSCE = new SolarCalenderEvent(
                        eventName, eventType, eventID, day, month, year);
                    SolarCalenderEvent.S_C_E_Grp.Add(newSCE);
                    Calender.calenderGrp[indexOfCalender].S_Grp.Add(newSCE);
                }
                else
                {
                    throw new Exception("InvalidNumber");
                }
                Console.WriteLine("Process finished successfully!");
            }
            catch (Exception error) when (error.Message == "InvalidTime")
            {
                Console.WriteLine("Invalid time format!");
            }
            catch (Exception error) when (error.Message == "InvalidNumber")
            {
                Console.WriteLine("Invalid number!");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid input!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input!");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Input is too big!");
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
        static public void DeleteEvent(int indexOfUser, int indexOfCalender)
        {
            try
            {
                if (Calender.calenderGrp[indexOfCalender].owner != User.userList[indexOfUser].userName)
                {
                    Console.WriteLine("This person does not own this calender!");
                }
                else
                {
                    Console.Write("Please enter the title of the event: ");
                    string title = Console.ReadLine();

                    Calender.calenderGrp[indexOfCalender].eventGrp.RemoveAt(Calender.calenderGrp[indexOfCalender].eventGrp.FindIndex(eve => eve.title == title));
                    if (SolarCalenderEvent.S_C_E_Grp.Find(s => s.specificEvent.title == title) != null)
                    {
                        Calender.calenderGrp[indexOfCalender].S_Grp.RemoveAt(Calender.calenderGrp[indexOfCalender].S_Grp.FindIndex(s => s.specificEvent.title == title));
                    }
                    if(LunerCalenderEvent.L_C_E_Grp.Find(s => s.specificEvent.title == title) != null)
                    {
                        Calender.calenderGrp[indexOfCalender].L_Grp.RemoveAt(Calender.calenderGrp[indexOfCalender].L_Grp.FindIndex(s => s.specificEvent.title == title));
                    }
                    Console.WriteLine("Event removed!");
                }
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
    }
    class User
    {
        static public List<User> userList = new List<User>();
        public List<Calender> calendersOfUser = new List<Calender>();
        public string userName
        {
            get;
            set;
        }
        string _password;
        public string password
        {
            get { return _password; }
            set { this._password = value; }
        }
        public User(string name, string password)
        {
            this.userName = name;
            this.password = password;
        }
    }
    class Time
    {
        int _hour;
        public int hour
        {
            get { return this._hour; }
        }
        int _minute;
        public int minute
        {
            get { return this._minute; }
        }
        public virtual void Change(int hour, int minute, string a = "one", string b = "bb")
        {
            if (hour > 23 || hour < 0)
            {
                throw new Exception("WrongTime");
            }

            if (minute > 59 || minute < 0)
            {
                throw new Exception("WrongTime");
            }
            this._hour = hour;
            this._minute = minute;
        }
    }
    class Day : Time
    {
        int _shamsiDay;
        public int shamsiDay
        {
            get { return this._shamsiDay; }
        }
        int _miladiDay;
        public int miladiDay
        {
            get { return this._miladiDay; }
        }
        public override void Change(int shamsiDay, int miladiDay, string a = "one", string b = "bb")
        {
            if (shamsiDay > 31 || shamsiDay < 1)
            {
                throw new Exception("WrongTime");
            }
            if (miladiDay > 31 || miladiDay < 1)
            {
                throw new Exception("WrongTime");
            }

            this._shamsiDay = shamsiDay;
            this._miladiDay = miladiDay;
        }
    }
    class Month : Day
    {
        string _milladiName;
        public string milladiName
        {
            get { return this._milladiName; }
            set { this._milladiName = value; }
        }
        string _shamsiName;
        public string shamsiName
        {
            get { return this.shamsiName; }
            set { this._shamsiName = value; }
        }
        int _milladiNumber;
        public int milladiNumber
        {
            get { return this._milladiNumber; }
        }
        int _shamsiNumber;
        public int shamsiNumber
        {
            get { return this._shamsiNumber; }
        }
        public override void Change(
            int shamsiNumber,
            int miladiNumber,
            string shamsiName,
            string milladiName
            )
        {
            if (miladiNumber > 12 || miladiNumber < 1)
            {
                throw new Exception("WrongTime");
            }
            if (shamsiNumber > 12 || shamsiNumber < 1)
            {
                throw new Exception("WrongTime");
            }
            this._milladiNumber = miladiNumber;
            this._shamsiNumber = shamsiNumber;
            this.shamsiName = shamsiName;
            this.milladiName = milladiName;
        }
    }
    class Calender : Month
    {
        public static List<Calender> calenderGrp = new List<Calender>();
        public List<Event> eventGrp = new List<Event>();
        public List<LunerCalenderEvent> L_Grp = new List<LunerCalenderEvent>();
        public List<SolarCalenderEvent> S_Grp = new List<SolarCalenderEvent>();
        public static int numberOfCalenders = 0;
        public bool enableOrDisable = false;
        string _owner;
        public string owner
        {
            get { return this._owner; }
        }
        int _ID;
        public int ID
        {
            get { return this._ID; }
        }
        public string calenderName;
        int _milladiYear;
        public int milladiYear
        {
            get { return this._milladiYear; }
        }
        int _shamsiYear;
        public int shamsiYear
        {
            get { return this._shamsiYear; }
        }
        public Calender()
        {
            numberOfCalenders++;
            this._ID = numberOfCalenders;
        }
        public override void Change(int shamsiYear, int milladiYear, string a = "aa", string b = "ss")
        {
            Change(shamsiYear, milladiYear, a, b);
            if (shamsiYear < 0 || milladiYear < 0)
            {
                throw new Exception("WrongTime");
            }
            this._shamsiYear = shamsiYear;
            this._milladiYear = milladiYear;
        }
    }
    struct Event
    {
        static public List<Event> eventGrp = new List<Event>();
        public string title;
        public meeting eventType;
        public int ID;
        public Event(string title, meeting eventType, int ID)
        {
            this.eventType = eventType;
            this.title = title;
            this.ID = ID;
        }
    }
    class LunerCalenderEvent
    {
        static public List<LunerCalenderEvent> L_C_E_Grp = new List<LunerCalenderEvent>();
        public int day;
        public int month;
        public int year;
        public Event specificEvent;
        public LunerCalenderEvent(string eventName, meeting eventType,
            int eventID, int day, int month, int year)
        {
            Event newEvent = new Event(eventName, eventType, eventID);
            this.day = day;
            this.month = month;
            this.year = year;
        }
    }
    class SolarCalenderEvent
    {
        static public List<SolarCalenderEvent> S_C_E_Grp = new List<SolarCalenderEvent>();
        public int day;
        public int month;
        public int year;
        public Event specificEvent;
        public SolarCalenderEvent(string eventName, meeting eventType,
            int eventID, int day, int month, int year)
        {
            Event newEvent = new Event(eventName, eventType, eventID);
            this.day = day;
            this.month = month;
            this.year = year;
        }
    }
}