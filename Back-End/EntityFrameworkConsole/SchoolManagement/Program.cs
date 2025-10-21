using System;
using SchoolManagement;
using Microsoft.EntityFrameworkCore;


Console.WriteLine("Hello, School Management!");





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

ApplicationDbContext context = new ApplicationDbContext();
context.Students.Add(student);
context.SaveChanges();
*/

//___Type : 2___
/*
ApplicationDbContext context = new ApplicationDbContext();

context.Students.Add(new Student()
{
    Name = "Mr DDD",
    Cgpa = 2.5,
    DateOfBirth = new DateTime(2000, 12, 30)
});
context.Students.Add(new Student
{
    Name = "Mr EEE",
    Cgpa = 4,
    DateOfBirth = new DateTime(2000, 12, 31)
});
context.Students.Add(new Student
{ Name = "Mr FFF", Cgpa = 4, DateOfBirth = new DateTime(2000, 12, 30) });
context.Students.Add(new Student() { Name = "Mr BBB", Cgpa = 3.5, DateOfBirth = new DateTime(2000, 12, 30) });
context.Students.Add(new Student { Name = "Mr CCC", Cgpa = 2.5, DateOfBirth = new DateTime(2000, 12, 31) });
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
    Console.WriteLine($"Name : {student.Name}; Cgpa : {student.Cgpa}");
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