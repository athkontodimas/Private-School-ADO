using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public int YearOfBirth { set; get; }
        public double TuitionFees { set; get; }
        public Student(int studentId,string firstName, string lastName, int yearOfBirth, double tuitionFees)
        {
            StudentId = studentId;
            FirstName = firstName;
            LastName = lastName;
            YearOfBirth = yearOfBirth;
            TuitionFees = tuitionFees;
        }
        public Student(string firstName, string lastName, int yearOfBirth, double tuitionFees)
        {
            FirstName = firstName;
            LastName = lastName;
            YearOfBirth = yearOfBirth;
            TuitionFees = tuitionFees;
        }

        public Student() {    }
    }//end class student
     //----------------------------------------------COURSE-------------------------------------------------------
    public class Course
    {
        public int CourseId { get; set; }
        public string Title { set; get; }
        public string Stream { set; get; }
        public string Type { set; get; }
        public DateTime startDate { set; get; } = new DateTime();
        public DateTime endDate { set; get; } = new DateTime();
    
        public Course(int courseId, string title, string stream, string type, DateTime startDate, DateTime endDate)
        {
            CourseId = courseId;
            Title = title;
            Stream = stream;
            Type = type;
            this.startDate = startDate;
            this.endDate = endDate;
        }
        public Course( string title, string stream, string type, DateTime startDate, DateTime endDate)
        {
            Title = title;
            Stream = stream;
            Type = type;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public Course(){      }
    }//end class course
     //------------------------------------------------TRAINER-----------------------------------------------------
    public class Trainer
    {
        public int TrainerId { get; set; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Subject { set; get; }
        
        public int CourseId { set; get; }

        public Trainer(int trainerId, string firstName, string lastName, string subject, int courseId)
        {
            TrainerId = trainerId;
            FirstName = firstName;
            LastName = lastName;
            Subject = subject;
            CourseId = courseId;
        }
        public Trainer(int trainerId, string firstName, string lastName, string subject)
        {
            TrainerId = trainerId;
            FirstName = firstName;
            LastName = lastName;
            Subject = subject;
        }
        public Trainer(string firstName, string lastName, string subject)
        {
            FirstName = firstName;
            LastName = lastName;
            Subject = subject;
        }

        public Trainer(){ }
    }//end class trainer
     //---------------------------------------------ASSIGNMENT--------------------------------------------------------
    public class Assignment
    {
        public int AssignmentId { get; set; }
        public string Title { set; get; }
        public string Description { set; get; }
        public DateTime? SubDateTime { set; get; } = new DateTime();
        public int? OralMark { set; get; }
        public int? TotalMark { set; get; }
        public int courseId;

     
        public Assignment(int assignmentId, string title, string description, DateTime subDateTime, int? oralMark, int? totalMark, int courseId)
        {
            AssignmentId = assignmentId;
            Title = title;
            Description = description;
            SubDateTime = subDateTime;
            OralMark = oralMark;
            TotalMark = totalMark;
            this.courseId = courseId;
        }
        public Assignment(int assignmentId, string title, string description, DateTime subDateTime, int? oralMark, int? totalMark)
        {
            AssignmentId = assignmentId;
            Title = title;
            Description = description;
            SubDateTime = subDateTime;
            OralMark = oralMark;
            TotalMark = totalMark;
        }
        public Assignment(int assignmentId, string title, string description, DateTime subDateTime)
        {
            AssignmentId = assignmentId;
            Title = title;
            Description = description;
            SubDateTime = subDateTime;
            
        }
        public Assignment(string title, string description, DateTime subDateTime)
        {
            Title = title;
            Description = description;
            SubDateTime = subDateTime;
        }

        public Assignment() { }
    }//end class assignment

    //-------------------------------------STUDENTS IN COURSE----------------------------------------------------------------
    public class StudentsInCourse
    {
        public Course Course { set; get; }
        public Student Student { set; get; }
        public int CourseId { set; get; }
        public int StudentId { set; get; }

        public StudentsInCourse(Course course, Student student)
        {
            Course = course;
            Student = student;
        }

        public StudentsInCourse(int courseId, int studentId)
        {
            CourseId = courseId;
            StudentId = studentId;
        }
          
    }//end class studentsInCourse

    //-----------------------------------------------------TRAINERS IN COURSE-------------------------------------
    public class TrainersInCourse
    {
        public Course Course { set; get; }
        //public Trainer Trainer { set; get; }
        public List<Trainer> trainersInCourse { set; get; } = new List<Trainer>();

        public TrainersInCourse(Course course, Trainer trainer)
        {
            Course = course;
            trainersInCourse.Add(trainer);
            //Trainer = trainer;
        }
        
    }//end trainersCourse
    //-------------------------------------------------------------------------- Assignments per course
    public class AssignmentsPerCourse
    {
        public Course Course { get; set; }
        public List<Assignment> assignmentsInCourseList = new List<Assignment>();
        public AssignmentsPerCourse(Course course, Assignment assignment)
        {
            Course = course;
            assignmentsInCourseList.Add(assignment);
        }

    }
        //---------------------------------ASSIGNMENTS PER COURSE PER STUDENT
    public class AssignmentsCourseStudent
    {

       
        public List<Assignment> assignmentlist { get; set; } = new List<Assignment>();
        public Student student { get; set; }
        public Course course { get; set; } 
        public AssignmentsCourseStudent(Assignment assignment, Student student, Course course)
        {

           assignmentlist.Add(assignment);
            this.student= student;
            this.course=course;

        }
    }//end AssignmentsCourseStudent

    //ΚΛΑΣΗ ΠΟΥ ΕΧΕΙ ΤΟΥΣ ΥΠΟΨΗΦΙΟΥΣ STUDENTS, COURSES, ASSIGNMENTS, TRAINERS ΓΙΑ ΝΑ ΕΙΣΑΓΕΙ Ο ΧΡΗΣΤΗΣ
    public class DataForUserInput
    {
        
        public static Student[] StudentsToEnroll = { new Student("Sotiris", "Papadopoulos", 1985, 2500) , new Student("Maria", "Giannakopoulou", 1993, 2250), new Student("Giannis", "Golas", 1998, 2500),
           new Student("Kyriakh", "Nolemh", 1979, 1200), new Student("Giwrgos", "Ipatiou", 1995, 2500), new Student("Mixalis", "Kwnstantinou", 1988, 2500),new Student("Athanasios", "Liopidas", 1990, 2250),
           new Student("Panagiotis", "Sekinas", 1989, 2500),new Student("Hlias", "Liopis", 1982, 2500),new Student("Vasilikh", "Xouliara", 1991, 2500) };

        public static Course[] courses ={ new Course(title: "CB8", stream: "C#", type: "Part-time", startDate: new DateTime(2019, 2, 10), endDate: new DateTime(2019, 8, 1)),
        new Course(title: "CB9", stream: "Java", type: "Part-time", startDate: new DateTime(2019, 4, 5), endDate: new DateTime(2019, 10, 1)),
        new Course(title: "CB9", stream: "C#", type: "Full-time", startDate: new DateTime(2019, 4, 5), endDate: new DateTime(2019, 7, 11)),
        new Course(title: "CB10", stream: "SQL", type: "Part-time", startDate: new DateTime(2019, 11, 07), endDate: new DateTime(2020, 1, 10)),
        new Course(title: "CB10", stream: "Java", type: "Full-time", startDate: new DateTime(2019, 11, 5), endDate: new DateTime(2020, 6, 1)),
        new Course(title: "CB11", stream: "HTML", type: "Full-time", startDate: new DateTime(2019, 12, 07), endDate: new DateTime(2020, 1, 10)) };

        public static Trainer[] trainers = {
            new Trainer("George", "Pikoulis", "Communications"),
            new Trainer("Giannis", "Sotiriou", "Databases"),
            new Trainer("Efthimios", "Dimitriou", "Unit Testing"),
            new Trainer("Petros", "Afentras", "Mobile Architectures"),
            new Trainer("Kyriakos", "Pampelas", "Security"),
            new Trainer("Xaralampos", "Eleutheriou", "ECDL") };
        
        public static Assignment[] assignments = {
            new Assignment("Eshop","Create an e-shop", new DateTime(2019,02,20)),
            new Assignment("Database", "Create a database", new DateTime(2019, 04, 05)),
            new Assignment("Website", "Create a dynamic website", new DateTime(2019, 06, 10)),
            new Assignment("School", "Create a school database", new DateTime(2020, 03, 12)),
            new Assignment("Blog", "Create a blog", new DateTime(2020, 03, 25)),
            new Assignment("Script", "Create a visual basic script", new DateTime(2020, 03, 25)),
            new Assignment("Mobile", "Create a mobile app", new DateTime(2020, 03, 25)) };
    }

    
}//end namespace
