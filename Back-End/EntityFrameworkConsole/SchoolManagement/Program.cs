using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using SchoolManagement;

Console.Title = "School Management System";
Console.WriteLine(
    "Welcome! Admin for Student, Teacher, Course, Topic, Enrollment\n");

while (true)
{
    Console.WriteLine("Main Menu:");
    Console.WriteLine("1) Students");
    Console.WriteLine("2) Teachers");
    Console.WriteLine("3) Courses & Topics");
    Console.WriteLine("4) Enrollments");
    Console.WriteLine("0) Exit");
    Console.Write("Choose: ");

    string mainChoice = ReadInput();

    if (mainChoice == "0")
        break;

    switch (mainChoice)
    {
        case "1":
            StudentMenu();
            break;
        case "2":
            TeacherMenu();
            break;
        case "3":
            CourseMenu();
            break;
        case "4":
            EnrollmentMenu();
            break;
        default:
            Console.WriteLine("Invalid option. Try again.\n");
            break;
    }
}

Console.WriteLine("Goodbye!");

/* -----------------------
   Input & Confirmation
   ----------------------- */
string ReadInput()
{
    var input = Console.ReadLine();
    if (input == null)
        return string.Empty;
    return input.Trim();
}

bool ConfirmYes(string message)
{
    Console.Write(message);
    var reply = ReadInput().ToLowerInvariant();
    return reply == "y" || reply == "yes";
}

/* -------------------- Student -------------------- */
void StudentMenu()
{
    while (true)
    {
        Console.WriteLine("\n-- Students --");
        Console.WriteLine("1) Add student");
        Console.WriteLine("2) List students");
        Console.WriteLine("3) Update student");
        Console.WriteLine("4) Delete student");
        Console.WriteLine("0) Back");
        Console.Write("Choose: ");
        string menuChoice = ReadInput();
        if (menuChoice == "0")
            return;

        switch (menuChoice)
        {
            case "1":
                AddStudent();
                break;
            case "2":
                ListStudents();
                break;
            case "3":
                UpdateStudent();
                break;
            case "4":
                DeleteStudent();
                break;
            default:
                Console.WriteLine("Invalid.");
                break;
        }
    }
}

void AddStudent()
{
    Console.Write("Name: ");
    string studentName = ReadInput();
    if (string.IsNullOrWhiteSpace(studentName))
    {
        Console.WriteLine("Name required.\n");
        return;
    }

    Console.Write("Cgpa (e.g. 3.25): ");
    double.TryParse(ReadInput(), out double studentCgpa);

    Console.Write("Date of Birth (yyyy-MM-dd): ");
    DateTime.TryParse(ReadInput(), out DateTime studentDob);

    Console.Write("Address (optional): ");
    string studentAddress = ReadInput();

    try
    {
        using var dbContext = new ApplicationDbContext();
        var newStudent =
            new Student
            {
                Name = studentName,
                Cgpa = studentCgpa,
                DateOfBirth = studentDob,
                Address = studentAddress
            };

        dbContext.Students.Add(newStudent);
        dbContext.SaveChanges();

        Console.WriteLine($"Student added (Id={newStudent.Id}).\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error saving student: " + ex.Message + "\n");
    }
}

void ListStudents()
{
    try
    {
        using var dbContext = new ApplicationDbContext();
        List<Student> students = dbContext.Students.OrderBy(s => s.Id).ToList();
        if (!students.Any())
        {
            Console.WriteLine("No students.\n");
            return;
        }

        foreach (var student in students)
        {
            string dobText = student.DateOfBirth == default(DateTime)
                                 ? ""
                                 : student.DateOfBirth.ToString("yyyy-MM-dd");
            Console.WriteLine(
                $"Id:{student.Id} | {student.Name} | Cgpa:{student.Cgpa} | Dob:{dobText} | Addr:{student.Address}");
        }
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error reading students: " + ex.Message + "\n");
    }
}

void UpdateStudent()
{
    Console.Write("Student Id to update: ");
    if (!int.TryParse(ReadInput(), out int studentId))
    {
        Console.WriteLine("Invalid id.\n");
        return;
    }

    try
    {
        using var dbContext = new ApplicationDbContext();
        var student = dbContext.Students.FirstOrDefault(x => x.Id == studentId);
        if (student == null)
        {
            Console.WriteLine("Student not found.\n");
            return;
        }

        Console.WriteLine($"Current Name: {student.Name}");
        Console.Write("New Name (leave blank to keep): ");
        string newName = ReadInput();
        if (!string.IsNullOrWhiteSpace(newName))
            student.Name = newName;

        Console.WriteLine($"Current Cgpa: {student.Cgpa}");
        Console.Write("New Cgpa (leave blank to keep): ");
        if (double.TryParse(ReadInput(), out double newCgpa))
            student.Cgpa = newCgpa;

        Console.WriteLine($"Current Dob: {student.DateOfBirth:yyyy-MM-dd}");
        Console.Write("New Dob (yyyy-MM-dd) (leave blank to keep): ");
        if (DateTime.TryParse(ReadInput(), out DateTime newDob))
            student.DateOfBirth = newDob;

        Console.WriteLine($"Current Address: {student.Address}");
        Console.Write("New Address (leave blank to keep): ");
        string newAddress = ReadInput();
        if (!string.IsNullOrWhiteSpace(newAddress))
            student.Address = newAddress;

        dbContext.SaveChanges();
        Console.WriteLine("Student updated.\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error updating student: " + ex.Message + "\n");
    }
}

void DeleteStudent()
{
    Console.Write("Student Id to delete: ");
    if (!int.TryParse(ReadInput(), out int studentId))
    {
        Console.WriteLine("Invalid id.\n");
        return;
    }

    try
    {
        using var dbContext = new ApplicationDbContext();
        var student = dbContext.Students.FirstOrDefault(x => x.Id == studentId);
        if (student == null)
        {
            Console.WriteLine("Student not found.\n");
            return;
        }

        if (!ConfirmYes($"Delete '{student.Name}' (Id={student.Id})? (y/n): "))
        {
            Console.WriteLine("Canceled.\n");
            return;
        }

        dbContext.Students.Remove(student);
        dbContext.SaveChanges();
        Console.WriteLine("Deleted.\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error deleting student: " + ex.Message + "\n");
    }
}

/* -------------------- Teacher -------------------- */
void TeacherMenu()
{
    while (true)
    {
        Console.WriteLine("\n-- Teachers --");
        Console.WriteLine("1) Add teacher");
        Console.WriteLine("2) List teachers");
        Console.WriteLine("3) Update teacher");
        Console.WriteLine("4) Delete teacher");
        Console.WriteLine("0) Back");
        Console.Write("Choose: ");
        string menuChoice = ReadInput();
        if (menuChoice == "0")
            return;

        switch (menuChoice)
        {
            case "1":
                AddTeacher();
                break;
            case "2":
                ListTeachers();
                break;
            case "3":
                UpdateTeacher();
                break;
            case "4":
                DeleteTeacher();
                break;
            default:
                Console.WriteLine("Invalid.");
                break;
        }
    }
}

void AddTeacher()
{
    Console.Write("Name: ");
    string teacherName = ReadInput();
    Console.Write("Username: ");
    string teacherUsername = ReadInput();
    Console.Write("Password: ");
    string teacherPassword = ReadInput();

    if (string.IsNullOrWhiteSpace(teacherName) ||
        string.IsNullOrWhiteSpace(teacherUsername))
    {
        Console.WriteLine("Name and username required.\n");
        return;
    }

    try
    {
        using var dbContext = new ApplicationDbContext();
        var teacher = new Teacher
        {
            Name = teacherName,
            Username = teacherUsername,
            Password = teacherPassword
        };
        dbContext.Teachers.Add(teacher);
        dbContext.SaveChanges();
        Console.WriteLine($"Teacher added (Id={teacher.Id}).\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error saving teacher: " + ex.Message + "\n");
    }
}

void ListTeachers()
{
    try
    {
        using var dbContext = new ApplicationDbContext();
        List<Teacher> teachers = dbContext.Teachers.OrderBy(t => t.Id).ToList();
        if (!teachers.Any())
        {
            Console.WriteLine("No teachers.\n");
            return;
        }

        foreach (var teacher in teachers)
            Console.WriteLine(
                $"Id:{teacher.Id} | {teacher.Name} | User:{teacher.Username}");
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error reading teachers: " + ex.Message + "\n");
    }
}

void UpdateTeacher()
{
    Console.Write("Teacher Id: ");
    if (!int.TryParse(ReadInput(), out int teacherId))
    {
        Console.WriteLine("Invalid id.\n");
        return;
    }

    try
    {
        using var dbContext = new ApplicationDbContext();
        var teacher = dbContext.Teachers.FirstOrDefault(x => x.Id == teacherId);
        if (teacher == null)
        {
            Console.WriteLine("Teacher not found.\n");
            return;
        }

        Console.Write($"New name (leave blank to keep) [{teacher.Name}]: ");
        string newName = ReadInput();
        if (!string.IsNullOrWhiteSpace(newName))
            teacher.Name = newName;

        Console.Write($"New username (leave blank to keep) [{teacher.Username}]: ");
        string newUsername = ReadInput();
        if (!string.IsNullOrWhiteSpace(newUsername))
            teacher.Username = newUsername;

        Console.Write("New password (leave blank to keep): ");
        string newPassword = ReadInput();
        if (!string.IsNullOrWhiteSpace(newPassword))
            teacher.Password = newPassword;

        dbContext.SaveChanges();
        Console.WriteLine("Teacher updated.\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error updating teacher: " + ex.Message + "\n");
    }
}

void DeleteTeacher()
{
    Console.Write("Teacher Id: ");
    if (!int.TryParse(ReadInput(), out int teacherId))
    {
        Console.WriteLine("Invalid id.\n");
        return;
    }

    try
    {
        using var dbContext = new ApplicationDbContext();
        var teacher = dbContext.Teachers.FirstOrDefault(x => x.Id == teacherId);
        if (teacher == null)
        {
            Console.WriteLine("Teacher not found.\n");
            return;
        }

        if (!ConfirmYes($"Delete '{teacher.Name}'? (y/n): "))
        {
            Console.WriteLine("Canceled.\n");
            return;
        }

        dbContext.Teachers.Remove(teacher);
        dbContext.SaveChanges();
        Console.WriteLine("Deleted.\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error deleting teacher: " + ex.Message + "\n");
    }
}

/* -------------------- Course & Topic -------------------- */
void CourseMenu()
{
    while (true)
    {
        Console.WriteLine("\n-- Courses & Topics --");
        Console.WriteLine("1) Add course");
        Console.WriteLine("2) List courses");
        Console.WriteLine("3) Add topic to course");
        Console.WriteLine("4) Remove topic from course");
        Console.WriteLine("5) Delete course");
        Console.WriteLine("0) Back");
        Console.Write("Choose: ");
        string menuChoice = ReadInput();
        if (menuChoice == "0")
            return;

        switch (menuChoice)
        {
            case "1":
                AddCourse();
                break;
            case "2":
                ListCourses();
                break;
            case "3":
                AddTopicToCourse();
                break;
            case "4":
                RemoveTopicFromCourse();
                break;
            case "5":
                DeleteCourse();
                break;
            default:
                Console.WriteLine("Invalid.");
                break;
        }
    }
}

void AddCourse()
{
    Console.Write("Title: ");
    string courseTitle = ReadInput();
    Console.Write("Fees (number): ");
    uint.TryParse(ReadInput(), out uint courseFees);

    if (string.IsNullOrWhiteSpace(courseTitle))
    {
        Console.WriteLine("Title required.\n");
        return;
    }

    try
    {
        using var dbContext = new ApplicationDbContext();
        var course = new Course { Title = courseTitle, Fees = courseFees };
        dbContext.Courses.Add(course);
        dbContext.SaveChanges();
        Console.WriteLine($"Course added (Id={course.Id}).\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error saving course: " + ex.Message + "\n");
    }
}

void ListCourses()
{
    try
    {
        using var dbContext = new ApplicationDbContext();
        List<Course> courses =
            dbContext.Courses.Include(c => c.Topics).OrderBy(c => c.Id).ToList();
        if (!courses.Any())
        {
            Console.WriteLine("No courses.\n");
            return;
        }

        foreach (var course in courses)
        {
            Console.WriteLine(
                $"CourseId:{course.Id} | {course.Title} | Fees:{course.Fees}");
            if (course.Topics != null && course.Topics.Any())
            {
                Console.WriteLine("  Topics:");
                foreach (var topic in course.Topics)
                    Console.WriteLine(
                        $"    Id:{topic.Id} | {topic.Name} | Duration:{topic.Duration}");
            }
        }
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error reading courses: " + ex.Message + "\n");
    }
}

void AddTopicToCourse()
{
    Console.Write("Course Id: ");
    if (!int.TryParse(ReadInput(), out int courseId))
    {
        Console.WriteLine("Invalid id.\n");
        return;
    }

    Console.Write("Topic name: ");
    string topicName = ReadInput();
    Console.Write("Duration (hours): ");
    uint.TryParse(ReadInput(), out uint topicDuration);

    if (string.IsNullOrWhiteSpace(topicName))
    {
        Console.WriteLine("Topic name required.\n");
        return;
    }

    try
    {
        using var dbContext = new ApplicationDbContext();
        var course = dbContext.Courses.Include(c => c.Topics)
                         .FirstOrDefault(c => c.Id == courseId);
        if (course == null)
        {
            Console.WriteLine("Course not found.\n");
            return;
        }

        course.Topics ??= new List<Topic>();
        var topic = new Topic
        {
            Name = topicName,
            Duration = topicDuration,
            CourseId = course.Id
        };
        course.Topics.Add(topic);
        dbContext.SaveChanges();
        Console.WriteLine("Topic added.\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error adding topic: " + ex.Message + "\n");
    }
}

void RemoveTopicFromCourse()
{
    Console.Write("Course Id: ");
    if (!int.TryParse(ReadInput(), out int courseId))
    {
        Console.WriteLine("Invalid id.\n");
        return;
    }
    Console.Write("Topic Id: ");
    if (!int.TryParse(ReadInput(), out int topicId))
    {
        Console.WriteLine("Invalid id.\n");
        return;
    }

    try
    {
        using var dbContext = new ApplicationDbContext();
        var course = dbContext.Courses.Include(c => c.Topics)
                         .FirstOrDefault(c => c.Id == courseId);
        if (course == null)
        {
            Console.WriteLine("Course not found.\n");
            return;
        }

        var topic = course.Topics?.FirstOrDefault(t => t.Id == topicId);
        if (topic == null)
        {
            Console.WriteLine("Topic not found.\n");
            return;
        }

        course.Topics.Remove(topic);
        dbContext.SaveChanges();
        Console.WriteLine("Topic removed.\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error removing topic: " + ex.Message + "\n");
    }
}

void DeleteCourse()
{
    Console.Write("Course Id to delete: ");
    if (!int.TryParse(ReadInput(), out int courseId))
    {
        Console.WriteLine("Invalid id.\n");
        return;
    }

    try
    {
        using var dbContext = new ApplicationDbContext();
        var course = dbContext.Courses.Include(x => x.Topics)
                         .FirstOrDefault(x => x.Id == courseId);
        if (course == null)
        {
            Console.WriteLine("Course not found.\n");
            return;
        }

        if (!ConfirmYes($"Delete course '{course.Title}'? (y/n): "))
        {
            Console.WriteLine("Canceled.\n");
            return;
        }

        dbContext.Courses.Remove(course);
        dbContext.SaveChanges();
        Console.WriteLine("Course deleted.\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error deleting course: " + ex.Message + "\n");
    }
}

/* -------------------- Enrollment (CourseStudent pivot) -------------------- */
void EnrollmentMenu()
{
    while (true)
    {
        Console.WriteLine("\n-- Enrollments --");
        Console.WriteLine("1) Enroll student to course");
        Console.WriteLine("2) List enrollments");
        Console.WriteLine("3) Remove enrollment");
        Console.WriteLine("0) Back");
        Console.Write("Choose: ");
        string menuChoice = ReadInput();
        if (menuChoice == "0")
            return;

        switch (menuChoice)
        {
            case "1":
                EnrollStudent();
                break;
            case "2":
                ListEnrollments();
                break;
            case "3":
                RemoveEnrollment();
                break;
            default:
                Console.WriteLine("Invalid.");
                break;
        }
    }
}

void EnrollStudent()
{
    Console.Write("Course Id: ");
    if (!int.TryParse(ReadInput(), out int courseId))
    {
        Console.WriteLine("Invalid id.\n");
        return;
    }
    Console.Write("Student Id: ");
    if (!int.TryParse(ReadInput(), out int studentId))
    {
        Console.WriteLine("Invalid id.\n");
        return;
    }

    try
    {
        using var dbContext = new ApplicationDbContext();
        var course = dbContext.Courses.FirstOrDefault(c => c.Id == courseId);
        var student = dbContext.Students.FirstOrDefault(s => s.Id == studentId);
        if (course == null || student == null)
        {
            Console.WriteLine("Course or student not found.\n");
            return;
        }

        bool enrollmentExists = dbContext.Set<CourseStudent>().Any(
            x => x.CourseId == courseId && x.StudentId == studentId);
        if (enrollmentExists)
        {
            Console.WriteLine("Student already enrolled in this course.\n");
            return;
        }

        dbContext.Set<CourseStudent>().Add(
            new CourseStudent { CourseId = courseId, StudentId = studentId });
        dbContext.SaveChanges();
        Console.WriteLine("Enrolled.\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error enrolling: " + ex.Message + "\n");
    }
}

void ListEnrollments()
{
    try
    {
        using var dbContext = new ApplicationDbContext();

        List<CourseStudent> enrollments = dbContext.Set<CourseStudent>()
                                              .Include(cs => cs.Course)
                                              .Include(cs => cs.Student)
                                              .OrderBy(cs => cs.CourseId)
                                              .ThenBy(cs => cs.StudentId)
                                              .ToList();

        if (!enrollments.Any())
        {
            Console.WriteLine("No enrollments.\n");
            return;
        }

        foreach (var enrollment in enrollments)
        {
            Console.WriteLine(
                $"CourseId:{enrollment.CourseId}({enrollment.Course?.Title}) | StudentId:{enrollment.StudentId}({enrollment.Student?.Name})");
        }
        Console.WriteLine();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error reading enrollments: " + ex.Message + "\n");
    }
}

void RemoveEnrollment()
{
    Console.Write("Course Id: ");
    if (!int.TryParse(ReadInput(), out int courseId))
    {
        Console.WriteLine("Invalid id.\n");
        return;
    }
    Console.Write("Student Id: ");
    if (!int.TryParse(ReadInput(), out int studentId))
    {
        Console.WriteLine("Invalid id.\n");
        return;
    }

    try
    {
        using var dbContext = new ApplicationDbContext();
        var enrollment = dbContext.Set<CourseStudent>().FirstOrDefault(
            x => x.CourseId == courseId && x.StudentId == studentId);

        if (enrollment == null)
        {
            Console.WriteLine("Enrollment not found.\n");
            return;
        }

        dbContext.Set<CourseStudent>().Remove(enrollment);
        dbContext.SaveChanges();
        Console.WriteLine("Enrollment removed.\n");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error removing enrollment: " + ex.Message + "\n");
    }
}
