using lab2.university;
using lab2.university.persons;
using System.Globalization;
using System.Linq.Expressions;
using System.Xml;

internal class Program
{
    public static void Main()
    {
        var persons = new List<IPerson>();

        const string studentsFilePath = "university\\persons\\data\\students.txt";
        using (var reader = new StreamReader(studentsFilePath))
            for (string? fileLine; (fileLine = reader.ReadLine()) != null; persons.Add(Student.Parse(fileLine))) ;

        const string teachersFilePath = "university\\persons\\data\\teachers.txt";
        using (var reader = new StreamReader(teachersFilePath))
            for (string? fileLine; (fileLine = reader.ReadLine()) != null; persons.Add(Teacher.Parse(fileLine))) ;

        var university = new University(persons);

        bool isOpen = true;
        while (isOpen)
        {
            Console.WriteLine("""
                MENU:
                1 - Find by last name
                2 - Find by Department
                3 - Add person
                4 - Remove person
                5 - Show people
                6 - Exit
                """);
            Console.Write("Select option: ");

            switch (Console.ReadLine())
            {
                case "1":
                    FindbyLastName(university);
                    break;
                case "2":
                    FindbyDepartment(university);
                    break;
                case "3":
                    AddPerson(university);
                    break;
                case "4":
                    RemovePerson(university);
                    break;
                case "5":
                    ShowPeople(university);
                    break;
                case "6":
                    isOpen = false;
                    break;
                default:
                    Console.WriteLine("Invalid input");
                    break;
            }

            Console.WriteLine("Click to continue");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private static void Print<T>(IEnumerable<T> items)
    {
        foreach (var item in items)
            Console.WriteLine(item);
    }

    private static void FindbyLastName(IUniversity university)
    {
        Console.Write("Enter last name: ");
        string? lastName = Console.ReadLine();
        if (lastName != null)
            Print(university.FindByLastName(lastName));
    }

    private static void FindbyDepartment(IUniversity university)
    {
        Console.Write("Enter department: ");
        string? department = Console.ReadLine();
        if (department != null)
            Print(university.FindByDepartment(department));
    }

    private static void AddPerson(IUniversity university)
    {
        Console.Write("""
                       Who do you want to add:
                       1 - Student
                       <Anoter char> - Teacher

                       """);

        if (Console.ReadLine() == "1")
        {
            Console.WriteLine("Enter the student's details");
            string? student = Console.ReadLine();
            try
            {
                university.Add(Student.Parse(student));
            }
            catch
            {
                Console.WriteLine("Invalid data");
            }
        }
        else
        {
            Console.WriteLine("Enter the teacher's details");
            string? teacher = Console.ReadLine();
            try
            {
                university.Add(Teacher.Parse(teacher));
            }
            catch
            {
                Console.WriteLine("Invalid data");
            }
        }
    }

    private static void RemovePerson(IUniversity university)
    {
        Console.Write("Enter last name: ");
        string? lastName = Console.ReadLine();
        List<IPerson> persons = university.FindByLastName(lastName).ToList();

        for (int i = 1; i <= persons.Count; ++i)
            Console.WriteLine($"{i}. {persons[i - 1]}");

        try
        {
            Console.Write("Enter the number of person: ");
            int choice = int.Parse(Console.ReadLine());
            university.Remove(persons[choice - 1]);
        }
        catch
        {
            Console.WriteLine("Invalid data");
        }
    }

    private static void ShowPeople(IUniversity university)
    {
        Console.WriteLine("""
                        1 - Teachers
                        2 - Students
                        <Another char> - All persons
                        """);

        string? option = Console.ReadLine();
        if (option == "1")
            Print(university.Teachers);
        else if (option == "2")
            Print(university.Students);
        else
            Print(university.Persons);
    }
}