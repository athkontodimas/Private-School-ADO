using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Processing_Services;
using Entities;

namespace Assignment_1_Part_B_Athanasios_Kontodimas
{
    class Menou
    {
        // =================  Μέθοδος για εισαγωγη στοιχείων από το χρήστη ================
        public static void InputData()
        {
            try
            {
                string input;
                do
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(new string('~', 50));
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("  -- ");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("  If you want to enter ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("student ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("data, press ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("1");
                    Console.WriteLine();

                    Console.Write("  -- ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("  If you want to enter ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("trainer ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("data, press ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("2");
                    Console.WriteLine();

                    Console.Write("  -- ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("  If you want to enter ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("course ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("data, press ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("3");
                    Console.WriteLine();

                    Console.Write("  -- ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("  If you want to enter ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("assignment ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("data, press ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("4");
                    Console.WriteLine();

                    Console.Write("  -- ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("  If you want to ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("enroll student to a course");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" , press ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("5");
                    Console.WriteLine();

                    Console.Write("  -- ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("  If you want to ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("add trainer to a course ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(", press ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("6");
                    Console.WriteLine();

                    Console.Write("  -- ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("  If you want to ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("give an assignment to a student ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(", press ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("7");
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(new string('~', 50));
                    Console.ForegroundColor = ConsoleColor.White;
                    string choice = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(choice) || !(int.Parse(choice) > 0) || !(int.Parse(choice) < 8)) //ελεγχος αν δωσει λαθος αριθμο ή κενο χαρακτηρα ή τιποτα
                    {
                        Console.WriteLine("\nPlease give a number between 1-7 from the following menu\n");
                        input = "Y";
                        continue;
                    }


                    switch (choice)
                    {
                        case "1":
                            Services.InputStudent();
                            break;
                        case "2":
                            Services.InputTrainer();
                            break;
                        case "3":
                            Services.InputCourse();
                            break;
                        case "4":
                            Services.InputAssignment();
                            break;
                        case "5":
                            Services.InputStudentsPerCourse();
                            break;
                        case "6":
                            Services.InputTrainerInCourse();
                            break;
                        case "7":
                            Services.InputAssignmentInCourseToStudent();
                            break;
                    }

                    input = continueGivingData();//---- ASK USER TO CONTINUE GIVING DATA

                } while (input == "Y"); //end do while


            } //END OF TRY

            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        // Μεθοδος που ρωταει το χρήστη αν θα συνεχισει να εισαγει δεδομενα -----------------------------------
        private static string continueGivingData()
        {
            string input;
            string valid;   // flag για τον ελεγχο του εσωτερικου while

            do
            {
                Console.WriteLine("Do you want to add more data? Y/N");
                input = Console.ReadLine().ToUpper();

                if (string.IsNullOrWhiteSpace(input) || !(input == "Y" || input == "N"))
                {
                    Console.WriteLine("Answer with Y or N");
                    valid = "Y";
                }
                else { valid = "N"; }// end else

                Console.Clear();


            } while (valid == "Y");//end do while
            return input;
        }

        //Ζηταει απο το χρηστη αν θελει να ζητησει κατι αλλο από το βασικό μενού επιλογών ή να φυγει τελειως απο το πρόγραμμα
        public static string AskUserToContinueInTheProgram()
        {
            string inputMenu;
            bool invalidInput;

            do
            {
                Console.WriteLine("Do you want to return to Menu? Y/N");
                inputMenu = Console.ReadLine().ToUpper();
                Console.WriteLine(new string('=', 100) + "\n");
                if (string.IsNullOrWhiteSpace(inputMenu) || ((inputMenu != "Y") && (inputMenu != "N")))   //if user gives y or n, condition's second half would return the  of true ("false") and proceed to "else"
                {
                    invalidInput = true;
                    Console.WriteLine("Please give a Y or N answer");
                }
                else
                {
                    invalidInput = false;
                    Console.Clear();
                    Console.WriteLine();
                }
            } while (invalidInput);
            return inputMenu;
        }

        //--------------------------------------------MENOU-------------------------------------------------------------
        public static void Menu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\t\t\t M E N U ");
            Console.WriteLine("Welcome to our school. What would you like to do?");
            Console.WriteLine(new string('=', 70));
            Console.ForegroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("1.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" If you want to ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("enter new students, courses, trainers or assignments, ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("press ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("1\n");


            Console.Write("2.");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write(" If you want to ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("see");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" the ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("list of current students from our database,");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" press ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("2\n");

            Console.Write("3.");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write(" If you want to ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("see");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" the ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("list of courses from this semester from database, ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("press ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("3\n");

            Console.Write("4.");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write(" If you want to ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("see");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" the ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("list of trainers from the database, ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("press ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("4\n"); ;

            Console.Write("5.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" If you want to ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("see");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" the ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("list of assignments from the database, ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("press ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("5\n"); ;

            Console.Write("6.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" If you want to ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("see");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" the ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("list of students in each course, ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("press ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("6\n"); ;

            Console.Write("7.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" If you want to ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("see");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" the ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("list of trainers in each course, ");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("press ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("7\n"); ;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("8.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" If you want to ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("see");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" the ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("list of assignments in each course, ");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("press ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("8\n"); ;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("9.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" If you want to ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("see");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" the ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("list of assignments per student, ");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("press ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("9\n"); ;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("10.");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("If you want to ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("see");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" the ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("list of students that belong to more than one course, ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("press ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("10\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("11.");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("If you want to ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("exit,");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" press ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("11");
            Console.ForegroundColor = ConsoleColor.White;

        }//end menou

        //καλει τις επιμερους μεθοδους αναλογα με την επιλογη του χρηστη
        public static void UserChoices()
        {
            string inputMenu;

            do
            {
                Menu();
                inputMenu = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(inputMenu))    //check if user entered correct number
                {
                    Console.WriteLine("Please give a number between 1-11");
                    inputMenu = Console.ReadLine();
                }//end while
                Console.Clear(); // καθαρίζει την κονσολα---σβηνει το μενου και αφηνει μονο τα prints
                switch (inputMenu)
                {
                    case "1":

                        InputData();
                        break;
                    case "2":
                        //Shows All Students
                        Services.ShowStudents(Services.GetAllStudents());
                        break;
                    case "3":

                        //Shows All courses
                        Services.ShowCourses(Services.GetAllCourses());
                        break;
                    case "4":
                        //Shows All Trainers
                        Services.ShowTrainers(Services.GetAllTrainers());
                        break;
                    case "5":
                        //Shows All Assignments
                        Services.ShowAllAssignments(Services.GetAllAssignments());
                        break;
                    case "6":
                        //Shows StudentsInCourse
                        Services.ShowStudentsInCourse(Services.GetStudentsInCourse());
                        break;
                    case "7":
                        //Shows Trainers In Course
                        Services.ShowTrainersInCourse(Services.GetTrainersInCourse());

                        break;
                    case "8":
                        //Shows Assignments Per Course
                        Services.ShowAssignmentsPerCourse(Services.GetAssignmentsPerCourse());
                        break;
                    case "9":
                        //Shows Assignments per course per student
                        Services.ShowAssignmentsCourseStudent(Services.GetAssignmentsCourseStudent());

                        break;
                    case "10":
                        //Shows Students In Multiple Courses
                        Services.ShowStudentsMultipleCourses(Services.GetStudentsMultipleCourses());
                        break;
                    case "11":
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("GoodBye!!");
                        Console.ForegroundColor = ConsoleColor.White;
                        inputMenu = "N";
                        continue;
                }

                inputMenu = AskUserToContinueInTheProgram();

                if (inputMenu == "N")
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("\tGoodbye!!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else { Console.Clear(); inputMenu = "Y"; }  //καθαριζει την κονσολα απο το προηγουμενο κειμενο
            } while (inputMenu == "Y");
        }
    }
}
