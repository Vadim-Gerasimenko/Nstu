namespace lab1;

internal class Program
{
    static void Main(string[] args)
    {
        var persons = new List<Person>();
        const string inFilePath = "..\\..\\..\\files\\in.txt";

        using (StreamReader inputFile = new StreamReader(inFilePath))
            for (string? fileLine; (fileLine = inputFile.ReadLine()) != null; persons.Add(Person.Parse(fileLine))) ;


        Console.Write("""
                Choose a sorting:
                1: Ascending
                2: Descending
                <Another char or seq>: Without sorting
                Enter: 
                """);

        switch (Console.ReadLine())
        {
            case "1":
                persons.Sort((person1, person2) => person1.Surname.CompareTo(person2.Surname));
                break;
            case "2":
                persons.Sort((person1, person2) => person2.Surname.CompareTo(person1.Surname));
                break;
        }

        const string outFilePath = "..\\..\\..\\files\\out.txt";

        using (StreamWriter outputFile = new StreamWriter(outFilePath))
            persons.ForEach(outputFile.WriteLine);
    }
}