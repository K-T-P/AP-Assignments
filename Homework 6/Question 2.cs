using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace tamrin_seri_6_soal_2
{
    class Program
    {
        enum personType { AdminType, StudentType, TeacherType, CustomerType }

        static void Main()
        {
            try
            {
                AutomaticAdminRegisteration();
                AutomaticClientRegisteration();
                AutomaticMediaRegisteration();
                if (Seller.sellersGrp.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("No seller was found in \"SellersInfo.txt\"! Program won't be able to run without sellers!\nProgram finished!");
                }
                else
                {
                    User_Admin_Menu();
                }
                AutomaticSaveToFileAdmin();
                AutomaticSaveToFileCustomers();
                AutomaticSaveToFileMedia();
            }
            catch (Exception error) when (error.Message == "EndProgram")
            {

            }
            catch (OutOfMemoryException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not enough memory on the device!\nProgram stopped!");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An Error occured!");
            }
        }

        static public void User_Admin_Menu()
        {
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Please enter your role : User\\Admin\tExit");
                    string roleOrder = Console.ReadLine();
                    if (roleOrder == "User")
                    {
                        try
                        {
                            while (true)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("Please enter your type : Student\\Teacher\\Customer\tExit");
                                string typeOrder = Console.ReadLine();
                                if (typeOrder == "Student")
                                {
                                    StudentLoginMenu();
                                }
                                else if (typeOrder == "Teacher")
                                {
                                    TeacherLoginMenu();
                                }
                                else if (typeOrder == "Customer")
                                {
                                    CustomerLoginMenu();
                                }
                                else if (typeOrder == "Exit")
                                {
                                    break;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Invalid input !");
                                }
                            }
                        }
                        catch (OverflowException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Input is too big!");
                        }
                    }
                    else if (roleOrder == "Admin")
                    {
                        AdminLoginMenu();
                    }
                    else if (roleOrder == "Exit")
                    {
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input !\n");
                    }
                }
                catch (OverflowException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input is too big!\n");
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("An Error occured!");
                }
            }
        }
        static public void AdminLoginMenu()
        {
            try
            {
                //          receive the admin's email
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Please enter your email : ");
                string email = Console.ReadLine();

                //          a null refrence to store the seller
                Seller seller = null;

                //          checks whether email exists or not
                if (Seller.sellersGrp.Find(seller => seller.UserName == email) == null)
                {
                    //      throw exception because entered admin was not found
                    throw new Exception("EnteredAdminWasNotFound");
                }

                //          store admin in the null refrence
                else
                {
                    seller = Seller.sellersGrp.Find(seller => seller.UserName == email);
                }

                //          receive the password
                Console.Write("Please enter your password : ");
                string password = Console.ReadLine();

                //          checks password validity
                if (seller.Password != password)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Wrong password!");
                }
                //          opens admin menu
                else
                {
                    SellerMenu(seller);
                }
            }
            catch (Exception error) when (error.Message == "EnteredAdminWasNotFound")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Entered admin was not found!");
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input is too big!");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An Error occured!");
            }
        }

        static public void AutomaticAdminRegisteration()
        {
            StreamReader reader = null;
            try
            {
                reader = new StreamReader("SellersInfo.txt");
                while (reader.EndOfStream == false)
                {
                    //Seller 1
                    //user name : ali
                    //password : MyShop1234$
                    //(empty line)
                    string whichSeller = reader.ReadLine();
                    try
                    {
                        string userName = reader.ReadLine().Split(" : ")[1];
                        string password = reader.ReadLine().Split(" : ")[1];
                        reader.ReadLine();
                        Seller newSeller = null;
                        if (password == "MyShop1234$")
                        {
                            newSeller = new Seller(userName);
                        }
                        else
                        {
                            newSeller = new Seller(userName, password);
                        }
                        Seller.sellersGrp.Add(newSeller);
                    }
                    catch (Exception error) when (error.Message == "InvalidUserName")
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("{0} has invalid user name and won't be registered! Automatic Register is still running!", whichSeller);
                    }
                    catch (Exception error) when (error.Message == "AlreadyRegistered")
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("There is another seller who has similar user name as {0}. {0} won't be registered automatically!" +
                            " Automatic Register is still running!", whichSeller);
                    }
                    catch (OverflowException)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("In {0} , the user name or password is too big to be used! {0} won't be registered automatically!" +
                            " Automatic Register is still running!", whichSeller);
                        try
                        {
                            string uselessInfo = reader.ReadLine().Split(" : ")[0];
                            if (uselessInfo != "")
                            {
                                reader.ReadLine();
                            }
                        }
                        catch (OverflowException)
                        {
                            reader.ReadLine();
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\"SellersInfo.txt\" was not found! Without it, it's impossible to run the program!" +
                    "\nPlease make sure that the \"SellersInfo.txt already exists!\nProgram stopped!");
                throw new Exception("EndProgram");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An Error Occured!");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        static public void AutomaticClientRegisteration()
        {
            StreamReader reader = null;
            try
            {
                reader = new StreamReader("CustomersInfo.txt");
                while (reader.EndOfStream == false)
                {
                    string[] firstLine = reader.ReadLine().Split(" : ");
                    string secondLine = reader.ReadLine();
                    if (firstLine[0] == "Student")
                    {
                        if (Student.CheckStudentIDIsTrueOrNot(secondLine))
                        {
                            Student newStudent = new Student(firstLine[1], secondLine);
                            Student.studentGrp.Add(newStudent);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else if (firstLine[0] == "Teacher")
                    {
                        Teacher newTeacher = new Teacher(firstLine[1], secondLine);
                        Teacher.teacherGrp.Add(newTeacher);
                    }
                    else if (firstLine[0] == "Customer")
                    {
                        if (Util.CheckSSNValidity(secondLine))
                        {
                            Customer newCustomer = new Customer(firstLine[1], secondLine);
                            Customer.customersGrp.Add(newCustomer);
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\"CustomersInfo.txt\" was not found!");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An Error occured!");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        static public void AutomaticMediaRegisteration()
        {
            StreamReader reader = null;
            try
            {
                reader = new StreamReader("MediaInfo.txt");
                while (reader.EndOfStream == false)
                {
                    string[] firstLine = reader.ReadLine().Split(" : ");
                    if (firstLine[0] == "Books")
                    {
                        //reader.Close();
                        //reader = null;
                        //AutomaticBooksRegisteration();
                        string bookName = reader.ReadLine();
                        double bookCost = double.Parse(reader.ReadLine());
                        string bookAuthor = reader.ReadLine();
                        string bookPublisher = reader.ReadLine();
                        Books newBook = new Books(bookName, bookCost, bookAuthor, bookPublisher);
                        newBook.SetProductCostWithToll();
                        Library.mediaGrp.Add(newBook);
                    }
                    else if (firstLine[0] == "Videos")
                    {
                        //reader.Close();
                        //reader = null;
                        //AutomaticVideosRegisteration();
                        string videoName = reader.ReadLine();
                        double videoCost = double.Parse(reader.ReadLine());
                        int videoTimeLength = int.Parse(reader.ReadLine());
                        int countOfCDs = int.Parse(reader.ReadLine());
                        Videos newVideo = new Videos(videoName, videoCost, videoTimeLength, countOfCDs);
                        newVideo.SetVideoCostWithToll();
                        Library.mediaGrp.Add(newVideo);
                    }
                    else if (firstLine[0] == "Magazines")
                    {
                        //reader.Close();
                        //reader = null;
                        //AutomaticMagazinesRegisteration();
                        string magazineName = reader.ReadLine();
                        double magazineCost = double.Parse(reader.ReadLine());
                        int numberOfPages = int.Parse(reader.ReadLine());
                        string magazinePublisher = reader.ReadLine();
                        Magazines newMagazine = new Magazines(magazineName, magazineCost, magazinePublisher, numberOfPages);
                        newMagazine.SetMagazineCostWithToll();
                        Library.mediaGrp.Add(newMagazine);
                    }
                    else
                    {
                        continue;
                    }
                }
                reader.Close();
                reader = null;
            }
            catch (FileNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\"MediaInfo.txt\" was not found!");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An Error occured!");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        static public void AutomaticBooksRegisteration()
        {
            StreamReader reader = null;
            try
            {
                string bookName = reader.ReadLine();
                double bookCost = double.Parse(reader.ReadLine());
                string bookAuthor = reader.ReadLine();
                string bookPublisher = reader.ReadLine();
                Books newBook = new Books(bookName, bookCost, bookAuthor, bookPublisher);
                Library.mediaGrp.Add(newBook);
                reader.Close();
                reader = null;
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input is too big!");

            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An Error occured!");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        static public void AutomaticVideosRegisteration()
        {
            StreamReader reader = null;
            try
            {
                string videoName = reader.ReadLine();
                double videoCost = double.Parse(reader.ReadLine());
                int videoTimeLength = int.Parse(reader.ReadLine());
                int countOfCDs = int.Parse(reader.ReadLine());
                Videos newVideo = new Videos(videoName, videoCost, videoTimeLength, countOfCDs);
                Library.mediaGrp.Add(newVideo);
                reader.Close();
                reader = null;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An Error occured!");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        static public void AutomaticMagazinesRegisteration()
        {
            StreamReader reader = null;
            try
            {
                string magazineName = reader.ReadLine();
                double magazineCost = double.Parse(reader.ReadLine());
                int numberOfPages = int.Parse(reader.ReadLine());
                string magazinePublisher = reader.ReadLine();
                Magazines newMagazine = new Magazines(magazineName, magazineCost, magazinePublisher, numberOfPages);
                Library.mediaGrp.Add(newMagazine);
                reader.Close();
                reader = null;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An Error occured!");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }

        static public void AutomaticSaveToFileAdmin()
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter("SellersInfo.txt");
                for (int i = 0; i < Seller.sellersGrp.Count; i++)
                {
                    writer.WriteLine("Seller {0}", i + 1);
                    writer.WriteLine("user name : {0}", Seller.sellersGrp[i].UserName);
                    writer.WriteLine("password : {0}", Seller.sellersGrp[i].Password);
                    writer.WriteLine();
                }
                writer.Close();
                writer = null;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An Error occured!");
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
        static public void AutomaticSaveToFileCustomers()
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter("CustomersInfo.txt");
                foreach (Student student in Student.studentGrp)
                {
                    writer.WriteLine("Student : " + student.UserName);
                    writer.WriteLine(student.StudentID);
                }
                foreach (Teacher teacher in Teacher.teacherGrp)
                {
                    writer.WriteLine("Teacher : " + teacher.UserName);
                    writer.WriteLine(teacher.institute);
                }
                foreach (Customer customer in Customer.customersGrp)
                {
                    writer.WriteLine("Customer : " + customer.UserName);
                    writer.WriteLine(customer.SSN);
                }
                writer.Close();
                writer = null;
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An Error occured!");
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
        static public void AutomaticSaveToFileMedia()
        {
            Library.SaveToFile();
        }

        static public void StudentLoginMenu()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Please enter your User name : ");
                string studentUserName = Console.ReadLine();
                if (Student.studentGrp.Find(student => student.UserName == studentUserName) != null)
                {
                    Student loginStudent = Student.studentGrp.Find(student => student.UserName == studentUserName);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("You have registered before!\nPlease enter your " +
                        "student ID : ");
                    string studentID = Console.ReadLine();
                    if (loginStudent.StudentID == studentID)
                    {
                        UserMenu((Person)loginStudent, 20);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Access denied!");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("You are new! Please enter your student ID to register : ");
                    string studentID = Console.ReadLine();
                    if (Student.studentGrp.Find(student => student.StudentID == studentID) != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("This student ID has already used!");
                    }
                    else
                    {
                        if (Student.CheckStudentIDIsTrueOrNot(studentID))
                        {
                            Student newStudent = new Student(studentUserName, studentID);
                            Student.studentGrp.Add(newStudent);
                            AutomaticSaveToFileCustomers();
                            UserMenu((Person)newStudent, 20);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid student ID! Access denied!");
                        }
                    }
                }
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input is too big!");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An Error occured!");
            }
        }
        static public void TeacherLoginMenu()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Please enter your user name : ");
                string teacherUserName = Console.ReadLine();
                if (Teacher.teacherGrp.Find(teacher => teacher.UserName == teacherUserName) != null)
                {
                    Teacher loginTeacher = Teacher.teacherGrp.Find(teacher => teacher.UserName == teacherUserName);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("You have registered before!\nPlease enter your institute : ");
                    string institute = Console.ReadLine();
                    if (loginTeacher.institute == institute)
                    {
                        UserMenu((Person)loginTeacher, 15);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Access denied!");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("You are new! Please enter your institute to register : ");
                    string teacherInstitute = Console.ReadLine();
                    Teacher newTeacher = new Teacher(teacherUserName, teacherInstitute);
                    Teacher.teacherGrp.Add(newTeacher);
                    AutomaticSaveToFileCustomers();
                    UserMenu((Person)newTeacher, 15);
                }
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input is too big!");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An Error occured!");
            }
        }
        static public void CustomerLoginMenu()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Please enter your user name : ");
                string customerUserName = Console.ReadLine();
                if (Customer.customersGrp.Find(customer => customer.UserName == customerUserName) != null)
                {
                    Customer loginCustomer = Customer.customersGrp.Find(customer => customer.UserName == customerUserName);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("You have registered before!\nPlease enter your SSN : ");
                    string SSN = Console.ReadLine();
                    if (loginCustomer.SSN == SSN)
                    {
                        UserMenu((Person)loginCustomer, 5);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid SSN! Access denied!");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("You are new!\nPlease enter your SSN : ");
                    string SSN = Console.ReadLine();
                    if (Customer.customersGrp.Find(customer => customer.SSN == SSN) != null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("This SSN has already used!");
                    }
                    else
                    {
                        if (Util.CheckSSNValidity(SSN))
                        {
                            Customer newCustomer = new Customer(customerUserName, SSN);
                            Customer.customersGrp.Add(newCustomer);
                            AutomaticSaveToFileCustomers();
                            UserMenu((Person)newCustomer, 5);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid SSN!");
                        }
                    }
                }
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input is too big!");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An Error occured!");
            }
        }

        static public void UserMenu(Person person, int OFF)
        {
            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nUser Menu");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Select\tEdit\tBuy\tChance\nExit");
                    string order = Console.ReadLine();
                    if (order == "Select")
                    {
                        Select(person);
                    }
                    else if (order == "Edit")
                    {
                        Edit(person);
                    }
                    else if (order == "Buy")
                    {
                        Buy(person);
                    }
                    else if (order == "Chance")
                    {
                        Chance(person);
                    }
                    else if (order == "Exit")
                    {
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid input!");
                    }
                }
                catch (OverflowException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input is too big !");
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("An Error occured!");
                }
            }
        }
        static public void SellerMenu(Seller salesperson)
        {
            while (true)
            {
                try
                {
                    //          Admin menu
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nSeller Menu");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Add\tDelete\tSearch\tShowAll\nShowCustomers\tChangePass");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("RegisterNewAdmin");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\tExit\n");

                    string order = Console.ReadLine();
                    if (order == "Add")
                    {
                        Library.AddMedia();
                    }
                    else if (order == "Delete")
                    {
                        Library.DelMedia();
                    }
                    else if (order == "Search")
                    {
                        Library.SearchMedia();
                    }
                    else if (order == "ShowCustomers")
                    {
                        StreamReader reader = null;
                        try
                        {
                            reader = new StreamReader("CustomersInfo.txt");
                            while (reader.EndOfStream == false)
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine(reader.ReadLine());
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine(reader.ReadLine());
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(reader.ReadLine());
                            }
                            reader.Close();
                            reader = null;
                        }
                        catch (FileNotFoundException)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\"CustomersInfo.txt\" was not found!");
                        }
                        finally
                        {
                            if (reader != null)
                            {
                                reader.Close();
                            }
                        }
                    }
                    else if (order == "ShowAll")
                    {
                        Library.ShowAllMediaID();
                    }
                    else if (order == "ChangePass")
                    {
                        salesperson.ChangePass();
                    }
                    else if (order == "RegisterNewAdmin")
                    {
                        salesperson.RegisterNewSeller();
                    }
                    else if (order == "Exit")
                    {
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Invalid input !");
                    }
                }
                catch (OverflowException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input is too big!");
                }
                catch (FileNotFoundException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\"CustomersInfo.txt\" was not found !");
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("An Error occured!");
                }
            }
        }

        static public void Select(Person person)
        {
            StreamReader reader = null;
            try
            {
                reader = new StreamReader("MediaInfo.txt");
                int numberToChangeColor = 0;
                while (reader.EndOfStream == false)
                {
                    if (numberToChangeColor % 5 == 0)
                    {
                        if ((numberToChangeColor / 5) % 2 == 0)
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                    }
                    Console.WriteLine(reader.ReadLine());
                    numberToChangeColor++;
                }
                reader.Close();
                reader = null;

                Console.Write("Please enter the name of the product : ");
                string productName = Console.ReadLine();
                if (Library.mediaGrp.Find(media => media.ProductName == productName) == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Entered product does not exist!");
                }
                else
                {
                    if (person.Cart.Count == 20)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The cart is already full!");
                    }
                    else
                    {
                        person.Cart.Add(Library.mediaGrp.Find(media => media.ProductName == productName));
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Product added!");
                    }
                }
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input (or file containment) is too big!");
            }
            catch (FileNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\"MediaInfo.txt\" was not found!");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An Error occured!");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        static public void Edit(Person person)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (person.Cart.Count > 0)
                {
                    for (int i = 0; i < person.Cart.Count; i++)
                    {
                        Console.WriteLine("Product name : " + person.Cart[i].ProductName);
                        Console.WriteLine("The cost of the product : " + person.Cart[i].ProductCostAfterTollIncreasement);
                        Console.WriteLine();
                    }
                    Console.Write("Please enter the name of the product : ");
                    string productName = Console.ReadLine();
                    if (person.Cart.Find(product => product.ProductName == productName) == null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Entered product does not exist in your cart!");
                    }
                    else
                    {
                        person.Cart.RemoveAt(person.Cart.FindIndex(product => product.ProductName == productName));
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("{0} removed successfully!", productName);
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Your cart is empty!");
                }
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input is too big!");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An Error occured!");
            }
        }
        static public void Buy(Person person)
        {
            try
            {
                double totalCost = 0;
                foreach (Media media in person.Cart)
                {
                    if (media is Books)
                    {
                        Books book = (Books)media;
                        totalCost += book.ProductCostAfterTollIncreasement;
                    }
                    else if (media is Videos)
                    {
                        Videos video = (Videos)media;
                        totalCost += video.ProductCostAfterTollIncreasement;
                    }
                    else if (media is Magazines)
                    {
                        Magazines magazine = (Magazines)media;
                        totalCost += magazine.ProductCostAfterTollIncreasement;
                    }
                    else
                    {
                        continue;
                    }
                }
                double OFF = 0;
                if (person is Student)
                {
                    OFF = ((Student)person).StudentsSpecialOFF();
                }
                else if (person is Teacher)
                {
                    OFF = ((Teacher)person).TeachersSpecialOFF();
                }
                else
                {
                    OFF = ((Customer)person).CustomersSpecialOFF();
                }

                totalCost *= (100.0 - OFF - person.ChanceOFF) / 100.0;

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Total purchase : " + totalCost);
                Console.Write("Would you like to buy? Y/N : ");
                string purchaseOrder = Console.ReadLine();
                if (purchaseOrder == "Y")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Successfull Purchase ! Thank you!");
                    foreach(Media product in person.Cart)
                    {
                        Library.mediaGrp.RemoveAt(Library.mediaGrp.FindIndex(media => media.MediaID == product.MediaID));
                    }
                    AutomaticSaveToFileMedia();
                    person.Cart = new List<Media>();
                }
                else if (purchaseOrder == "N")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Purchase failed!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input!");
                }
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Your total purchase is too big!");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An Error occured!");
            }
        }
        static public void Chance(Person person)
        {
            try
            {
                if (person.ChanceOFFAllowed == true)
                {
                    int[] chance = new int[9] { 0, 2, 3, 5, 7, 10, 15, 25, 30 };
                    Random rand = new Random();
                    int chanceOFF = chance[Math.Abs(rand.Next()) % 9];
                    person.ChanceOFF += chanceOFF;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    person.ChanceOFFAllowanceTurnToFalse();
                    Console.WriteLine("You earned a {0} percent OFF!", chanceOFF);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You've used this item before!");
                }
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("An Error occured!");
            }
        }
    }
    class Person
    {
        private bool _ChanceOffAllowed = true;
        public bool ChanceOFFAllowed
        {
            get { return this._ChanceOffAllowed; }
            private set { this._ChanceOffAllowed = value; }
        }
        public void ChanceOFFAllowanceTurnToFalse()
        {
            this.ChanceOFFAllowed = false;
        }

        protected int _ChanceOFF = 0;
        public int ChanceOFF
        {
            get { return this._ChanceOFF; }
            set { this._ChanceOFF = value; }
        }

        public List<Media> Cart = new List<Media>();
    }
    class Student : Person
    {
        static public List<Student> studentGrp = new List<Student>();

        //          User name
        private string _userName;
        public string UserName
        {
            get { return this._userName; }
            private set { this._userName = value; }
        }

        //          ID
        private string _studentID;
        public string StudentID
        {
            get { return this._studentID; }
            private set { this._studentID = value; }
        }

        //          Constructor
        public Student(string studentName, string studentID)
        {
            this._studentID = studentID;
            this.UserName = studentName;
        }

        public void SaveToFile()
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter("CustomersInfo.txt");
                writer.WriteLine("Student : " + this.UserName);
                writer.WriteLine(this.StudentID);
                writer.Close();
                writer = null;
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("CustomersInfo.txt file is readonly!");
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
        static public bool CheckStudentIDIsTrueOrNot(string password)
        {
            Regex rx = new Regex(@"^9\d{7}$");
            MatchCollection matches = rx.Matches(password);
            if (matches.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public double StudentsSpecialOFF()
        {
            return 20;
        }
    }
    class Seller
    {
        //          group to save all sellers
        static public List<Seller> sellersGrp = new List<Seller>();

        //          User name ( it must be have an Email format )
        private string _userName;
        public string UserName
        {
            get { return this._userName; }
            private set { this._userName = value; }
        }

        //          password (default =  MyShop1234$)
        private string _password = "MyShop1234$";
        public string Password
        {
            get { return this._password; }
            private set { this._password = value; }
        }

        //          password modification tme
        private string dateOfPasswordModification;

        //          two costructors 

        //          registering with entering password
        public Seller(string userName, string password)
        {
            if (sellersGrp.Find(seller => seller.UserName == userName) != null)
            {
                throw new Exception("AlreadyRegistered");
            }
            else
            {
                Regex rx = new Regex(@"\w+\@\w+\.\w+");
                MatchCollection matches = rx.Matches(userName);
                if (matches.Count == 0)
                {
                    throw new Exception("InvalidUserName");
                }
                else
                {
                    this.UserName = userName;
                    this.Password = password;
                    DateTime time = DateTime.Now;
                    this.dateOfPasswordModification = time.ToString("F");
                }
            }
        }

        //          registering without password  (default password)
        public Seller(string userName)
        {
            if (sellersGrp.Find(seller => seller.UserName == userName) != null)
            {
                throw new Exception("AlreadyRegistered");
            }
            else
            {
                Regex rx = new Regex(@"\w+\@\w+\.\w+");
                MatchCollection matches = rx.Matches(userName);
                if (matches.Count == 0)
                {
                    throw new Exception("InvalidUserName");
                }
                else
                {
                    this.UserName = userName;
                    DateTime time = DateTime.Now;
                    this.dateOfPasswordModification = time.ToString("F");
                }
            }
        }

        //          registers new seller. only a seller can do this
        public void RegisterNewSeller()
        {
            try
            {
                //      receive new admin user name
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Please enter the new admin user name : ");
                string newAdminName = Console.ReadLine();

                //      receive new password
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Please enter new password : ");
                string newPassword = Console.ReadLine();

                if (newPassword == "MyShop1234$")
                {
                    Seller newSeller = new Seller(newAdminName);
                    Seller.sellersGrp.Add(newSeller);
                }
                else
                {
                    Seller newSeller = new Seller(newAdminName, newPassword);
                    Seller.sellersGrp.Add(newSeller);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("New Seller registered!");
            }
            catch (Exception error) when (error.Message == "AlreadyRegistered")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Entered user name has already exist!");
            }
            catch (Exception error) when (error.Message == "InvalidUserName")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Entered user name has invalid format!");
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input is too big!");
            }
        }

        //          changes password
        public void ChangePass()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Password last modification time : " + dateOfPasswordModification);
                Console.Write("Please enter your new password : ");
                string password = Console.ReadLine();
                if (password == "")
                {
                    this.Password = "MyShop1234$";
                }
                else
                {
                    this.Password = password;
                }
                Console.WriteLine("Password modified!");
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input is too big!");
            }
        }

        //static public void SaveToFile()
        //{
        //    StreamWriter writer = null;
        //    try
        //    {
        //        writer = new StreamWriter("SellersInfo.txt");
        //        for (int i = 0; i < Seller.sellersGrp.Count; i++)
        //        {
        //            //Seller 1
        //            //user name : ali
        //            //password : MyShop1234$
        //            //(empty line)
        //            writer.WriteLine("Seller {0}", i + 1);
        //            writer.WriteLine("User name : " + Seller.sellersGrp[i].UserName);
        //            writer.WriteLine("Password : " + Seller.sellersGrp[i].Password);
        //            writer.WriteLine("");
        //        }
        //        writer.Close();
        //        writer = null;
        //    }
        //    catch (UnauthorizedAccessException)
        //    {
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine("Access denied to \"SellersInfo.txt\" !");
        //    }
        //    finally
        //    {
        //        if (writer != null)
        //        {
        //            writer.Close();
        //        }
        //    }
        //}
    }
    class Teacher : Person
    {
        static public List<Teacher> teacherGrp = new List<Teacher>();

        //          User Name
        private string _userName;
        public string UserName
        {
            get { return this._userName; }
            private set { this._userName = value; }
        }

        //      institute
        private string _institute;
        public string institute
        {
            get { return this._institute; }
            private set { this._institute = value; }
        }

        public Teacher(string teacherName, string institute)
        {
            this.UserName = teacherName;
            this.institute = institute;
        }

        //      Save to file
        public void SaveToFile()
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter("CustomersInfo.txt");
                writer.WriteLine("Teacher : " + this.UserName);
                writer.WriteLine(this.institute);
                writer.Close();
                writer = null;
            }
            catch (System.UnauthorizedAccessException)
            {
                Console.WriteLine("Access denied to CustomersInfo.txt !");
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("CustomersInfo.txt file is readonly !");
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        public double TeachersSpecialOFF()
        {
            if (this.Cart.Count > 3)
            {
                return 15;
            }
            else
            {
                return 0;
            }
        }
    }
    class Customer : Person
    {
        static public List<Customer> customersGrp = new List<Customer>();

        //          User Name
        private string _userName;
        public string UserName
        {
            get { return this._userName; }
            private set { this._userName = value; }
        }

        //          SSN
        private string _SSN;
        public string SSN
        {
            get { return this._SSN; }
            private set { this._SSN = value; }
        }

        public Customer(string customerName, string customerSSN)
        {
            this.UserName = customerName;
            this.SSN = customerSSN;
        }

        public void SaveToFile()
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter("CustomersInfo.txt");
                writer.WriteLine("Customer : " + this.UserName);
                writer.WriteLine(this.SSN);
                writer.Close();
                writer = null;
            }
            catch (System.UnauthorizedAccessException)
            {
                Console.WriteLine("Access denied to CustomersInfo.txt !");
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("CustomersInfo.txt file is readonly !");
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        public double CustomersSpecialOFF()
        {
            if (this.Cart.Count > 5)
            {
                return 5;
            }
            else
            {
                return 0;
            }
        }
    }
    static class Util
    {
        public static bool CheckSSNValidity(this string SSN)
        {
            try
            {

                if (SSN.Length != 10)
                {
                    return false;
                }

                //      defining variables a, b, c
                int a = (int)SSN[0]-48;
                int b = (    ((int)SSN[0]-48) * 10 + ((int)SSN[1]-48) * 9 + ((int)SSN[2]-48) * 8 + ((int)SSN[3]-48) * 7 + ((int)SSN[4]-48)
                    * 6 + ((int)SSN[5]-48) * 5 + ((int)SSN[6]-48) * 4 + ((int)SSN[7]-48) * 3 + ((int)SSN[8]-48) * 2);
                int c = b % 11;

                //      check to avoid SSN in format 9999999999 , ext.
                if (int.Parse(SSN) % 1111111111 == 0)
                {
                    return false;
                }

                //      check SSN validity
                if ((c == 0) && (a == 0))
                {
                    return true;
                }
                else if ((c == 1) && (a == 1))
                {
                    return true;
                }
                else if ((c > 1) && (a == Math.Abs(c - 11)))
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (FormatException)
            {
                return false;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }
    }
    class Media
    {
        static private int numberOfProducts = 0;

        //      the name of the product
        private string _productName;
        public string ProductName
        {
            get { return this._productName; }
            private set { this._productName = value; }
        }

        //      the real value of the product
        private double _productCost;
        public double ProductCostWithoutIncreasement
        {
            get { return this._productCost; }
            private set { this._productCost = value; }
        }

        private double _productCostAfterTollIncreasement;
        public double ProductCostAfterTollIncreasement
        {
            get { return this._productCostAfterTollIncreasement; }
            protected set { this._productCostAfterTollIncreasement = value; }
        }

        private int _mediaID;
        public int MediaID
        {
            get { return this._mediaID; }
            private set { this._mediaID = value; }
        }

        public Media(string productName, double productCost)
        {
            this.ProductName = productName;
            this.ProductCostWithoutIncreasement = productCost;
            numberOfProducts++;
            this.MediaID = numberOfProducts;
        }
    }
    class Books : Media
    {
        //      author
        private string _author;
        public string Author
        {
            get { return this._author; }
            set { this._author = value; }
        }

        //      publisher
        private string _publisher;
        public string Publisher
        {
            get { return this._publisher; }
            private set { this._publisher = value; }
        }

        public Books(string productName, double productCost, string author, string publisher) : base(productName, productCost)
        {
            this.Author = author;
            this.Publisher = publisher;
        }

        //      arzesh afzode
        public double Toll()
        {
            //return (this.ProductCost * 110) / 100;
            return 10.0;
        }

        public void SetProductCostWithToll()
        {
            try
            {
                this.ProductCostAfterTollIncreasement = this.ProductCostWithoutIncreasement * (100.0 + this.Toll()) / 100.0;
            }
            catch (OverflowException)
            {
                this.ProductCostAfterTollIncreasement = this.ProductCostWithoutIncreasement;
            }
        }
    }
    class Videos : Media
    {
        //          Modat zamane filme amozehi be daghighe
        private int _videoTimeLength;
        public int VideoTimeLength
        {
            get { return this._videoTimeLength; }
            private set { this._videoTimeLength = value; }
        }

        //          count of the CDs in the pachage
        private int _countOfCDs;
        public int CountOfCDs
        {
            get { return this._countOfCDs; }
            private set { this._countOfCDs = value; }
        }

        public Videos(string productName, double productCost, int videoTimeLength, int countOfCDs) : base(productName, productCost)
        {
            this.VideoTimeLength = videoTimeLength;
            this.CountOfCDs = countOfCDs;
        }

        //          Toll calculation
        public double TollCalculation()
        {
            double tollIncreasementForCountOfCDs = this.CountOfCDs * 3.0;
            double tollIncreasementForLengthOfVideos = (this.VideoTimeLength / 60) * 5;
            return tollIncreasementForCountOfCDs + tollIncreasementForLengthOfVideos;
        }

        public void SetVideoCostWithToll()
        {
            try
            {
                this.ProductCostAfterTollIncreasement = this.ProductCostWithoutIncreasement * (100.0 + this.TollCalculation()) / 100.0;
            }
            catch (OverflowException)
            {
                this.ProductCostAfterTollIncreasement = this.ProductCostWithoutIncreasement;
            }
        }
    }
    class Magazines : Media
    {
        //          Publisher of the magazine
        private string _publisher;
        public string Publisher
        {
            get { return this._publisher; }
            private set { this._publisher = value; }
        }

        //          Number of pages
        private int _numberOfPages;
        public int NumberOfPages
        {
            get { return this._numberOfPages; }
            private set { this._numberOfPages = value; }
        }

        public Magazines(string productName, double productCost, string publisher, int numberOfPags) : base(productName, productCost)
        {
            this.Publisher = publisher;
            this.NumberOfPages = numberOfPags;
        }

        //          Toll Increasement Calculation
        public double tollIncreasement()
        {
            if ((this.NumberOfPages >= 1) && (this.NumberOfPages <= 20))
            {
                return 2.0;
            }
            else if ((this.NumberOfPages >= 21) && (this.NumberOfPages <= 50))
            {
                return 3.0;
            }
            else
            {
                return 5.0;
            }
        }

        public void SetMagazineCostWithToll()
        {
            try
            {
                this.ProductCostAfterTollIncreasement = this.ProductCostWithoutIncreasement * (100.0 + this.tollIncreasement()) / 100.0;
            }
            catch (OverflowException)
            {
                this.ProductCostAfterTollIncreasement = this.ProductCostWithoutIncreasement;
            }
        }
    }
    class Library
    {
        static public List<Media> mediaGrp = new List<Media>();

        static public void AddMedia()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Please enter which type would you like to add (Books\\Videos\\Magazines) : ");
                string whichProduct = Console.ReadLine();
                if (whichProduct == "Books")
                {
                    Library.AddBooks();
                }
                else if (whichProduct == "Videos")
                {
                    Library.AddVideos();
                }
                else if (whichProduct == "Magazines")
                {
                    AddMagazines();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input!");
                }
                SaveToFile();
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input is too big!");
            }
        }
        static public void AddBooks()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Please enter the name of the product : ");
                string productName = Console.ReadLine();
                if (Library.mediaGrp.Find(product => product.ProductName == productName) == null)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Please enter the cost of the product : ");
                    double cost = double.Parse(Console.ReadLine());
                    if (cost <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The cost of the product cannot be less than or equal to zero!");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Please enter the author of the book : ");
                        string author = Console.ReadLine();
                        Console.Write("Please enter the publisher of the book : ");
                        string publisher = Console.ReadLine();
                        Books newBook = new Books(productName, cost, author, publisher);
                        Library.mediaGrp.Add(newBook);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("New book added!");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Entered product already exists!");
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input!");
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input is too big!");
            }
        }
        static public void AddVideos()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Please enter the name of the video : ");
                string productName = Console.ReadLine();
                if (Library.mediaGrp.Find(product => product.ProductName == productName) == null)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Please enter the cost of the video : ");
                    double productCost = double.Parse(Console.ReadLine());
                    if (productCost <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Entered cost cannot be less than or equal to zero!");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Please enter the time span of the video int minute (must be integer) : ");
                        int videoLength = int.Parse(Console.ReadLine());
                        if (videoLength <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Time span cannot be less than or equal to zero!");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("Please enter the number of CDs : ");
                            int numberOfCDs = int.Parse(Console.ReadLine());
                            if (numberOfCDs <= 0)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("The number of the CDs cannot be less than or equal to zero!");
                            }
                            else
                            {
                                Videos newVideo = new Videos(productName, productCost, videoLength, numberOfCDs);
                                Library.mediaGrp.Add(newVideo);
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("{0} added!", productName);
                            }
                        }
                    }
                }
                else{
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("This name has already used!");
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input!");
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input is too big!");
            }
        }
        static public void AddMagazines()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Please enter the name of the product : ");
                string productName = Console.ReadLine();
                if (Library.mediaGrp.Find(product => product.ProductName == productName) == null)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Please enter the cost of the magazine : ");
                    double productCost = double.Parse(Console.ReadLine());
                    if (productCost <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("The cost of the magazine cannot be less than or equal to zero!");
                    }
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Pleas enter the number of the pages : ");
                        int numberOfPages = int.Parse(Console.ReadLine());
                        if (numberOfPages <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The number of the pages cannot be less than or equal to zero!");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("Please enter the publisher of the magazine : ");
                            string publisher = Console.ReadLine();

                            Magazines newMagazine = new Magazines(productName, productCost, publisher, numberOfPages);
                            Library.mediaGrp.Add(newMagazine);
                            Console.WriteLine("{0} added!", productName);
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("A product already exists with this name!");
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input!");
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input is too big!");
            }
        }

        static public void SaveToFile()
        {
            StreamWriter writer = new StreamWriter("MediaInfo.txt");
            for (int i = 0; i < mediaGrp.Count; i++)
            {
                Media media = mediaGrp[i];
                string type = "";
                if (media is Books)
                {
                    type = "Books";
                }
                else if (media is Videos)
                {
                    type = "Videos";
                }
                else
                {
                    type = "Magazines";
                }
                writer.WriteLine(type + " : " + media.MediaID);
                writer.WriteLine(media.ProductName);
                writer.WriteLine(media.ProductCostWithoutIncreasement);
                if (media is Books)
                {
                    Books book = (Books)media;
                    writer.WriteLine(book.Author);
                    writer.WriteLine(book.Publisher);
                }
                else if (media is Videos)
                {
                    Videos video = (Videos)media;
                    writer.WriteLine(video.VideoTimeLength);
                    writer.WriteLine(video.CountOfCDs);
                }
                else if (media is Magazines)
                {
                    Magazines magazine = (Magazines)media;
                    writer.WriteLine(magazine.NumberOfPages);
                    writer.WriteLine(magazine.Publisher);
                }
            }
            writer.Close();
        }
        static public void DelMedia()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Please enter the ID of the media : ");
                int mediaID = int.Parse(Console.ReadLine());
                if (Library.mediaGrp.Find(media => media.MediaID == mediaID) == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Entered media was not found!");
                }
                else
                {
                    int indexOfMedia = Library.mediaGrp.FindIndex(media => media.MediaID == mediaID);
                    Library.mediaGrp.RemoveAt(indexOfMedia);
                    SaveToFile();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Entered media removed!");
                }
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input!");
            }
            catch (OverflowException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Input is too big!");
            }
        }

        static public void SearchMedia()
        {
            StreamReader reader = null;
            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Please enter the ID of the media : ");
                string mediaID = Console.ReadLine();
                reader = new StreamReader("MediaInfo.txt");
                bool flag = true;
                string[] line = new string[2];
                while (reader.EndOfStream == false)
                {
                    line = reader.ReadLine().Split(" : ");
                    if (line[1] == mediaID)
                    {
                        flag = false;
                        Console.WriteLine(line[0] + " : " + line[1]);
                        Console.WriteLine(reader.ReadLine());
                        Console.WriteLine(reader.ReadLine());
                        Console.WriteLine(reader.ReadLine());
                        Console.WriteLine(reader.ReadLine());
                        break;
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        reader.ReadLine();
                    }
                }
                reader.Close();
                if (flag)
                {
                    Console.WriteLine("Entered ID was not found!");
                }
            }
            catch (FileNotFoundException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\"MediaInfo.txt\" was not found!");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        static public void ShowAllMediaID()
        {
            if (Library.mediaGrp.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nothing found!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (Media media in Library.mediaGrp)
                {
                    Console.WriteLine(media.ProductName + ",product ID : " + media.MediaID);
                }
            }
        }
    }
}
