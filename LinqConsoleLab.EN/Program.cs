using System.Text;
using LinqConsoleLab.EN.Data;
using LinqConsoleLab.EN.Exercises;

Console.OutputEncoding = Encoding.UTF8;
UniversityData.Seed();

var exercises = new LinqExercises();
var options = new List<(string Key, string Description, Func<IEnumerable<string>> Action)>
{
    ("0", "Show input data overview", UniversityData.GetOverview),
    ("1", "Students from Warsaw", exercises.Task01_StudentsFromWarsaw),
    ("2", "Student email addresses", exercises.Task02_StudentEmailAddresses),
    ("3", "Students sorted alphabetically", exercises.Task03_StudentsSortedAlphabetically),
    ("4", "First Analytics course", exercises.Task04_FirstAnalyticsCourse),
    ("5", "Check whether any inactive enrollment exists", exercises.Task05_IsThereAnyInactiveEnrollment),
    ("6", "Check whether all lecturers have a department", exercises.Task06_DoAllLecturersHaveDepartment),
    ("7", "Count active enrollments", exercises.Task07_CountActiveEnrollments),
    ("8", "Distinct student cities", exercises.Task08_DistinctStudentCities),
    ("9", "Three newest enrollments", exercises.Task09_ThreeNewestEnrollments),
    ("10", "Second page of courses", exercises.Task10_SecondPageOfCourses),
    ("11", "Join students with enrollments", exercises.Task11_JoinStudentsWithEnrollments),
    ("12", "Student-course pairs", exercises.Task12_StudentCoursePairs),
    ("13", "Group enrollments by course", exercises.Task13_GroupEnrollmentsByCourse),
    ("14", "Average grade per course", exercises.Task14_AverageGradePerCourse),
    ("15", "Lecturers and their course counts", exercises.Task15_LecturersAndCourseCounts),
    ("16", "Highest grade of each student", exercises.Task16_HighestGradePerStudent),
    ("17", "Challenge: students with more than one active course", exercises.Challenge01_StudentsWithMoreThanOneActiveCourse),
    ("18", "Challenge: April 2026 courses without final grades", exercises.Challenge02_AprilCoursesWithoutFinalGrades),
    ("19", "Challenge: lecturers and average grade across their courses", exercises.Challenge03_LecturersAndAverageGradeAcrossTheirCourses),
    ("20", "Challenge: cities and number of active enrollments", exercises.Challenge04_CitiesAndActiveEnrollmentCounts)
};

var optionMap = options.ToDictionary(option => option.Key, StringComparer.OrdinalIgnoreCase);

while (true)
{
    Console.Clear();
    Console.WriteLine("LINQconsole");
    Console.WriteLine("==============================================");
    Console.WriteLine("Choose task by number to run the selected method.");
    Console.WriteLine("If the method still empty the app will show TODO message.");
    Console.WriteLine();

    foreach (var option in options)
    {
        Console.WriteLine($"{option.Key,2}. {option.Description}");
    }

    Console.WriteLine();
    Console.Write("Enter a task number or X to exit: ");
    var choice = Console.ReadLine()?.Trim();

    if (string.Equals(choice, "x", StringComparison.OrdinalIgnoreCase))
    {
        break;
    }

    if (choice is null || !optionMap.TryGetValue(choice, out var selectedOption))
    {
        Console.WriteLine();
        Console.WriteLine("Unknown option. Press Enter to return to the menu.");
        Console.ReadLine();
        continue;
    }

    Console.WriteLine();
    Console.WriteLine($"=== {selectedOption.Description} ===");
    Console.WriteLine();

    try
    {
        DisplayResult(selectedOption.Action());
    }
    catch (NotImplementedException ex)
    {
        Console.WriteLine(ex.Message);
    }
    catch (Exception ex)
    {
        Console.WriteLine("An unexpected error occurred:");
        Console.WriteLine(ex.Message);
    }

    Console.WriteLine();
    Console.WriteLine("Press Enter to return to the menu.");
    Console.ReadLine();
}

static void DisplayResult(IEnumerable<string> result)
{
    var rows = result.ToList();

    if (rows.Count == 0)
    {
        Console.WriteLine("No results to display.");
        return;
    }

    for (var i = 0; i < rows.Count; i++)
    {
        Console.WriteLine($"{i + 1,2}. {rows[i]}");
    }
}
