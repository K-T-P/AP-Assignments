using System;
using System.Collections.Generic;

namespace tamrin_seri_3_soal_3
{
    enum customerType { casualCustomer, BadCustomer }
    enum accountType { shortPeriod = 10, longPeriod = 30, special = 50, expired }
    enum vamType { six = 6, twelve = 12 }
    class Program
    {
        static void Main()
        {
            List<Customer> customerGrp = new List<Customer>();
            List<Bank> bankGrp = new List<Bank>();
            string order = "";
            string prompt = "\nPlease enter again!";
            while (true)
            {
                Console.WriteLine("Menu :\nadd customer\tadd bank\tadd account" +
                    "\nget money\tpay loan\tupdate\tshow info\texit");
                try
                {
                    order = Console.ReadLine();
                    if (order == "add customer")
                    {
                        string customerName;
                        double storedMoney;
                        Console.WriteLine("Please enter the customer's name :");
                        while (true)
                        {
                            try
                            {
                                customerName = Console.ReadLine();
                                cancelProcess(customerName);
                                for (int i = 0; i < customerGrp.Count; i++)
                                {
                                    if (customerGrp[i].name == customerName)
                                    {
                                        throw new Exception("enteredNameAlreadyUsed");
                                    }
                                }
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Entered name is too long!" + prompt);
                            }
                            catch (Exception error) when (error.Message == "enteredNameAlreadyUsed")
                            {
                                Console.WriteLine("Entered name has already used!" + prompt);
                            }
                        }
                        Console.WriteLine("Please enter your money : ");
                        while (true)
                        {
                            try
                            {
                                string money = Console.ReadLine();
                                cancelProcess(money);
                                storedMoney = double.Parse(money);
                                if (storedMoney < 0)
                                {
                                    throw new Exception("minusMoney");
                                }
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Entered cash is too much!" + prompt);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Input is invalid!" + prompt);
                            }
                            catch (Exception error) when (error.Message == "minusMoney")
                            {
                                Console.WriteLine("Entered money cannot " +
                                    "be less that zero!" + prompt);
                            }
                        }
                        Customer customer = new Customer(customerName, storedMoney);
                        customerGrp.Add(customer);
                        Console.WriteLine("Customer added!");
                    }
                    else if (order == "add bank")
                    {
                        string bankName = "";
                        Console.WriteLine("Please enter the name of the bank!");
                        while (true)
                        {
                            try
                            {
                                bankName = Console.ReadLine();
                                cancelProcess(bankName);
                                for (int i = 0; i < bankGrp.Count; i++)
                                {
                                    if (bankGrp[i].bankName == bankName)
                                    {
                                        throw new Exception("EnteredNameAlreadyExist");
                                    }
                                }
                                Console.WriteLine("\"" + bankName + "\" addad!");
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Entered name is too long!" + prompt);
                            }
                            catch (Exception error) when (error.Message == "EnteredNameAlreadyExist")
                            {

                                Console.WriteLine("Entered name has already used!" + prompt);
                            }
                        }
                        Bank bank = new Bank(bankName);
                        bankGrp.Add(bank);
                    }
                    else if (order == "add account")
                    {
                        string customerName = "";
                        int indexOfCustomer = 0;
                        Console.WriteLine("Please enter the customer's name : ");
                        while (true)
                        {
                            try
                            {
                                bool flag = true;
                                customerName = Console.ReadLine();
                                cancelProcess(customerName);
                                for (int i = 0; i < customerGrp.Count; i++)
                                {
                                    if (customerGrp[i].name == customerName)
                                    {
                                        indexOfCustomer = i;
                                        flag = false;
                                    }
                                }
                                if (flag)
                                {
                                    Console.WriteLine("There is no customer " +
                                        "using this name!" + prompt);
                                    continue;
                                }
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Entered name is too long!" + prompt);
                            }
                        }

                        double money;
                        Console.WriteLine("Please enter your money :");
                        while (true)
                        {
                            try
                            {
                                string mmoney = Console.ReadLine();
                                cancelProcess(mmoney);
                                money = double.Parse(mmoney);
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Entered money is too much!");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("The format of the" +
                                    " Entered money is invalid");
                            }
                        }

                        string bankName = "";
                        Console.WriteLine("Please enter the name of the bank :");
                        int indexOfBank = 0;
                        while (true)
                        {
                            try
                            {
                                bool flag = true;
                                bankName = Console.ReadLine();
                                cancelProcess(bankName);
                                for (int i = 0; i < bankGrp.Count; i++)
                                {
                                    if (bankGrp[i].bankName == bankName)
                                    {
                                        indexOfBank = i;
                                        flag = false;
                                        break;
                                    }
                                }
                                if (flag)
                                {
                                    Console.WriteLine("Bank was not found!" + prompt);
                                    continue;
                                }
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Entered name is too long!");
                            }
                        }

                        Console.WriteLine("Please enter the type of the account :" +
                            "\nshortPeriod\tlongPeriod\tspecial");
                        accountType aType;
                        while (true)
                        {
                            try
                            {
                                aType = (accountType)Enum.Parse(typeof(accountType),
                                Console.ReadLine(),
                                true);
                                if (aType == accountType.expired)
                                {
                                    throw new InvalidCastException();
                                }
                                break;
                            }
                            catch (ArgumentException)
                            {
                                Console.WriteLine("Entered type is invalid." + prompt);
                            }
                        }

                        Console.WriteLine("Please enter the period : ");
                        int time = 0;
                        while (true)
                        {
                            try
                            {
                                string sTime = Console.ReadLine();
                                cancelProcess(sTime);
                                time = int.Parse(sTime);
                                if (time <= 0)
                                {
                                    Console.WriteLine("Input is invalid!" + prompt);
                                    continue;
                                }
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("The entered period is too much!" + prompt);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Entered time is invalid" + prompt);
                            }

                        }
                        try
                        {
                            bankGrp[indexOfBank].AddAccount(
                                customerName,
                                money,
                                customerGrp[indexOfCustomer],
                                aType,
                                time
                                );
                        }
                        catch (Exception error)
                        {
                            if (error.Message == "NotEnoughBudget")
                            {
                                Console.WriteLine("Not enough money is available in your storage!" +
                                                                    "\nProcess failed!");
                            }
                        }
                    }
                    else if (order == "get money")
                    {
                        Console.WriteLine("Please enter the customer's name :");
                        string customerName = "";
                        while (true)
                        {
                            try
                            {
                                bool flag = true;
                                customerName = Console.ReadLine();
                                cancelProcess(customerName);
                                for (int i = 0; i < customerGrp.Count; i++)
                                {
                                    if (customerGrp[i].name == customerName)
                                    {
                                        flag = false;
                                        break;
                                    }
                                }
                                if (flag)
                                {
                                    Console.WriteLine("Customer was not found!" + prompt);
                                    continue;
                                }
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Entered name is too long!");
                            }
                        }

                        Console.WriteLine("Please enter the number of the account :");
                        int accountNumber = 0;
                        while (true)
                        {
                            try
                            {
                                string receivedString = Console.ReadLine();
                                cancelProcess(receivedString);
                                accountNumber = int.Parse(receivedString);
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Entered number is too big!");
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Entered number is invalid!");
                            }
                        }
                        for (int i = 0; i < bankGrp.Count; i++)
                        {
                            try
                            {
                                bankGrp[i].getMoney(customerName, accountNumber);
                            }
                            catch
                            {
                                if (i == bankGrp.Count - 1)
                                {
                                    Console.WriteLine("No bank account exists with " +
                                        "this number");
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                    else if (order == "pay loan")
                    {
                        Console.WriteLine("Please enter the amount of the loan :");
                        double loanAmount = 0;
                        while (true)
                        {
                            try
                            {
                                string loanAmountString = Console.ReadLine();
                                cancelProcess(loanAmountString);
                                loanAmount = double.Parse(loanAmountString);
                                if (loanAmount < 0)
                                {
                                    Console.WriteLine("Loan amount cannot be less " +
                                        "than zero!" + prompt);
                                    continue;
                                }
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Entered number is too big!" + prompt);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Input is invalid!");
                            }
                        }

                        Console.WriteLine("Please enter the benefit :");
                        double benefit = 0;
                        while (true)
                        {
                            try
                            {
                                string benefitString = Console.ReadLine();
                                cancelProcess(benefitString);
                                benefit = double.Parse(benefitString);
                                if (benefit < 0)
                                {
                                    Console.WriteLine("Benefit cannot be less than " +
                                        "zero!" + prompt);
                                    continue;
                                }
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Entered benefit is too much!" + prompt);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Entered benefit is invalid!" + prompt);
                            }
                        }

                        Console.WriteLine("Please enter " +
                            "type of your payment :\nsix\ttwelve");
                        vamType vType;
                        while (true)
                        {
                            try
                            {
                                string vTypeString = Console.ReadLine();
                                cancelProcess(vTypeString);
                                vType = (vamType)Enum.Parse(
                                    typeof(vamType),
                                    vTypeString,
                                    true
                                    );
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Entered payment is invalid!" + prompt);
                                continue;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Entered payment is invalid!" + prompt);
                                continue;
                            }
                        }

                        Console.WriteLine("Please enter how long you want your loan" +
                            "be :");
                        int loanTime = 0;
                        while (true)
                        {
                            try
                            {
                                string loanTimeString = Console.ReadLine();
                                cancelProcess(loanTimeString);
                                loanTime = int.Parse(loanTimeString);
                                if (loanTime <= 0)
                                {
                                    Console.WriteLine("The time of the loan " +
                                        "must be positive !" + prompt);
                                    continue;
                                }
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Entered time is too big!" + prompt);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input!" + prompt);
                            }
                        }

                        Console.WriteLine("Please enter the name of the bank :");
                        string bankName = "";
                        int indexOfBank = 0;
                        while (true)
                        {
                            try
                            {
                                bool flag = true;
                                bankName = Console.ReadLine();
                                cancelProcess(bankName);
                                for (int i = 0; i < bankGrp.Count; i++)
                                {
                                    if (bankGrp[i].bankName == bankName)
                                    {
                                        flag = false;
                                        indexOfBank = i;
                                        break;
                                    }
                                }
                                if (flag)
                                {
                                    Console.WriteLine("\"" + bankName + "\"was not found!" + prompt);
                                    continue;
                                }
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Entered name is too long!" + prompt);
                            }
                        }

                        Console.WriteLine("Please enter the name of the person :");
                        int indexOfPerson = 0;
                        string customerName = "";
                        while (true)
                        {
                            try
                            {
                                bool flag = true;
                                customerName = Console.ReadLine();
                                cancelProcess(customerName);
                                for (int i = 0; i < customerGrp.Count; i++)
                                {
                                    if (customerGrp[i].name == customerName)
                                    {
                                        indexOfPerson = i;
                                        flag = false;
                                        break;
                                    }
                                }
                                if (flag)
                                {
                                    Console.WriteLine("\"" + customerName + "\"was not " +
                                        "found!" + prompt);
                                    continue;
                                }
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Entered name is too long!" + prompt);
                            }
                        }

                        try
                        {
                            bankGrp[indexOfBank].payLoan(
                                customerName,
                                loanAmount,
                                benefit,
                                loanTime,
                                vType, customerGrp[indexOfPerson]
                                );
                        }
                        catch (Exception error)
                        {
                            if (error.Message == "LoanPaymentNotAllowed")
                            {
                                Console.WriteLine("This person is not " +
                                    "allowed to receive loan!");
                            }
                        }
                    }
                    else if (order == "update")
                    {
                        int timeSpan = 0;
                        while (true)
                        {
                            try
                            {
                                string timeSpanString = Console.ReadLine();
                                cancelProcess(timeSpanString);
                                timeSpan = int.Parse(timeSpanString);
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Entered time is too big!" + prompt);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input!" + prompt);
                            }
                        }
                        for (int i = 0; i < bankGrp.Count; i++)
                        {
                            bankGrp[i].updateSystem(timeSpan);
                        }
                    }
                    else if (order == "show info")
                    {
                        Console.WriteLine("enter customer's name: ");
                        string customerName = "";
                        while (true)
                        {
                            try
                            {
                                bool flag = true;
                                customerName = Console.ReadLine();
                                cancelProcess(customerName);
                                for (int i = 0; i < customerGrp.Count; i++)
                                {
                                    if (customerGrp[i].name == customerName)
                                    {
                                        flag = false;
                                    }
                                }
                                if (flag)
                                {
                                    Console.WriteLine("\"" + customerName + "\"" +
                                        " was not found!" + prompt);
                                    continue;
                                }
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Entered name is too long!" + prompt);
                            }
                        }

                        for (int i = 0; i < bankGrp.Count; i++)
                        {
                            bankGrp[i].showInfo(customerName);
                        }
                    }
                    else if (order == "exit") { break; }
                    else
                    {
                        throw new Exception("wrongOrder");
                    }
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Entered order is too long!");
                }
                catch (Exception error)
                {
                    if (error.Message == "wrongOrder")
                    {
                        Console.WriteLine("\"" + order + "\" does not exist!\n");
                    }
                    else if (error.Message == "CancelProcess")
                    {
                        Console.WriteLine("Process cancelled.");
                    }
                    //else if (error.GetType() == OverflowException) 
                    //{
                    //}
                }
            }
        }
        static void cancelProcess(string order)
        {
            if (order == "cancel")
            {
                throw new Exception("CancelProcess");
            }
        }
    }
    class Bank
    {
        public string bankName;
        private List<specializedCustomer> bankCustomers = new List<specializedCustomer>();
        public Bank(string bankName)
        {
            this.bankName = bankName;
        }
        public void showInfo(string customerName)
        {
            int indexOfCustomer = -1;
            for (int i = 0; i < bankCustomers.Count; i++)
            {
                if (bankCustomers[i].mainCustomer.name == customerName)
                {
                    bankCustomers[i].showInfo();
                }
            }
        }
        public void updateSystem(int timeSpan)
        {
            for (int i = 0; i < bankCustomers.Count; i++)
            {
                bankCustomers[i].updateSystem(timeSpan);
            }
        }
        public void payLoan(
            string customerName,
            double loanAmount,
            double benefit,
            int loanTime,
            vamType vType,
            Customer customer
            )
        {
            if (customer.customerType == customerType.BadCustomer)
            {
                throw new Exception("LoanPaymentNotAllowed");
            }
            int customerIndex = -1;
            for (int i = 0; i < bankCustomers.Count; i++)
            {
                if (bankCustomers[i].mainCustomer.name == customerName)
                {
                    customerIndex = i;
                    break;
                }
            }
            if (customerIndex == -1)
            {
                specializedCustomer newCustomer = new specializedCustomer(customer);
                bankCustomers.Add(newCustomer);
            }
            Vam vam = new Vam(customerName, loanAmount, benefit, loanTime, vType, customer);
            bankCustomers[customerIndex].vam.Add(vam);
            Console.WriteLine("Loan paid successfully!");
        }
        public void AddAccount(
            string customerName,
            double money,
            Customer customer,
            accountType aType,
            int time
            )
        {
            int index = -1;
            for (int i = 0; i < bankCustomers.Count; i++)
            {
                if (bankCustomers[i].mainCustomer.name == customer.name)
                {
                    index = i;
                }
            }
            if (index == -1)
            {
                specializedCustomer newCustomer = new specializedCustomer(customer);
                bankCustomers.Add(newCustomer);
                index = bankCustomers.Count - 1;
            }

            bankAccount account = new bankAccount(aType, money, time, customer);

            bankCustomers[index].account.Add(account);
        }
        public void getMoney(string customerName, int accountNumber)
        {
            bool flag = true;
            for (int i = 0; i < bankCustomers.Count; i++)
            {
                if (bankCustomers[i].mainCustomer.name == customerName)
                {
                    for (int j = 0; j < bankCustomers[i].account.Count; j++)
                    {
                        if (bankCustomers[i].account[j].getAccountNumber() == accountNumber)
                        {
                            bankCustomers[i].account[j].returnMoney();
                            flag = false;
                        }
                    }
                }
            }
            if (flag)
            {
                throw new Exception("NotFound");
            }
        }
    }
    class specializedCustomer
    {
        public List<bankAccount> account = new List<bankAccount>();
        public List<Vam> vam = new List<Vam>();
        public Customer mainCustomer;
        public void updateSystem(int timeSpan)
        {
            for (int i = 0; i < vam.Count; i++)
            {
                vam[i].updateSystem(timeSpan);
            }
            for (int i = 0; i < account.Count; i++)
            {
                account[i].updateSystem(timeSpan);
            }
        }
        public specializedCustomer(Customer mainCustomer)
        {
            this.mainCustomer = mainCustomer;
        }
        public void showInfo()
        {
            for (int i = 0; i < account.Count; i++)
            {
                //for (int j = 0; j < 3; j++)
                //{
                    Console.WriteLine("Account number : " + account[i].showAccountNumber());
              //  }
                //Console.WriteLine();
            }
        }
    }
    class Customer
    {
        public string name;
        public double storage;
        public customerType customerType = customerType.casualCustomer;
        private int minusPoint = 0;
        private int numberOfAccount = 0;

        public Customer(string name, double storage)
        {
            this.name = name;
            this.storage = storage;
        }
        public void showInfo()
        {
            Console.WriteLine("Stored money : " + storage);
            Console.WriteLine("Customer type : " + customerType.ToString());
            Console.WriteLine("Minus point : " + minusPoint);
        }
        public double getMoney(double cash)
        {
            if (storage < cash)
            {
                throw new Exception("NotEnoughBudget");
            }
            else
            {
                storage -= cash;
                return cash;
            }
        }
        public int getNumberOfAccount()
        {
            return numberOfAccount;
        }
        public void increaseNumberOfAccount()
        {
            numberOfAccount += 1;
        }
        public void takeMoney(double cash)
        {
            storage += cash;
        }
    }
    class bankAccount
    {
        Customer customer;
        accountType accountType;
        private int accountNumber;
        private double _benefitPercentage;
        private int accountTime;
        private double storedCash;
        public void returnMoney()
        {
            if (accountTime <= 0)
            {
                _benefitPercentage += 1;
                try
                {
                    customer.takeMoney((storedCash * _benefitPercentage) / 100.0);
                }
                catch
                {
                    customer.takeMoney(storedCash);
                }
                finally
                {
                    accountType = accountType.expired;
                }
            }
            else
            {
                try
                {
                    customer.takeMoney(storedCash);
                }
                catch
                {
                    customer.takeMoney(0);
                }
                finally
                {
                    accountType = accountType.expired;
                }
            }
        }
        public int getAccountNumber()
        {
            if (accountType == accountType.expired)
            {
                throw new Exception("ExpiredAccount");
            }
            else
            {
                return accountNumber;
            }
        }
        public void updateSystem(int timeSpan)
        {
            this.accountTime -= timeSpan;
            if (accountTime <= 0)
            {
                returnMoney();
            }
        }
        public bankAccount(accountType accountType,
            double cash,
            int accountTime,
            Customer customer)
        {
            _benefitPercentage = (int)accountType;
            //if (accountType == accountType.shortPeriod)
            //{
            //    _benefitPercentage = 10;
            //}
            //else if (accountType == accountType.longPeriod)
            //{
            //    _benefitPercentage = 30;
            //}
            //else
            //{
            //    _benefitPercentage = 50;
            //}
            accountNumber = customer.getNumberOfAccount() + 1;
            storedCash = customer.getMoney(cash);
            this.accountTime = accountTime;
            this.customer = customer;
            customer.increaseNumberOfAccount();
        }
        public int showAccountNumber()
        {
            return accountNumber;
        }

    }
    class Vam
    {
        vamType vamType;
        Customer customer;
        private double benefitPercentage;
        private double vamAmount;
        private int vamTime;
        private int timeBetweenLoans;
        private string customerName;
        public void updateSystem(int timeSpan)
        {
            if (timeBetweenLoans > timeSpan)
            {

            }
        }
        public Vam(
            string customerName,
            double vamAmount,
            double benefitPercentage,
            int vamTime,
            vamType vType,
            Customer customer
            )
        {
            this.customerName = customerName;
            this.benefitPercentage = benefitPercentage;
            this.vamTime = vamTime;
            this.vamAmount = vamAmount;
            this.vamType = vType;
            timeBetweenLoans = vamTime / (int)vType;
        }
    }
}

