using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace tamrin_seri_4_soal_2
{
    enum Produ_Type { Phone = 1, car, watch, T_shirt, Laptop, Tablet, Charger, Glass, Robot }
    enum gender { male = 1, female, others }
    class Program
    {
        static void Main()
        {
            try
            {
                Category newCategory = new Category(Produ_Type.Phone);
                Category.allCategories.Add(newCategory);
                newCategory = new Category(Produ_Type.car);
                Category.allCategories.Add(newCategory);
                newCategory = new Category(Produ_Type.watch);
                Category.allCategories.Add(newCategory);
                newCategory = new Category(Produ_Type.T_shirt);
                Category.allCategories.Add(newCategory);
                newCategory = new Category(Produ_Type.Laptop);
                Category.allCategories.Add(newCategory);
                newCategory = new Category(Produ_Type.Tablet);
                Category.allCategories.Add(newCategory);
                newCategory = new Category(Produ_Type.Charger);
                Category.allCategories.Add(newCategory);
                newCategory = new Category(Produ_Type.Glass);
                Category.allCategories.Add(newCategory);
                newCategory = new Category(Produ_Type.Robot);
                Category.allCategories.Add(newCategory);
                string order;
                while (true)
                {
                    try
                    {
                        Console.WriteLine("\nMenu :\nCategory\tCart\tExit");
                        order = Console.ReadLine();
                        if (order == "Category")
                        {
                            try
                            {
                                Console.WriteLine("\nPlease enter the name of the " +
                                    "category:\nPhone, car, watch, T_shirt, Laptop, Tablet, Charger, Glass, Robot");
                                Produ_Type categoryName = (Produ_Type)
                                    Enum.Parse(typeof(Produ_Type),
                                    Console.ReadLine(),
                                    true);
                                bool flag = true;
                                int categoryIndex = 0;
                                for (int i = 0; i < Category.allCategories.Count; i++)
                                {
                                    if (Category.allCategories[i].prductName == categoryName)
                                    {
                                        categoryIndex = i;
                                        flag = false;
                                        break;
                                    }
                                }
                                if (flag)
                                {
                                    throw new Exception("CategoryNotFound");
                                }
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu\nAddProductCategory\t" +
                                            "FilterByPrice\tShowSupply\tBack");
                                        string innerOrder = Console.ReadLine();
                                        if (innerOrder == "AddProductCategory")
                                        {
                                            Console.Write("\nPlease enter how many " +
                                                "products would you like to enter: ");
                                            int howMany = int.Parse(Console.ReadLine());
                                            if (howMany <= 0)
                                            {
                                                throw new Exception("MinusNumber");
                                            }
                                            for (int i = 0; i < howMany; i++)
                                            {
                                                try
                                                {
                                                    Console.Write("\nPlease enter the name of" +
                                                        " the product: ");
                                                    string name = Console.ReadLine();
                                                    Console.Write("\nPlease enter the ID: ");
                                                    double ID = double.Parse(Console.ReadLine());
                                                    for (int j = 0; j < Product.prductList.Count; j++)
                                                    {
                                                        if (Product.prductList[j].ID == ID)
                                                        {
                                                            throw new Exception("IDAlreadyUsed");
                                                        }
                                                    }
                                                    Console.Write("\nPlease enter the cost: ");
                                                    double cost = double.Parse(Console.ReadLine());
                                                    if (cost <= 0)
                                                    {
                                                        throw new Exception("MinusCost");
                                                    }
                                                    Console.Write("\nPlease enter the point: ");
                                                    double point = double.Parse(Console.ReadLine());
                                                    Product newProduct = new Product(ID,
                                                        cost,
                                                        name,
                                                        point);
                                                    Product.prductList.Add(newProduct);
                                                    Category.allCategories[categoryIndex].prductList.Add(newProduct);
                                                    Console.WriteLine("\nProduct Added!");
                                                }
                                                catch (Exception error) when (error.Message == "InvalidPrice")
                                                {
                                                    Console.WriteLine("\nInvalid price!");
                                                    break;
                                                }
                                                catch (Exception error) when (error.Message == "MinusCost")
                                                {
                                                    Console.WriteLine("\nThe cost of the product" +
                                                        " must be more than zero!");
                                                    break;
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("\nInvalid input");
                                                    break;
                                                }
                                                catch (Exception error) when (error.Message == "IDAlreadyUsed")
                                                {
                                                    Console.WriteLine("\nThis ID has already used!");
                                                    break;
                                                }
                                                catch (OverflowException)
                                                {
                                                    Console.WriteLine("\nInput is too big!");
                                                    break;
                                                }
                                            }
                                        }
                                        else if (innerOrder == "FilterByPrice")
                                        {
                                            try
                                            {
                                                Console.WriteLine("\nPlease enter the range of the price.");
                                                Console.Write("\nPlease enter the highest price:");
                                                double highestPrice = double.Parse(Console.ReadLine());
                                                Console.Write("\nPlease enter the lowest price:");
                                                double lowestPrice = double.Parse(Console.ReadLine());
                                                if(lowestPrice<0 || highestPrice < 0)
                                                {
                                                    throw new Exception("MinusCost");
                                                }
                                                if (highestPrice < lowestPrice)
                                                {
                                                    throw new Exception("InvalidInput");
                                                }
                                                List<Product> prductList = Category.allCategories[categoryIndex].FilterByPrice(lowestPrice, highestPrice);
                                                if (prductList.Count == 0)
                                                {
                                                    Console.WriteLine("\nNothing found!");
                                                }
                                                else
                                                {
                                                    Console.WriteLine(Category.allCategories[categoryIndex].prductName + "\t" + Category.allCategories[categoryIndex].ID);
                                                    for (int i = 0; i < prductList.Count; i++)
                                                    {
                                                        Console.WriteLine(
                                                            prductList[i].name + "\t" +
                                                            prductList[i].ID + "\t" +
                                                            prductList[i].cost + "\t" +
                                                            prductList[i].point + "\t" +
                                                            prductList[i].factory);
                                                    }
                                                }
                                            }
                                            catch(Exception error)when(error.Message== "MinusCost")
                                            {
                                                Console.WriteLine("\nEntered price must be more than zero!");
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("Invalid input!");
                                            }
                                            catch (OverflowException)
                                            {
                                                Console.WriteLine("Input is too big!");
                                            }
                                            catch (Exception error) when (error.Message == "InvalidInput")
                                            {
                                                Console.WriteLine("Highest price is lower than the lowest price!");
                                            }
                                        }
                                        else if (innerOrder == "ShowSupply")
                                        {
                                            Console.WriteLine("\nCategory name: " + Category.allCategories[categoryIndex].prductName +
                                                "\t" + "Category number: " + Category.allCategories[categoryIndex].ID);
                                            Category.allCategories[categoryIndex].ShowSupply();
                                        }
                                        else if (innerOrder == "Back")
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nInvalid input!");
                                        }
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("\nInvalid input!");
                                    }
                                    catch (Exception error) when (error.Message == "MinusNumber")
                                    {
                                        Console.WriteLine("\nEntered number must be more" +
                                            " than zero!");
                                    }
                                    catch (OverflowException)
                                    {
                                        Console.WriteLine("\nInput is too big!");
                                    }
                                }
                            }
                            catch (Exception error) when (error.Message == "CategoryNotFound")
                            {
                                Console.WriteLine("\nEntered category was not found!");
                            }
                            catch (ArgumentException)
                            {
                                Console.WriteLine("\nInvalid input!");
                            }
                            catch (OutOfMemoryException)
                            {
                                Console.WriteLine("\nNot enough memory on the device!");
                            }
                        }
                        else if (order == "Cart")
                        {
                            try
                            {
                                Console.Write("\nPlease enter your name: ");
                                string name = Console.ReadLine();

                                Console.Write("\nPlease enter your family name: ");
                                string familyName = Console.ReadLine();

                                Console.Write("\nPlease enter your age: ");
                                int age = 0;
                                while (true)
                                {
                                    try
                                    {
                                        age = int.Parse(Console.ReadLine());
                                        if (age <= 0)
                                        {
                                            throw new Exception("InvalidAge");
                                        }
                                        break;
                                    }
                                    catch (Exception error) when (error.Message == "InvalidAge")
                                    {
                                        Console.WriteLine("\nInvalid input!\nPlease try again!");
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("\nInvalid input!\nPlease try again!");
                                    }
                                    catch (OverflowException)
                                    {
                                        Console.WriteLine("\nInput is too big!\nPlease try again");
                                    }
                                }

                                Console.WriteLine("\nPlease enter your gender:\nmale\tfemale\tothers");
                                gender gender;
                                while (true)
                                {
                                    try
                                    {
                                        gender = (gender)Enum.Parse(typeof(gender), Console.ReadLine(), true);
                                        break;
                                    }
                                    catch (OverflowException)
                                    {
                                        Console.WriteLine("\nInput is too big!\nPlease try again!");
                                    }
                                    catch (ArgumentException)
                                    {
                                        Console.WriteLine("\nInvalid input!\nPlease try again!");
                                    }
                                }
                                Console.Write("\nPlease enter your phone number: ");
                                string phoneNumber;
                                while (true)
                                {
                                    try
                                    {
                                        phoneNumber = Console.ReadLine();
                                        Regex regex = new Regex(@"^09[0-9]{9}$");
                                        MatchCollection matches = regex.Matches(phoneNumber);
                                        if (matches.Count == 0)
                                        {
                                            throw new Exception("InvalidPhoneNumber");
                                        }
                                        break;
                                    }
                                    catch (OverflowException)
                                    {
                                        Console.WriteLine("\nInput is too big!\nPlease try again!");
                                    }
                                    catch (Exception error) when (error.Message == "InvalidPhoneNumber")
                                    {
                                        Console.WriteLine("\nInvalid phone number!\nPlease try again!");
                                    }
                                }

                                People newPeople = new People(name, familyName, age, gender, phoneNumber);
                                bool flag = true;
                                int indexPeople = 0;
                                for (int i = 0; i < People.peopleList.Count; i++)
                                {
                                    if (People.peopleList[i] == newPeople)
                                    {
                                        indexPeople = i;
                                        flag = false;
                                        break;
                                    }
                                }
                                if (flag)
                                {
                                    People.peopleList.Add(newPeople);
                                    Cart newCart = new Cart(newPeople);
                                    Cart.allCarts.Add(newCart);
                                }
                                int index = 0;
                                for (int i = 0; i < Cart.allCarts.Count; i++)
                                {
                                    if (Cart.allCarts[i].owner == newPeople)
                                    {
                                        index = i;
                                        break;
                                    }
                                }
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nAddProductToCart\tCalculatePrice" +
                                            "\tBack\nDeleteProduct\tEditProfile");
                                        string cartOrder = Console.ReadLine();
                                        if (cartOrder == "AddProductToCart")
                                        {
                                            Console.Write("\nPlease enter how many Product would you like to enter: ");
                                            int howMany = int.Parse(Console.ReadLine());
                                            if (howMany <= 0)
                                            {
                                                throw new Exception("InvalidHowMany");
                                            }
                                            for(int ii=0;ii<howMany;ii++)
                                            {
                                                try
                                                {
                                                    Console.Write("\nPlease enter the name of the product: ");
                                                    string prductName = Console.ReadLine();

                                                    Console.Write("\nPlease enter the ID of the product: ");
                                                    double ID = double.Parse(Console.ReadLine());

                                                    bool wasFoundOrNot = true;
                                                    for (int i = 0; i < Product.prductList.Count; i++)
                                                    {
                                                        if (Product.prductList[i].ID == ID)
                                                        {
                                                            if (Product.prductList[i].name != prductName)
                                                            {
                                                                throw new Exception("ProductNotFound");
                                                            }
                                                            else
                                                            {
                                                                wasFoundOrNot = false;
                                                                Cart.allCarts[index].AddProductToCart(Product.prductList[i]);
                                                                Console.WriteLine('\n'+Product.prductList[i].name + " added!");
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    if (wasFoundOrNot)
                                                    {
                                                        Console.WriteLine("\nEntered product was not found!");
                                                    }
                                                }
                                                catch (Exception error) when (error.Message == "ProductNotFound")
                                                {
                                                    Console.WriteLine("\nEntered product was not found!");
                                                }
                                                catch (FormatException)
                                                {
                                                    Console.WriteLine("\nInput is invalid!");
                                                }
                                                catch (OverflowException)
                                                {
                                                    Console.WriteLine("\nInput is too big!");
                                                }
                                            }
                                        }
                                        else if (cartOrder == "CalculatePrice")
                                        {
                                            Cart.allCarts[index].CalculatePrice();
                                        }
                                        else if (cartOrder == "Back")
                                        {
                                            break;
                                        }
                                        else if (cartOrder == "DeleteProduct")
                                        {
                                            try
                                            {
                                                Console.Write("\nPlease enter how many products would you like to delete: ");
                                                int howMany = int.Parse(Console.ReadLine());
                                                if (howMany <= 0)
                                                {
                                                    throw new Exception("MinusPoint");
                                                }
                                                List<Product> prductList = new List<Product>();
                                                for (int i = 0; i < howMany; i++)
                                                {
                                                    Console.WriteLine("\nPlease enter the ID of the product: ");
                                                    double ID = double.Parse(Console.ReadLine());
                                                    bool wasFoundOrNot = true;
                                                    for (int j = 0; j < Product.prductList.Count; j++)
                                                    {
                                                        if (Product.prductList[j].ID == ID)
                                                        {
                                                            wasFoundOrNot = false;
                                                            prductList.Add(Product.prductList[i]);
                                                        }
                                                    }
                                                    if (wasFoundOrNot)
                                                    {
                                                        Console.WriteLine("\nEntered ID was not found!");
                                                    }
                                                }
                                                Cart.allCarts[index].DeleteProduct(prductList);
                                            }
                                            catch (Exception error) when (error.Message == "MinusPoint")
                                            {
                                                Console.WriteLine("\nEntered number has to be more than zero!");
                                            }
                                            catch (OverflowException)
                                            {
                                                Console.WriteLine("\nInput is too big!");
                                            }
                                            catch (FormatException)
                                            {
                                                Console.WriteLine("\nInput is invalid!");
                                            }
                                        }
                                        else if (cartOrder == "EditProfile")
                                        {
                                            while (true)
                                            {
                                                try
                                                {
                                                    Console.WriteLine("\nAge\tname and family name\tphone number\tBack");
                                                    string EditProfileOrder = Console.ReadLine();
                                                    if (EditProfileOrder == "Age")
                                                    {
                                                        Console.Write("\nPlease enter your age: ");
                                                        int newAge = int.Parse(Console.ReadLine());
                                                        if (newAge <= 0)
                                                        {
                                                            throw new Exception("MinusAge");
                                                        }
                                                        People.peopleList[indexPeople].EditProfile(newAge);
                                                        Console.WriteLine("\nAge modified!");
                                                    }
                                                    else if (EditProfileOrder == "name and family name")
                                                    {
                                                        Console.Write("\nPlease enter your name: ");
                                                        string modifyName = Console.ReadLine();
                                                        Console.Write("\nPlease enter your family name: ");
                                                        string modifyLastName = Console.ReadLine();
                                                        People.peopleList[indexPeople].EditProfile(modifyName, modifyLastName);
                                                        Console.WriteLine("\nFirst name and last name modified!");
                                                    }
                                                    else if (EditProfileOrder == "phone number")
                                                    {
                                                        Console.Write("\nPlease enter your phone number: ");
                                                        string newPhoneNumber = Console.ReadLine();
                                                        People.peopleList[indexPeople].EditProfile(newPhoneNumber);
                                                    }
                                                    else if (EditProfileOrder == "Back")
                                                    {
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("\nInvalid order!\nPlease try again!");
                                                    }
                                                }
                                                catch (Exception error) when (error.Message == "InvalidPhoneNumber")
                                                {
                                                    Console.WriteLine("\nInvalid phone number!");
                                                }
                                                catch (Exception error) when (error.Message == "MinusAge")
                                                {
                                                    Console.WriteLine("\nEntered age cannot be less than zero!");
                                                }
                                                catch (OverflowException)
                                                {
                                                    Console.WriteLine("\nInput is too big!\nPlease try again!");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nInvalid input!\nPlease try again!");
                                        }
                                    }
                                    catch(Exception error)when(error.Message== "InvalidHowMany")
                                    {
                                        Console.WriteLine("\nEntered number must be more than zero!");
                                    }
                                    catch (FormatException)
                                    {
                                        Console.WriteLine("\nInvalid input!");
                                    }
                                    catch (OverflowException)
                                    {
                                        Console.WriteLine("\nInput is too big!\nPlease try again!");
                                    }
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("\nInvalid input!");
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("\nInput is too big!");
                            }
                        }
                        else if (order == "Exit")
                        {
                            Console.WriteLine("\nProgram finished!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nInvalid input!");
                        }
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("\nInput is too long!");
                    }
                }
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("\nNot enough memory on the device!" +
                    "\nProgram stopped!");
            }
        }
    }
    //checked
    struct People
    {
        public static List<People> peopleList = new List<People>();
        string _name;
        public string name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        string _familyName;
        public string familyName
        {
            get { return this._familyName; }
            set { this._familyName = value; }
        }
        int _age;
        public int age
        {
            get { return _age; }
            set
            { _age = value; }
        }
        gender _gender;
        public gender gender
        {
            get { return _gender; }
            set { this._gender = value; }
        }
        string _phoneNumber;
        public string phoneNumber
        {
            get { return this._phoneNumber; }
            set { this._phoneNumber = value; }
        }
        public static bool operator ==(People people1, People people2)
        {
            if (people1.name != people2.name)
            {
                return false;
            }
            else if (people1.familyName != people2.familyName)
            {
                return false;
            }
            else if (people1.age == people2.age)
            {
                return false;
            }
            else if (people1.gender != people2.gender)
            {
                return false;
            }
            else if (people1.phoneNumber != people2.phoneNumber)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool operator !=(People people1, People people2)
        {
            return true;
        }
        public People(string name, string familyName, int age, gender gender, string phoneNumber)
        {
            Regex regex = new Regex(@"^09[0-9]{9}$");
            MatchCollection matches = regex.Matches(phoneNumber);
            if (matches.Count == 0)
            {
                throw new Exception("InvalidPhoneNumber");
            }

            this._name = name;
            this._familyName = familyName;
            this._age = age;
            this._gender = gender;
            this._phoneNumber = phoneNumber;
        }
        public void EditProfile(string phoneNumber)
        {
            Regex regex = new Regex(@"^09[0-9]{9}$");
            MatchCollection matches = regex.Matches(phoneNumber);
            if (matches.Count == 0)
            {
                throw new Exception("InvalidPhoneNumber");
            }
            this.phoneNumber = phoneNumber;
            Console.WriteLine("\nProfile edited seccessfully!");
        }
        public void EditProfile(int age)
        {
            this.age = age;
            Console.WriteLine("\nProfile edited seccessfully!");
        }
        public void EditProfile(string name, string familyName)
        {
            this.name = name;
            this.familyName = familyName;
            Console.WriteLine("\nProfile edited seccessfully!");
        }
    }
    class Product
    {
        static public List<Product> prductList = new List<Product>();
        double _ID;
        public double ID
        {
            get { return this._ID; }
            set
            {
                for (int i = 0; i < Product.prductList.Count; i++)
                {
                    if (Product.prductList[i].ID == ID)
                    {
                        throw new Exception("This ID has already used!");
                    }
                }
                this._ID = value;
            }
        }
        double _cost;
        public double cost
        {
            get { return this._cost; }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("InvalidPrice");
                }
                this._cost = value;
            }
        }
        string _name;
        public string name
        {
            get { return this._name; }
            set { this._name = value; }
        }
        double _point;
        public double point
        {
            get { return this._point; }
            set { this._point = value; }
        }
        string _factory;
        public string factory
        {
            get { return this._factory; }
        }
        public Product(double ID, double cost, string name, double point)
        {
            this.ID = ID;
            this.cost = cost;
            this.name = name;
            this.point = point;
            if (ID >= 1 && ID <= 5)
            {
                this._factory = 'a'.ToString();
            }
            else if (ID > 5 && ID <= 10)
            {
                this._factory = 'b'.ToString();
            }
            else if (ID > 10)
            {
                this._factory = 'c'.ToString();
            }
            else
            {
                throw new Exception("InvalidID");
            }
        }
    }
    class Category
    {
        static public List<Category> allCategories = new List<Category>();
        public List<Product> prductList = new List<Product>();
        int _ID;
        public int ID
        {
            get { return this._ID; }
            set { this._ID = value; }
        }
        Produ_Type _prductName;
        public Produ_Type prductName
        {
            get { return this._prductName; }
            set { this._prductName = value; }
        }
        public List<Product> FilterByPrice(double lowestPrice, double highestPrice)
        {
            List<Product> returnList = new List<Product>();
            for (int i = 0; i < prductList.Count; i++)
            {
                if ((prductList[i].cost >= lowestPrice) && (prductList[i].cost <= highestPrice))
                {
                    returnList.Add(prductList[i]);
                }
            }
            return returnList;
        }
        public void ShowSupply()
        {
            //sorts the list
            if (prductList.Count >= 2)
            {
                for (int i = 0, j = 0; i < prductList.Count * prductList.Count; i++)
                {
                    if (prductList[j].cost > prductList[j + 1].cost)
                    {
                        Product cheatSheet = prductList[j];
                        prductList[j] = prductList[j + 1];
                        prductList[j + 1] = cheatSheet;
                    }
                    j++;
                    if (j == prductList.Count - 1)
                    {
                        j = 0;
                    }
                }
            }
            if (prductList.Count == 0)
            {
                Console.WriteLine("\nCategory is empty!");
            }
            else
            {
                for (int i = 0; i < prductList.Count; i++)
                {
                    Console.WriteLine("\nProduct name: " + prductList[i].name +
                        "\tPrice: " + prductList[i].cost);
                }
            }
        }
        public void AddPrductCategory(List<Product> prductList)
        {
            for (int i = 0; i < prductList.Count; i++)
            {
                this.prductList.Add(prductList[i]);
            }
        }
        public Category(Produ_Type prductType)
        {
            this.ID = (int)prductType;
            this.prductName = prductType;
        }
    }

    //checked
    class Cart
    {
        public static List<Cart> allCarts = new List<Cart>();
        People _owner;
        public People owner
        {
            get { return this._owner; }
            set { this._owner = value; }
        }
        public List<Product> prductList = new List<Product>();
        public Cart(People owner)
        {
            this.owner = owner;
        }
        public void AddProductToCart(params Product[] prductList)
        {
            for (int i = 0; i < prductList.Length; i++)
            {
                this.prductList.Add(prductList[i]);
            }
        }
        public void DeleteProduct(List<Product> prductList)
        {
            for (int i = 0; i < prductList.Count; i++)
            {
                for (int j = 0; j < this.prductList.Count; j++)
                {
                    if (this.prductList[j].ID == prductList[i].ID)
                    {
                        this.prductList.RemoveAt(j);
                        break;
                    }
                }
            }
            for (int i = 0; i < this.prductList.Count; i++)
            {
                Console.WriteLine('\n'+this.prductList[i].name + "\t" + this.prductList[i].cost);
            }
        }
        public void CalculatePrice()
        {
            try
            {
                double totalPrice = 0;
                for (int i = 0; i < this.prductList.Count; i++)
                {
                    Console.WriteLine('\n'+this.prductList[i].name +
                        "\t" +
                        this.prductList[i].cost);
                    totalPrice += this.prductList[i].cost;
                }
                Console.WriteLine("\nTotal price: " + totalPrice + '\n');
            }
            catch (OverflowException)
            {
                Console.WriteLine("\nThe price of the products is too big!");
            }
        }
    }
}
