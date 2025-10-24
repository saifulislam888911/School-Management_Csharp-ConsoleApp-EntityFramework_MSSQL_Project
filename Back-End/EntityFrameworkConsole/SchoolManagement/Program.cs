using System;
using SchoolManagement;
using Microsoft.EntityFrameworkCore;


Console.WriteLine("Hello,Welcome to School Management!");


/* ---------------------------
Topic : Single Table : Student
------------------------------ */

/* ---------- Insert ---------- */

//___Type : 1___
/*
Student student = new Student();
student.Name = "AAA";
student.Cgpa = 3.5;
student.DateOfBirth = new DateTime(2000, 12, 30);
// student.Address = "Dhaka";

ApplicationDbContext context = new ApplicationDbContext();
context.Students.Add(student);
context.SaveChanges();
*/

//___Type : 2___
/*
ApplicationDbContext context = new ApplicationDbContext();
context.Students.Add(new Student()
{
    Name = "BBB",
    Cgpa = 2.5,
    DateOfBirth = new DateTime(2000, 12, 30),
    // Address = "Dhaka"
});
context.Students.Add(new Student
{
    Name = "CCC",
    Cgpa = 4,
    DateOfBirth = new DateTime(2000, 12, 31),
    // Address = "Dhaka"
});
context.Students.Add(new Student
{ Name = "DDD", Cgpa = 4, DateOfBirth = new DateTime(2000, 12, 30), Address = "Dhaka" });
context.Students.Add(new Student() { Name = "Mr BBB", Cgpa = 3.5, DateOfBirth = new DateTime(2000, 12, 30), Address = "Dhaka" });
context.Students.Add(new Student { Name = "Mr CCC", Cgpa = 2.5, DateOfBirth = new DateTime(2000, 12, 31), Address = "Dhaka" });
context.SaveChanges();
*/


/* ---------------------------
Topic : Single Table : Student
------------------------------ */

/* ---------- Insert ---------- */

/*
ApplicationDbContext context = new ApplicationDbContext();
context.Teachers.Add(new Teacher()
{
    Name = "Teacher CCC",
    Username = "tccc",
    Password = "123456"
});
context.SaveChanges();
*/


/* ---------- Get ---------- */

// ApplicationDbContext context = new ApplicationDbContext();
// Student student = context.Students.Where(x => x.Cgpa <= 3.2).First();
// Student? student = context.Students.Where(x => x.Cgpa <= 3.2).FirstOrDefault();
// Student student = context.Students.Where(x => x.Cgpa <= 3.2).Single();
// Student student = context.Students.Where(x => x.Cgpa <= 3.2).SingleOrDefault();

/*
ApplicationDbContext context = new ApplicationDbContext();
List<Student> students = context.Students.ToList();
// List<Student> students = context.Students.Where(x => x.Cgpa <= 3.2 ).ToList();

foreach (var student in students)
{
    Console.WriteLine($"Name : {student.Name}; Cgpa : {student.Cgpa};");
}
*/


/* ---------- Update ---------- */

/*
ApplicationDbContext context = new ApplicationDbContext();
List<Student> students = context.Students.Where(x => x.Cgpa <= 3.2 && x.DateOfBirth > new DateTime(2000, 12, 30)).ToList();

foreach (var student in students)
{
    Console.WriteLine($"Name : {student.Name}; Cgpa : {student.Cgpa}");

    student.Address = "Mirpur";
}

context.SaveChanges();
*/


/* ---------- Delete ---------- */

/*
ApplicationDbContext context = new ApplicationDbContext();
Student s1 = context.Students.Where(x => x.Id == 2).FirstOrDefault();

if(s1 != null)
{
    context.Students.Remove(s1);
    context.SaveChanges();
}
*/


/* -------------------------------------------------------------------------
Topic : Table Relationship : One to Many, One to One (Table : Course, Topic)
---------------------------------------------------------------------------- */

/* ---------- Insert ---------- */

//___Type : 1___
/*
Course course = new Course()
{
    Title = "C#",
    Fees = 8000
};

course.Topics = new List<Topic>();
course.Topics.Add(new Topic
{
    Name = "Getting Started",
    Duration = 2
});
*/

//___Type : 2___
/*
Course course = new Course()
{
    Title = "C# Crash",
    Fees = 4000,
};

course.Topics = new List<Topic>()
{
    new Topic
    {
        Name = "Getting Started",
        Duration = 1
    } 
};
*/

//___Type : 3___
/*
var course = new Course()
{
    Title = "C# Beginner to Advanced",
    Fees = 15000,
    Topics = new List<Topic>()
    {
        new Topic { Name = "Basic", Duration = 1 },
        new Topic { Name = "Intermediate", Duration = 1 },
        new Topic { Name = "Advanced", Duration = 1 }
    }
};
*/

/*
ApplicationDbContext context = new ApplicationDbContext();
context.Courses.Add(course);
context.SaveChanges();
*/


/* ---------- Get ---------- */

//___Table : Courses : (Get All)___
/*
ApplicationDbContext context = new ApplicationDbContext();
List<Course> courses = context.Courses.ToList();

foreach (var course in courses)
{
    Console.WriteLine($"Course Id : {course.Id}; Course Title : {course.Title}; Course Fees : {course.Fees}");
}
*/

//___Table : Courses, Topics : (Get All)___
/*
ApplicationDbContext context = new ApplicationDbContext();
var courses = context.Courses.Include(x => x.Topics).ToList();

foreach (var course in courses)
{
    Console.WriteLine($"Course Id : {course.Id}; Course Title : {course.Title}; Course Fees : {course.Fees}");
    Console.WriteLine(" Topics ===>");

    if (course.Topics != null)
    {
        foreach (var topic in course.Topics)
        {
            Console.WriteLine($"    Topic ID : {topic.Id}, Name : {topic.Name}, Duration : {topic.Duration} ;");
        }
    }

    Console.WriteLine();
}
*/

//___Table : Courses, Topics : (Get by Element)___
/*
ApplicationDbContext context = new ApplicationDbContext();
var course = context.Courses.Include(x => x.Topics).FirstOrDefault();

Console.WriteLine($"Course Id : {course.Id}; Course Title : {course.Title}; Course Fees : {course.Fees}");
Console.WriteLine(" Topics ===>");

if(course.Topics != null)
{
    foreach (var topic in course.Topics)
    {
        Console.WriteLine($"Topic ID : {topic.Id}, Name : {topic.Name}, Duration : {topic.Duration} ;");
    }
}
*/


/* ---------- Update ---------- */

//___Table : Topics : (New Topic Adding According to Course Id)___
/*
ApplicationDbContext context = new ApplicationDbContext();
var course = context.Courses.Include(x => x.Topics).FirstOrDefault();
// var course = context.Courses.Include(x => x.Topics).Where(x => x.Id == 6).FirstOrDefault();

course.Topics.Add(new Topic()
{
    Name = "Newly Added Topic 1",
    Duration = 0
});
context.SaveChanges();
*/

//___Table : Topics : (Topic Updating According to Course Id)___
/*
ApplicationDbContext context = new ApplicationDbContext();
var course = context.Courses.Include(x => x.Topics).FirstOrDefault();

// course.Topics[3].Name = "up 1";
course.Topics.ToList()[3].Name = "up 2";
course.Topics.ToList()[4].Name = "up to dlt 1";
course.Topics.ToList()[4].Duration = 0;
context.SaveChanges();
*/


/* ---------- Delete ---------- */

//___Table : Courses, Topics : (Course Delete -> Topics will be deleted automatically according to Course Id)___
/*
ApplicationDbContext context = new ApplicationDbContext();
var course = context.Courses.Include(x => x.Topics).Where(x => x.Id == 6).FirstOrDefault();

context.Courses.Remove(course);
context.SaveChanges();
*/

//___Table : Courses, Topics : (Topic Delete according to Course Id)___
/*
ApplicationDbContext context = new ApplicationDbContext();
var course = context.Courses.Include(x => x.Topics).FirstOrDefault();

var topic5 = course.Topics[4];

course.Topics.Remove(topic5);
context.SaveChanges();
*/


/* ----------------------------------------------------------------------- 
Topic : Table Relationship : Many to Many (Table : Course, Topic, Student)
-------------------------------------------------------------------------- */

/* ---------- Insert ---------- */

/*
var course2 = new Course()
{
    Title = "ASP)",
    Fees = 30000,
    Topics = new List<Topic>()
    {
        new Topic() { Name = "Getting Start", Duration = 1 },
        new Topic() { Name = "Basic", Duration = 1 }
    },
    Students = new List<CourseStudent>()
    {
        new CourseStudent()
        {
            Student = new Student() { Name = "kkk", Cgpa = 2.9, Address = "Dhaka", DateOfBirth = new DateTime(2000, 01, 01) }
        },
        new CourseStudent()
        {
            Student = new Student() { Name = "lll", Cgpa = 3.4, Address = "Dhaka", DateOfBirth = new DateTime(2000, 01, 01) }
        }
    }
};
*/

/*
ApplicationDbContext context = new ApplicationDbContext();
context.Courses.Add(course2);
context.SaveChanges();
*/