using LinqConsoleLab.EN.Models;

namespace LinqConsoleLab.EN.Data;

public static class UniversityData
{
    private static bool _isSeeded;

    public static List<Student> Students { get; } = [];

    public static List<Course> Courses { get; } = [];

    public static List<Lecturer> Lecturers { get; } = [];

    public static List<Enrollment> Enrollments { get; } = [];

    public static void Seed()
    {
        if (_isSeeded)
        {
            return;
        }

        Students.AddRange(
        [
            new Student { Id = 1, IndexNumber = "s30101", FirstName = "Anna", LastName = "Kowalska", Email = "anna.kowalska@uni.local", City = "Warsaw", BirthDate = new DateTime(2002, 5, 12) },
            new Student { Id = 2, IndexNumber = "s30102", FirstName = "Marek", LastName = "Nowak", Email = "marek.nowak@uni.local", City = "Gdansk", BirthDate = new DateTime(2001, 11, 3) },
            new Student { Id = 3, IndexNumber = "s30103", FirstName = "Julia", LastName = "Zielinska", Email = "julia.zielinska@uni.local", City = "Krakow", BirthDate = new DateTime(2003, 1, 8) },
            new Student { Id = 4, IndexNumber = "s30104", FirstName = "Tomasz", LastName = "Lis", Email = "tomasz.lis@uni.local", City = "Warsaw", BirthDate = new DateTime(2002, 9, 17) },
            new Student { Id = 5, IndexNumber = "s30105", FirstName = "Olga", LastName = "Maj", Email = "olga.maj@uni.local", City = "Wroclaw", BirthDate = new DateTime(2001, 7, 26) },
            new Student { Id = 6, IndexNumber = "s30106", FirstName = "Pawel", LastName = "Kurek", Email = "pawel.kurek@uni.local", City = "Poznan", BirthDate = new DateTime(2000, 12, 14) },
            new Student { Id = 7, IndexNumber = "s30107", FirstName = "Lena", LastName = "Adamska", Email = "lena.adamska@uni.local", City = "Warsaw", BirthDate = new DateTime(2002, 3, 30) },
            new Student { Id = 8, IndexNumber = "s30108", FirstName = "Karol", LastName = "Wysocki", Email = "karol.wysocki@uni.local", City = "Lublin", BirthDate = new DateTime(2001, 2, 20) }
        ]);

        Lecturers.AddRange(
        [
            new Lecturer { Id = 1, FirstName = "Ewa", LastName = "Morawska", Department = "Data Engineering", EmploymentDate = new DateTime(2018, 10, 1) },
            new Lecturer { Id = 2, FirstName = "Piotr", LastName = "Jaworski", Department = "Cloud Solutions", EmploymentDate = new DateTime(2020, 2, 15) },
            new Lecturer { Id = 3, FirstName = "Marta", LastName = "Lewandowska", Department = "Business Analytics", EmploymentDate = new DateTime(2017, 9, 1) },
            new Lecturer { Id = 4, FirstName = "Adam", LastName = "Sowa", Department = "Software Design", EmploymentDate = new DateTime(2019, 5, 10) }
        ]);

        Courses.AddRange(
        [
            new Course { Id = 1, Title = "LINQ Foundations", Category = ".NET", Ects = 4, LecturerId = 1, StartDate = new DateTime(2026, 3, 10), EndDate = new DateTime(2026, 6, 20) },
            new Course { Id = 2, Title = "API Design", Category = "Web", Ects = 5, LecturerId = 4, StartDate = new DateTime(2026, 3, 15), EndDate = new DateTime(2026, 6, 30) },
            new Course { Id = 3, Title = "Data Analysis in C#", Category = "Analytics", Ects = 6, LecturerId = 3, StartDate = new DateTime(2026, 4, 1), EndDate = new DateTime(2026, 7, 10) },
            new Course { Id = 4, Title = "Cloud for Developers", Category = "Cloud", Ects = 5, LecturerId = 2, StartDate = new DateTime(2026, 4, 10), EndDate = new DateTime(2026, 7, 15) },
            new Course { Id = 5, Title = "Application Databases", Category = "Databases", Ects = 6, LecturerId = 1, StartDate = new DateTime(2026, 5, 5), EndDate = new DateTime(2026, 7, 25) },
            new Course { Id = 6, Title = "Software Testing", Category = "Quality", Ects = 4, LecturerId = 4, StartDate = new DateTime(2026, 3, 22), EndDate = new DateTime(2026, 6, 18) }
        ]);

        Enrollments.AddRange(
        [
            new Enrollment { Id = 1, StudentId = 1, CourseId = 1, EnrollmentDate = new DateTime(2026, 2, 15), FinalGrade = 4.5, IsActive = true },
            new Enrollment { Id = 2, StudentId = 1, CourseId = 3, EnrollmentDate = new DateTime(2026, 2, 18), FinalGrade = null, IsActive = true },
            new Enrollment { Id = 3, StudentId = 2, CourseId = 1, EnrollmentDate = new DateTime(2026, 2, 20), FinalGrade = 3.5, IsActive = false },
            new Enrollment { Id = 4, StudentId = 2, CourseId = 4, EnrollmentDate = new DateTime(2026, 2, 21), FinalGrade = 4.0, IsActive = true },
            new Enrollment { Id = 5, StudentId = 3, CourseId = 2, EnrollmentDate = new DateTime(2026, 2, 22), FinalGrade = 5.0, IsActive = true },
            new Enrollment { Id = 6, StudentId = 3, CourseId = 5, EnrollmentDate = new DateTime(2026, 2, 23), FinalGrade = null, IsActive = true },
            new Enrollment { Id = 7, StudentId = 4, CourseId = 2, EnrollmentDate = new DateTime(2026, 2, 24), FinalGrade = 3.0, IsActive = false },
            new Enrollment { Id = 8, StudentId = 4, CourseId = 6, EnrollmentDate = new DateTime(2026, 2, 26), FinalGrade = null, IsActive = true },
            new Enrollment { Id = 9, StudentId = 5, CourseId = 3, EnrollmentDate = new DateTime(2026, 2, 27), FinalGrade = 4.0, IsActive = true },
            new Enrollment { Id = 10, StudentId = 6, CourseId = 4, EnrollmentDate = new DateTime(2026, 2, 28), FinalGrade = 4.5, IsActive = true },
            new Enrollment { Id = 11, StudentId = 7, CourseId = 1, EnrollmentDate = new DateTime(2026, 3, 1), FinalGrade = null, IsActive = true },
            new Enrollment { Id = 12, StudentId = 7, CourseId = 5, EnrollmentDate = new DateTime(2026, 3, 2), FinalGrade = 5.0, IsActive = true },
            new Enrollment { Id = 13, StudentId = 8, CourseId = 6, EnrollmentDate = new DateTime(2026, 3, 3), FinalGrade = 3.5, IsActive = true },
            new Enrollment { Id = 14, StudentId = 8, CourseId = 2, EnrollmentDate = new DateTime(2026, 3, 5), FinalGrade = null, IsActive = true },
            new Enrollment { Id = 15, StudentId = 5, CourseId = 5, EnrollmentDate = new DateTime(2026, 3, 6), FinalGrade = 4.5, IsActive = false },
            new Enrollment { Id = 16, StudentId = 6, CourseId = 3, EnrollmentDate = new DateTime(2026, 3, 7), FinalGrade = null, IsActive = true }
        ]);

        _isSeeded = true;
    }
    public static IEnumerable<string> GetOverview()
    {
        yield return $"Students: {Students.Count}";
        yield return $"Lecturers: {Lecturers.Count}";
        yield return $"Courses: {Courses.Count}";
        yield return $"Enrollments: {Enrollments.Count}";
        yield return string.Empty;
        yield return "Sample students:";

        foreach (var student in Students.Take(3))
        {
            yield return $"{student.IndexNumber} | {student.FirstName} {student.LastName} | {student.City}";
        }

        yield return string.Empty;
        yield return "Sample courses:";

        foreach (var course in Courses.Take(3))
        {
            yield return $"{course.Title} | {course.Category} | start: {course.StartDate:yyyy-MM-dd}";
        }
    }
}
