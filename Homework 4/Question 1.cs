using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace tamrin_seri_4_soal_1
{
    enum Currency { franc = 29, dirham = 7, pound = 35, Dollar = 30 }
    enum Airline { Pars = 5, Saha = 4, Kish = 3, Turkish = 2, Mahan = 1 }
    class Program
    {
        static void Main()
        {
            try
            {
                StreamReader reader = null;
                try
                {
                    //country,province,city,street,plate
                    reader = new StreamReader("place.txt");
                    for (int i = 0; reader.EndOfStream == false; i++)
                    {
                        string[] line = reader.ReadLine().Split(',');
                        Location newLocation = new Location(line[0], line[1], line[2], line[3], line[4]);
                        Location.locationList.Add(newLocation);
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Warning! \"place.txt\" was not found!.");
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
                try
                {
                    reader = new StreamReader("time.txt");
                    for (int i = 0; reader.EndOfStream == false; i++)
                    {
                        //year,month,day,hour,minute
                        string[] time = reader.ReadLine().Split(',');
                        Time newTime = new Time(int.Parse(time[0]), int.Parse(time[1]), int.Parse(time[2]), int.Parse(time[3]), int.Parse(time[4]));
                        Time.timeList.Add(newTime);
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Warning! \"time.txt\" was not found!");
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
                try
                {
                    //rows,chair,airline
                    reader = new StreamReader("airplane.txt");
                    for (int i = 0; reader.EndOfStream == false; i++)
                    {
                        string[] line = reader.ReadLine().Split(',');
                        Airplane newAirplane = new Airplane(int.Parse(line[0]), int.Parse(line[1]), (Airline)Enum.Parse(typeof(Airline), line[2], true));
                        Airplane.airplaneList.Add(newAirplane);
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Warning! \"airplane.txt\" was not found!.");
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
                try
                {
                    reader = new StreamReader("flight.txt");
                    for (int i = 0; reader.EndOfStream == false; i++)
                    {
                        reader.ReadLine();
                        string flightNumber = reader.ReadLine();

                        string[] location = reader.ReadLine().Split(',');
                        Location newLocation = new Location(location[0], location[1], location[2], location[3], location[4]);

                        string[] destination = reader.ReadLine().Split(',');
                        Location newDestination = new Location(destination[0], destination[1], destination[2], destination[3], destination[4]);

                        string[] landingTime = reader.ReadLine().Split(',');
                        Time newLandingTime = new Time(
                            int.Parse(landingTime[0]), int.Parse(landingTime[1]), int.Parse(landingTime[2]), int.Parse(landingTime[3]), int.Parse(landingTime[4]));

                        string[] flightTime = reader.ReadLine().Split(',');
                        Time newFlightTime = new Time
                            (int.Parse(flightTime[0]), int.Parse(flightTime[1]), int.Parse(flightTime[2]), int.Parse(flightTime[3]), int.Parse(flightTime[4]));

                        string ID = reader.ReadLine();

                        string[] airplane = reader.ReadLine().Split(',');
                        Airplane newAirplane = new Airplane(int.Parse(airplane[0]), int.Parse(airplane[1]), (Airline)Enum.Parse(typeof(Airline), airplane[2], true));

                        string priceInToman = reader.ReadLine();
                        PriceInToman newPrice = new PriceInToman(double.Parse(priceInToman));
                        Flight newFlight = new Flight(flightNumber, newLocation, newDestination, newLandingTime, newFlightTime, int.Parse(ID), newAirplane, newPrice);
                        Flight.flightList.Add(newFlight);
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.WriteLine("Warning! \"flight.txt\" was not found!.");
                }
                finally
                {
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }
                string order;
                while (true)
                {
                    try
                    {
                        Console.WriteLine("\nMenu:\nAdd_flight\t\tSort_flight" +
                            "\nChange_data_flight\texit");
                        order = Console.ReadLine();
                        if (order == "Add_flight")
                        {
                            Add_flight();
                        }
                        else if (order == "Sort_flight")
                        {
                            Sort_flight();
                        }
                        else if (order == "Change_data_flight")
                        {
                            Change_data_flight();
                        }
                        else if (order == "exit")
                        {
                            Console.WriteLine("Program finished!");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                        }
                    }
                    catch (OutOfMemoryException)
                    {
                        Console.WriteLine("Not enough memory on the device!\nProgram stopped!");
                        break;
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Input is too big!");
                    }
                }
                StreamWriter writer = null;
                try
                {
                    writer = new StreamWriter("place.txt");
                    for (int i = 0; i < Location.locationList.Count; i++)
                    {
                        string stringLocation = Location.locationList[i].country + ',' + Location.locationList[i].province + ',' +
                            Location.locationList[i].city + ',' + Location.locationList[i].street + ',' + Location.locationList[i].plate;
                        writer.WriteLine(stringLocation);
                    }
                }
                catch (FileNotFoundException)
                {

                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
                try
                {
                    writer = new StreamWriter("time.txt");
                    for (int i = 0; i < Time.timeList.Count; i++)
                    {
                        string timeString =
                            Time.timeList[i].year.ToString() + ',' + Time.timeList[i].month.ToString() + ',' + Time.timeList[i].day.ToString() + ',' +
                            Time.timeList[i].hour.ToString() + ',' + Time.timeList[i].minute.ToString();
                        writer.WriteLine(timeString);
                    }
                }
                catch (FileNotFoundException)
                {

                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
                try
                {
                    writer = new StreamWriter("airplane.txt");
                    for (int i = 0; i < Airplane.airplaneList.Count; i++)
                    {
                        string airplane = Airplane.airplaneList[i].rows.ToString() + ',' + Airplane.airplaneList[i].chairCountInRow.ToString() + ','
                            + Airplane.airplaneList[i].airline.ToString();
                        writer.WriteLine(airplane);
                    }
                }
                catch (FileNotFoundException)
                {

                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
                try
                {
                    writer = new StreamWriter("flight.txt");
                    for (int i = 0; i < Flight.flightList.Count; i++)
                    {
                        writer.WriteLine("Flight " + (i + 1));
                        writer.WriteLine(Flight.flightList[i].flightNumber);
                        string location = Flight.flightList[i].location.country + ',' + Flight.flightList[i].location.province + ',' + Flight.flightList[i].location.city + ',' +
                            Flight.flightList[i].location.street + ',' + Flight.flightList[i].location.plate;
                        writer.WriteLine(location);

                        string destination = Flight.flightList[i].destination.country + ',' + Flight.flightList[i].destination.province + ',' +
                            Flight.flightList[i].destination.city + ',' +
                        Flight.flightList[i].destination.street + ',' + Flight.flightList[i].destination.plate;
                        writer.WriteLine(destination);

                        string landingTime = Flight.flightList[i].landingTime.year.ToString() + ',' + Flight.flightList[i].landingTime.month.ToString() + ',' +
                            Flight.flightList[i].landingTime.day.ToString() + ',' + Flight.flightList[i].landingTime.hour.ToString() + ',' +
                            Flight.flightList[i].landingTime.minute.ToString();
                        writer.WriteLine(landingTime);

                        string flightTime = Flight.flightList[i].flightTime.year.ToString() + ',' + Flight.flightList[i].flightTime.month.ToString() + ',' +
                            Flight.flightList[i].flightTime.day.ToString() + ',' + Flight.flightList[i].flightTime.hour.ToString() + ',' +
                            Flight.flightList[i].flightTime.minute.ToString();
                        writer.WriteLine(flightTime);

                        writer.WriteLine(Flight.flightList[i].ID);

                        string airplane = Flight.flightList[i].airplane.rows.ToString() + ',' + Flight.flightList[i].airplane.chairCountInRow.ToString() + ',' +
                            Flight.flightList[i].airplane.airline.ToString();

                        writer.WriteLine(airplane);
                        writer.WriteLine(Flight.flightList[i].priceInToman.price);

                    }
                }
                catch (FileNotFoundException)
                {

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
                Console.WriteLine("Not Enough Memory On The Device!\n" +
                    "Program Stopped!");
            }
        }

        //checked
        static public void Add_flight()
        {
            try
            {
                //receives the number of the flight

                Console.Write("Please enter the number of the flight: ");
                string flightNumber = Console.ReadLine();

                Regex regex = new Regex(@"^[A-Za-z]{2}\d{4}[\@\#\$]$",
                    RegexOptions.Compiled);
                MatchCollection matches = regex.Matches(flightNumber.ToString());
                if (matches.Count == 0)
                {
                    throw new Exception("InvalidFlightNumber");
                }

                //Location information

                //receives the location country
                Console.Write("Please enter the country of the location: ");
                string locationCountry = Console.ReadLine();

                //receives the location province
                Console.Write("Please enter the province of the location: ");
                string locationProvince = Console.ReadLine();

                //receives the city of the location
                Console.Write("Please enter the city of the location: ");
                string locationCity = Console.ReadLine();

                //receives the street of the location
                Console.Write("Please enter the street of the location: ");
                string locationStreet = Console.ReadLine();

                //receives the plate of the location
                Console.Write("Please enter the plate of the location: ");
                string locationPlate = Console.ReadLine();

                //destinatoin information

                //receives the country of the destination
                Console.Write("Please enter the country of the destination: ");
                string destinationCountry = Console.ReadLine();

                //receives the province of the destination
                Console.Write("Please enter the province of the destination: ");
                string destinationProvince = Console.ReadLine();

                //receives the city of the destination
                Console.Write("Please enter the city of the destination: ");
                string destinationCity = Console.ReadLine();

                //receives the street of the destination
                Console.Write("Please enter the street of the destination: ");
                string destinationStreet = Console.ReadLine();

                //receives the plate of the destination
                Console.Write("Please enter the plate of the destination: ");
                string destinationPlate = Console.ReadLine();

                //receives flight time

                //receives the year of the flight
                Console.Write("PLease enter the year of the flight: ");
                int flightYear = int.Parse(Console.ReadLine());
                if (flightYear < 1000)
                {
                    throw new Exception("WrongTime");
                }

                //receives the month of the flight
                Console.Write("Please enter the month of the flight: ");
                int flightMonth = int.Parse(Console.ReadLine());
                if ((flightMonth < 1) || (flightMonth > 12))
                {
                    throw new Exception("WrongTime");
                }

                //receives the day of the flight
                Console.Write("Please enter the day of the flight: ");
                int flightDay = int.Parse(Console.ReadLine());
                if ((flightDay > 31) || (flightDay < 1))
                {
                    throw new Exception("WrongTime");
                }

                //receives the hour of the flight
                Console.Write("Please enter the hour of the flight: ");
                int flightHour = int.Parse(Console.ReadLine());
                if ((flightHour < 0) || (flightHour > 23))
                {
                    throw new Exception("WrongTime");
                }

                //receives the minute of the flight
                Console.Write("Please enter the minute of the flight: ");
                int flightMinute = int.Parse(Console.ReadLine());
                if ((flightMinute < 0) || (flightMinute > 59))
                {
                    throw new Exception("WrongTime");
                }
                //receives landing

                //receives the year of the landing
                Console.Write("Please enter the year of the landing: ");
                int landingYear = int.Parse(Console.ReadLine());
                if (landingYear < 1000)
                {
                    throw new Exception("WrongTime");
                }

                //receives the month of the landing
                Console.Write("Please enter the month of the landing: ");
                int landingMonth = int.Parse(Console.ReadLine());
                if ((landingMonth > 12) || (landingMonth < 1))
                {
                    throw new Exception("WrongTime");
                }

                //receives the day of the landing
                Console.Write("Please enter the day of the landing: ");
                int landingDay = int.Parse(Console.ReadLine());
                if ((landingDay < 1) || (landingDay > 31))
                {
                    throw new Exception("WrongTime");
                }

                //receives the hour of the landing
                Console.Write("Please enter the hour of the landing: ");
                int landingHour = int.Parse(Console.ReadLine());
                if ((landingHour > 23) || (landingHour < 0))
                {
                    throw new Exception("WrongTime");
                }

                //recievs the minute of the landing
                Console.Write("Please enter the minute of the landing: ");
                int landingMinute = int.Parse(Console.ReadLine());
                if ((landingMinute > 59) || (landingMinute < 0))
                {
                    throw new Exception("WrongTime");
                }

                //receives ID

                Console.Write("Please enter the ID: ");
                int ID = int.Parse(Console.ReadLine());

                //receives the info of the airplane

                //receives the number of the rows
                Console.Write("Please enter the number of the rows: ");
                int row = int.Parse(Console.ReadLine());
                if (row <= 0)
                {
                    throw new Exception("InvalidInput");
                }

                //receives the number of the chairs in a row
                Console.Write("Please enter the number of the chairs in each row: ");
                int chairInARow = int.Parse(Console.ReadLine());
                if (chairInARow <= 0)
                {
                    throw new Exception("InvalidInput");
                }

                //receives the airline
                Console.WriteLine("Please enter your airline:\nPars\tSaha\tKish\tTurkish\tMahan");
                Airline airline = (Airline)Enum.Parse(
                    typeof(Airline),
                    Console.ReadLine(),
                    true
                    );

                //recieves the cost of the flight

                Console.Write("Please enter the cost of the flight: ");
                double cost = double.Parse(Console.ReadLine());
                if (cost < 0)
                {
                    throw new Exception("InvalidInput");
                }

                Console.WriteLine("Please enter your currency:" +
                    "\nfranc\tdirham\tpound\tDollar");
                Currency currency = (Currency)Enum.Parse(
                    typeof(Currency),
                    Console.ReadLine(),
                    true);
                PriceInForeignCountry newPriceInForeign = new PriceInForeignCountry(cost, currency);
                PriceInToman newPriceInToman = ~newPriceInForeign;

                Location newLocation = new Location(locationCountry, locationProvince, locationCity, locationStreet, locationPlate);
                Location.locationList.Add(newLocation);

                Location newDestination = new Location(destinationCountry, destinationProvince, destinationCity, destinationStreet, destinationPlate);
                Location.locationList.Add(newDestination);

                Time newFlightTime = new Time(flightYear, flightMonth, flightDay, flightHour, flightMinute);
                Time.timeList.Add(newFlightTime);

                Time newLandingTime = new Time(landingYear, landingMonth, landingDay, landingHour, landingMinute);
                Time.timeList.Add(newLandingTime);

                Airplane newAirplane = new Airplane(row, chairInARow, airline);
                Airplane.airplaneList.Add(newAirplane);

                Flight newFlight = new Flight(flightNumber, newLocation, newDestination, newLandingTime, newFlightTime, ID, newAirplane, newPriceInToman);
                for (int i = 0; i < Flight.flightList.Count; i++)
                {
                    if (Flight.flightList[i] == newFlight)
                    {
                        throw new Exception("FlightAlreadyExist");
                    }
                }
                Flight.flightList.Add(newFlight);

                Console.WriteLine("Flight added successfully!");
            }
            catch (Exception error) when (error.Message == "WrongTime")
            {
                Console.WriteLine("Invalid time!");
            }
            catch (Exception error) when (error.Message == "FlightAlreadyExist")
            {
                Console.WriteLine("Entered flight already exist!");
            }
            catch (Exception error) when (error.Message == "InvalidFlightNumber")
            {
                Console.WriteLine("Invalid flight number!");
            }
            catch (Exception error) when (error.Message == "InvalidInput")
            {
                Console.WriteLine("Invalid input!");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid input!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input!");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("Not Enough Memory on the device!");
            }
        }

        //checked
        static public void Sort_flight()
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("Please enter the number :\n1-sort by cost\t2-sort" +
                        " by location and destination");
                    int number = int.Parse(Console.ReadLine());
                    if (number == 1)
                    {
                        Console.Write("Please enter your price: ");
                        double price = double.Parse(Console.ReadLine());
                        if (price < 0)
                        {
                            throw new Exception("MinusPrice");
                        }
                        if (Flight.flightList.Count >= 1)
                        {
                            if (Flight.flightList.Count >= 2)
                            {
                                for (int i = 0, j = 0; i < Flight.flightList.Count * Flight.flightList.Count; i++)
                                {
                                    double price1 = Flight.flightList[j].priceInToman.price - price;
                                    double price2 = Flight.flightList[j + 1].priceInToman.price - price;
                                    if (price1 < 0)
                                    {
                                        price1 *= (-1);
                                    }
                                    if (price2 < 0)
                                    {
                                        price2 *= (-1);
                                    }
                                    if (price1 > price2)
                                    {
                                        Flight tmp = Flight.flightList[j];
                                        Flight.flightList[j] = Flight.flightList[j + 1];
                                        Flight.flightList[j + 1] = tmp;
                                    }
                                    j++;
                                    if (j == Flight.flightList.Count - 1)
                                    {
                                        j = 0;
                                    }
                                }
                            }
                            for (int i = 0; i < Flight.flightList.Count; i++)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(Flight.flightList[i].flightNumber + "\t" + Flight.flightList[i].priceInToman.price + " Toman");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.WriteLine("No flight already exist!");
                        }
                        break;
                    }
                    else if (number == 2)
                    {
                        Console.Write("Please enter the country of your location: ");
                        string locationCountry = Console.ReadLine();

                        Console.Write("Please enter the province of your location: ");
                        string locationProvince = Console.ReadLine();

                        Console.Write("Please enter the city of your location: ");
                        string locationCity = Console.ReadLine();

                        Console.Write("Please enter the street of your location: ");
                        string locationStreet = Console.ReadLine();

                        Console.Write("Please enter the plate of your location: ");
                        string locationPlate = Console.ReadLine();

                        Location newLocation = new Location(locationCountry, locationProvince, locationCity, locationStreet, locationPlate);

                        Console.Write("Please enter the country of your destination: ");
                        string destinationCountry = Console.ReadLine();

                        Console.Write("Please enter the province of your destination: ");
                        string destinationProvince = Console.ReadLine();

                        Console.Write("Please enter the city of your destination: ");
                        string destinationCity = Console.ReadLine();

                        Console.Write("Please enter the street of your destination: ");
                        string destinationStreet = Console.ReadLine();

                        Console.Write("Please enter the plate of your destination: ");
                        string destinationPlate = Console.ReadLine();

                        Location newDestination = new Location(destinationCountry, destinationProvince, destinationCity, destinationStreet, destinationPlate);

                        List<Flight> flightList = new List<Flight>();
                        for (int i = 0; i < Flight.flightList.Count; i++)
                        {
                            if (Flight.flightList[i].location == newLocation && Flight.flightList[i].destination == newDestination)
                            {
                                flightList.Add(Flight.flightList[i]);
                            }
                        }

                        for (int i = 0; i < flightList.Count; i++)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(flightList[i].flightNumber + "\t" + flightList[i].ID);
                            Console.ResetColor();
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid input!");
                        break;
                    }
                }
            }
            catch (Exception error) when (error.Message == "MinusPrice")
            {
                Console.WriteLine("Price cnnot be less than zero!");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("Not enough memory on the device!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input!");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Input is too big!");
            }
        }

        //checked
        static public void Change_data_flight()
        {
            try
            {
                bool flag = true;
                Console.Write("Please enter the ID of your flight: ");
                int ID = int.Parse(Console.ReadLine());
                int IDIndex = 0;
                for (int i = 0; i < Flight.flightList.Count; i++)
                {
                    if (Flight.flightList[i].ID == ID)
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
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Enter which field would you like to change:\n"
                            + "Flight number\tLocation\tDestination\tFlight time\nLanding" +
                            " time\tAirplane\tFlight price\nExit");
                        string order = Console.ReadLine();
                        if (order == "Flight number")
                        {
                            try
                            {
                                Console.Write("Please enter your flight number: ");
                                string flightNumber = Console.ReadLine();
                                Regex regex = new Regex(@"^[A-Za-z]{2}\d{4}[\@\#\$]$", RegexOptions.Compiled);
                                MatchCollection matches = regex.Matches(flightNumber.ToString());
                                if (matches.Count == 0)
                                {
                                    throw new Exception("InvalidFlightNumber");
                                }

                                Flight.flightList[IDIndex].flightNumber = flightNumber;
                                Console.WriteLine("Data changed successfully!");
                                break;
                            }
                            catch (OutOfMemoryException)
                            {
                                Console.WriteLine("Not enough memory on the device!");
                                break; ;
                            }
                            catch (Exception error) when (error.Message == "InvalidFlightNumber")
                            {
                                Console.WriteLine("The number of the flight is not in the correct form!");
                                break; ;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Input is too big!");
                                break; ;
                            }
                        }
                        else if (order == "Location")
                        {
                            try
                            {
                                Console.Write("Please enter the country of your location: ");
                                string locationCountry = Console.ReadLine();

                                Console.Write("Please enter the province of your country: ");
                                string locationProvince = Console.ReadLine();

                                Console.Write("Please enter the city of your location: ");
                                string locationCity = Console.ReadLine();

                                Console.Write("Please enter the street of your country: ");
                                string locationStreet = Console.ReadLine();

                                Console.Write("Please enter the plate of your country: ");
                                string locationPlate = Console.ReadLine();

                                Location newLocation = new Location(locationCountry, locationProvince, locationCity, locationStreet, locationPlate);

                                Flight.flightList[IDIndex].location = newLocation;
                                Console.WriteLine("Data changed successfully!");
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Input is too big!");
                                break;
                            }
                            catch (OutOfMemoryException)
                            {
                                Console.WriteLine("Not enough memory on the device!");
                                break;
                            }
                        }
                        else if (order == "Destination")
                        {
                            try
                            {
                                Console.Write("Please enter the country of your destination: ");
                                string destinationCountry = Console.ReadLine();

                                Console.Write("Please enter the province of your destination: ");
                                string destinationProvince = Console.ReadLine();

                                Console.Write("Please enter the city of your destination: ");
                                string destinationCity = Console.ReadLine();

                                Console.Write("Please enter the street of your destination: ");
                                string destinationStreet = Console.ReadLine();

                                Console.Write("Please enter the plate of your destination: ");
                                string destinationPlate = Console.ReadLine();

                                Location newDestination = new Location(destinationCountry, destinationProvince, destinationCity, destinationStreet, destinationPlate);

                                Flight.flightList[IDIndex].destination = newDestination;
                                Console.WriteLine("Data changed successfully!");
                                break;
                            }
                            catch (OutOfMemoryException)
                            {
                                Console.WriteLine("Not enough memory on the device!");
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Input is too big!");
                                break;
                            }
                        }
                        else if (order == "Flight time")
                        {
                            try
                            {
                                Console.Write("Please enter the year of your flight: ");
                                int flightYear = int.Parse(Console.ReadLine());
                                if (flightYear < 1000)
                                {
                                    throw new Exception("WrongTime");
                                }

                                Console.Write("Please enter the month of your flight: ");
                                int flightMonth = int.Parse(Console.ReadLine());
                                if ((flightMonth < 1) || (flightMonth > 12))
                                {
                                    throw new Exception("WrongTime");
                                }

                                Console.Write("Please enter the day of your flight: ");
                                int flightDay = int.Parse(Console.ReadLine());
                                if ((flightDay < 1) || (flightDay > 31))
                                {
                                    throw new Exception("WrongTime");
                                }

                                Console.Write("Please enter the hour of your flight: ");
                                int flightHour = int.Parse(Console.ReadLine());
                                if ((flightHour > 23) || (flightHour < 0))
                                {
                                    throw new Exception("WrongTime");
                                }

                                Console.Write("Please enter the minute of your flight: ");
                                int flightMinute = int.Parse(Console.ReadLine());
                                if ((flightMinute < 0) || (flightMinute > 59))
                                {
                                    throw new Exception("WrongTime");
                                }

                                Time newFlightTime = new Time(flightYear, flightMonth, flightDay, flightHour, flightMinute);
                                Flight.flightList[IDIndex].flightTime = newFlightTime;
                                Console.WriteLine("Data changed successfully!");
                                break;
                            }
                            catch (Exception error) when (error.Message == "WrongTime")
                            {
                                Console.WriteLine("Invalid time!");
                                break;
                            }
                            catch (OutOfMemoryException)
                            {
                                Console.WriteLine("Not enough memory on the device!");
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Input is too big!");
                                break;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input!");
                                break;
                            }
                        }
                        else if (order == "Landing time")
                        {
                            try
                            {
                                Console.Write("Please enter the year of your landing: ");
                                int landingYear = int.Parse(Console.ReadLine());
                                if (landingYear < 1000)
                                {
                                    throw new Exception("WrongTime");
                                }

                                Console.Write("Please enter the month of your landing: ");
                                int landingMonth = int.Parse(Console.ReadLine());
                                if ((landingMonth > 12) || (landingMonth < 1))
                                {
                                    throw new Exception("WrongTime");
                                }

                                Console.Write("Please enter the day of your landing: ");
                                int landingDay = int.Parse(Console.ReadLine());
                                if ((landingDay > 31) || (landingDay < 1))
                                {
                                    throw new Exception("WrongTime");
                                }

                                Console.Write("Please enter the hour of your landing: ");
                                int landingHour = int.Parse(Console.ReadLine());
                                if ((landingHour > 23) || (landingHour < 0))
                                {
                                    throw new Exception("WrongTime");
                                }

                                Console.Write("Please enter the minute of your landing: ");
                                int landingMinute = int.Parse(Console.ReadLine());
                                if ((landingMinute > 59) || (landingMinute < 0))
                                {
                                    throw new Exception("WrongTime");
                                }

                                Time newLandingTime = new Time(landingYear, landingMonth, landingDay, landingHour, landingMinute);
                                Flight.flightList[IDIndex].landingTime = newLandingTime;
                                Console.WriteLine("Data changed successfully!");
                                break;
                            }
                            catch(Exception error)when(error.Message== "WrongTime")
                            {
                                Console.WriteLine("Invalid time!");
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Input is too big!");
                                break;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input!");
                                break;
                            }
                            catch (OutOfMemoryException)
                            {
                                Console.WriteLine("Not enough memory on the device!");
                                break;
                            }
                        }
                        else if (order == "Airplane")
                        {
                            try
                            {
                                Console.Write("Please enter the number rows of the airplane: ");
                                int rows = int.Parse(Console.ReadLine());
                                if (rows <= 0)
                                {
                                    throw new Exception("Invalid input!");
                                }

                                Console.Write("Please enter the number of the seats in each row: ");
                                int chairCountInRow = int.Parse(Console.ReadLine());
                                if (chairCountInRow <= 0)
                                {
                                    throw new Exception("Invalid input!");
                                }

                                Console.WriteLine("Please enter the airline:\nPars\tSaha\tKish\tTurkish\tMahan");
                                Airline airline = (Airline)Enum.Parse(
                                    typeof(Airline),
                                    Console.ReadLine(),
                                    true
                                    );

                                Airplane newAirplane = new Airplane(rows, chairCountInRow, airline);
                                Flight.flightList[IDIndex].airplane = newAirplane;
                                Console.WriteLine("Data changed successfully!");
                                break;
                            }
                            catch(Exception error)when(error.Message=="Invalid input!")
                            {
                                Console.WriteLine("Invalid input!");
                                break;
                            }
                            catch (OutOfMemoryException)
                            {
                                Console.WriteLine("Not enough memory on the device!");
                                break;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input1");
                                break;
                            }
                            catch (ArgumentException)
                            {
                                Console.WriteLine("Invalid input!");
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Input is too big!");
                                break;
                            }
                        }
                        else if (order == "Flight price")
                        {
                            try
                            {
                                Console.Write("Please enter your price: ");
                                double price = double.Parse(Console.ReadLine());
                                if (price <= 0)
                                {
                                    throw new Exception("Invalid input!");
                                }
                                Console.WriteLine("Please enter your currency:\nfranc\tdirham\tpound\tDollar");
                                Currency currency = (Currency)Enum.Parse(
                                    typeof(Currency),
                                    Console.ReadLine(),
                                    true);

                                PriceInForeignCountry newPrice = new PriceInForeignCountry(price, currency);
                                Flight.flightList[IDIndex].priceInToman = ~newPrice;
                                Console.WriteLine("Data changed successfully!");
                                break;
                            }
                            catch(Exception error)when(error.Message=="Invalid input!")
                            {
                                Console.WriteLine("Invalid input");
                                break;
                            }
                            catch (ArgumentException)
                            {
                                Console.WriteLine("Invalid input!");
                                break;
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Invalid input!");
                                break;
                            }
                            catch (OutOfMemoryException)
                            {
                                Console.WriteLine("Not enough memory on the device!");
                                break;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Input is too big!");
                                break;
                            }
                        }
                        else if (order == "Exit")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input!");
                            break;
                        }
                    }
                    catch (OutOfMemoryException)
                    {
                        Console.WriteLine("Not enough memory on the device!");
                        break;
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Input is too big!");
                        break; ;
                    }
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input!");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("Not Enough memory on the device!");
            }
            catch (Exception error) when (error.Message == "IDNotFound")
            {
                Console.WriteLine("Entered ID was not found!");
            }
        }
    }
    class Flight
    {
        static public List<Flight> flightList = new List<Flight>();
        public Flight(string flightNumber, Location location, Location destination, Time landingTime, Time flightTime, int ID, Airplane airplane, PriceInToman priceInToman)
        {
            this.flightNumber = flightNumber;
            this.location = location;
            this.destination = destination;
            this.landingTime = landingTime;
            this.flightTime = flightTime;
            this.ID = ID;
            this.airplane = airplane;
            this.priceInToman = priceInToman;
        }
        public static bool operator ==(Flight flight1, Flight flight2)
        {
            if ((flight1.airplane == flight2.airplane) != true)
            {
                return false;
            }
            else if ((flight1.location == flight2.location) != true)
            {
                return false;
            }
            else if ((flight1.destination == flight2.destination) != true)
            {
                return false;
            }
            else if ((flight1.flightNumber == flight2.flightNumber) != true)
            {
                return false;
            }
            else if ((flight1.flightTime == flight2.flightTime) != true)
            {
                return false;
            }
            else if ((flight1.landingTime == flight2.landingTime) != true)
            {
                return false;
            }
            else if ((flight1.ID == flight2.ID) != true)
            {
                return false;
            }
            else if ((flight1.priceInToman == flight2.priceInToman) != true)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool operator !=(Flight flight1, Flight flight2)
        {
            return true;
        }
        string _flightNumber;
        public string flightNumber
        {
            get { return this._flightNumber; }
            set { this._flightNumber = value; }
        }
        Location _location;
        public Location location
        {
            get { return this._location; }
            set { this._location = value; }
        }
        Location _destination;
        public Location destination
        {
            get { return this._destination; }
            set { this._destination = value; }
        }
        Time _landingTime;
        public Time landingTime
        {
            get { return this._landingTime; }
            set { this._landingTime = value; }
        }
        Time _flightTime;
        public Time flightTime
        {
            get { return this._flightTime; }
            set { this._flightTime = value; }
        }
        int _ID;
        public int ID
        {
            get { return this._ID; }
            set { this._ID = value; }
        }
        Airplane _airplane;
        public Airplane airplane
        {
            get { return this._airplane; }
            set { this._airplane = value; }
        }
        PriceInToman _priceInToman;
        public PriceInToman priceInToman
        {
            get { return this._priceInToman; }
            set { this._priceInToman = value; }
        }
    }
    class Airplane
    {
        static public List<Airplane> airplaneList = new List<Airplane>();
        int _rows;
        public int rows
        {
            get { return this._rows; }
            set { this._rows = value; }
        }
        int _chairCountInRow;
        public int chairCountInRow
        {
            get { return this._chairCountInRow; }
            set { this._chairCountInRow = value; }
        }
        Airline _airline;
        public Airline airline
        {
            get { return this._airline; }
            set { this._airline = value; }
        }
        public static bool operator ==(Airplane airplane1, Airplane airplane2)
        {
            if (airplane1.rows != airplane2.rows)
            {
                return false;
            }
            else if (airplane1.chairCountInRow != airplane2.chairCountInRow)
            {
                return false;
            }
            else if (airplane1.airline != airplane2.airline)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool operator !=(Airplane airplane1, Airplane airplane2)
        {
            return true;
        }
        public Airplane(int rows, int chairCountInRow, Airline airline)
        {
            this._rows = rows;
            this._chairCountInRow = chairCountInRow;
            this._airline = airline;
        }
    }
    struct Location
    {
        static public List<Location> locationList = new List<Location>();
        string _country;
        public string country
        {
            get { return this._country; }
            set { this._country = value; }
        }
        string _province;
        public string province
        {
            get { return this._province; }
            set { this._province = value; }
        }
        string _city;
        public string city
        {
            get { return this._city; }
            set { this._city = value; }
        }
        string _street;
        public string street
        {
            get { return this._street; }
            set { this._street = value; }
        }
        string _plate;
        public string plate
        {
            get { return this._plate; }
            set { this._plate = value; }
        }
        public static bool operator ==(Location location1, Location location2)
        {
            if (location1.country != location2.country)
            {
                return false;
            }
            else if (location1.province != location2.province)
            {
                return false;
            }
            else if (location1.city != location2.city)
            {
                return false;
            }
            else if (location1.street != location2.street)
            {
                return false;
            }
            else if (location1.plate != location2.plate)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool operator !=(Location location1, Location location2)
        {
            return true;
        }
        public Location(string country, string province, string city, string street, string plate)
        {
            this._country = country;
            this._province = province;
            this._city = city;
            this._street = street;
            this._plate = plate;
        }
    }
    struct Time
    {
        static public List<Time> timeList = new List<Time>();
        int _year;
        public int year
        {
            get { return this._year; }
            set { this._year = value; }
        }
        int _month;
        public int month
        {
            get { return this._month; }
            set { this._month = value; }
        }
        int _day;
        public int day
        {
            get { return this._day; }
            set { this._day = value; }
        }
        int _hour;
        public int hour
        {
            get { return this._hour; }
            set { this._hour = value; }
        }
        int _minute;
        public int minute
        {
            get { return this._minute; }
            set { this._minute = value; }
        }
        public static bool operator ==(Time time1, Time time2)
        {
            if (time1.year != time2.year)
            {
                return false;
            }
            else if (time1.month != time2.month)
            {
                return false;
            }
            else if (time1.day != time2.day)
            {
                return false;
            }
            else if (time1.hour != time2.hour)
            {
                return false;
            }
            else if (time1.minute != time2.minute)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool operator !=(Time time1, Time time2)
        {
            return false;
        }
        public Time(int year, int month, int day, int hour, int minute)
        {
            this._year = year;
            this._month = month;
            this._day = day;
            this._hour = hour;
            this._minute = minute;
        }
    }
    struct PriceInForeignCountry
    {
        static public List<PriceInForeignCountry> PriceInForeignCountriesList = new List<PriceInForeignCountry>();
        public static PriceInToman operator ~(PriceInForeignCountry foreignPrice)
        {
            double convertedPrice = ((double)foreignPrice.currency) * foreignPrice.money;
            PriceInToman newPrice = new PriceInToman(convertedPrice);
            PriceInToman.PriceInTomenList.Add(newPrice);
            return newPrice;
        }
        double _money;
        public double money
        {
            get { return this._money; }
            set { this._money = value; }
        }
        Currency _currency;
        public Currency currency
        {
            get { return this._currency; }
            set { this._currency = value; }
        }
        public PriceInForeignCountry(double money, Currency currency)
        {
            _currency = currency;
            _money = money;
        }
    }
    struct PriceInToman
    {
        public static List<PriceInToman> PriceInTomenList = new List<PriceInToman>();
        double _price;
        public double price
        {
            get { return _price; }
            set { _price = value; }
        }
        public static bool operator ==(PriceInToman priceInToman1, PriceInToman priceInToman2)
        {
            if (priceInToman1.price != priceInToman2.price)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool operator !=(PriceInToman priceInToman1, PriceInToman priceInToman2)
        {
            return true;
        }
        public PriceInToman(double price)
        {
            _price = price;
        }
    }
}
