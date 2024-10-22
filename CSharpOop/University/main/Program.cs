using lab2.university;
using lab2.university.enums;
using lab2.university.persons;

internal class Program
{
    public static void Main()
    {
        var students = new List<Student>();
        const string studentsFilePath = "..\\..\\..\\university\\persons\\data\\students.txt";

        using (var reader = new StreamReader(studentsFilePath))
            for (string? fileLine; (fileLine = reader.ReadLine()) != null; students.Add(Student.Parse(fileLine))) ;

        var teachers = new List<Teacher>();
        const string teachersFilePath = "..\\..\\..\\university\\persons\\data\\teachers.txt";

        using (var reader = new StreamReader(teachersFilePath))
            for (string? fileLine; (fileLine = reader.ReadLine()) != null; teachers.Add(Teacher.Parse(fileLine))) ;

        var university = new University(students, teachers);
        string studentData = "Open Sslevich Grep 20.06.2005 1 AM-01 5.0";

        university.Add(Student.Parse(studentData));
        Console.WriteLine("University students after adding a new student:");
        print(university.Students);

        university.Remove(Student.Parse(studentData));
        Console.WriteLine("University students after removing a new student:");
        print(university.Students);

        Console.WriteLine("All university persons:");
        print(university.Persons);

        const string lastName = "Grep";
        Console.WriteLine($"Namesakes with last name '{lastName}':");
        print(university.FindByLastName(lastName));

        const float gpa = (float)4.5;
        Console.WriteLine($"Students with a {gpa:F2} GPA:");
        print(university.FindByGpa(gpa));

        const Departments department = Departments.TheoreticalAndAppliedInformatics;
        Console.WriteLine($"Teachers of the {department} department:");
        print(university.FindByDepartment(department));

        Console.Write("""
            Choose a sorting method:
            1: Ascending
            2: Descending
            <Another char or seq>: Without sorting
            Enter: 
            """);

        switch (Console.ReadLine())
        {
            case "1":
                university.Sort((p1, p2) => string.Compare(p1.LastName, p2.LastName));
                break;
            case "2":
                university.Sort((p1, p2) => string.Compare(p2.LastName, p1.LastName));
                break;
        }

        Console.WriteLine("Persons:");
        print(university.Persons);

        Console.WriteLine("Students:");
        print(university.Students);

        Console.WriteLine("Teachers:");
        print(university.Teachers);
    }

    private static void print<T>(IEnumerable<T> items)
    {
        foreach (var item in items)
            Console.WriteLine(item);
    }
}