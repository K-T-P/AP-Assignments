using System;
using System.Collections.Generic;
using System.Linq;

namespace tamrin_seri_5_soal_2
{
    enum Sex { male, female, others }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string prompt = "\nMenu\nregister student\tregister professor\tmake unit\tadd student" +
                    "\nadd professor\tset student teaching assistant\tset student research assistant" +
                    "\nstudent status\tprofessor status\tunit status\n" +
                    "set final mark\tmark student\tmark list\taverage mark professor\n" +
                    "top student\nexit";
                while (true)
                {
                    try
                    {
                        Console.WriteLine(prompt);
                        string order = Console.ReadLine();
                        if (order == "register student") { register_student(); }
                        else if (order == "register professor") { register_professor(); }
                        else if (order == "make unit") { make_unit(); }
                        else if (order == "add student") { add_student(); }
                        else if (order == "add professor") { add_professor(); }
                        else if (order == "set student teaching assistant") { set_student_teaching_assistant(); }
                        else if (order == "set student research assistant") { set_student_research_assistant(); }
                        else if (order == "student status") { student_status(); }
                        else if (order == "professor status") { professor_status(); }
                        else if (order == "unit status") { unit_status(); }
                        else if (order == "set final mark") { set_final_matk(); }
                        else if (order == "mark student") { mark_student(); }
                        else if (order == "mark list") { mark_list(); }
                        else if (order == "average mark professor") { average_mark_professor(); }
                        else if (order == "top student") { top_student(); }
                        else if (order == "exit") { break; }
                        else { Console.WriteLine("Invalid input"); }
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Input is too big!");
                    }
                }
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("Not enough memory on the device! Program stopped!");
            }
            catch
            {
                Console.WriteLine("An Error occured! Program stopped!");
            }
        }
        //checked
        static public void register_student()
        {
            try
            {
                Console.Write("Please enter your name: ");
                string name = Console.ReadLine();
                if (name.Length > 20 || name.Length < 3)
                {
                    throw new Exception("InvalidNameFormat");
                }

                Console.Write("Please enter your SSN: ");
                string SSN = Console.ReadLine();
                if (SSN.Length != 10)
                {
                    throw new Exception("InvalidSSNFormat");
                }
                if (Student.studentGrp.Find(student => student.SSN == SSN) != null)
                {
                    throw new Exception("SSNAlreadyExist");
                }
                if (Professor.professorsGrp.Find(prof => prof.SSN == SSN) != null)
                {
                    throw new Exception("SSNAlreadyExist");
                }

                Console.Write("Please enter your entering year: ");
                int enteringYear = int.Parse(Console.ReadLine());
                if (enteringYear < 1350 || enteringYear > DateTime.Now.Year)
                {
                    throw new Exception("InvalidEnteringYearFormat");
                }

                Console.Write("Please enter your field: ");
                string field = Console.ReadLine();
                if (field.Length > 20 || field.Length < 3)
                {
                    throw new Exception("EnteredFieldHasInvalidFormat");
                }

                Console.Write("Please enter your gender(male, female, others): ");
                Sex gender = (Sex)Enum.Parse(typeof(Sex), Console.ReadLine(), true);
                Student newStudent = new Student(name, SSN, field, gender, enteringYear);
                Student.studentGrp.Add(newStudent);
                Console.WriteLine("Process finished sucessfully!");
            }
            catch (Exception error) when (error.Message == "EnteredFieldHasInvalidFormat")
            {
                Console.WriteLine("Entered field doesn't have valid format!");
            }
            catch (Exception error) when (error.Message == "InvalidEnteringYearFormat")
            {
                Console.WriteLine("Entering year is out of range!");
            }
            catch (Exception error) when (error.Message == "InvalidSSNFormat")
            {
                Console.WriteLine("Entered SSN doesn't have valid format!");
            }
            catch (Exception error) when (error.Message == "InvalidNameFormat")
            {
                Console.WriteLine("Entered name doesn't have valid format!");
            }
            catch (Exception error) when (error.Message == "SSNAlreadyExist")
            {
                Console.WriteLine("Entered SSN already exist!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input!");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Input is too big!");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid Sex!");
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
        //checked
        static public void register_professor()
        {
            try
            {
                Console.Write("Please enter your name: ");
                string name = Console.ReadLine();
                if (name.Length < 3 || name.Length > 20)
                {
                    throw new Exception("InvalidNameFormat");
                }

                Console.Write("Please enter your SSN: ");
                string SSN = Console.ReadLine();
                if (SSN.Length != 10)
                {
                    throw new Exception("InvalidSSNFormat");
                }
                if (Student.studentGrp.Find(student => student.SSN == SSN) != null)
                {
                    throw new Exception("SSNAlreadyExist");
                }
                if (Professor.professorsGrp.Find(prof => prof.SSN == SSN) != null)
                {
                    throw new Exception("SSNAlreadyExist");
                }

                Console.Write("Please enter your field: ");
                string field = Console.ReadLine();
                if (field.Length < 3 || field.Length > 20)
                {
                    Console.WriteLine("InvalidFieldFormat");
                }

                Console.Write("Please enter your gender(male, female, others): ");
                Sex gender = (Sex)Enum.Parse(typeof(Sex), Console.ReadLine(), true);

                Console.Write("Please enter the minimum number of hours per week" +
                    " for Research assistants(it has to be an integer): ");
                int minTRA = int.Parse(Console.ReadLine());
                if (minTRA < 0 || minTRA > 168)
                {
                    throw new Exception("InvalidHourFormat");
                }

                int randomNumber;
                while (true)
                {
                    Random rand = new Random();
                    randomNumber = Math.Abs((rand.Next() % 1000)) + 1;
                    if (Professor.professorsGrp.Find(prof => prof.roomNumber == randomNumber) != null)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                Professor newProfessor = new Professor(name, SSN, field, gender, minTRA, randomNumber);
                Professor.professorsGrp.Add(newProfessor);
                Console.WriteLine("Process finished successfully!");
            }
            catch (Exception error) when (error.Message == "InvalidHourFormat")
            {
                Console.WriteLine("Entered hour is invalid!");
            }
            catch (Exception error) when (error.Message == "InvalidFieldFormat")
            {
                Console.WriteLine("Entered field doesn't have valid format!");
            }
            catch (Exception error) when (error.Message == "InvalidSSNFormat")
            {
                Console.WriteLine("Entered SSN doesn't have valid format!");
            }
            catch (Exception error) when (error.Message == "InvalidNameFormat")
            {
                Console.WriteLine("Entered name doesn't have valid format!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input!");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Input is too big!");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid gender!");
            }
            catch (Exception error) when (error.Message == "SSNAlreadyExist")
            {
                Console.WriteLine("Entered SSN already exist!");
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
        //ckecked
        static public void make_unit()
        {
            try
            {
                Console.Write("Please enter the ID of the unit: ");
                int unitID = int.Parse(Console.ReadLine());
                if (Unit.unitList.Find(unit => unit.unitld == unitID) != null)
                {
                    throw new Exception("IDAlreadyUsed");
                }

                Console.Write("Please enter the name of the unit: ");
                string name = Console.ReadLine();
                if (name.Length > 20 || name.Length < 3)
                {
                    throw new Exception("InvalidNameFormat");
                }

                Console.Write("Please enter the field of the unit: ");
                string field = Console.ReadLine();
                if (field.Length > 20 || field.Length < 3)
                {
                    throw new Exception("InvalidFieldFormat");
                }

                Console.Write("Please enter the maximum size of the unit: ");
                int maxSize = int.Parse(Console.ReadLine());
                if (maxSize < 10 | maxSize > 180)
                {
                    throw new Exception("WrongMaxSize");
                }

                Unit newUnit = new Unit(name, unitID, field, maxSize);
                Unit.unitList.Add(newUnit);
                Console.WriteLine("Unit added successfully!");
            }
            catch (Exception error) when (error.Message == "InvalidFieldFormat")
            {
                Console.WriteLine("Entered field doesn't have valid format!");
            }
            catch (Exception error) when (error.Message == "InvalidNameFormat")
            {
                Console.WriteLine("Entered name doesn't have valid format!");
            }
            catch (Exception error) when (error.Message == "WrongMaxSize")
            {
                Console.WriteLine("The maximum size is invalid!");
            }
            catch (Exception error) when (error.Message == "IDAlreadyUsed")
            {
                Console.WriteLine("This ID has already used!");
            }
            catch (FormatException)
            {
                Console.WriteLine("Input is invalid!");
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
        static public void add_student()
        {
            try
            {
                Console.Write("Please enter the SSN of the student: ");
                string SSN = Console.ReadLine();
                int indexOfStudent = 0;
                if (Student.studentGrp.Find(student => student.SSN == SSN) == null)
                {
                    throw new Exception("SSNWasNotFound");
                }
                else
                {
                    indexOfStudent = Student.studentGrp.FindIndex(student => student.SSN == SSN);
                }

                Console.Write("Please enter the ID of the unit: ");
                int unitID = int.Parse(Console.ReadLine());
                int indexOfUnit = 0;
                if (Unit.unitList.Find(unit => unit.unitld == unitID) == null)
                {
                    throw new Exception("EnteredIDWasNotFound");
                }
                else
                {
                    indexOfUnit = Unit.unitList.FindIndex(unit => unit.unitld == unitID);
                }

                if (Unit.unitList.Find(unit => unit.unitld == unitID).field != Student.studentGrp.Find(student => student.SSN == SSN).field)
                {
                    throw new Exception("FieldsNotMatch");
                }

                if (Unit.unitList[indexOfUnit].students.Find(student => student.SSN == SSN) != null)
                {
                    throw new Exception("AlreadyAssigned");
                }

                if (Unit.unitList[indexOfUnit].students.Count >= Unit.unitList[indexOfUnit].maxSize)
                {
                    throw new Exception("UnitAlreadyFull");
                }

                Unit.unitList[indexOfUnit].students.Add(Student.studentGrp[indexOfStudent]);
                Console.WriteLine("Student added!");
            }
            catch (Exception error) when (error.Message == "UnitAlreadyFull")
            {
                Console.WriteLine("This unit is already full!");
            }
            catch (Exception error) when (error.Message == "AlreadyAssigned")
            {
                Console.WriteLine("This student has already assigned!");
            }
            catch (Exception error) when (error.Message == "FieldsNotMatch")
            {
                Console.WriteLine("The field of the student and the field of the unit does not match eachother!");
            }
            catch (Exception error) when (error.Message == "EnteredIDWasNotFound")
            {
                Console.WriteLine("Entered Unit was not found!");
            }
            catch (Exception error) when (error.Message == "SSNWasNotFound")
            {
                Console.WriteLine("Entered SSN was not found!");
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
        //checked
        static public void add_professor()
        {
            try
            {
                Console.Write("Please enter professor's SSN : ");
                string SSN = Console.ReadLine();
                int indexOfProfessor = 0;
                if (Professor.professorsGrp.Find(prof => prof.SSN == SSN) == null)
                {
                    throw new Exception("SSNWasNotFound");
                }
                else
                {
                    indexOfProfessor = Professor.professorsGrp.FindIndex(prof => prof.SSN == SSN);
                }

                Console.Write("Please enter the ID of the unit : ");
                int ID = int.Parse(Console.ReadLine());
                int indexOfUnit = 0;
                if (Unit.unitList.Find(unit => unit.unitld == ID) == null)
                {
                    throw new Exception("EnteredUnitWasNotFound");
                }
                else
                {
                    indexOfUnit = Unit.unitList.FindIndex(unit => unit.unitld == ID);
                }

                if (Professor.professorsGrp[indexOfProfessor].field != Unit.unitList[indexOfUnit].field)
                {
                    throw new Exception("FieldNotMatch");
                }

                if (Unit.unitList[indexOfUnit].professorSSN != "")
                {
                    throw new Exception("UnitAlreadyHasProfessor");
                }

                Unit.unitList[indexOfUnit].professorSSN = SSN;
                Professor.professorsGrp[indexOfProfessor].teachingUnits.Add(Unit.unitList[indexOfUnit]);
                Console.WriteLine("Process finished successfully!");
            }
            catch (Exception error) when (error.Message == "UnitAlreadyHasProfessor")
            {
                Console.WriteLine("This unit already has a professor!");
            }
            catch (Exception error) when (error.Message == "FieldNotMatch")
            {
                Console.WriteLine("Fields does not match!");
            }
            catch (Exception error) when (error.Message == "EnteredUnitWasNotFound")
            {
                Console.WriteLine("Entered unit does not exist!");
            }
            catch (Exception error) when (error.Message == "SSNWasNotFound")
            {
                Console.WriteLine("Entered professor was not found!");
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
        //checked
        static public void set_student_teaching_assistant()
        {
            try
            {
                Console.Write("Please enter the SSN of the student: ");
                string SSN = Console.ReadLine();
                int indexOfStudent = 0;
                if (Student.studentGrp.Find(student => student.SSN == SSN) == null)
                {
                    throw new Exception("SSNWasNotFound");
                }
                else
                {
                    indexOfStudent = Student.studentGrp.FindIndex(student => student.SSN == SSN);
                }

                Console.Write("Please enter the ID of the unit: ");
                int unitID = int.Parse(Console.ReadLine());
                int indexOfUnit = 0;
                if (Unit.unitList.Find(unit => unit.unitld == unitID) == null)
                {
                    throw new Exception("UnitWasNotFound");
                }
                else
                {
                    indexOfUnit = Unit.unitList.FindIndex(unit => unit.unitld == unitID);
                }

                if (Student.studentGrp[indexOfStudent].field != Unit.unitList[indexOfUnit].field)
                {
                    throw new Exception("FieldsNotMatch");
                }

                if (Unit.unitList[indexOfUnit].professorSSN == "")
                {
                    throw new Exception("FieldDoesNotHaveProfessor");
                }
                if (Student.studentGrp[indexOfStudent].acceptedRoleRatherThanStudentOrNot == true)
                {
                    throw new Exception("HadRoleBefore");
                }

                Student.studentGrp[indexOfStudent].acceptedRoleRatherThanStudentOrNot = true;
                TeacherAssiatant newTassis = new TeacherAssiatant(
                    Student.studentGrp[indexOfStudent].name,
                    Student.studentGrp[indexOfStudent].SSN,
                    Student.studentGrp[indexOfStudent].field,
                    Student.studentGrp[indexOfStudent].gender,
                    Student.studentGrp[indexOfStudent].enteringYear,
                    unitID);

                TeacherAssiatant.teachingAssistantGrp.Add(newTassis);
                Unit.unitList[indexOfUnit].teachingAssistants.Add(newTassis);
                Professor.professorsGrp.Find(prof => prof.SSN == Unit.unitList[indexOfUnit].professorSSN).TA_Grp.Add(newTassis);

                Console.WriteLine("Process done successfully!");
            }
            catch (Exception error) when (error.Message == "HadRoleBefore")
            {
                Console.WriteLine("This student had a role before!");
            }
            catch (Exception error) when (error.Message == "FieldDoesNotHaveProfessor")
            {
                Console.WriteLine("This field does not have a professor!");
            }
            catch (Exception error) when (error.Message == "FieldsNotMatch")
            {
                Console.WriteLine("Invalid field!");
            }
            catch (Exception error) when (error.Message == "UnitWasNotFound")
            {
                Console.WriteLine("Entered unit was not found!");
            }
            catch (Exception error) when (error.Message == "SSNWasNotFound")
            {
                Console.WriteLine("Entered student was not found!");
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
        //checked
        static public void set_student_research_assistant()
        {
            try
            {
                Console.Write("Please enter student's SSN: ");
                string SSN_Student = Console.ReadLine();
                int indexOfStudent = 0;
                if (Student.studentGrp.Find(student => student.SSN == SSN_Student) == null)
                {
                    throw new Exception("StudentWasNotFound");
                }
                else
                {
                    indexOfStudent = Student.studentGrp.FindIndex(student => student.SSN == SSN_Student);
                }

                Console.Write("Please enter professor's SSN: ");
                string SSN_Professor = Console.ReadLine();
                int indexOfProfessor = 0;
                if (Professor.professorsGrp.Find(prof => prof.SSN == SSN_Professor) == null)
                {
                    throw new Exception("ProfessorWasNotFound");
                }
                else
                {
                    indexOfProfessor = Professor.professorsGrp.FindIndex(prof => prof.SSN == SSN_Professor);
                }
                if (Student.studentGrp[indexOfStudent].field != Professor.professorsGrp[indexOfProfessor].field)
                {
                    throw new Exception("FieldNotMatch");
                }
                Console.Write("Please enter the name of the project: ");
                string projectName = Console.ReadLine();
                if (projectName.Length > 30 || projectName.Length < 1)
                {
                    throw new Exception("InvalidProjectName");
                }

                Console.Write("Please enter the free time in the week: ");
                int freeTimeInWeek = int.Parse(Console.ReadLine());
                if (freeTimeInWeek < 0 || freeTimeInWeek > 168)
                {
                    throw new Exception("InvalidTimeInWeek");
                }
                if (freeTimeInWeek < Professor.professorsGrp[indexOfProfessor].minTRA)
                {
                    throw new Exception("NotEnoughFreeTime");
                }
                if (Student.studentGrp[indexOfStudent].acceptedRoleRatherThanStudentOrNot == true)
                {
                    throw new Exception("CannotRoleAnotherRole");
                }
                ReasearchAssistant newRA = new ReasearchAssistant(
                    Student.studentGrp[indexOfStudent].name,
                    Student.studentGrp[indexOfStudent].SSN,
                    Student.studentGrp[indexOfStudent].field,
                    Student.studentGrp[indexOfStudent].gender,
                    Student.studentGrp[indexOfStudent].enteringYear,
                    projectName,
                    freeTimeInWeek,
                    Professor.professorsGrp[indexOfProfessor].SSN);

                ReasearchAssistant.RA_Grp.Add(newRA);
                Student.studentGrp[indexOfStudent].acceptedRoleRatherThanStudentOrNot = true;

                Console.WriteLine("Process finished successfully!");
            }
            catch (Exception error) when (error.Message == "NotEnoughFreeTime")
            {
                Console.WriteLine("Student doesn't have enough free time in a week!");
            }
            catch (Exception error) when (error.Message == "FieldNotMatch")
            {
                Console.WriteLine("Fields don't match!");
            }
            catch (Exception error) when (error.Message == "InvalidProjectName")
            {
                Console.WriteLine("Invalid Project Name!");
            }
            catch (Exception error) when (error.Message == "CannotRoleAnotherRole")
            {
                Console.WriteLine("This student cannot be research assiatant!");
            }
            catch (Exception error) when (error.Message == "InvalidTimeInWeek")
            {
                Console.WriteLine("Entered time is invalid!");
            }
            catch (Exception error) when (error.Message == "ProfessorWasNotFound")
            {
                Console.WriteLine("Entered professor was not found!");
            }
            catch (Exception error) when (error.Message == "StudentWasNotFound")
            {
                Console.WriteLine("Entered student was not found!");
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
        //checked
        static public void student_status()
        {
            try
            {
                Console.Write("Please enter student's SSN: ");
                string SSN = Console.ReadLine();

                if (Student.studentGrp.Find(stu => stu.SSN == SSN) == null)
                {
                    throw new Exception("StudentNotFound");
                }
                else
                {
                    int indexOfStudent = Student.studentGrp.FindIndex(stu => stu.SSN == SSN);
                    Console.WriteLine(
                        "Student name: " + Student.studentGrp[indexOfStudent].name +
                        "\nStudent gender: " + Student.studentGrp[indexOfStudent].gender +
                        "\nStudent entering year: " + Student.studentGrp[indexOfStudent].enteringYear +
                        "\nStudent field: " + Student.studentGrp[indexOfStudent].field);
                    List<Unit> unitGrp = new List<Unit>();
                    for (int i = 0; i < Unit.unitList.Count; i++)
                    {
                        if (Unit.unitList[i].students.Find(stu => stu.SSN == SSN) != null)
                        {
                            unitGrp.Add(Unit.unitList[i]);
                        }
                    }
                    Console.WriteLine("The name of the units that the student participates: ");
                    foreach (Unit unit in unitGrp)
                    {
                        Console.Write(' ' + unit.name);
                    }
                    Console.WriteLine();
                    if (TeacherAssiatant.teachingAssistantGrp.Find(TA => TA.SSN == SSN) != null)
                    {
                        Console.WriteLine("The student also works as Teacher Assistant in these units (the ID of the units are mentioned) : ");
                        foreach (TeacherAssiatant TA in TeacherAssiatant.teachingAssistantGrp.FindAll(TA => TA.SSN == SSN))
                        {
                            Console.Write(' ' + TA.unitld);
                        }
                        Console.WriteLine();
                    }
                    else if (ReasearchAssistant.RA_Grp.Find(RA => RA.SSN == SSN) != null)
                    {
                        Console.WriteLine("The student also works as Research Assiatant in these projects: ");
                        foreach (ReasearchAssistant RA in ReasearchAssistant.RA_Grp.FindAll(RA => RA.SSN == SSN))
                        {
                            Console.WriteLine("Project name: " + RA.projectName + "Professor SSN: " + RA.professorSSN + "Free time: " + RA.freeTime);
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception error) when (error.Message == "StudentNotFound")
            {
                Console.WriteLine("Entered student was not found!");
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
        static public void professor_status()
        {
            try
            {
                Console.Write("Please enter the professor's SSN: ");
                string SSN = Console.ReadLine();

                if (Professor.professorsGrp.Find(prof => prof.SSN == SSN) == null)
                {
                    throw new Exception("ProfessorNotFound");
                }
                else
                {
                    int indexOfProfessor = Professor.professorsGrp.FindIndex(prof => prof.SSN == SSN);
                    Console.WriteLine(
                        "Professor name: " + Professor.professorsGrp[indexOfProfessor].name +
                        "\nProfessor field: " + Professor.professorsGrp[indexOfProfessor].field +
                        "\nProfessor room number: " + Professor.professorsGrp[indexOfProfessor].roomNumber +
                        "\nProfessor minimum time for researcher assistants: " + Professor.professorsGrp[indexOfProfessor].minTRA);

                    if (Professor.professorsGrp[indexOfProfessor].teachingUnits.Count > 0)
                    {
                        Console.WriteLine("The name of the units which the professor teachs: ");
                        foreach (Unit unit in Professor.professorsGrp[indexOfProfessor].teachingUnits)
                        {
                            Console.Write(unit.name + ' ');
                        }
                        Console.WriteLine();
                    }
                    if (Professor.professorsGrp[indexOfProfessor].TA_Grp.Count > 0)
                    {
                        Console.WriteLine("Teacher assistants' names of this professor: ");
                        foreach (TeacherAssiatant TA in Professor.professorsGrp[indexOfProfessor].TA_Grp)
                        {
                            Console.Write(TA.name + ' ');
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("Process finished successfully!");
                }
            }
            catch (Exception error) when (error.Message == "ProfessorNotFound")
            {
                Console.WriteLine("Entered professor was not found!");
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
        static public void unit_status()
        {
            try
            {
                Console.Write("Please enter the ID of the unit: ");
                int unitID = int.Parse(Console.ReadLine());

                if (Unit.unitList.Find(unit => unit.unitld == unitID) == null)
                {
                    throw new Exception("UnitNotFound");
                }
                else
                {
                    Unit unit = Unit.unitList.Find(unit => unit.unitld == unitID);
                    if (unit.professorSSN == "")
                    {
                        Console.WriteLine("None");
                    }
                    else
                    {
                        Console.WriteLine("Professor's name: ");
                        Console.WriteLine(Professor.professorsGrp.Find(prof => prof.SSN == unit.professorSSN).name);
                    }
                    if (unit.students.Count > 0)
                    {
                        Console.WriteLine("Students' name: ");
                        foreach (Student stu in unit.students)
                        {
                            Console.Write(stu.name + ' ');
                        }
                        Console.WriteLine();
                    }
                    if (unit.teachingAssistants.Count > 0)
                    {
                        Console.WriteLine("Teacher assistants' name:");
                        foreach (TeacherAssiatant TA in unit.teachingAssistants)
                        {
                            Console.Write(TA.name + ' ');
                        }
                        Console.WriteLine();
                    }
                }
                Console.WriteLine("Process finished successfully!");
            }
            catch (Exception error) when (error.Message == "UnitNotFound")
            {
                Console.WriteLine("Entered unit was not found!");
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
                Console.WriteLine("An Error occured!");
            }
        }
        //checked
        static public void set_final_matk()
        {
            try
            {
                Console.Write("Please enter the professor's SSN: ");
                string professorSSN = Console.ReadLine();
                int indexOfProfessor = 0;
                if (Professor.professorsGrp.Find(prof => prof.SSN == professorSSN) == null)
                {
                    throw new Exception("ProfessorWasNotFound");
                }
                else
                {
                    indexOfProfessor = Professor.professorsGrp.FindIndex(prof => prof.SSN == professorSSN);
                }

                Console.Write("Please enter the student's SSN: ");
                string studentSSN = Console.ReadLine();
                int indexOfStudent = 0;
                if (Student.studentGrp.Find(stu => stu.SSN == studentSSN) == null)
                {
                    throw new Exception("StudentWasNotFound");
                }
                else
                {
                    indexOfStudent = Student.studentGrp.FindIndex(stu => stu.SSN == studentSSN);
                }

                Console.Write("Please enter the ID of the unit: ");
                int unitID = int.Parse(Console.ReadLine());
                int indexOfUnit = 0;
                if (Unit.unitList.Find(unit => unit.unitld == unitID) == null)
                {
                    throw new Exception("UnitWasNotFound");
                }
                else
                {
                    indexOfUnit = Unit.unitList.FindIndex(unit => unit.unitld == unitID);
                }

                if (Unit.unitList[indexOfUnit].professorSSN != Professor.professorsGrp[indexOfProfessor].SSN)
                {
                    throw new Exception("ProfessorDoesNotTeachThisUnit");
                }
                if (Unit.unitList[indexOfUnit].students.Contains(Student.studentGrp[indexOfStudent]) == false)
                {
                    throw new Exception("StudentHasNotTakenThisUnit");
                }

                Console.Write("Please enter the mark of the student: ");
                double mark = double.Parse(Console.ReadLine());
                if (mark < 0 || mark > 20)
                {
                    throw new Exception("InvalidMark");
                }

                if (Student.studentGrp[indexOfStudent].listOfStudentsMark.Find(mark => mark.unit == Unit.unitList[indexOfUnit]) == null)
                {
                    Mark newMark = new Mark(Unit.unitList[indexOfUnit], Student.studentGrp[indexOfStudent], mark, Professor.professorsGrp[indexOfProfessor]);
                    Student.studentGrp[indexOfStudent].listOfStudentsMark.Add(newMark);
                    Mark.allMarks.Add(newMark);
                    Unit.unitList[indexOfUnit].markList.Add(newMark);
                }
                else
                {
                    Student.studentGrp[indexOfStudent].listOfStudentsMark.Find(mark => mark.unit == Unit.unitList[indexOfUnit]).finalMark = mark;
                }

                Console.WriteLine("Process finished successfully!");
            }
            catch (Exception error) when (error.Message == "InvalidMark")
            {
                Console.WriteLine("Entered mark is invalid!");
            }
            catch (Exception error) when (error.Message == "StudentHasNotTakenThisUnit")
            {
                Console.WriteLine("This student didn't registered in this unit!");
            }
            catch (Exception error) when (error.Message == "ProfessorDoesNotTeachThisUnit")
            {
                Console.WriteLine("This professor does not teach this unit!");
            }
            catch (Exception error) when (error.Message == "UnitWasNotFound")
            {
                Console.WriteLine("Entered unit was not found!");
            }
            catch (Exception error) when (error.Message == "StudentWasNotFound")
            {
                Console.WriteLine("Entered student was not found!");
            }
            catch (Exception error) when (error.Message == "ProfessorWasNotFound")
            {
                Console.WriteLine("Entered professor was not found!");
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
                Console.WriteLine("An Error occured!");
            }
        }
        //checked
        static public void mark_student()
        {
            try
            {
                Console.Write("Please enter the student's ID: ");
                string studentSSN = Console.ReadLine();
                Student student = null;
                if (Student.studentGrp.Find(student => student.SSN == studentSSN) == null)
                {
                    throw new Exception("EnteredStudentWasNotFound");
                }
                else
                {
                    student = Student.studentGrp.Find(student => student.SSN == studentSSN);
                }

                Console.Write("Please enter the ID of the unit: ");
                int unitID = int.Parse(Console.ReadLine());
                Unit unit = null;
                if (Unit.unitList.Find(unit => unit.unitld == unitID) == null)
                {
                    throw new Exception("EnteredUnitWasNotFound");
                }
                else
                {
                    unit = Unit.unitList.Find(unit => unit.unitld == unitID);
                }

                if (unit.students.Contains(student) == false)
                {
                    throw new Exception("StudentWasNotRegistered");
                }
                if (student.listOfStudentsMark.Find(mark => mark.unit == unit) == null)
                {
                    Console.WriteLine("None");
                }
                else
                {
                    Console.WriteLine(student.listOfStudentsMark.FindLast(mark => mark.unit == unit).finalMark);
                }
                Console.WriteLine("Process finished successfully!");
            }
            catch (Exception error) when (error.Message == "StudentWasNotRegistered")
            {
                Console.WriteLine("Entered student hasn't registered in this unit!");
            }
            catch (Exception error) when (error.Message == "EnteredUnitWasNotFound")
            {
                Console.WriteLine("Entered unit was not found!");
            }
            catch (Exception error) when (error.Message == "EnteredStudentWasNotFound")
            {
                Console.WriteLine("Entered student was not found!");
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
        //checked
        static public void mark_list()
        {
            try
            {
                Console.Write("Please enter the ID of the unit: ");
                int unitID = int.Parse(Console.ReadLine());
                Unit unit = null;
                if (Unit.unitList.Find(unit => unit.unitld == unitID) == null)
                {
                    throw new Exception("UnitWasNotFound");
                }
                else
                {
                    unit = Unit.unitList.Find(unit => unit.unitld == unitID);
                }

                if (unit.students.Count == 0)
                {
                    Console.WriteLine("no student");
                }
                else
                {
                    unit.students.OrderBy(student => student.name).ToList();
                    foreach (Student stu in unit.students)
                    {
                        if (stu.listOfStudentsMark.Find(mark => mark.unit == unit) == null)
                        {
                            Console.WriteLine(stu.name + " : No mark entered");
                        }
                        else
                        {
                            Mark studentMark = stu.listOfStudentsMark.Find(mark => mark.unit == unit);
                            Console.WriteLine(stu.name + " : " + studentMark.finalMark);
                        }
                    }
                }
                Console.WriteLine("Process finished successfully!");
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
        //checked
        static public void average_mark_professor()
        {
            try
            {
                Console.Write("Please enter professor's SSN: ");
                string professorSSN = Console.ReadLine();

                if (Professor.professorsGrp.Find(prof => prof.SSN == professorSSN) == null)
                {
                    throw new Exception("EnteredProfessorWasNotFound");
                }

                double sumOfMarks = 0;
                double numberOfMarks = 0;
                foreach (Student student in Student.studentGrp)
                {
                    if (student.listOfStudentsMark.Find(mark => mark.professor.SSN == professorSSN) != null)
                    {
                        sumOfMarks += student.listOfStudentsMark.FindLast(mark => mark.professor.SSN == professorSSN).finalMark;
                        numberOfMarks++;
                    }
                }
                if (numberOfMarks == 0)
                {
                    Console.WriteLine("None");
                }
                else
                {
                    Console.WriteLine(Math.Round((Double)(sumOfMarks / numberOfMarks), 2));
                }
                Console.WriteLine("Process finished successfully!");
            }
            catch (Exception error) when (error.Message == "EnteredProfessorWasNotFound")
            {
                Console.WriteLine("Entered professor was not found!");
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
        static public void top_student()
        {
            try
            {
                Console.Write("Please enter student's field: ");
                string studentField = Console.ReadLine();

                Console.Write("Please enter student's entering year: ");
                int enteringYear = int.Parse(Console.ReadLine());

                List<Student> stuGrp;//= new List<Student>();
                if (Student.studentGrp.Find(stu => stu.field == studentField && stu.enteringYear == enteringYear) == null)
                {
                    throw new Exception("EnteredStudentNotFound");
                }
                else
                {
                    stuGrp = Student.studentGrp.FindAll(stu => stu.field == studentField && stu.enteringYear == enteringYear);
                }

                List<studentAverageMark> stuMarkGrp = new List<studentAverageMark>();
                foreach (Student stu in stuGrp)
                {
                    double[] marks = new double[stu.listOfStudentsMark.Count];
                    for (int i = 0; i < stu.listOfStudentsMark.Count; i++)
                    {
                        marks[i] = stu.listOfStudentsMark[i].finalMark;
                    }
                    studentAverageMark a = new studentAverageMark(averageCalculater(marks), stu);
                    stuMarkGrp.Add(a);
                }
                stuMarkGrp.OrderByDescending(x => x.averageMark);
                if (stuMarkGrp.Count == 1)
                {
                    Console.WriteLine("Student name: " + stuMarkGrp[0].student.name + "\tAverage mark: " + stuMarkGrp[0].averageMark);
                }
                else
                {
                    if (stuMarkGrp[0].averageMark > stuMarkGrp[1].averageMark)
                    {
                        Console.WriteLine("Student name: " + stuMarkGrp[0].student.name + "\tAverage mark: " + stuMarkGrp[0].averageMark);
                    }
                    else
                    {
                        List<studentAverageMark> stuList = new List<studentAverageMark>();
                        for (int i = 0; i < stuMarkGrp.Count; i++)
                        {
                            if (stuMarkGrp[i].averageMark == stuMarkGrp[i + 1].averageMark)
                            {
                                stuList.Add(stuMarkGrp[i]);
                            }
                            else
                            {
                                stuList.Add(stuMarkGrp[i]);
                                break;
                            }
                        }
                        stuList.OrderByDescending(x => x.averageMark);
                        foreach (studentAverageMark stu in stuList)
                        {
                            Console.WriteLine("Student name: " + stu.student.name + "\tAverage mark: " + stu.averageMark);
                        }
                    }
                }
            }
            catch (Exception error) when (error.Message == "EnteredStudentNotFound")
            {
                Console.WriteLine("None");
            }
            catch (FormatException)
            {
                Console.WriteLine("Input is invalid!");
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
        static public double averageCalculater(double[] numbers)
        {
            double sumOfNumbers = 0;
            double finalAverage = 0;
            foreach (double number in numbers)
            {
                sumOfNumbers += number;
            }
            finalAverage = (sumOfNumbers / numbers.Length);
            finalAverage = Math.Round((Double)sumOfNumbers, 2);
            return finalAverage;
        }
    }
    class Person
    {
        static public List<Person> personGrp = new List<Person>();
        public string name;
        public string SSN;
        public string field;
        public Sex gender;
        public Person(
            string name,
            string SSN,
            string field,
            Sex gender
            )
        {
            this.name = name;
            this.SSN = SSN;
            this.field = field;
            this.gender = gender;
        }
    }
    class Professor : Person
    {
        static public List<Professor> professorsGrp = new List<Professor>();
        public List<Unit> teachingUnits = new List<Unit>();
        public List<TeacherAssiatant> TA_Grp = new List<TeacherAssiatant>();
        public int roomNumber;
        public int minTRA;
        public Professor(string name, string SSN, string field,
            Sex gender, int minTRA, int roomNumber) : base(name, SSN, field, gender)
        {
            this.roomNumber = roomNumber;
            this.minTRA = minTRA;
        }

    }
    class Student : Person
    {
        static public List<Student> studentGrp = new List<Student>();
        public List<Mark> listOfStudentsMark = new List<Mark>();
        public int enteringYear;
        public bool acceptedRoleRatherThanStudentOrNot = false;
        public Student(string name, string SSN, string field, Sex gender, int enteringYear) : base(name, SSN, field, gender)
        {
            this.enteringYear = enteringYear;
        }
    }
    class TeacherAssiatant : Student
    {
        static public List<TeacherAssiatant> teachingAssistantGrp = new List<TeacherAssiatant>();
        public int unitld;
        public TeacherAssiatant(string name, string SSN, string field, Sex gender, int enteringYear, int unitld) : base(name, SSN, field, gender, enteringYear)
        {
            this.unitld = unitld;
        }
    }
    class ReasearchAssistant : Student
    {
        static public List<ReasearchAssistant> RA_Grp = new List<ReasearchAssistant>();
        public string projectName;
        public int freeTime;
        public string professorSSN;
        public ReasearchAssistant(string name, string SSN, string field, Sex gender,
            int enteringYear, string projectName, int freeTime, string professorSSN) :
            base(name, SSN, field, gender, enteringYear)
        {
            this.projectName = projectName;
            this.freeTime = freeTime;
            this.professorSSN = professorSSN;
        }
    }
    class Unit
    {
        public List<TeacherAssiatant> teachingAssistants = new List<TeacherAssiatant>();
        static public List<Unit> unitList = new List<Unit>();
        public List<Student> students = new List<Student>();
        public List<Mark> markList = new List<Mark>();
        public int unitld;
        public string name;
        public string field;
        public int maxSize;
        public string professorSSN = "";
        public Unit(string name, int unitID, string field, int maxSize)
        {
            this.name = name;
            this.field = field;
            this.maxSize = maxSize;
            this.unitld = unitID;
        }
    }
    class Mark
    {
        static public List<Mark> allMarks = new List<Mark>();
        public Unit unit;
        public Student student;
        public Professor professor;
        public double finalMark;
        public Mark(Unit unit, Student student, double finalMark, Professor professor)
        {
            this.unit = unit;
            this.finalMark = finalMark;
            this.student = student;
            this.professor = professor;
        }
    }
    struct studentAverageMark
    {
        public double averageMark;
        public Student student;
        public studentAverageMark(double averageMark, Student student)
        {
            this.averageMark = averageMark;
            this.student = student;
        }
    }
}

