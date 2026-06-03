using System.Globalization;
using LinqConsoleLab.EN.Data;
using LinqConsoleLab.EN.Models;

namespace LinqConsoleLab.EN.Exercises;

public sealed class LinqExercises
{
    // Task:
    // Find all students who live in Warsaw.
    //Return the index number, full name, and city.
    public IEnumerable<string> Task01_StudentsFromWarsaw()
    {
        EnsureSeeded();

        return UniversityData.Students
            .Where(student => student.City == "Warsaw")
            .Select(FormatStudentRow);
    }

    // Task:
    // Build a list of all student email addresses.
    // Use projection so that you do not return whole objects.
    public IEnumerable<string> Task02_StudentEmailAddresses()
    {
        EnsureSeeded();

        return UniversityData.Students
            .Select(student => student.Email);
    }

    // Task:
    // Sort students alphabetically by last name and then by first name.
    // Return the index number and full name.
    public IEnumerable<string> Task03_StudentsSortedAlphabetically()
    {
        EnsureSeeded();

        return UniversityData.Students
            .OrderBy(student => student.LastName)
            .ThenBy(student => student.FirstName)
            .Select(student => $"{student.IndexNumber} | {student.FirstName} {student.LastName}");
    }

    // Task:
    // Find the first course from the Analytics category.
    // If such a course does not exist, return a text message.

    public IEnumerable<string> Task04_FirstAnalyticsCourse()
    {
        EnsureSeeded();

        var course = UniversityData.Courses.FirstOrDefault(course => course.Category == "Analytics");

        if (course is null)
        {
            yield return "No Analytics course found.";
            yield break;
        }

        yield return $"{course.Title} | start: {FormatDate(course.StartDate)}";
    }
    // Task:
    // Check whether there is at least one inactive enrollment in the data set.
    // Return one line with a True/False or Yes/No answer.
    public IEnumerable<string> Task05_IsThereAnyInactiveEnrollment()
    {
        EnsureSeeded();

        var exists = UniversityData.Enrollments.Any(enrollment => !enrollment.IsActive);
        yield return exists ? "Yes" : "No";
    }

    // Task:
    // Check whether every lecturer has a department assigned.
    // Use a method that validates the condition for the whole collection.

    public IEnumerable<string> Task06_DoAllLecturersHaveDepartment()
    {
        EnsureSeeded();

        var allHaveDepartment = UniversityData.Lecturers.All(lecturer => !string.IsNullOrWhiteSpace(lecturer.Department));
        yield return allHaveDepartment ? "Yes" : "No";
    }
    // Task:
    // Count how many active enrollments exist in the system.

    public IEnumerable<string> Task07_CountActiveEnrollments()
    {
        EnsureSeeded();

        yield return UniversityData.Enrollments.Count(enrollment => enrollment.IsActive).ToString(CultureInfo.InvariantCulture);
    }
    // Task:
    // Return a sorted list of distinct student cities.
    public IEnumerable<string> Task08_DistinctStudentCities()
    {
        EnsureSeeded();

        return UniversityData.Students
            .Select(student => student.City)
            .Distinct()
            .OrderBy(city => city);
    }
    
    // Return the three newest enrollments.
    // Show the enrollment date, student identifier, and course identifier.
 
    public IEnumerable<string> Task09_ThreeNewestEnrollments()
    {
        EnsureSeeded();

        return UniversityData.Enrollments
            .OrderByDescending(enrollment => enrollment.EnrollmentDate)
            .Take(3)
            .Select(enrollment =>
                $"{FormatDate(enrollment.EnrollmentDate)} | studentId: {enrollment.StudentId} | courseId: {enrollment.CourseId}");
    }

    // Task
    // Implement simple pagination for the course list.
    // Assume a page size of 2 and return the second page of data.

    public IEnumerable<string> Task10_SecondPageOfCourses()
    {
        EnsureSeeded();

        return UniversityData.Courses
            .OrderBy(course => course.Title)
            .Skip(2)
            .Take(2)
            .Select(course => $"{course.Title} | {course.Category}");
    }

    // Task :
    // Join students with enrollments by StudentId.
    // Return the full student name and the enrollment date.
    public IEnumerable<string> Task11_JoinStudentsWithEnrollments()
    {
        EnsureSeeded();

        return UniversityData.Students
            .Join(
                UniversityData.Enrollments,
                student => student.Id,
                enrollment => enrollment.StudentId,
                (student, enrollment) => new { student, enrollment })
            .OrderBy(item => item.enrollment.EnrollmentDate)
            .ThenBy(item => item.student.LastName)
            .ThenBy(item => item.student.FirstName)
            .Select(item => $"{item.student.FirstName} {item.student.LastName} | {FormatDate(item.enrollment.EnrollmentDate)}");
    }
    //Task:
    // Prepare all student-course pairs based on enrollments.
    // Use an approach that flattens the data into a single result sequence.

    public IEnumerable<string> Task12_StudentCoursePairs()
    {
        EnsureSeeded();

        return UniversityData.Students
            .SelectMany(
                student => UniversityData.Enrollments.Where(enrollment => enrollment.StudentId == student.Id),
                (student, enrollment) => new { student, enrollment })
            .Join(
                UniversityData.Courses,
                item => item.enrollment.CourseId,
                course => course.Id,
                (item, course) => $"{item.student.FirstName} {item.student.LastName} | {course.Title}")
            .OrderBy(row => row);
    }

    //Task
    //Group enrollments by course and return the course title together with the number of enrollments.
    public IEnumerable<string> Task13_GroupEnrollmentsByCourse()
    {
        EnsureSeeded();

        return UniversityData.Enrollments
            .Join(
                UniversityData.Courses,
                enrollment => enrollment.CourseId,
                course => course.Id,
                (enrollment, course) => course.Title)
            .GroupBy(title => title)
            .OrderBy(group => group.Key)
            .Select(group => $"{group.Key} | enrollments: {group.Count()}");
    }
    //Task 
    // Calculate the average final grade for each course.
    // Ignore records where the final grade is null.
    public IEnumerable<string> Task14_AverageGradePerCourse()
    {
        EnsureSeeded();

        return UniversityData.Enrollments
            .Where(enrollment => enrollment.FinalGrade.HasValue)
            .Join(
                UniversityData.Courses,
                enrollment => enrollment.CourseId,
                course => course.Id,
                (enrollment, course) => new { course.Title, enrollment.FinalGrade })
            .GroupBy(item => item.Title)
            .OrderBy(group => group.Key)
            .Select(group =>
            {
                var average = group.Average(item => item.FinalGrade!.Value);
                return $"{group.Key} | average grade: {FormatNumber(average)}";
            });
    }

    // Task:
    // For each lecturer, count how many courses are assigned to that lecturer.
    // Return the full lecturer name and the course count.
    public IEnumerable<string> Task15_LecturersAndCourseCounts()
    {
        EnsureSeeded();

        return UniversityData.Lecturers
            .GroupJoin(
                UniversityData.Courses,
                lecturer => lecturer.Id,
                course => course.LecturerId,
                (lecturer, courses) => new { lecturer, courseCount = courses.Count() })
            .OrderBy(item => item.lecturer.LastName)
            .ThenBy(item => item.lecturer.FirstName)
            .Select(item => $"{item.lecturer.FirstName} {item.lecturer.LastName} | courses: {item.courseCount}");
    }

    //Task:
    //For each student, find the highest final grade.
    // Skip students who do not have any graded enrollment yet.

    public IEnumerable<string> Task16_HighestGradePerStudent()
    {
        EnsureSeeded();

        return UniversityData.Enrollments
            .Where(enrollment => enrollment.FinalGrade.HasValue)
            .Join(
                UniversityData.Students,
                enrollment => enrollment.StudentId,
                student => student.Id,
                (enrollment, student) => new { student, enrollment.FinalGrade })
            .GroupBy(item => new { item.student.FirstName, item.student.LastName })
            .OrderBy(group => group.Key.LastName)
            .ThenBy(group => group.Key.FirstName)
            .Select(group =>
            {
                var highest = group.Max(item => item.FinalGrade!.Value);
                return $"{group.Key.FirstName} {group.Key.LastName} | highest grade: {FormatNumber(highest)}";
            });
    }

    // Challenge:
    //Find students who have more than one active enrollment.
    // Return the full name and the number of active courses.
    public IEnumerable<string> Challenge01_StudentsWithMoreThanOneActiveCourse()
    {
        EnsureSeeded();

        return UniversityData.Enrollments
            .Where(enrollment => enrollment.IsActive)
            .Join(
                UniversityData.Students,
                enrollment => enrollment.StudentId,
                student => student.Id,
                (enrollment, student) => student)
            .GroupBy(student => new { student.FirstName, student.LastName })
            .Where(group => group.Count() > 1)
            .OrderBy(group => group.Key.LastName)
            .ThenBy(group => group.Key.FirstName)
            .Select(group => $"{group.Key.FirstName} {group.Key.LastName} | active courses: {group.Count()}");
    }

    // Challenge:
    // List the courses that start in April 2026 and do not have any final grades assigned yet.
    public IEnumerable<string> Challenge02_AprilCoursesWithoutFinalGrades()
    {
        EnsureSeeded();

        return UniversityData.Courses
            .Where(course => course.StartDate.Year == 2026 && course.StartDate.Month == 4)
            .GroupJoin(
                UniversityData.Enrollments,
                course => course.Id,
                enrollment => enrollment.CourseId,
                (course, enrollments) => new { course, enrollments })
            .Where(item => item.enrollments.All(enrollment => enrollment.FinalGrade is null))
            .OrderBy(item => item.course.Title)
            .Select(item => item.course.Title);
    }

    // Challenge:
    // Calculate the average final grade for every lecturer across all of their courses.
    // Ignore missing grades but still keep the lecturers in mind as the reporting dimension.

    public IEnumerable<string> Challenge03_LecturersAndAverageGradeAcrossTheirCourses()
    {
        EnsureSeeded();

        return UniversityData.Lecturers
            .GroupJoin(
                UniversityData.Courses,
                lecturer => lecturer.Id,
                course => course.LecturerId,
                (lecturer, courses) => new
                {
                    lecturer,
                    grades = courses
                        .SelectMany(course => UniversityData.Enrollments.Where(enrollment => enrollment.CourseId == course.Id))
                        .Where(enrollment => enrollment.FinalGrade.HasValue)
                        .Select(enrollment => enrollment.FinalGrade!.Value)
                        .ToList()
                })
            .OrderBy(item => item.lecturer.LastName)
            .ThenBy(item => item.lecturer.FirstName)
            .Select(item =>
            {
                var average = item.grades.Count == 0 ? (double?)null : item.grades.Average();
                var averageText = average.HasValue ? FormatNumber(average.Value) : "N/A";
                return $"{item.lecturer.FirstName} {item.lecturer.LastName} | average grade: {averageText}";
            });
    }
    // Challenge:
    // Show student cities and the number of active enrollments created by students from each city.
    // Sort the result by the active enrollment count in descending order.
    public IEnumerable<string> Challenge04_CitiesAndActiveEnrollmentCounts()
    {
        EnsureSeeded();

        return UniversityData.Enrollments
            .Where(enrollment => enrollment.IsActive)
            .Join(
                UniversityData.Students,
                enrollment => enrollment.StudentId,
                student => student.Id,
                (enrollment, student) => student.City)
            .GroupBy(city => city)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key)
            .Select(group => $"{group.Key} | active enrollments: {group.Count()}");
    }

    private static void EnsureSeeded()
    {
        UniversityData.Seed();
    }

    private static string FormatStudentRow(Student student)
    {
        return $"{student.IndexNumber} | {student.FirstName} {student.LastName} | {student.City}";
    }

    private static string FormatDate(DateTime value)
    {
        return value.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
    }

    private static string FormatNumber(double value)
    {
        return value.ToString("0.##", CultureInfo.InvariantCulture);
    }

    private static NotImplementedException NotImplemented(string methodName)
    {
        return new NotImplementedException(
            $"Complete method {methodName} in Exercises/LinqExercises.cs and runthe command again");
    }
}
