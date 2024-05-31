using System;
using System.Collections.Generic;

namespace tamrin_seri_6_soal_1
{
    enum illness { healthy, underProtection, ill }
    class Program
    {
        static void Main()
        {
            try
            {
                while (true)
                {
                    try
                    {
                        Console.WriteLine("\nMenu\nRegister Patient\tRegister General Practitioner\tRegister Dentist\tRegister Surgeon\n" +
                            "Add Patient to a doctor\tShow Doctor's Graduation\tShow Doctor's Field\tUpdate Patient's state\tCompare Doctors\n" +
                            "Show All Doctors\tShow All Patients\tExit");
                        string order = Console.ReadLine();
                        if (order == "Register Patient") 
                        {
                            PatientRegister();
                        }
                        else if (order == "Register General Practitioner") 
                        {
                            GPRegister();
                        }
                        else if (order == "Register Dentist") 
                        {
                            DentistRegister();
                        }
                        else if (order == "Register Surgeon") 
                        {
                            SurgeonRegister();
                        }
                        else if (order == "Add Patient to a doctor") 
                        {
                            AddPatientToDoctor();
                        }
                        else if (order == "Show Doctor's Graduation") 
                        {
                            ShowDoctorsGraduation();
                        }
                        else if (order == "Show Doctor's Field") 
                        {
                            ShowDoctorsField();
                        }
                        else if (order == "Update Patient's state") 
                        {
                            updatePatientState();
                        }
                        else if (order == "Compare Doctors") 
                        {
                            CompareDoctors();
                        }
                        else if (order == "Show All Doctors") 
                        {
                            ShowAllDoctors();
                        }
                        else if(order== "Show All Patients")
                        {
                            ShowAllPatients();
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
                    catch(OverflowException)
                    {
                        Console.WriteLine("Input is too big!");
                    }
                }
            }
            catch
            {
                Console.WriteLine("An Error Occured!");
            }
        }

        static public void PatientRegister()
        {
            try
            {
                Console.Write("Please enter your first name : ");
                string firstName = Console.ReadLine();

                Console.Write("Please enter your last name : ");
                string lastName = Console.ReadLine();

                Console.Write("Please enter your SSN : ");
                string SSN = Console.ReadLine();
                if (CheckSSNBeenUsed(SSN))
                {
                    throw new Exception("SSNAlreadyUsed");
                }

                Console.Write("Please enter your disease : ");
                string disease = Console.ReadLine();

                Console.Write("Please enter your state (healthy,underProtection,ill) : ");
                illness state = (illness)Enum.Parse(typeof(illness), Console.ReadLine(), true);

                Patient newPatient = new Patient(firstName, lastName, SSN, disease, state);
                Patient.patientsList.Add(newPatient);
                Console.WriteLine("Patient registered successfully!");
            }
            catch (Exception error) when (error.Message == "SSNAlreadyUsed")
            {
                Console.WriteLine("This SSN has already used!");
            }
            catch
            {
                Console.WriteLine("An Error Occured!");
            }
        }
        static public void GPRegister()
        {
            try
            {
                Console.Write("Please enter your first name : ");
                string firstName = Console.ReadLine();

                Console.Write("Please enter your last name : ");
                string lastName = Console.ReadLine();

                Console.Write("Please enter your SSN : ");
                string SSN = Console.ReadLine();
                if (CheckSSNBeenUsed(SSN))
                {
                    throw new Exception("SSNAlreadyUsed");
                }

                Console.Write("Please enter your field : ");
                string field = Console.ReadLine();

                Console.Write("Please enter your salary : ");
                long salary = long.Parse(Console.ReadLine());

                Console.Write("Please enter your university : ");
                string university = Console.ReadLine();

                Console.Write("Please enter the rank of your university : ");
                int universityRank = int.Parse(Console.ReadLine());

                if (universityRank <= 0)
                {
                    throw new Exception("InvalidRank");
                }

                GeneralPractitioner newGP = new GeneralPractitioner(firstName,
                    lastName, SSN, field, salary, university,universityRank);
                GeneralPractitioner.GPList.Add(newGP);
                Doctors.DoctorList.Add(newGP);
                Console.WriteLine("GeneralPractitioner registered successfully!");
            }
            catch(Exception error)when(error.Message== "InvalidRank")
            {
                Console.WriteLine("Invalid Rank!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input!");
            }
            catch (Exception error) when (error.Message == "SSNAlreadyUsed")
            {
                Console.WriteLine("This SSN has already used!");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Input is too big!");
            }
            catch
            {
                Console.WriteLine("An Error Occured!");
            }
        }
        static public void DentistRegister()
        {
            try
            {
                Console.Write("Please enter your first name : ");
                string firstName = Console.ReadLine();

                Console.Write("Please enter your last name : ");
                string lastName = Console.ReadLine();

                Console.Write("Please enter your SSN : ");
                string SSN = Console.ReadLine();
                if (CheckSSNBeenUsed(SSN))
                {
                    throw new Exception("SSNHasAlreadyUsed");
                }

                Console.Write("Please enter your field : ");
                string field = Console.ReadLine();

                Console.Write("Please enter your salary : ");
                long salary = long.Parse(Console.ReadLine());

                if (salary <= 0)
                {
                    throw new Exception("InvalidSalary");
                }

                Console.Write("Please enter your university : ");
                string university = Console.ReadLine();

                Dentist newDentist = new Dentist(firstName, lastName, SSN, field, salary, university);
                Dentist.dentistsList.Add(newDentist);
                Doctors.DoctorList.Add(newDentist);

                Console.WriteLine("Dentist registered successfully!");
            }
            catch(Exception error)when(error.Message== "InvalidSalary")
            {
                Console.WriteLine("Wrong salary input");
            }
            catch (Exception error) when (error.Message == "SSNHasAlreadyUsed")
            {
                Console.WriteLine("Entered SSN has already used!");
            }
            catch
            {
                Console.WriteLine("An Error Occured!");
            }
        }
        static public void SurgeonRegister()
        {
            try
            {
                Console.Write("Please enter your first name : ");
                string firstName = Console.ReadLine();

                Console.Write("Please enter your last name : ");
                string lastName = Console.ReadLine();

                Console.Write("Please enter your SSN : ");
                string SSN = Console.ReadLine();

                if (CheckSSNBeenUsed(SSN))
                {
                    throw new Exception("SSNAlreadyUsed");
                }

                Console.Write("Please enter your field : ");
                string field = Console.ReadLine();

                Console.Write("Please enter your salary ; ");
                long salary = long.Parse(Console.ReadLine());

                Console.Write("Please enter your university : ");
                string university = Console.ReadLine();

                Surgeon newSurgeon = new Surgeon(firstName, lastName, SSN, field, salary, university);
                Surgeon.surgeonsList.Add(newSurgeon);
                Doctors.DoctorList.Add(newSurgeon);

                Console.WriteLine("Suregeon registered successfully!");
            }
            catch (Exception error) when (error.Message == "SSNAlreadyUsed")
            {
                Console.WriteLine("This SSN has already used!");
            }
            catch
            {
                Console.WriteLine("An Error Occured!");
            }
        }

        static public void AddPatientToDoctor()
        {
            try
            {
                Console.WriteLine("Please enter patient's ID : ");
                int ID = int.Parse(Console.ReadLine());

                Patient patient = null;

                if (Patient.patientsList.Find(pat => pat.PatientID == ID) == null)
                {
                    throw new Exception("EnteredIDWasNotFound");
                }
                else
                {
                    patient = Patient.patientsList.Find(pat => pat.PatientID == ID);
                }

                Console.WriteLine("Please enter Doctor's ID : ");
                int DoctorID = int.Parse(Console.ReadLine());

                if (GeneralPractitioner.GPList.Find(GP => GP.GPID == DoctorID) != null)
                {
                    GeneralPractitioner GP = GeneralPractitioner.GPList.Find(GP => GP.GPID == DoctorID);
                    Console.WriteLine(GP + patient);
                }
                else if (Dentist.dentistsList.Find(dentist => dentist.DentistID == ID) != null)
                {
                    Dentist dentist = Dentist.dentistsList.Find(dentist => dentist.DentistID == ID);
                    Console.WriteLine(dentist + patient);
                }
                else if (Surgeon.surgeonsList.Find(surgeon => surgeon.SurgeonID == ID) != null)
                {
                    Surgeon surgeon = Surgeon.surgeonsList.Find(surgeon => surgeon.SurgeonID == ID);
                    Console.WriteLine(surgeon + patient);
                }
                else
                {
                    throw new Exception("EnteredIDWasNotFound");
                }
            }
            catch (Exception error) when (error.Message == "EnteredIDWasNotFound")
            {
                Console.WriteLine("Entered ID was not found!");
            }
            catch
            {
                Console.WriteLine("An Error Occured!");
            }
        }
        static public void ShowDoctorsGraduation()
        {
            try
            {
                Console.Write("Please enter doctor's ID : ");
                int doctorID = int.Parse(Console.ReadLine());
                if (GeneralPractitioner.GPList.Find(gp => gp.GPID == doctorID) != null)
                {
                    Console.WriteLine(GeneralPractitioner.GPList.Find(gp => gp.GPID == doctorID).GraduatedFrom());
                }
                else if (Surgeon.surgeonsList.Find(surgeon => surgeon.SurgeonID == doctorID) != null)
                {
                    Console.WriteLine(Surgeon.surgeonsList.Find(surgeon => surgeon.SurgeonID == doctorID).GraduatedFrom());
                }
                else if (Dentist.dentistsList.Find(dentist => dentist.DentistID == doctorID) != null)
                {
                    Console.WriteLine(Dentist.dentistsList.Find(dentist => dentist.DentistID == doctorID).GraduatedFrom());
                }
                else
                {
                    Console.WriteLine("Entered input was not found!");
                }
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
                Console.WriteLine("An Error Occured!");
            }
        }
        static public void ShowDoctorsField()
        {
            try
            {
                Console.Write("Please enter doctor's ID : ");
                int doctorID = int.Parse(Console.ReadLine());

                if (GeneralPractitioner.GPList.Find(GP => GP.GPID == doctorID) != null)
                {
                    Console.WriteLine(GeneralPractitioner.GPList.Find(GP => GP.GPID == doctorID).Work());
                }
                else if (Dentist.dentistsList.Find(dentist => dentist.DentistID == doctorID) != null)
                {
                    Console.WriteLine(Dentist.dentistsList.Find(dentist => dentist.DentistID == doctorID).Work());
                }
                else if (Surgeon.surgeonsList.Find(surgeon => surgeon.SurgeonID == doctorID) != null)
                {
                    Console.WriteLine(Surgeon.surgeonsList.Find(surgeon => surgeon.SurgeonID == doctorID).Work());
                }
                else
                {
                    Console.WriteLine("Entered ID was not found!");
                }
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
                Console.WriteLine("An Error Occured!");
            }
        }
        static public void updatePatientState()
        {
            try
            {
                Console.Write("Please enter patient's ID : ");
                int ID = int.Parse(Console.ReadLine());
                if (Patient.patientsList.Find(patient => patient.PatientID == ID) != null)
                {
                    Console.Write("Please enter patient's new state (healthy,underProtection,ill) : ");
                    illness newState = (illness)Enum.Parse(typeof(illness), Console.ReadLine(), true);

                    Patient.patientsList.Find(patient => patient.PatientID == ID).recovered = newState;
                    Console.WriteLine("Patient's state modified!");
                }
                else
                {
                    Console.WriteLine("Entered ID was not found!");
                }
            }
            catch (OverflowException)
            {
                Console.WriteLine("Input is too big!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input!");
            }
            catch
            {
                Console.WriteLine("An Error Occured!");
            }
        }
        static public void CompareDoctors()
        {
            try
            {
                Console.Write("Please enter the first doctor's ID : ");
                int firstID = int.Parse(Console.ReadLine());

                if (GeneralPractitioner.GPList.Find(GP => GP.GPID == firstID) != null)
                {
                    GeneralPractitioner GP1 = GeneralPractitioner.GPList.Find(GP => GP.GPID == firstID);

                    Console.Write("Please enter the second doctor's ID : ");
                    int secondID = int.Parse(Console.ReadLine());

                    if (GeneralPractitioner.GPList.Find(GP => GP.GPID == secondID) != null)
                    {
                        GeneralPractitioner GP2 = GeneralPractitioner.GPList.Find(GP => GP.GPID == secondID);
                        if (GP1 > GP2)
                        {
                            Console.WriteLine("The University rank of the first General Partitioner is higher than the second one!");
                        }
                        else if (GP1 < GP2)
                        {
                            Console.WriteLine("The University rank of the second General Partitioner is higher than the first one!");
                        }
                        else
                        {
                            Console.WriteLine("Their universities have equal rank!");
                        }
                    }
                    else if (Surgeon.surgeonsList.Find(surgeon => surgeon.SurgeonID == secondID) != null)
                    {
                        Console.WriteLine("These doctors cannot be compared to each other!");
                    }
                    else if (Dentist.dentistsList.Find(dentist => dentist.DentistID == secondID) != null)
                    {
                        Console.WriteLine("These doctors cannot be compared to each other!");
                    }
                    else
                    {
                        Console.WriteLine("Entered ID was not found!");
                    }
                }
                else if (Surgeon.surgeonsList.Find(surgeon => surgeon.SurgeonID == firstID) != null)
                {
                    Surgeon surgeon1 = Surgeon.surgeonsList.Find(surgeon => surgeon.SurgeonID == firstID);

                    Console.Write("Please enter the second doctor's ID : ");
                    int secondID = int.Parse(Console.ReadLine());

                    if (GeneralPractitioner.GPList.Find(GP => GP.GPID == secondID) != null)
                    {
                        Console.WriteLine("These doctors cannot be compared to each other!");
                    }
                    else if (Surgeon.surgeonsList.Find(surgeon => surgeon.SurgeonID == secondID) != null)
                    {
                        Surgeon surgeon2 = Surgeon.surgeonsList.Find(surgeon => surgeon.SurgeonID == secondID);

                        if (surgeon1 > surgeon2)
                        {
                            Console.WriteLine("The first surgeon has more patients than the second one!");
                        }
                        else if (surgeon1 < surgeon2)
                        {
                            Console.WriteLine("The second surgeon has more patients than the first one!");
                        }
                        else
                        {
                            Console.WriteLine("These surgeons have same number of patients!");
                        }
                    }
                    else if (Dentist.dentistsList.Find(dentist => dentist.DentistID == secondID) != null)
                    {
                        Console.WriteLine("These doctors cannot be compared to each other!");
                    }
                    else
                    {
                        Console.WriteLine("Entered ID was not found!");
                    }
                }
                else if (Dentist.dentistsList.Find(dentist => dentist.DentistID == firstID) != null)
                {
                    Dentist dentist1 = Dentist.dentistsList.Find(dentist => dentist.DentistID == firstID);

                    Console.Write("Please enter the second doctor's ID : ");
                    int secondID = int.Parse(Console.ReadLine());

                    if (GeneralPractitioner.GPList.Find(GP => GP.GPID == secondID) != null)
                    {
                        Console.WriteLine("These doctors cannot be compared to each other!");
                    }
                    else if (Surgeon.surgeonsList.Find(surgeon => surgeon.SurgeonID == secondID) != null)
                    {
                        Console.WriteLine("These doctors cannot be compared to each other!");
                    }
                    else if (Dentist.dentistsList.Find(dentist => dentist.DentistID == secondID) != null)
                    {
                        Dentist dentist2 = Dentist.dentistsList.Find(dentist => dentist.DentistID == secondID);
                        if (dentist1 > dentist2)
                        {
                            Console.WriteLine("The first dentist has more salary than the second one!");
                        }
                        else if (dentist1 < dentist2)
                        {
                            Console.WriteLine("The second dentist has more salary than the first one!");
                        }
                        else
                        {
                            Console.WriteLine("These dentists have same salary!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Entered ID was not found!");
                    }
                }
                else
                {
                    Console.WriteLine("Entered ID was not found!");
                }
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
                Console.WriteLine("An Error Occured!");
            }
        }

        static public void ShowAllDoctors()
        {
            try
            {
                if (Doctors.DoctorList.Count > 0)
                {
                    foreach (Doctor doctor in Doctors.DoctorList)
                    {
                        if (doctor is GeneralPractitioner)
                        {
                            GeneralPractitioner GP = (GeneralPractitioner)doctor;
                            Console.WriteLine("GeneralPractitioner - ID : " + GP.GPID);
                            Console.WriteLine("Full name : " + GP.FirstName + ' ' + GP.LastName);
                            Console.WriteLine("Field : " + GP.Field);
                        }
                        else if (doctor is Dentist)
                        {
                            Dentist dentist = (Dentist)doctor;
                            Console.WriteLine("Dentist - ID : " + dentist.DentistID);
                            Console.WriteLine("Full name : " + dentist.FirstName + ' ' + dentist.LastName);
                            Console.WriteLine("Field : " + dentist.Field); ;
                        }
                        else if (doctor is Surgeon)
                        {
                            Surgeon surgeon = (Surgeon)doctor;
                            Console.WriteLine("Surgeon - ID : " + surgeon.SurgeonID);
                            Console.WriteLine("Full name : " + surgeon.FirstName + ' ' + surgeon.LastName);
                            Console.WriteLine("Field : " + surgeon.Field);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Nothing found!");
                }
            }
            catch
            {
                Console.WriteLine("An Error Occured!");
            }
        }
        static public void ShowAllPatients()
        {
            try
            {
                if (Patient.patientsList.Count > 0)
                {
                    foreach(Patient patient in Patient.patientsList)
                    {
                        Console.WriteLine(patient.FirstName+' '+patient.LastName+" : "+patient.PatientID);
                        Console.WriteLine("Patient disease : "+patient.disease);
                        Console.WriteLine("Patient state : "+patient.recovered.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("Nothing found!");
                }
            }
            catch
            {
                Console.WriteLine("An Error Occured!");
            }
        }
        static public bool CheckSSNBeenUsed(string SSN)
        {
            if (Patient.patientsList.Find(patient => patient.SSN == SSN) != null)
            {
                return true;
            }
            else if (GeneralPractitioner.GPList.Find(GP => GP.SSN == SSN) != null)
            {
                return true;
            }
            else if (Dentist.dentistsList.Find(dentist => dentist.SSN == SSN) != null)
            {
                return true;
            }
            else if (Surgeon.surgeonsList.Find(surgeon => surgeon.SSN == SSN) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    interface IPerson
    {
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string SSN
        {
            get;
            set;
        }
    }
    interface IDoctor
    {
        public string Field
        {
            get;
            set;
        }
        public long Salary
        {
            get;
            set;
        }
        public string University
        {
            get;
            set;
        }
        public List<Patient> Patients
        {
            get;
            set;
        }
        string Work();
    }
    class Doctor
    {
        static public int numberOfDotors = 0;
    }
    class Patient : IPerson
    {
        static public List<Patient> patientsList = new List<Patient>();

        private string _firstName_PrivateMode;
        public string FirstName
        {
            get { return this._firstName_PrivateMode; }
            set { this._firstName_PrivateMode = value; }
        }

        private string _lastName_PrivateMode;
        public string LastName
        {
            get { return this._lastName_PrivateMode; }
            set { this._lastName_PrivateMode = value; }
        }

        private string _SSN_PrivateMode;
        public string SSN
        {
            get { return this._SSN_PrivateMode; }
            set { this._SSN_PrivateMode = value; }
        }

        private int _patientID;
        public int PatientID
        {
            get { return this._patientID; }
            private set { this._patientID = value; }
        }

        public string disease;

        public illness recovered;

        public Patient(string firstName, string lastName, string SSN, string disease, illness illnessState)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.SSN = SSN;
            this.disease = disease;
            this.recovered = illnessState;
            this.PatientID = Patient.patientsList.Count + 1;
        }
    }
    class GeneralPractitioner : Doctor, IDoctor, IPerson
    {
        static public List<GeneralPractitioner> GPList = new List<GeneralPractitioner>();

        private string _firstName_PrivateMode;
        public string FirstName
        {
            get { return this._firstName_PrivateMode; }
            set { this._firstName_PrivateMode = value; }
        }

        private string _lastName_PrivateMode;
        public string LastName
        {
            get { return this._lastName_PrivateMode; }
            set { this._lastName_PrivateMode = value; }
        }

        private string _SSN_PrivateMode;
        public string SSN
        {
            get { return this._SSN_PrivateMode; }
            set { this._SSN_PrivateMode = value; }
        }

        private int _GPID;
        public int GPID
        {
            get { return this._GPID; }
            private set { this._GPID = value; }
        }

        //Career information
        private string _field_PrivateMode;
        public string Field
        {
            get { return this._field_PrivateMode; }
            set { this._field_PrivateMode = value; }
        }

        private long _salary_PrivateMode;
        public long Salary
        {
            get { return this._salary_PrivateMode; }
            set { this._salary_PrivateMode = value; }
        }

        private string _university_PrivateMode;
        public string University
        {
            get { return this._university_PrivateMode; }
            set { this._university_PrivateMode = value; }
        }

        private int _universityRank;
        public int UniversityRank
        {
            get { return this._universityRank; }
            private set { this._universityRank = value; }
        }

        private List<Patient> _Patients_PrivateMode = new List<Patient>();
        public List<Patient> Patients
        {
            get { return this._Patients_PrivateMode; }
            set { this._Patients_PrivateMode = value; }
        }

        public GeneralPractitioner(string firstName, string lastNAme, string SSN,
            string field, long salary, string university,int universityRank)
        {
            this.FirstName = firstName;
            this.LastName = lastNAme;
            this.SSN = SSN;
            this.Field = field;
            this.Salary = salary;
            this.University = university;
            Doctor.numberOfDotors++;
            this.GPID = numberOfDotors;
            this.UniversityRank = universityRank;
        }

        static public bool operator >(GeneralPractitioner GP1, GeneralPractitioner GP2)
        {
            if (GP1.UniversityRank > GP2.UniversityRank)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public bool operator <(GeneralPractitioner GP1, GeneralPractitioner GP2)
        {
            if (GP1.UniversityRank < GP2.UniversityRank)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GraduatedFrom()
        {
            return this.FirstName + ' ' + this.LastName + ' ' + "is graduated from " + this.University;
        }

        public string Work()
        {
            return "This General Practitioner works on " + this.Field;
        }

        static public string operator +(GeneralPractitioner GP, Patient patient)
        {
            if (patient.disease.Contains("Cough"))
            {
                GP.Patients.Add(patient);
                return "Patient added successfully!";
            }
            else if (patient.disease.Contains("Sneezing"))
            {
                GP.Patients.Add(patient);
                return "Patient added successfully!";
            }
            else if (patient.disease.Contains("Sore throat"))
            {
                GP.Patients.Add(patient);
                return "Patient added successfully!";
            }
            else
            {
                return "This patient cannot be added!";
            }
        }
    }
    class Dentist : Doctor, IDoctor, IPerson
    {
        static public List<Dentist> dentistsList = new List<Dentist>();

        private int _dentistID;
        public int DentistID
        {
            get { return this._dentistID; }
            private set { this._dentistID = value; }
        }

        private string _firstName;
        public string FirstName
        {
            get { return this._firstName; }
            set { this._firstName = value; }
        }

        //              Last Name
        private string _lastName_PrivateMode;
        public string LastName
        {
            get { return this._lastName_PrivateMode; }
            set { this._lastName_PrivateMode = value; }
        }

        //              SSN
        private string _SSN;
        public string SSN
        {
            get { return this._SSN; }
            set { this._SSN = value; }
        }

        //              Field
        private string _field;
        public string Field
        {
            get { return this._field; }
            set { this._field = value; }
        }

        //              Salary
        private long _salary;
        public long Salary
        {
            get { return this._salary; }
            set { this._salary = value; }
        }

        //              University
        private string _university;
        public string University
        {
            get { return this._university; }
            set { this._university = value; }
        }

        public Dentist(string firstName, string lastName, string SSN, string field,
            long salary, string university)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.SSN = SSN;
            this.Field = field;
            this.Salary = salary;
            this.University = university;
            Doctor.numberOfDotors++;
            this.DentistID = numberOfDotors;
        }

        //              Patients
        private List<Patient> _patients = new List<Patient>();
        public List<Patient> Patients
        {
            get { return this._patients; }
            set
            {
                foreach (Patient patient in value)
                {
                    this._patients.Add(patient);
                }
            }
        }
        static public bool operator >(Dentist dentist1, Dentist dentist2)
        {
            if (dentist1.Salary > dentist2.Salary)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        static public bool operator <(Dentist dentist1, Dentist dentist2)
        {
            if (dentist1.Salary < dentist2.Salary)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        static public string operator +(Dentist dentist, Patient patient)
        {
            if (patient.disease.Contains("Teeth"))
            {
                dentist.Patients.Add(patient);
                return "Patient added successfully!";
            }
            else if (patient.disease.Contains("Dental"))
            {
                dentist.Patients.Add(patient);
                return "Patient added successfully!";
            }
            else if (patient.disease.Contains("Toothache"))
            {
                dentist.Patients.Add(patient);
                return "Patient added successfully!";
            }
            else
            {
                return "Not Related Field";
            }
        }
        public string GraduatedFrom()
        {
            return this.FirstName + ' ' + this.LastName + " is graduated from " + this.University;
        }
        public string Work()
        {
            return "This dentist works on ";
        }
    }
    class Surgeon : Doctor, IDoctor, IPerson
    {
        static public List<Surgeon> surgeonsList = new List<Surgeon>();

        private string _firstName;
        public string FirstName
        {
            get { return this._firstName; }
            set { this._firstName = value; }
        }

        private string _lastName;
        public string LastName
        {
            get { return this._lastName; }
            set { this._lastName = value; }
        }

        private string _SSN;
        public string SSN
        {
            get { return this.SSN; }
            set { this._SSN = value; }
        }

        private int _SurgeonID;
        public int SurgeonID
        {
            get { return this._SurgeonID; }
            private set { this._SurgeonID = value; }
        }

        private string _field;
        public string Field
        {
            get { return this._field; }
            set { this._field = value; }
        }

        private long _salary;
        public long Salary
        {
            get { return this._salary; }
            set { this._salary = value; }
        }

        private string _university;
        public string University
        {
            get { return this._university; }
            set { this._university = value; }
        }

        public Surgeon(string firstName, string lastName, string SSN, string field,
            long salary, string university)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.SSN = SSN;
            this.Field = field;
            this.Salary = salary;
            this.University = university;
            Doctor.numberOfDotors++;
            this.SurgeonID = numberOfDotors;
        }

        private List<Patient> _patients = new List<Patient>();
        public List<Patient> Patients
        {
            get { return this._patients; }
            set
            {
                foreach (Patient patient in Patients)
                {
                    _patients.Add(patient);
                }
            }
        }

        static public string operator +(Surgeon surgeon, Patient patient)
        {
            if (patient.disease.Contains("Cancer"))
            {
                surgeon.Patients.Add(patient);
                return "Patient added sucessfully!";
            }
            else if (patient.disease.Contains("Appendix"))
            {
                surgeon.Patients.Add(patient);
                return "Patient added sucessfully!";
            }
            else if (patient.disease.Contains("Kidney"))
            {
                surgeon.Patients.Add(patient);
                return "Patient added sucessfully!";
            }
            else
            {
                return "This patient cannot be added to this doctor!";
            }
        }

        static public bool operator <(Surgeon surgeon1, Surgeon surgeon2)
        {
            if (surgeon1.Patients.Count < surgeon2.Patients.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static public bool operator >(Surgeon surgeon1, Surgeon surgeon2)
        {
            if (surgeon1.Patients.Count > surgeon2.Patients.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GraduatedFrom()
        {
            return this.FirstName + ' ' + this.LastName + ' ' + "is gratuated form" + ' ' + this.University;
        }

        public string Work()
        {
            return "This Surgeon works on " + this.Field;
        }
    }
    class Doctors
    {
        static public List<Doctor> DoctorList = new List<Doctor>();
        static public List<Patient> ListOfRecoveredPatient()
        {
            List<Patient> recoveredPatientsList = new List<Patient>();
            foreach (Doctor doctor in DoctorList)
            {
                if (doctor is GeneralPractitioner)
                {
                    GeneralPractitioner GP = (GeneralPractitioner)doctor;
                    foreach (Patient patient in GP.Patients)
                    {
                        if (patient.recovered == illness.healthy)
                        {
                            recoveredPatientsList.Add(patient);
                        }
                    }
                }
                else if (doctor is Dentist)
                {
                    Dentist dentist = (Dentist)doctor;
                    foreach (Patient patient in dentist.Patients)
                    {
                        if (patient.recovered == illness.healthy)
                        {
                            recoveredPatientsList.Add(patient);
                        }
                    }
                }
                else if (doctor is Surgeon)
                {
                    Surgeon surgeon = (Surgeon)doctor;
                    foreach (Patient patient in surgeon.Patients)
                    {
                        if (patient.recovered == illness.healthy)
                        {
                            recoveredPatientsList.Add(patient);
                        }
                    }
                }
            }
            return recoveredPatientsList;
        }
        
    }
}
