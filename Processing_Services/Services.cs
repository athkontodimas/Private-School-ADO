using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Entities;
using System.Text.RegularExpressions; //Regex expression

namespace Processing_Services
{
    
    public class Services
    {
        

        static string connectionString = @"Data Source=DESKTOP-GQVBVOF\SQLEXPRESS; Initial Catalog = private_school_Athanasios_Kontodimas; Integrated Security = True";

          
        //φερνει ολους τους επιλεγμενους μαθητες σε λιστα----------------------------------
        public static List<Student> GetAllStudents()
        {
            DataSet data = new DataSet();
           
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select studentId,firstname,lastname,birthyear,tuitionFees from Students";

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(data);
                adapter.Dispose();

            }//end using
            var table = data.Tables;
            var table1 = table[0];
            object studentId;
            object firstname;
            object lastname;
            object birthyear;
            object tuitionFees;

            List<Student> students = new List<Student>();

            for (int i = 0; i < table1.Rows.Count; i++)
            {
                var row = table1.Rows[i];
                studentId = row[0];
                firstname = row[1];
                lastname = row[2];
                birthyear = row[3];
                tuitionFees = row[4];
                Student s = new Student(Convert.ToInt32(studentId),firstname.ToString(),lastname.ToString(), Convert.ToInt32(birthyear),Convert.ToInt32(tuitionFees));
                students.Add(s);
            }//end for
                return students;
        }//end GetAllStudents

        //-----------εμφανιζει τη λιστα με τους επιλεγμενους μαθητες
        public static void ShowStudents(List<Student> students)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{"Student List",50}");
            Console.WriteLine(new string('-', 76));
            Console.ForegroundColor = ConsoleColor.Green;
            string title = $"{"StudentNo",10}\t{"Firstname",5}\t{"Lastname",5}\tYear of Birth\tTuition Fees";
            Console.WriteLine(title);
            Console.WriteLine(new string(' ', 1) + new string('=', title.Length + 19));
            Console.ForegroundColor = ConsoleColor.White;
            for (int i=0; i<students.Count; i++)
            {
                if (i % 2 == 0) { Console.ForegroundColor = ConsoleColor.Cyan; }
                string result = $" Student {students[i].StudentId}:\t{students[i].FirstName,-8}\t{students[i].LastName,-8}\t{students[i].YearOfBirth,10}\t" +
                   $"{students[i].TuitionFees} euros";
                Console.WriteLine(result);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" {new string('-', 73)}");
                Console.ForegroundColor = ConsoleColor.White;
            }
            
        }//show students

        //φερνει ολους τους επιλεγμενους Trainers σε λιστα----------------------------------
        public static List<Trainer> GetAllTrainers()
        {
            List<Trainer> trainers = new List<Trainer>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select trainerid,firstname,lastname,[subject] from trainers";
                
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["trainerId"]);
                    string firstname = reader["firstname"].ToString();
                    string lastName = reader["lastname"].ToString();
                    string subject= reader["subject"].ToString();
                   
                    Trainer tr = new Trainer(id,firstname,lastName,subject);
                    trainers.Add(tr);
                }
                
            };//end using
            return trainers;
        }//end GetAllTrainers

        //-----------εμφανιζει τη λιστα με τους επιλεγμενους Trainers
        public static void ShowTrainers(List<Trainer> trainerList)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{"Trainer List",35}");
            Console.WriteLine(new string('-',64));
            Console.ForegroundColor = ConsoleColor.Green;
            string title = $"{"TrainerNo",10}\tFirstname\tLastname\tSubject";
            Console.WriteLine(title);
            Console.WriteLine(new string(' ', 1) + new string('=', title.Length + 25));
            Console.ForegroundColor = ConsoleColor.White;
            int counter = 0; //counter for the foreach loop, used for counting rows
            foreach (var item in trainerList)
            {
                if (counter % 2 == 0) { Console.ForegroundColor = ConsoleColor.Cyan; }  //applies font color to every other row/line
                
                string result = $"{item.TrainerId,5}\t\t{item.FirstName,-15}\t{item.LastName,-15}\t{item.Subject,-15}";
                Console.WriteLine(result);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" {new string('-', result.Length + 8)}");
                Console.ForegroundColor = ConsoleColor.White;
                counter++;
            }
        }//end ShowAllTrainers

        //φερνει ολα τα επιλεγμενα courses σε λιστα----------------------------------
        public static List<Course> GetAllCourses()
        {
            List<Course> courses = new List<Course>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select courseId,title,stream,[type],startDate,endDate from courses";

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["courseId"]);
                    string title = reader["title"].ToString();
                    string stream = reader["stream"].ToString();
                    string type = reader["type"].ToString();
                    DateTime startDate =new DateTime();
                    startDate = Convert.ToDateTime(reader["startDate"]);
                    DateTime endDate = new DateTime();
                    endDate = Convert.ToDateTime(reader["endDate"]);
                    
                    Course c = new Course(id, title, stream, type, startDate,endDate);
                    courses.Add(c);
                }

            };//end using
            return courses;
        }//end GetAllCourses

        //-----------εμφανιζει τη λιστα με τα επιλεγμενα Courses
        public static void ShowCourses(List<Course> coursesList)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{"Courses List",50}");
            Console.WriteLine(new string('-', 92));
            Console.ForegroundColor = ConsoleColor.Green;
            string title = $"{"CourseNo",9}\tTitle\tStream\t{"Type",6}\t\t{"startDate",15}\t\t{"endDate",13}";
            Console.WriteLine(title);
            Console.WriteLine(new string(' ', 1) + new string('=', title.Length + 30));
            Console.ForegroundColor = ConsoleColor.White;
            int counter = 0; //counter for the foreach loop, used for counting rows
            foreach (var item in coursesList)
            {
                if (counter % 2 == 0) { Console.ForegroundColor = ConsoleColor.Cyan; }  //applies font color to every other row/line
               
               string result = $"{item.CourseId,3}\t\t{item.Title}\t{item.Stream,5}\t{item.Type,5}\t{item.startDate,5}\t{item.endDate,5}";
                Console.WriteLine(result);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" {new string('-', result.Length + 24)}");
                Console.ForegroundColor = ConsoleColor.White;
                counter++;
            }
        }//end ShowAllCourses

        //φερνει ολα τα επιλεγμενα Assignments σε λιστα----------------------------------
        public static List<Assignment> GetAllAssignments()
        {
            List<Assignment> assignments = new List<Assignment>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "select distinct a.assignmentId, a.title, a.[description], a.subDateTime from assignments a";

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["assignmentId"]);
                    string title = reader["title"].ToString();
                    string description = reader["description"].ToString();
                    DateTime subDateTime = Convert.ToDateTime(reader["subDateTime"]);
                    
                    Assignment a = new Assignment(id,title, description, subDateTime);
                    assignments.Add(a);
                }
            };//end using
            return assignments;
        }//end GetAllAssignments

        //-----------εμφανιζει τη λιστα με τα επιλεγμενα Assignments
        public static void ShowAllAssignments(List<Assignment> assignmentList)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{"Assignment List",50}");
            Console.WriteLine(new string('-', 85));
            Console.ForegroundColor = ConsoleColor.Green;
            string title = $"{"AssignmentNo",13}\tTitle\t\t\t{"Description",13}\t\t\tSubDateTime";
            Console.WriteLine(title);
            Console.WriteLine(new string(' ', 1) + new string('=', title.Length + 35));
            Console.ForegroundColor = ConsoleColor.White;
            int counter = 0; //counter for the foreach loop, used for counting rows
            foreach (var item in assignmentList)
            {
                if (counter % 2 == 0) { Console.ForegroundColor = ConsoleColor.Cyan; }  //applies font color to every other row/line
                string result = $"{item.AssignmentId,2}\t\t{item.Title,-10}{item.Description,30}\t{item.SubDateTime,-5}";
                Console.WriteLine(result);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" {new string('-', result.Length + 18)}");
                Console.ForegroundColor = ConsoleColor.White;
                counter++;
            }
        }//end ShowAllAssignments

        //φερνει ολους τους επιλεγμενους Students Per Course σε λιστα----------------------------------
        public static List<StudentsInCourse> GetStudentsInCourse()
        {
            List<StudentsInCourse> studentsInCourse = new List<StudentsInCourse>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                 
                string query = "select studentId,csCourseId from courseStudent";

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int studentId = Convert.ToInt32(reader["studentId"]);
                    Student st = null; 
                    foreach (var item in GetAllStudents())
                    {
                        if (item.StudentId == studentId) { st = item; } 
                    }
                    Course c = null;
                    int courseId = Convert.ToInt32(reader["csCourseId"]);
                    foreach (var item in GetAllCourses())
                    {
                        if (item.CourseId == courseId) {  c = item; }
                    }
                    StudentsInCourse sc = new StudentsInCourse(c,st);
                    studentsInCourse.Add(sc);
                }
            };//end using
            return studentsInCourse;
        }//end GetStudentsInCourse

        //-----------εμφανιζει τη λιστα με τους επιλεγμενους Student Per Course
        public static void ShowStudentsInCourse(List<StudentsInCourse> studentsInCourseList)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{"Students enrolled in each course",50}");
            Console.WriteLine(new string('-', 72));
            Console.ForegroundColor = ConsoleColor.Green;
            string title = $"{"CourseNo",9}\tTitle\tStream\tType\t\tFirstName\tLastName";
            Console.WriteLine(title);
            Console.WriteLine(new string(' ', 1) + new string('=', title.Length + 24));
            Console.ForegroundColor = ConsoleColor.White;
            
            int indexing = 1;  //counter for the foreach loop, used for counting rows
            foreach (var item in studentsInCourseList)
            {
                if (indexing % 2 != 0) { Console.ForegroundColor = ConsoleColor.Cyan; }  //applies font color to every other row/line
                string result = $"{indexing,6}\t{item.Course.Title,12}\t{item.Course.Stream,-5}\t{item.Course.Type}\t{item.Student.FirstName,-10}\t{item.Student.LastName}";
                Console.WriteLine(result);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($" {new string('-', result.Length + 16)}");
                Console.ForegroundColor = ConsoleColor.White;
                indexing++;
            }
        }//end ShowStudentsInCourse

        //------------------------ //φερνει ολους τους επιλεγμενoυς Trainers Per Course σε λιστα---------------------------------------------------------------------------------------------
        public static List<TrainersInCourse> GetTrainersInCourse()
        {
            List<TrainersInCourse> trainersInCourse = new List<TrainersInCourse>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"select c.title,c.stream, c.[type],c.startDate,c.endDate, t.firstname, t.lastname,t.[subject], t.trainerId,ct.courseId from courseTrainer ct " +
                    $"inner join trainers t on ct.trainerId = t.trainerId " +
                    $"inner join courses c on ct.courseId = c.courseId";

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int trainerId = Convert.ToInt32(reader["trainerId"]);
                    Trainer t = null;
                    foreach (var item in GetAllTrainers())
                    {
                        if (item.TrainerId == trainerId) { t = item; }
                    }
                    Course c = null;
                    int courseId = Convert.ToInt32(reader["courseId"]);
                    foreach (var item in GetAllCourses())
                    {
                        if (item.CourseId == courseId) { c = item; }
                    }
                    TrainersInCourse tc = new TrainersInCourse(c,t);
                    trainersInCourse.Add(tc);
                }
            };//end using
            return trainersInCourse;
        }//end GetTrainersInCourse

        //---------------Show Trainers In Course--------------------------------------
        public static void ShowTrainersInCourse(List<TrainersInCourse> trainersInCourseList)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{"Trainers who teach to each course",50}");
            Console.WriteLine(new string('-', 72));
            Console.ForegroundColor = ConsoleColor.Green;
            string title = $"{"TrainerNo",11}\tTitle\tStream\tType\t\tFirstName\tLastName";
            Console.WriteLine(title);
            Console.WriteLine(new string(' ', 1) + new string('=', title.Length + 22));
            Console.ForegroundColor = ConsoleColor.White;
            int trainerNo = 1;
            
            foreach (var trainerCourse in trainersInCourseList)
            {
                foreach (var trainer in trainerCourse.trainersInCourse)
                {
                   if (trainerNo % 2 != 0) { Console.ForegroundColor = ConsoleColor.Cyan; }  //applies font color to every other row/line
                    string result=$"{trainerNo,5}\t\t{trainerCourse.Course.Title,-6}\t{trainerCourse.Course.Stream}\t{trainerCourse.Course.Type}\t{trainer.FirstName,-10}\t{trainer.LastName,-10}";
                    Console.WriteLine(result);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(new string('-', result.Length+25));
                    Console.ForegroundColor = ConsoleColor.White;
                    trainerNo++;
                }
            }
        }//end ShowTrainersInCourse

        //---------------------------------------------------- //φερνει ολα τα επιλεγμενα Assignments Per Course σε λιστα-----------------------------------------
        public static List<AssignmentsPerCourse> GetAssignmentsPerCourse()
        {
            List<AssignmentsPerCourse> assignmentsInCourse = new List<AssignmentsPerCourse>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"select c.title,c.stream,c.[type],a.title,a.[description],a.subDateTime,a.assignmentId,c.courseId" +
                    $" from courseAssignment ca inner join Courses c on ca.courseId = c.courseId " +
                    $"inner join assignments a on ca.assignmentId = a.assignmentId order by c.courseId,c.stream,c.[type]";

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int assignmentId = Convert.ToInt32(reader["assignmentId"]);
                    Assignment a = null;
                    foreach (var item in GetAllAssignments())
                    {

                        if (item.AssignmentId == assignmentId) { a = item; }
                    }
                    Course c = null;
                    int courseId = Convert.ToInt32(reader["courseId"]);
                    foreach (var item in GetAllCourses())
                    {
                        if (item.CourseId == courseId) { c = item; }
                    }
                    AssignmentsPerCourse ac = new AssignmentsPerCourse(c, a);
                    assignmentsInCourse.Add(ac);
                }
            };//end using
            return assignmentsInCourse;
        }//end GetAssignmentsInCourse

        //--------------------------  εμφανιζει ολα τα επιλεγμενα Assignments Per Course  -------------------
        public static void ShowAssignmentsPerCourse(List<AssignmentsPerCourse> assignmentsInCourseList)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{"Assignments in each course",65}");
            Console.WriteLine(new string('-', 125));
            Console.ForegroundColor = ConsoleColor.Green;
            string title = $"{"AssignmentNo",8}  {"Course Title",8} {"Stream",8}\t{"Type",6}\t{"Assignment Title",20}\t{"Description",14}\t{"SubDateTime",30}";
            Console.WriteLine(title);
            Console.WriteLine(new string('=', title.Length + 15));
            Console.ForegroundColor = ConsoleColor.White;
           
            int courseNumbering = 1;
            foreach (var assignmentCourse in assignmentsInCourseList)
            {
                foreach (var assignment in assignmentCourse.assignmentsInCourseList)
                {
                    if (courseNumbering % 2 != 0) { Console.ForegroundColor = ConsoleColor.Cyan; }  //applies font color to every other row/line

                    Console.WriteLine($"{courseNumbering,3}\t\t{assignmentCourse.Course.Title,-1}\t{assignmentCourse.Course.Stream,8}\t{assignmentCourse.Course.Type,0}\t" +
                        $"{assignment.Title,8}\t{assignment.Description,-29}\t{assignment.SubDateTime,15}");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(new string('-', 125));
                    Console.ForegroundColor = ConsoleColor.White;
                }
                courseNumbering++;
            }
        }//end ShowAssignmentsPerCourse

     //--------------------------------------φερνει ολους τους επιλεγμενoυς Students που παρακολουθούν παραπανω από 1 Course σε λιστα----------------------------------------- --------------------------------------
        public static List<Student> GetStudentsMultipleCourses()
        {
            List<Student> studentsMultipleCourses = new List<Student>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"select cs.studentId,count(cs.csCourseId) from courseStudent cs group by cs.studentId having count(cs.csCourseId)>1";
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int studentId = Convert.ToInt32(reader["studentId"]);
                                       
                    Student s = null;
                    foreach (var item in GetAllStudents())
                    {

                        if (item.StudentId == studentId) { s = item; }
                    }
                                      
                    studentsMultipleCourses.Add(s);
                }
            };//end using
            return studentsMultipleCourses;
        }//end GetAssignmentsInCourse
        
        //------------------------------------- Show Students in Multiple Courses ------------------------------------------------------------------------
        public static void ShowStudentsMultipleCourses(List<Student> students)
        {
            int studentNumbering = 1;
            
            string title = $"\t{"FirstName"}\tLastName";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Students enrolled to more than 1 courses");
            Console.WriteLine(new string('-', 40));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(title);
            Console.WriteLine(new string(' ', 1) + new string('=', title.Length + 18));
            Console.ForegroundColor = ConsoleColor.White;

            foreach (var student in students)
            {
                if (studentNumbering % 2 != 0) { Console.ForegroundColor = ConsoleColor.Cyan; }  //applies font color to every other row/line
                Console.WriteLine($"{studentNumbering,3}\t{student.FirstName,-10}\t{student.LastName,-10}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(new string('-', 38));
                Console.ForegroundColor = ConsoleColor.White;
                studentNumbering++;
            }
               
        }//end ShowAssignmentsPerCourse

         //----------------------------- //φερνει ολα τα Assignments Per Course Per Student σε λιστα-----------------------------------------
        public static List<AssignmentsCourseStudent> GetAssignmentsCourseStudent()
        {
            List<AssignmentsCourseStudent> assignmentCourseStudent = new List<AssignmentsCourseStudent>();
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = $"select s.firstname,s.lastname,c.title,c.stream,c.[type],a.title,a.[description],ca.oralMark, ca.totalMark,c.courseId,s.studentId,a.assignmentId " +
                    $" from courseAssignment ca inner join courses c on ca.courseId = c.courseId inner join students s on ca.studentId = s.studentId " +
                    $"inner join assignments a on ca.assignmentId = a.assignmentId";
                 
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int studentId = Convert.ToInt32(reader["studentId"]);
                    int courseId = Convert.ToInt32(reader["courseId"]);
                    int assignmentId = Convert.ToInt32(reader["assignmentId"]);

                    //ελεγχος αν οι βαθμοι ειναι null, στην περιπτωση αναθεσης κανουργιου assignment σε μαθητη
                    int? oralMark= string.IsNullOrWhiteSpace(reader["oralMark"].ToString())?(int?)null:Convert.ToInt32(reader["oralMark"]);
                    int? totalMark = string.IsNullOrWhiteSpace(reader["totalMark"].ToString()) ? (int?)null : Convert.ToInt32(reader["totalMark"]); 
                    
                    Student s = null; //new Student();
                    Course c = null;//new Course();
                    Assignment a = null;//new Assignment();
                    
                    foreach (var item in GetAllStudents())
                    {
                        if (item.StudentId == studentId) { s = item; }
                    }
                    foreach (var course in GetAllCourses())
                    {
                        if (course.CourseId == courseId) { c = course; }
                    }
                    foreach (var assignment in GetAllAssignments())
                    {
                        if (assignment.AssignmentId == assignmentId)
                        {
                            a = assignment;
                            a.OralMark = oralMark;
                            a.TotalMark = totalMark;
                        }
                    }
                    AssignmentsCourseStudent acs = new AssignmentsCourseStudent(a,s,c);
                    assignmentCourseStudent.Add(acs);
                }
            };//end using
            return assignmentCourseStudent;
        }//end GetAssignmentsInCourse
        
        //------------------------------------- Show Assignments Per Course Per Student ------------------------------------------------------------------------
        public static void ShowAssignmentsCourseStudent(List<AssignmentsCourseStudent> assignmentCourseStudentList)
        {
            int assignNo = 1;
            int counter = 1;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{"Student assignments for each course",68}");
            Console.WriteLine(new string('-', 113));
            string title = $"{"AssignNo"}  {"FirstName"}\t{"LastName",-10} {"Course Title",-10}  {"Course Stream"}  {"Course Type"}  " +
                $"{"Assign. Title"}  {"Oral Mark"}  {"Total Mark"}";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(title);
            Console.WriteLine(new string('=', title.Length+4));
            Console.ForegroundColor = ConsoleColor.White;
            foreach (var item in assignmentCourseStudentList)
            {
                Course c = item.course;
                Student s = item.student;

                if (counter % 2 == 0) { Console.ForegroundColor = ConsoleColor.White; } else { Console.ForegroundColor = ConsoleColor.Cyan; }
                foreach (var assignment in item.assignmentlist)
                {
                    Console.WriteLine($"{assignNo,3}\t  {s.FirstName,-12} {s.LastName,-15}\t{c.Title,0}\t{c.Stream,8}\t{c.Type,-10}  {assignment.Title,8} " +
                        $"{assignment.OralMark,11}\t{assignment.TotalMark,9}"); 
                    
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(new string('-', 109));
                    Console.ForegroundColor = ConsoleColor.White;
                    assignNo++;
                }
                counter++;
            }
        }//end ShowAssignmentsPerCourse


        // -----------------------------------------------INPUTS ----------------------------------------
        //------------------------------------------------------user inputs student----------------------------------------------
        public static void InputStudent()
        {
            string input;
           
                do
                {
                  using (SqlConnection con = new SqlConnection(connectionString))
                  {
                    Console.WriteLine("Which student do you want to enroll to our school? Choose the number of the candidate student from the following table");
                    int studentNum = 1;
                    // εμφανιζω τους διαθεσιμους students
                    Console.WriteLine($"{"StudentId",-10}\t{"First name",-10}\t{"Last name",-10}\t{"Year Of Birth",-10}\t{"Tuition Fees",-10}");
                    string title = $"{"StudentId",-10}\t{"First name",-10}\t{"Last name",-10}\t{"Year Of Birth",-10}\t{"Tuition Fees",-10}";
                    Console.WriteLine(new string('-', title.Length + 17));
                    foreach (var item in DataForUserInput.StudentsToEnroll)
                    {
                        Console.WriteLine($"{studentNum}\t\t{item.FirstName,-10}\t{item.LastName,-10}\t{item.YearOfBirth,-10}\t{item.TuitionFees,-10}");
                        studentNum++;
                    }

                    input = Console.ReadLine();

                    // ελεγχος αν ο χρηστης εισαγει σωστές τιμές
                    input = UserInputCheckStudent(DataForUserInput.StudentsToEnroll.ToList(), input);
                    //Αποθηκευω τον επιλεγμενο μαθητη απο το χρηστη σε ενα αντιεκιμενο Student s
                    Student s = DataForUserInput.StudentsToEnroll[Convert.ToInt32(input) - 1];


                    //Καταχωρώ τον επιλεγμένο μαθητη (που αποθηκευσα παραπανω) στη βαση
                    string query = $"insert into Students(firstname,lastname,birthyear,tuitionfees)" +
                        $" values(@firstname,@lastname,@birthyear,@tuitionFees)";
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue("@firstname", s.FirstName);
                        cmd.Parameters.AddWithValue("@lastname", s.LastName);
                        cmd.Parameters.AddWithValue("@birthyear", s.YearOfBirth);
                        cmd.Parameters.AddWithValue("@tuitionFees", s.TuitionFees);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Student added!");
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Student was not added. " + e.Message);
                    }
                    finally { }
                    Console.WriteLine("Do you want to add another student? Answer with Y/N");
                    input = Console.ReadLine();
                  }//end using
                } while (input.ToUpper() == "Y");
            
        }//end of input student


        //ΚΑΝΕΙ ΤΟΝ ΕΛΕΓΧΟ ΓΙΑ ΤΟ USER INPUT ΤΟΥ STUDENT
        private static string UserInputCheckStudent(List<Student> students,string input)
        {
            bool invalidInput;
            do
            {
                if (string.IsNullOrWhiteSpace(input) || Regex.IsMatch(input, @"[a-z]+$", RegexOptions.IgnoreCase) || 
                    Convert.ToInt32(input) < 1 || Convert.ToInt32(input) >students.Count)   //check if user gives null/space character or letter or valid number
                {
                    Console.WriteLine($"Please give a number between 1-{students.Count}.");
                    input = Console.ReadLine();
                    invalidInput = true;
                    //condition = false;
                }
                else
                {
                    invalidInput = false;
                    Console.Clear();
                    Console.WriteLine();
                }
            } while (invalidInput);
            return input;
        }

        //--------------------- user inputs course -------------------------------------------------------
        public static void InputCourse()
        {
           
            string input;
            
                do
                {
                  using (SqlConnection con = new SqlConnection(connectionString))
                  {
                    Console.WriteLine("Which course do you want to add to our school? Choose the number of the available courses from the following table");
                    int courseNum = 1;
                    // εμφανιζω τα διαθεσιμα courses
                    Console.WriteLine($"{"CourseId",-10}\t{"Title",-10}\t{"Stream",-10}\t{"Type",-10}\t{"startDate",-10}\t\t{"endDate",-10}");
                    string title = $"{"CourseId",-10}\t{"Title",-10}\t{"Stream",-10}\t{"Type",-10}\t{"startDate",-10}\t{"endDate",-10}";
                    Console.WriteLine(new string('-', title.Length + 30));
                    foreach (var item in DataForUserInput.courses)
                    {
                        Console.WriteLine($"{courseNum}\t\t{item.Title,-10}\t{item.Stream,-10}\t{item.Type,-10}\t{item.startDate,-10}\t{item.endDate,-10}");
                        courseNum++;
                    }

                    input = Console.ReadLine();
                    // ελεγχος αν ο χρηστης εισαγει σωστές τιμές
                    input = UserInputCheckCourse(DataForUserInput.courses.ToList(), input);

                    //αποθηκευω το επιλεγμενο μαθημα του χρηστη σε ενα αντικειμενο Course
                    Course c = DataForUserInput.courses[Convert.ToInt32(input) - 1];

                    //καταχωρω το επιλεγμενο μαθημα στη βαση
                    string query = $"insert into Courses(title,stream,type,startDate,endDate)" +
                        $" values(@title,@stream,@type,@startDate,@endDate)";
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue("@title", c.Title);
                        cmd.Parameters.AddWithValue("@stream", c.Stream);
                        cmd.Parameters.AddWithValue("@type", c.Type);
                        cmd.Parameters.AddWithValue("@startDate", c.startDate);
                        cmd.Parameters.AddWithValue("@endDate", c.endDate);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Course added!");
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Course was not added. " + e.Message);
                    }
                    finally { }
                    Console.WriteLine("Do you want to add another course? Answer with Y/N");
                    input = Console.ReadLine();
                  }//end using
                } while (input.ToUpper() == "Y");
            
            
        }//end of input course

        //ΚΑΝΕΙ ΤΟΝ ΕΛΕΓΧΟ ΓΙΑ ΤΟ USER INPUT ΤΟΥ COURSE
        private static string UserInputCheckCourse(List<Course> courses, string input)
        {
            bool invalidInput;
            do
            {
                if (string.IsNullOrWhiteSpace(input) || Regex.IsMatch(input, @"[a-z]+$", RegexOptions.IgnoreCase) || Convert.ToInt32(input) < 1 || 
                    Convert.ToInt32(input) > courses.Count())   //check if user gives null/space character or letter or number outside [1-10]
                {
                    Console.WriteLine($"Please give a number between 1-{courses.Count()}.");
                    input = Console.ReadLine();
                    invalidInput = true;
                    
                }
                else
                {
                    invalidInput = false;
                    Console.Clear();
                    Console.WriteLine();
                }
            } while (invalidInput);
            return input;
        }

        //---------------------Input trainer--------------------------------------------------------------------------------------------------------------------
        public static void InputTrainer()
        {
            string input;
            
                do
                {
                   using (SqlConnection con = new SqlConnection(connectionString))
                   {
                        Console.WriteLine("Which trainer do you want to hire for our school? Choose the number of the soon-to-be-hired- trainer from the following table");
                    int trainerNum = 1;
                    //εμφανιζω τους διαθεσιμους trainers
                    string title = $"{"TrainerId",-10}\t{"First name",-10}\t{"Last name",-10}\t{"Subject",-10}";
                    Console.WriteLine(title);
                    Console.WriteLine(new string('-', title.Length + 20));
                    foreach (var item in DataForUserInput.trainers)
                    {
                        Console.WriteLine($"{trainerNum}\t\t{item.FirstName,-10}\t{item.LastName,-10}\t{item.Subject,-10}");
                        trainerNum++;
                    }

                    input = Console.ReadLine();
                    //ΕΛΕΓΧΟΣ ΑΝ Ο ΧΡΗΣΤΗΣ ΕΙΣΑΓΕΙ ΤΟ ΣΩΣΤΟ INPUT
                    input = UserInputCheckTrainer(DataForUserInput.trainers.ToList(), input);

                    //αποθηκευω τον επιλεγμενο Trainer σε ενα αντικειμενο Trainer t
                    Trainer t = DataForUserInput.trainers[Convert.ToInt32(input) - 1];

                    //καταχωρώ τον Trainer στη βαση
                    string query = $"insert into Trainers(firstname,lastname,subject)" +
                        $" values(@firstname,@lastname,@subject)";
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue("@firstname", t.FirstName);
                        cmd.Parameters.AddWithValue("@lastname", t.LastName);
                        cmd.Parameters.AddWithValue("@subject", t.Subject);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Trainer added!");
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Trainer was not added. " + e.Message);
                    }
                    finally { }
                    Console.WriteLine("Do you want to add another trainer? Answer with Y/N");
                    input = Console.ReadLine();
                    }//end using
                } while (input.ToUpper() == "Y");
           
        }//end of input trainer
        
        //ΕΛΕΓΧΟΣ ΓΙΑ ΤΟ ΣΩΣΤΟ USER INPUT ΓΙΑ ΤΟΝ TRAINER
        public static string UserInputCheckTrainer(List<Trainer> trainers,string input)
        {
            bool invalidInput;
            do
            {
                if (string.IsNullOrWhiteSpace(input) || Regex.IsMatch(input, @"[a-z]+$", RegexOptions.IgnoreCase) || Convert.ToInt32(input) < 1 
                    || Convert.ToInt32(input) > trainers.Count())   //check if user gives null/space character or letter or an invalid number
                {
                    Console.WriteLine($"Please give a number between 1-{trainers.Count()}");
                    input = Console.ReadLine();
                    invalidInput = true;
                    //condition = false;
                }
                else
                {
                    invalidInput = false;
                    Console.Clear();
                    Console.WriteLine();
                }
            } while (invalidInput);
            return input;
        }

        //---------------------Input ASSIGNMENT -------------------------------------------------
        public static void InputAssignment()
        {
            string input;
            
                do
                {
                  using (SqlConnection con = new SqlConnection(connectionString))
                  {
                    Console.WriteLine("Which assignment do you want to add to our syllabus? Choose the number of the available assignments from the following table");
                    int assignmentNum = 1;
                    //εμφανιζω τα διαθεσιμα assignments
                    string title = $"{"AssignmentId",-10}\t{"Title",-10}\t{"Description",-10}\t{"Submission date",-10}";
                    Console.WriteLine(title);
                    Console.WriteLine(new string('-', title.Length + 20));
                    foreach (var item in DataForUserInput.assignments)
                    {
                        Console.WriteLine($"{assignmentNum}\t\t{item.Title,-10}\t{item.Description,-10}\t{item.SubDateTime,-10}");
                        assignmentNum++;
                    }

                    input = Console.ReadLine();

                    //ελεγχος αν ο χρηστης δινει σωστο input (σωστα νουμερα,οχι γραμματα ή το κενο)
                    input = UserInputCheckAssignment(DataForUserInput.assignments.ToList(), input);

                    //αποθηκευω σε ενα νεο αντικειμενο τυπου Assignment, το επιλεγμενο assignment του χρηστη
                    Assignment a = DataForUserInput.assignments[Convert.ToInt32(input) - 1];

                    string query = $"insert into Assignments(title,description,subDateTime)" +
                        $" values(@title,@description,@subDateTime)";
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue("@title", a.Title);
                        cmd.Parameters.AddWithValue("@description", a.Description);
                        cmd.Parameters.AddWithValue("@subDateTime", a.SubDateTime);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Assignment added!");
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Assignment was not added. " + e.Message);
                    }
                    finally { }
                    Console.WriteLine("Do you want to add another assignment? Answer with Y/N");
                    input = Console.ReadLine();
                  }//end using
                 } while (input.ToUpper() == "Y");
            
        }//end of input assignment
        
        //ΚΑΝΕΙ ΤΟΝ ΕΛΕΓΧΟ ΓΙΑ ΤΟ USER INPUT ΤΟΥ ASSIGNMENT
        private static string UserInputCheckAssignment(List<Assignment> assignments,string input)
        {
            bool invalidInput;

            do
            {
                if (string.IsNullOrWhiteSpace(input) || Regex.IsMatch(input, @"[a-z]+$", RegexOptions.IgnoreCase) || 
                    Convert.ToInt32(input) < 1 || Convert.ToInt32(input) > assignments.Count())   //check if user gives null/space character or letter or valid number 
                {
                    Console.WriteLine($"Please give a number between 1-{assignments.Count()}.");
                    input = Console.ReadLine();
                    invalidInput = true;
                    //condition = false;
                }
                else
                {
                    invalidInput = false;
                    Console.Clear();
                    Console.WriteLine();
                }
            } while (invalidInput);
            return input;
        }

        //---------------------Input Students per Course -------------------------------------------------
        public static void InputStudentsPerCourse()
        {
            string input;
                do
                {
                  using (SqlConnection con = new SqlConnection(connectionString))
                  {
                    Console.WriteLine("Pick the student you want to enroll. Choose the student number of the available students from the following table");
                    //Βαζω σε μια λιστα ολους τους students(τους υπαρχοντες και τους καινουργιους που εισήγαγε ο χρηστης)
                    List<Student> students = GetAllStudents();
                    //εμφανιζω τη λιστα των μαθητων
                    ShowStudents(students);

                    input = Console.ReadLine();

                    //ελεγχος αν δινει σωστα στοιχεια ο χρηστης
                    input = UserInputCheckStudent(students, input);

                    //αποθηκευω το μαθητη
                    Student s = students[Convert.ToInt32(input) - 1];

                    Console.WriteLine("In which course do you want to enroll him/her? Choose the student number of the available courses from the following table");
                    List<Course> courses = GetAllCourses();
                    ShowCourses(courses);
                    input = Console.ReadLine();

                    //ελεγχος αν ο χρηστης δινει σωστα στοιχεια
                    input = UserInputCheckCourse(courses, input);

                    //αποθηκευω το μαθημα
                    Course c = courses[Convert.ToInt32(input) - 1];

                    string query = $"insert into courseStudent(studentId,csCourseId)" +
                        $" values(@studentId,@csCourseId)";
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue("@studentId", s.StudentId);
                        cmd.Parameters.AddWithValue("@csCourseId", c.CourseId);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Student enrolled in course!");
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Student did not enrolled in course! " + e.Message);
                    }
                    finally { }
                    Console.WriteLine("Do you want to enroll another student or enroll this student to another course? Answer with Y/N");
                    input = Console.ReadLine();
                  }//end using
                } while (input.ToUpper() == "Y");
        }//end of input students in course

        //-------------------------------------------------------input trainer in course
        public static void InputTrainerInCourse()
        {
            string input;
                do
                {
                  using (SqlConnection con = new SqlConnection(connectionString))
                  {
                    Console.WriteLine("Pick the trainer you want to teach to the course. " +
                        "Choose the trainer number of the available trainers from the following table");
                    //Βαζω σε μια λιστα ολους τους trainers(τους υπαρχοντες και τους καινουργιους που εισήγαγε ο χρηστης)
                    List<Trainer> trainers = GetAllTrainers();
                    //εμφανιζω τη λιστα με τους trainers
                    ShowTrainers(trainers);
                    input = Console.ReadLine();
                    //ελεγχος αν ο χρηστης δινει σωστα στοιχεια
                    input = UserInputCheckTrainer(trainers, input);

                    //αποθηκευω τον trainer
                    Trainer t = trainers[Convert.ToInt32(input) - 1];

                    Console.WriteLine("In which course do you want to teach? Choose the course number of the available courses from the following table");
                    //Βαζω σε μια λιστα ολα τα courses(τα υπαρχοντα και τα καινουργια που εισήγαγε ο χρηστης)
                    List<Course> courses = GetAllCourses();
                    //εμφανιζω τη λιστα των μαθηματων
                    ShowCourses(courses);
                    input = Console.ReadLine();
                    //ελεγχος αν ο χρηστης δινει σωστα στοιχεια
                    input = UserInputCheckCourse(courses, input);
                    //αποθηκευω το μαθημα
                    Course c = courses[Convert.ToInt32(input) - 1];

                    string query = $"insert into courseTrainer(trainerId,courseId) values(@trainerId,@courseId)";
                    try
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);

                        cmd.Parameters.AddWithValue("@trainerId", t.TrainerId);
                        cmd.Parameters.AddWithValue("@courseId", c.CourseId);
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Trainer teaches in course!");
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Trainer does not teach in course! " + e.Message);
                    }
                    finally { }
                    Console.WriteLine("Do you want to add another trainer to teach to a course? Answer with Y/N");
                    input = Console.ReadLine();
                  }// end using
                } while (input.ToUpper() == "Y");
        }//end of input trainers in course

        //--------------------------------------Input Assignment per course per student
        public static void InputAssignmentInCourseToStudent()
        {
            string input;
                do
                {
                  using (SqlConnection con = new SqlConnection(connectionString))
                  {
                    Console.WriteLine("Give an assignment to a student. Which assignment do you want? Choose the assignment number " +
                        "of the available assignments from the following table");
                    //Βαζω σε μια λιστα ολα τα assignments(τα υπαρχοντα και τα καινουργια που εισήγαγε ο χρηστης)
                    List<Assignment> assignments = GetAllAssignments();
                    //δειχνω τη λιστα με τα assignments
                    ShowAllAssignments(assignments);
                    input = Console.ReadLine();


                    input = UserInputCheckAssignment(assignments, input);
                    //αποθηκευω το assignment
                    Assignment a = assignments[Convert.ToInt32(input) - 1];

                    Console.WriteLine("Who is the student you picked for the assignment? " +
                        "Choose the student number of the enrolled students from the following table");
                    //Βαζω σε μια λιστα ολους τους students(τους υπαρχοντες και τους καινουργιους που εισήγαγε ο χρηστης)
                    List<Student> student = GetAllStudents();
                    //εμφανιζω τη λιστα των μαθητων
                    ShowStudents(student);

                    input = Console.ReadLine();
                    //ελεγχος αν ο χρηστης δινει σωστα στοιχεια
                    input = UserInputCheckStudent(student, input);
                    //αποθηκευω το μαθητη
                    Student s = student[Convert.ToInt32(input) - 1];

                    Console.WriteLine("In which course does this assignment belong to? Choose the course number of the available courses from the following table");
                    //Βαζω σε μια λιστα ολα τα courses(τα υπαρχοντα και τα καινουργια που εισήγαγε ο χρηστης)
                    List<Course> courses = GetAllCourses();
                    //εμφανιζω τη λιστα των μαθηματων
                    ShowCourses(courses);
                    input = Console.ReadLine();
                    //ελεγχος αν ο χρηστης δινει σωστα στοιχεια
                    input = UserInputCheckCourse(courses, input);
                    //αποθηκευω το μαθημα
                    Course c = courses[Convert.ToInt32(input) - 1];

                    try
                    {
                        //καταχωρώ και συσχετίζω το assignment με το course στη βάση
                        string query = $"insert into courseAssignment (assignmentId,courseId,studentId)values(@assignmentId,@courseId,@studentId)";
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        cmd.Parameters.AddWithValue("@courseId", c.CourseId);
                        cmd.Parameters.AddWithValue("@studentId", s.StudentId);
                        cmd.Parameters.AddWithValue("@assignmentId", a.AssignmentId);

                        cmd.ExecuteNonQuery();

                        Console.WriteLine(" The assignment for your student was added successfully!");
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("The assignment for your student was not added! " + e.Message);
                    }
                    finally { }

                    Console.WriteLine("Do you want to add another assignment to a student? Answer with Y/N");
                    input = Console.ReadLine();
                  }//end using
                } while (input.ToUpper() == "Y");
        }//end of input assignments per course per student
    }//end class services
}//end namespace
