using System;
using System.Collections.Generic;
using System.IO;

namespace tamrin_seri_3_soal_2_2
{
    enum AnimalType { Monkey = 10, Lion, Elephant, Bear, Tiger, Giraffe }
    class Program
    {
        static void Main()
        {
            try
            {
                string tryAgain = "\nPlease try again!";
                Console.WriteLine("Enter the number of animals to save:");
                int numberOfAllAnimals;
                while (true)
                {
                    try
                    {
                        numberOfAllAnimals = int.Parse(Console.ReadLine());
                        if (numberOfAllAnimals <= 0)
                        {
                            Console.WriteLine("Invalid input!" + tryAgain);
                            continue;
                        }
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Input is invalid!" + tryAgain);
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Input is too big!" + tryAgain);
                    }
                }
                for (int i = 0; i < numberOfAllAnimals; i++)
                {
                    while (true)
                    {
                        try
                        {
                            Console.WriteLine("Enter the type:");
                            AnimalType animalType = (AnimalType)
                                Enum.Parse(typeof(AnimalType),
                                Console.ReadLine(),
                                true)
                                ;
                            Console.WriteLine("Enter the Name:");
                            string animalName = Console.ReadLine();
                            Zoo.IsValidName(animalName);

                            Console.WriteLine("Enter the location:");
                            string location = Console.ReadLine();

                            Console.WriteLine("Enter the food:");
                            string food = Console.ReadLine();

                            Zoo newAnimal = new Zoo(animalName,
                                food,
                                location,
                                animalType
                                );
                            Zoo.groupOfAnimals.Add(newAnimal);
                            newAnimal.SaveToFile();
                            break;

                        }
                        catch (System.ArgumentException)
                        {
                            Console.WriteLine("Invalid input!"+tryAgain);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input" + tryAgain);
                        }
                        catch (OverflowException)
                        {
                            Console.WriteLine("Input is too big!" + tryAgain);
                        }
                        catch (Exception error) when (error.Message == "NameIsAlreadyUsed")
                        {
                            Console.WriteLine("This name was used!" + tryAgain);
                        }
                    }
                }
                Console.WriteLine("It’s time to change information.");
                int howManyAnimalWibbBeChanged = 0;
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Enter the number of animals you" +
                            " want to change their information:");
                        howManyAnimalWibbBeChanged = int.Parse(Console.ReadLine());
                        if (howManyAnimalWibbBeChanged > numberOfAllAnimals)
                        {
                            throw new Exception("NumberIsMoreThanAnimals");
                        }
                        break;
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Input is too big!" + tryAgain);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Invalid input!" + tryAgain);
                    }
                    catch (Exception error) when (error.Message == "NumberIsMoreThanAnimals")
                    {
                        Console.WriteLine("Wrong! Total number of animals in the zoo is " +
                            "{0}.Enter a smaller number:", numberOfAllAnimals);
                    }
                }
                for (int i = 0; i < howManyAnimalWibbBeChanged; i++)
                {
                    try
                    {
                        bool flag = true;
                        Console.WriteLine("Enter the name:");
                        string animalName = Console.ReadLine();
                        for (int j = 0; j < Zoo.groupOfAnimals.Count; j++)
                        {
                            if (Zoo.groupOfAnimals[j].theNameOfTheAnimal == animalName)
                            {
                                flag = false;
                                break;
                            }
                        }
                        if (flag)
                        {
                            throw new Exception("AnimalNotFound");
                        }
                        string food = "";
                        string location = "";
                        while (true)
                        {
                            Console.WriteLine("Enter the new food: (if you do not" +
                                " want to change it press enter)");
                            food = Console.ReadLine();
                            Console.WriteLine("Enter the new location: " +
                                "(if you do not want to change it press enter)");
                            location = Console.ReadLine();
                            if ((food == "") && (location == ""))
                            {
                                Console.WriteLine("None of the values has not entered." +
                                    " Enter the values again:");
                                continue;
                            }
                            break;
                        }
                        if (food == "")
                        {
                            Zoo.change(animalName, location, 1);
                        }
                        else if (location == "")
                        {
                            Zoo.change(animalName, food);
                        }
                        else
                        {
                            Zoo.change(animalName, food, location);
                        }
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Input is too big!");
                    }
                    catch (Exception error) when (error.Message == "AnimalNotFound")
                    {
                        Console.WriteLine("Entered animal does not exist!" + tryAgain);
                    }
                }
                Zoo.AllInfo();
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("There are not enough memory on the device!" +
                    "\nProgram stopped!");
            }
        }
    }
    class Zoo
    {
        static public List<Zoo> groupOfAnimals = new List<Zoo>();
        static private int howManyAnimalsAreCreated = 0;
        private int animalID;
        AnimalType animalType;
        public string theNameOfTheAnimal;
        string theLocationOfTheAnimal;
        string food;
        public Zoo(
            string name,
            string food,
            string location,
            AnimalType type
            )
        {
            try
            {
                this.theNameOfTheAnimal = name;
                this.food = food;
                this.theLocationOfTheAnimal = location;
                this.animalType = type;
                Zoo.howManyAnimalsAreCreated++;
                this.animalID = Zoo.howManyAnimalsAreCreated;
            }
            catch
            {

            }
        }
        static public void IsValidName(string givenNameWhichWillBeTested)
        {
            for (int i = 0; i < Zoo.groupOfAnimals.Count; i++)
            {
                if (Zoo.groupOfAnimals[i].theNameOfTheAnimal ==
                    givenNameWhichWillBeTested)
                {
                    throw new Exception("NameIsAlreadyUsed");
                }
            }
        }
        public void SaveToFile()
        {
            try
            {
                StreamWriter writer = null;
                try
                {
                    writer = new StreamWriter(
                        ((int)animalType).ToString() + ".txt", append: true);
                    
                    writer.WriteLine("Animal ID: " + animalID);
                    writer.WriteLine("Animal name: " + theNameOfTheAnimal);
                    writer.WriteLine("Animal location: " + theLocationOfTheAnimal);
                    writer.WriteLine("Animal food: " + food.Replace(',', '-'));
                    
                    writer.WriteLine();
                    writer.Close();
                    writer = null;
                }

                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("There are not enough memory on the device!");
            }
        }
        static public void change(
            string animalName,
            string animalLocation,
            int number
            )
        {
            StreamReader reader = null;
            StreamWriter writer = null;
            try
            {
                bool flag = true;
                int indexOfAnimal = 0;
                for (int i = 0; i < Zoo.groupOfAnimals.Count; i++)
                {
                    if (Zoo.groupOfAnimals[i].theNameOfTheAnimal == animalName)
                    {
                        indexOfAnimal = i;
                        Zoo.groupOfAnimals[i].theLocationOfTheAnimal = animalLocation;
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    throw new Exception("AnimalNotFound");
                }

                reader = new StreamReader(((int)Zoo.groupOfAnimals[indexOfAnimal]
                    .animalType).ToString() + ".txt");
                List<string> listGrp = new List<string>();
                bool flag_1 = false;
                for (int i = 0; reader.EndOfStream == false; i++)
                {
                    string line = reader.ReadLine();
                    if (line.Contains(animalName))
                    {
                        flag_1 = true;
                    }
                    if (line.Contains("Animal location") && flag_1)
                    {
                        line = "Animal location: " + animalLocation;
                        flag_1 = false;
                    }
                    listGrp.Add(line);
                }
                reader.Close();
                reader = null;
                writer = new StreamWriter(((int)Zoo.groupOfAnimals[indexOfAnimal]
                    .animalType).ToString() + ".txt");
                for (int i = 0; i < listGrp.Count; i++)
                {
                    writer.WriteLine(listGrp[i]);
                }
                writer.Close();
                writer = null;
                Console.WriteLine("The location of the animal " +
                    "was changed successfully!");
            }
            catch (Exception error) when (error.Message == "AnimalNotFound")
            {
                Console.WriteLine("Entered animal does not exist!");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("There are not enough memory on the device!");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
        static public void change(
            string animalName,
            string animalFood
            )
        {
            StreamReader reader = null;
            StreamWriter writer = null;
            try
            {
                int indexOfAnimal = 0;
                bool flag = true;
                for (int i = 0; i < Zoo.groupOfAnimals.Count; i++)
                {
                    if (Zoo.groupOfAnimals[i].theNameOfTheAnimal == animalName)
                    {
                        indexOfAnimal = i;
                        Zoo.groupOfAnimals[i].food = animalFood;
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    throw new Exception("AnimalNotFound");
                }

                reader = new StreamReader
                    (((int)Zoo.groupOfAnimals[indexOfAnimal].animalType).ToString()
                    + ".txt");
                bool flag_1 = false;
                List<string> listGrp = new List<string>();
                for (int i = 0; reader.EndOfStream == false; i++)
                {
                    string line = reader.ReadLine();
                    if (line.Contains(animalName))
                    {
                        flag_1 = true;
                    }
                    if (line.Contains("Animal food") && flag_1)
                    {
                        line = line.Replace(',', '-');
                    }
                    listGrp.Add(line);
                }
                reader.Close();
                reader = null;
                writer = new StreamWriter(((int)Zoo.groupOfAnimals[indexOfAnimal].animalType).ToString()
                    + ".txt");
                for (int i = 0; i < listGrp.Count; i++)
                {
                    writer.WriteLine(listGrp[i]);
                }
                writer.Close();
                writer = null;
                Console.WriteLine("The food of the animal was changed successfully!");
            }
            catch (Exception error) when (error.Message == "AnimalNotFound")
            {
                Console.WriteLine("Entered animal does not exist!");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("There not enough memory on the device!");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
        static public void change(
            string animalName,
            string animalFood,
            string animalLocation
            )
        {
            StreamReader reader = null;
            StreamWriter writer = null;
            try
            {
                bool flag = true;
                bool flag_1 = false;
                int indexOfAnimal = 0;
                for (int i = 0; i < Zoo.groupOfAnimals.Count; i++)
                {
                    if (Zoo.groupOfAnimals[i].theNameOfTheAnimal == animalName)
                    {
                        indexOfAnimal = i;
                        flag = false;
                        Zoo.groupOfAnimals[i].food = animalFood;
                        Zoo.groupOfAnimals[i].theLocationOfTheAnimal = animalLocation;
                        break;
                    }
                }
                if (flag)
                {
                    throw new Exception("AnimalNotFound");
                }
                reader = new StreamReader(((int)Zoo.groupOfAnimals[indexOfAnimal].animalType).ToString()
                    + ".txt");
                List<string> lineGrp = new List<string>();
                bool endFlag_1 = false;
                bool endFlag_2 = false;
                for (int i = 0; reader.EndOfStream; i++)
                {
                    string line = reader.ReadLine();
                    if (line.Contains(animalName))
                    {
                        flag_1 = true;
                    }
                    if (line.Contains("Animal location") && flag_1)
                    {
                        line=line.Replace(Zoo.groupOfAnimals[indexOfAnimal].
                            theLocationOfTheAnimal, animalLocation);
                        endFlag_1 = true;
                    }
                    if (line.Contains("Animal food") && flag_1)
                    {
                        line = line.Replace(Zoo.groupOfAnimals[indexOfAnimal]
                            .food, animalFood);
                        endFlag_2 = true;
                    }
                    if (endFlag_2 && endFlag_1)
                    {
                        flag_1 = false;
                        endFlag_1 = false;
                        endFlag_2 = false;
                    }
                    lineGrp.Add(line);
                }
                reader.Close();
                reader = null;
                writer = new StreamWriter(((int)Zoo.groupOfAnimals[indexOfAnimal].animalType).ToString()
                    + ".txt");
                for(int i = 0; i < lineGrp.Count; i++)
                {
                    writer.WriteLine(lineGrp[i]);
                }
                writer.Close();
                writer = null;
                Console.WriteLine("The food and the location of the animal were changed" +
                    " successfully!");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("There are not enough memory on the device!");
            }
            catch (Exception error) when (error.Message == "AnimalNotFound")
            {
                Console.WriteLine("Entered animal does not exist!");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }
        static public void AllInfo()
        {
            try
            {
                StreamWriter writer = null;
                try
                {
                    writer = new StreamWriter("AllInfo.txt");
                    int num_Monkey = 0;
                    int num_Lion = 0;
                    int num_Elephant = 0;
                    int num_Bear = 0;
                    int num_Tiger = 0;
                    int num_Giraffe = 0;
                    int numOfAllAnimals = 0;
                    int vowel_Monkey = 0;
                    int vowel_Lion = 0;
                    int vowel_Elephant = 0;
                    int vowel_Bear = 0;
                    int vowel_Tiger = 0;
                    int vowel_Giraffe = 0;
                    for (int i = 0; i < Zoo.groupOfAnimals.Count; i++)
                    {
                        switch (Zoo.groupOfAnimals[i].animalType)
                        {
                            case AnimalType.Monkey: num_Monkey++; break;
                            case AnimalType.Lion: num_Lion++; break;
                            case AnimalType.Elephant: num_Elephant++; break;
                            case AnimalType.Bear: num_Bear++; break;
                            case AnimalType.Tiger: num_Tiger++; break;
                            case AnimalType.Giraffe: num_Giraffe++; break;
                        }
                        for (int j = 0; j < Zoo.groupOfAnimals[i].theNameOfTheAnimal.Length; j++)
                        {

                            if (Zoo.groupOfAnimals[i].animalType == AnimalType.Monkey)
                            {
                                switch ((int)Zoo.groupOfAnimals[i].theNameOfTheAnimal[j])
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
                                    case 85: vowel_Monkey++; break;
                                    default: break;
                                }
                            }
                            else if (Zoo.groupOfAnimals[i].animalType == AnimalType.Bear)
                            {
                                switch ((int)Zoo.groupOfAnimals[i].theNameOfTheAnimal[j])
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
                                    case 85: vowel_Bear++; break;
                                    default: break;
                                }
                            }
                            else if (Zoo.groupOfAnimals[i].animalType == AnimalType.Lion)
                            {
                                switch ((int)Zoo.groupOfAnimals[i].theNameOfTheAnimal[j])
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
                                    case 85: vowel_Lion++; break;
                                    default: break;
                                }
                            }
                            else if (Zoo.groupOfAnimals[i].animalType == AnimalType.Elephant)
                            {
                                switch ((int)Zoo.groupOfAnimals[i].theNameOfTheAnimal[j])
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
                                    case 85: vowel_Elephant++; break;
                                    default: break;
                                }
                            }
                            else if (Zoo.groupOfAnimals[i].animalType == AnimalType.Giraffe)
                            {
                                switch ((int)Zoo.groupOfAnimals[i].theNameOfTheAnimal[j])
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
                                    case 85: vowel_Giraffe++; break;
                                    default: break;
                                }
                            }
                            else if (Zoo.groupOfAnimals[i].animalType == AnimalType.Tiger)
                            {
                                switch ((int)Zoo.groupOfAnimals[i].theNameOfTheAnimal[j])
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
                                    case 85: vowel_Tiger++; break;
                                    default: break;
                                }
                            }
                        }
                    }
                    numOfAllAnimals = Zoo.groupOfAnimals.Count;
                    writer.WriteLine(
                        "Number of " +
                        AnimalType.Monkey.ToString() +
                        " : " +
                        num_Monkey);

                    writer.WriteLine(
                        "Number of " +
                        AnimalType.Lion.ToString() +
                        " : " +
                        num_Lion);

                    writer.WriteLine(
                        "Number of " +
                        AnimalType.Bear.ToString() +
                        " : " +
                        num_Bear);

                    writer.WriteLine(
                        "Number of " +
                        AnimalType.Elephant.ToString() +
                        " : " +
                        num_Elephant);

                    writer.WriteLine(
                        "Number of " +
                        AnimalType.Tiger.ToString() +
                        " : " +
                        num_Tiger);

                    writer.WriteLine(
                        "Number of " +
                        AnimalType.Giraffe.ToString() +
                        " : " +
                        num_Giraffe);

                    writer.WriteLine(
                        "Number of vowel sounds in " +
                        AnimalType.Monkey.ToString() +
                        " : " +
                        vowel_Monkey
                        );
                    writer.WriteLine(
                        "Number of vowel sounds in " +
                        AnimalType.Lion.ToString() +
                        " : " +
                        vowel_Lion
                        );
                    writer.WriteLine(
                        "Number of vowel sounds in " +
                        AnimalType.Giraffe.ToString() +
                        " : " +
                        vowel_Giraffe
                        );
                    writer.WriteLine(
                        "Number of vowel sounds in " +
                        AnimalType.Tiger.ToString() +
                        " : " +
                        vowel_Tiger
                        ); writer.WriteLine(
                         "Number of vowel sounds in " +
                         AnimalType.Elephant.ToString() +
                         " : " +
                         vowel_Elephant
                         );
                    writer.WriteLine(
                        "Number of vowel sounds in " +
                        AnimalType.Bear.ToString() +
                        " : " +
                        vowel_Bear
                        );
                    writer.WriteLine("Number of all animals : " + numOfAllAnimals);
                    writer.Close();
                }
                catch (OverflowException)
                {
                    Console.WriteLine("There are too much information to be analized!" +
                        "Analysis process failed!");
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("There are not enough memory on the device!" +
                    "\nProcess failed!");
            }
        }
    }
}
