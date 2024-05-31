using System;
using System.Collections.Generic;
using System.IO;

namespace tamrin_seri_3_soal_5
{
    class Program
    {
        static void Main()
        {
            try
            {
                StreamReader reader = new StreamReader("User.txt");
                for (int i = 0; reader.EndOfStream == false; i++)
                {
                    string[] customer = reader.ReadLine().Split(", ");
                    Customer newCustomer = new Customer(
                        customer[0],
                        int.Parse(customer[1]),
                        int.Parse(customer[2])
                        );
                    Customer.groupOfCustomers.Add(newCustomer);
                }
                reader.Close();
            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("Warning! \"User.txt\" file was not found!");
            }
            int specialOff = 1;
            string order = "";
            string prompt = "\nPlease try again!";
            string processFailedString = "\nProcess failed!";
            while (true)
            {
                Console.WriteLine("\nMenu :\nAdd Customer\tincrease balance of customer" +
                    "\tAdd warehouse material\tincrease warehouse material\tAdd food" +
                    "\nincrease food\tAdd discount code\tAdd discount code to customer" +
                    "\nSell food\tAccept transaction\tPrint transaction list\tPrint " +
                    "income\nExit");
                try
                {
                    order = Console.ReadLine();
                    if (order == "Add Customer")
                    {
                        try
                        {
                            int ID = 0;
                            Console.Write("id: ");
                            
                            ID = int.Parse(Console.ReadLine());
                            for (int i = 0; i < Customer.groupOfCustomers.Count; i++)
                            {
                                if (Customer.groupOfCustomers[i].ID == ID)
                                {
                                    throw new Exception("IDDetected");
                                }
                            }
                            Console.WriteLine("ID is received successfully!");

                            string customerName = "";
                            Console.Write("Name: ");
                            
                            customerName = Console.ReadLine();
                            
                            Customer newCustomer = new Customer(customerName, ID);
                            Customer.groupOfCustomers.Add(newCustomer);
                            Console.WriteLine("Customer added!");
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input!" + processFailedString);
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Entered name is too long!" + processFailedString);
                        }
                        catch (Exception error) when (error.Message == "IDDetected")
                        {
                            Console.WriteLine("This customer has already " +
                                    "assigned!" + processFailedString);
                        }
                    }
                    else if (order == "increase balance of customer")
                    {
                        try
                        {
                            Console.Write("Id: ");
                            int ID = 0;
                            //while (true)
                            //{
                            //try
                            //{
                            bool flag = true;
                            ID = int.Parse(Console.ReadLine());
                            for (int i = 0; i < Customer.groupOfCustomers.Count; i++)
                            {
                                if (Customer.groupOfCustomers[i].ID == ID)
                                {
                                    flag = false;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                throw new Exception("IDNotFound");
                            }
                            //}

                            //                            }

                            Console.Write("Amount: ");
                            double amount = 0;
                            //while (true)
                            //{
                            //try
                            //{
                            amount = double.Parse(Console.ReadLine());
                            if (amount < 0)
                            {
                                throw new Exception("MinusAmount");
                            }
                            //}

                            //}


                            for (int i = 0; i < Customer.groupOfCustomers.Count; i++)
                            {
                                if (Customer.groupOfCustomers[i].ID == ID)
                                {
                                    Customer.groupOfCustomers[i].chargeVallet(amount);
                                }
                            }
                            Console.WriteLine("Charging vallet completed successfully!");




                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Input is too big!" +
                                processFailedString);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input!" +
                                processFailedString);
                        }
                        catch (Exception error) when (error.Message == "IDNotFound")
                        {
                            Console.WriteLine("Customer was not found!" +
                               processFailedString);

                        }
                        catch (Exception error) when (error.Message == "MinusAmount")
                        {
                            Console.WriteLine("Entered amount cannot be less " +
                                "than zero!" + processFailedString);
                        }
                        catch (Exception error) when (error.Message == "ValletAlreadyFull")
                        {
                            Console.WriteLine("Customer's vallet is already full!\n" +
                                "no more money is accepted!");
                        }
                    }
                    else if (order == "Sell food")
                    {
                        string nameOfFood;
                        int numberOfFood;
                        int ID;
                        try
                        {
                            int indexOf_Food = 0;
                            int indexOf_Person = 0;
                            bool flagToFind_Food = true;
                            bool flagToFind_ID = true;

                            //receive name of food
                            Console.Write("Food Name: ");
                            nameOfFood = Console.ReadLine();

                            //check that food exists or not
                            for (int i = 0; i < food.foodList.Count; i++)
                            {
                                if (food.foodList[i].name == nameOfFood)
                                {
                                    indexOf_Food = i;
                                    flagToFind_Food = false;
                                    break;
                                }
                            }
                            if (flagToFind_Food)
                            {
                                throw new Exception("FoodNotFound");
                            }

                            //receive how many food
                            Console.Write("Amount: ");
                            numberOfFood = int.Parse(Console.ReadLine());

                            //check entered number for food is valid or not
                            if (numberOfFood <= 0)
                            {
                                throw new Exception("InvalidNumberOfFood");
                            }
                            if (food.foodList[indexOf_Food].numberOfFood < numberOfFood)
                            {
                                throw new Exception("NotEnoughFood");
                            }

                            //receives customer's ID
                            Console.Write("Customer ID: ");
                            ID = int.Parse(Console.ReadLine());

                            //check whether ID exists or not
                            for (int i = 0; i < Customer.groupOfCustomers.Count; i++)
                            {
                                if (Customer.groupOfCustomers[i].ID == ID)
                                {
                                    indexOf_Person = i;
                                    flagToFind_ID = false;
                                    break;
                                }
                            }
                            if (flagToFind_ID)
                            {
                                throw new Exception("IDNotFound");
                            }

                            //checks whether customer has enough money to buy
                            //food or not
                            if (food.foodList[indexOf_Food].cost * numberOfFood > Customer.groupOfCustomers[indexOf_Person].customerVallet)
                            {
                                throw new Exception("NotEnoughMoneyToBuyFood");
                            }
                            Transaction newTransaction;
                            if (specialOff == 1)
                            {
                                newTransaction = new Transaction(
                                    ID,
                                    nameOfFood,
                                    numberOfFood,
                                    5);
                                Transaction.transactionsList.Add(newTransaction);
                                specialOff -= 1;
                            }
                            else
                            {
                                newTransaction = new Transaction(
                                    ID,
                                    nameOfFood,
                                    numberOfFood);
                                Transaction.transactionsList.Add(newTransaction);
                            }
                            Console.WriteLine("Food was sold successfully!");
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Input is too big!" + processFailedString);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Input does not have correct format!"
                                + processFailedString);
                        }
                        catch (Exception error) when (error.Message == "FoodNotFound")
                        {
                            Console.WriteLine("Entered food does not exist!"
                                + processFailedString);
                        }
                        catch (Exception error) when (error.Message == "NotEnoughFood")
                        {
                            Console.WriteLine("There are not enough food!" +
                                processFailedString);
                        }
                        catch (Exception error) when (error.Message == "InvalidNumberOfFood")
                        {
                            Console.WriteLine("The number of food cannot be zero or" +
                                "less than zero." + processFailedString);
                        }
                        catch (Exception error) when (error.Message == "IDNotFound")
                        {
                            Console.WriteLine("Entered ID was not found!" +
                                processFailedString);
                        }
                        catch (Exception error) when (error.Message == "NotEnoughMoneyToBuyFood")
                        {
                            Console.WriteLine("Customer does not have enough money!" +
                                processFailedString);
                        }

                    }       //
                    else if (order == "Add warehouse material")
                    {
                        string materialName = "";
                        int amount = 0;
                        try
                        {
                            Console.Write("Material Name: ");
                            materialName = Console.ReadLine();
                            for (int i = 0; i < Storage.storage.Count; i++)
                            {
                                if (Storage.storage[i].materialName == materialName)
                                {
                                    throw new Exception("MaterialAlreadyExist");
                                }
                            }

                            Console.Write("Amount: ");
                            amount = int.Parse(Console.ReadLine());
                            if (amount <= 0)
                            {
                                throw new Exception("InvalidInput");
                            }

                            Storage newMaterial = new Storage(materialName, amount);
                            Storage.storage.Add(newMaterial);
                            Console.WriteLine("\"" + materialName + "\" " +
                                "added to the storage!");
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Input is too big!" + processFailedString);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input!");
                        }
                        catch (Exception error) when (error.Message == "MaterialAlreadyExist")
                        {
                            Console.WriteLine("Entered material already exist!" +
                                processFailedString);
                        }
                        catch (Exception error) when (error.Message == "InvalidInput")
                        {
                            Console.WriteLine("The amount of the material cannot" +
                                "be equal or less than zero!" + processFailedString);
                        }
                    }
                    else if (order == "increase warehouse material")
                    {
                        string nameOfMaterial = "";
                        int amount = 0;
                        try
                        {
                            int indexOfMaterial = 0;
                            bool flag = true;
                            Console.Write("Material Name: ");
                            nameOfMaterial = Console.ReadLine();
                            for (int i = 0; i < Storage.storage.Count; i++)
                            {
                                if (Storage.storage[i].materialName == nameOfMaterial)
                                {
                                    indexOfMaterial = i;
                                    flag = false;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                throw new Exception("MaterialNotFound");
                            }

                            Console.Write("Amount: ");
                            amount = int.Parse(Console.ReadLine());
                            if (amount <= 0)
                            {
                                throw new Exception("MinusAmount");
                            }
                            Storage.storage[indexOfMaterial].numberOfMaterial += amount;
                            Console.WriteLine("The amount of material increased " +
                                "seccessfully!");
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Input is too big!"
                                + processFailedString);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input" +
                                processFailedString);
                        }
                        catch (Exception error) when (error.Message == "MaterialNotFound")
                        {
                            Console.WriteLine("Entered material does not exist!"
                                + processFailedString);
                        }
                        catch (Exception error) when (error.Message == "StorageAlreadyFull!")
                        {
                            Console.WriteLine("Storage is full!" +
                                processFailedString);
                        }
                        catch (Exception error) when (error.Message == "MinusAmount")
                        {
                            Console.WriteLine("Entered amount cannot be less than " +
                                "or equal to zero!" + processFailedString);
                        }
                    }
                    else if (order == "Add food")
                    {
                        string foodName = "";
                        int priceOfFood = 0;
                        try
                        {
                            Console.Write("Name: ");
                            foodName = Console.ReadLine();
                            for (int i = 0; i < food.foodList.Count; i++)
                            {
                                if (food.foodList[i].name == foodName)
                                {
                                    throw new Exception("EnteredFoodExists");
                                }
                            }
                            Console.Write("Price: ");
                            priceOfFood = int.Parse(Console.ReadLine());
                            if (priceOfFood <= 0)
                            {
                                throw new Exception("MinusPrice");
                            }

                            Console.Write("Materials: ");
                            string[] materialsList = Console.ReadLine().Split(", ");
                            for (int i = 0; i < materialsList.Length; i++)
                            {
                                bool flag = true;
                                string[] material_1_Name_2_Price = materialsList[i].Split();
                                for (int j = 0; j < Storage.storage.Count; j++)
                                {
                                    if (Storage.storage[j].materialName
                                        == material_1_Name_2_Price[0])
                                    {
                                        flag = false;
                                        if (Storage.storage[j].numberOfMaterial
                                            < int.Parse(material_1_Name_2_Price[1]))
                                        {
                                            throw new Exception("NotEnoughMaterial");
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                                if (flag)
                                {
                                    throw new Exception("MaterialNotFound");
                                }
                            }
                            food newFood = new food(foodName, priceOfFood);
                            for (int i = 0; i < materialsList.Length; i++)
                            {
                                string[] _1_materialName_2_count = materialsList[i].Split();

                                Material newMaterial = new Material(
                                    _1_materialName_2_count[0],
                                    int.Parse(_1_materialName_2_count[1]),
                                    newFood.name
                                    );
                                newFood.materialsOfThisFood.Add(newMaterial);
                            }
                            food.foodList.Add(newFood);
                            Console.WriteLine("Food added successfully!");
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Input is too long!" +
                                processFailedString);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Input is invalid!" +
                                processFailedString);
                        }
                        catch (Exception error) when (error.Message == "EnteredFoodExists")
                        {
                            Console.WriteLine("Entered food already exist!" +
                                processFailedString);
                        }
                        catch (Exception error) when (error.Message == "MinusPrice")
                        {
                            Console.WriteLine("Entered price cannot be less than or " +
                                "equal to zero!" + processFailedString);
                        }
                        catch (Exception error) when (error.Message == "NotEnoughMaterial")
                        {
                            Console.WriteLine("There are not enough materials " +
                                "in storage!" + processFailedString);
                        }
                        catch (Exception error) when (error.Message == "MaterialNotFound")
                        {
                            Console.WriteLine("Some of entered materials don't exist!" +
                                        processFailedString);
                        }
                    }
                    else if (order == "increase food")
                    {
                        string foodName = "";
                        int amount = 0;
                        try
                        {
                            int indexOfFood = 0;
                            bool flagToCheckWhetherFoodExistsOrNot = true;
                            bool flag_For_Material = true;
                            Console.Write("Name: ");
                            foodName = Console.ReadLine();
                            for (int i = 0; i < food.foodList.Count; i++)
                            {
                                if (food.foodList[i].name == foodName)
                                {
                                    indexOfFood = i;
                                    flagToCheckWhetherFoodExistsOrNot = false;
                                    break;
                                }
                            }
                            if (flagToCheckWhetherFoodExistsOrNot)
                            {
                                throw new Exception("FoodNotFound");
                            }
                            Console.Write("Amount: ");
                            amount = int.Parse(Console.ReadLine());
                            if (amount <= 0)
                            {
                                throw new Exception("MinusAmount");
                            }
                            for (int i = 0; i < food.foodList[indexOfFood]
                                .materialsOfThisFood.Count; i++)
                            {
                                for (int j = 0; j < Storage.storage.Count; j++)
                                {
                                    if (Storage.storage[j].materialName ==
                                        food.foodList[indexOfFood].
                                        materialsOfThisFood[i].materialName)
                                    {
                                        flag_For_Material = false;
                                        if (Storage.storage[j].numberOfMaterial <
                                            food.foodList[indexOfFood].
                                            materialsOfThisFood[j].howMany * amount)
                                        {
                                            throw new Exception("NotEnoughMaterial");
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    }
                                }
                                if (flag_For_Material)
                                {
                                    throw new Exception("MaterialNotFound");
                                }
                            }

                            food.foodList[indexOfFood].addFood(amount);

                            for (int i = 0; i < food.foodList[indexOfFood].
                                materialsOfThisFood.Count; i++)
                            {
                                for (int j = 0; j < Storage.storage.Count; j++)
                                {
                                    if (food.foodList[indexOfFood].materialsOfThisFood
                                        [i].materialName ==
                                        Storage.storage[j].materialName)
                                    {
                                        Storage.storage[j].numberOfMaterial -=
                                            (food.foodList[indexOfFood].materialsOfThisFood
                                            [i].howMany * amount
                                            );
                                    }
                                }
                            }
                            Console.WriteLine(amount + "\"" + foodName + "\" added!");
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Input is too big!" +
                                processFailedString);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Input is invalid!" +
                                processFailedString);
                        }
                        catch (Exception error) when (error.Message == "FoodNotFound")
                        {
                            Console.WriteLine("Entered food does not exist!" +
                                processFailedString);
                        }
                        catch (Exception error) when (error.Message == "MinusAmount")
                        {
                            Console.WriteLine("Entered amount cannot be equal to" +
                                " or less than zero!" + processFailedString);
                        }
                        catch (Exception error) when (error.Message == "NotEnoughMaterial")
                        {
                            Console.WriteLine("There are not enough materials" +
                                " to be consumed!" + processFailedString);
                        }
                        catch (Exception error) when (error.Message == "MaterialNotFound")
                        {
                            Console.WriteLine("Some of materials do not exist!"
                                + processFailedString);
                        }
                        catch (Exception error) when (error.Message == "NoMoreFood")
                        {
                            Console.WriteLine("No more food can be added!" +
                                processFailedString);
                        }
                    }
                    else if (order == "Add discount code")
                    {
                        try
                        {
                            int code = 0;
                            int price = 0;
                            Console.Write("Code: ");
                            code = int.Parse(Console.ReadLine());
                            for (int i = 0; i < Restaurant.discountCodesList.Count; i++)
                            {
                                if (Restaurant.discountCodesList[i].code == code)
                                {
                                    throw new Exception("DiscountCodeAlreadyExist");
                                }
                            }
                            Console.Write("Price: ");
                            price = int.Parse(Console.ReadLine());
                            if (price <= 0)
                            {
                                throw new Exception("MinusPrice");
                            }
                            discountCode newCode = new discountCode(code, price);
                            Restaurant.discountCodesList.Add(newCode);
                            Console.WriteLine("New discount code added!");
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Input is too big!" + processFailedString);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input" + processFailedString);
                        }
                        catch (Exception error) when (error.Message == "DiscountCodeAlreadyExist")
                        {
                            Console.WriteLine("This discount code already exists!"
                                + processFailedString);
                        }
                        catch (Exception error) when (error.Message == "MinusPrice")
                        {
                            Console.WriteLine("The price cannot be equal to or" +
                                "less than zero!" + processFailedString);
                        }
                    }
                    else if (order == "Add discount code to customer")
                    {
                        try
                        {
                            bool flag_CodeExistsOrNot = true;
                            bool flag_CustomerIDExistsOrNot = true;
                            int code = 0;
                            int customerID = 0;
                            int indexOfCustomer = 0;
                            Console.Write("Code: ");
                            code = int.Parse(Console.ReadLine());
                            for (int i = 0; i < Restaurant.discountCodesList.Count; i++)
                            {
                                if (code == Restaurant.discountCodesList[i].code)
                                {
                                    flag_CodeExistsOrNot = false;
                                    break;
                                }
                            }
                            if (flag_CodeExistsOrNot)
                            {
                                throw new Exception("CodeNotFound");
                            }
                            Console.Write("Customer ID: ");
                            customerID = int.Parse(Console.ReadLine());
                            for (int i = 0; i < Customer.groupOfCustomers.Count; i++)
                            {
                                if (Customer.groupOfCustomers[i].ID == customerID)
                                {
                                    if (Customer.groupOfCustomers[i].discountCode != 1)
                                    {
                                        throw new Exception("CustomerHasDiscountCode");
                                    }
                                    indexOfCustomer = i;
                                    flag_CustomerIDExistsOrNot = false;
                                    break;
                                }
                            }
                            if (flag_CustomerIDExistsOrNot)
                            {
                                throw new Exception("CustomerIDNotFound");
                            }
                            Customer.groupOfCustomers[indexOfCustomer].discountCode = code;
                            Console.WriteLine("Process finished successfully!");
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Input is too big!" +
                                processFailedString);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Input is invalid!" +
                                processFailedString);
                        }
                        catch (Exception error) when (error.Message == "CodeNotFound")
                        {
                            Console.WriteLine("Entered code does not exist!" +
                                processFailedString);
                        }
                        catch (Exception error) when (error.Message == "CustomerHasDiscountCode")
                        {
                            Console.WriteLine("This customer already has a discount" +
                                " code!" + processFailedString);
                        }
                        catch (Exception error) when (error.Message == "CustomerIDNotFound")
                        {
                            Console.WriteLine("Customer was not found!" +
                                processFailedString);
                        }
                    }
                    else if (order == "Print income")
                    {
                        Console.WriteLine(Restaurant.restaurantVallet);
                    }
                    else if (order == "Accept transaction")  //
                    {
                        try
                        {
                            bool flag = true;
                            int customerIndex = 0;
                            int IDIndex = 0;

                            Console.Write("ID: ");
                            int transactionID = int.Parse(Console.ReadLine());
                            for (int i = 0; i < Transaction.transactionsList.Count; i++)
                            {
                                if (Transaction.transactionsList[i].transactionID == transactionID)
                                {
                                    IDIndex = i;
                                    flag = false;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                throw new Exception("IDNotFound");
                            }
                            Restaurant.restaurantVallet += Transaction.transactionsList
                                [IDIndex].money;
                            Transaction.transactionsList[IDIndex].
                                transactionAcceptanse = true;
                        }
                        catch(Exception error)when(error.Message== "IDNotFound")
                        {
                            Console.WriteLine("ID was not found!"+processFailedString);
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Input is too big!" +
                                processFailedString);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input!" +
                                processFailedString);
                        }

                    }   //
                    else if (order == "Print transaction list")
                    {
                        try
                        {
                            for (int i = 0; i < Transaction.transactionsList.Count; i++)
                            {
                                Console.WriteLine(Transaction.transactionsList[i].transactionID
                                    + ": " + Transaction.transactionsList[i].customerID + ' ' +
                                    Transaction.transactionsList[i].moneyBeforeDiscount + ' ' +
                                    Transaction.transactionsList[i].discountID + ' ' +
                                    Transaction.transactionsList[i].money);
                            }
                        }
                        catch
                        {

                        }
                    }
                    else if (order == "Exit")
                    {
                        StreamWriter writer = new StreamWriter("User.txt");
                        for (int i = 0; i < Customer.groupOfCustomers.Count; i++)
                        {
                            writer.WriteLine(Customer.groupOfCustomers[i].name + ", " +
                                Customer.groupOfCustomers[i].ID + ", " +
                                Customer.groupOfCustomers[i].specialOffCount);
                        }
                        writer.Close();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Order not found!");
                    }
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Entered order is too long!" + prompt);
                }
            }
        }
    }
    class Restaurant
    {
        static public int restaurantVallet = 0;
        static public List<discountCode> discountCodesList = new List<discountCode>();

    }
    class Customer
    {
        static public List<Customer> groupOfCustomers = new List<Customer>();
        public string name;
        public int ID;
        public double customerVallet = 0;
        public int specialOffCount = 0;
        public int discountCode = 1;
        public bool doesCustomerHaveSpecialOfferToday = false;
        public Customer(string name, int ID, int specialOffCount)
        {
            this.name = name;
            this.ID = ID;
            this.specialOffCount = specialOffCount;
        }
        public Customer(string name, int ID)
        {
            this.name = name;
            this.ID = ID;
        }
        public void chargeVallet(double moneyToChargeVallet)
        {
            try
            {
                this.customerVallet += moneyToChargeVallet;
            }
            catch (OverflowException)
            {
                throw new Exception("ValletAlreadyFull");
            }
        }
    }
    class food
    {
        static public List<food> foodList = new List<food>();
        public string name = "";
        public int cost = 0;
        public int numberOfFood = 0;
        public List<Material> materialsOfThisFood = new List<Material>();
        public food(string foodName, int cost)
        {
            this.name = foodName;
            this.cost = cost;
        }
        public void addFood(int amount)
        {
            try
            {
                this.numberOfFood += amount;
            }
            catch (OverflowException)
            {
                throw new Exception("NoMoreFood");
            }
        }
    }
    class Storage
    {
        static public List<Storage> storage = new List<Storage>();
        public string materialName = "";
        public int numberOfMaterial = 0;
        public Storage(string materialName, int numberOfMaterial)
        {
            this.materialName = materialName;
            this.numberOfMaterial = numberOfMaterial;
        }
        public void increaseMaterial(int materialIndex, int amount)
        {
            try
            {
                storage[materialIndex].numberOfMaterial += amount;
            }
            catch (OverflowException)
            {
                throw new Exception("StorageAlreadyFull!");
            }
        }
    }
    class Transaction
    {
        static public List<Transaction> transactionsList = new List<Transaction>();
        static public int transactionIDForAll = 0;
        public int transactionID = 0;
        public int customerID;
        public int discountID = 1;
        public int money;
        public int moneyBeforeDiscount = 0;
        public int off = 0;
        public bool transactionAcceptanse = false;
        public Transaction(
            int customerID,
            string foodName,
            int howManyFood,
            int off = 0
            )
        {
            try
            {
                for (int i = 0; i < food.foodList.Count; i++)
                {
                    if (food.foodList[i].name == foodName)
                    {
                        this.moneyBeforeDiscount = food.foodList[i].cost * howManyFood;
                        this.money = food.foodList[i].cost * howManyFood;
                        break;
                    }
                }
                this.customerID = customerID;
                for (int i = 0; i < Customer.groupOfCustomers.Count; i++)
                {
                    if (Customer.groupOfCustomers[i].ID == customerID)
                    {
                        if (Customer.groupOfCustomers[i].discountCode != 1)
                        {
                            for (int j = 0; j < Restaurant.discountCodesList.Count; j++)
                            {
                                if (Restaurant.discountCodesList[j].code ==
                                    Customer.groupOfCustomers[i].discountCode)
                                {
                                    this.money -= Restaurant.discountCodesList[j].price;
                                    Customer.groupOfCustomers[i].discountCode = 1;
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
                this.money = ((100 - off) * this.money) / 100;
                
                this.transactionID = Transaction.transactionIDForAll+1;
                Transaction.transactionIDForAll += 1;
                this.off = off;
            }
            catch (OverflowException)
            {

            }
        }

    }
    class Material
    {
        public string materialName;
        public int howMany = 0;
        public string inWhichFoodItWillBeUsed;
        public Material(
            string materialName,
            int howMany,
            string inWhichFoodItWillBeUsed
            )
        {
            this.materialName = materialName;
            this.inWhichFoodItWillBeUsed = inWhichFoodItWillBeUsed;
            this.howMany = howMany;
        }
    }
    class discountCode
    {
        public int code;
        public int price;
        public discountCode(int code, int price)
        {
            this.code = code;
            this.price = price;
        }
    }
}

