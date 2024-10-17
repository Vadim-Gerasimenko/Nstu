using System.Globalization;

namespace lab1;

internal record Person(string Surname, string Name, string Patronymic, bool Gender, float Height, DateTime BirthDate)
{
    private readonly bool gender = Gender;
    public int Age
    {
        get
        {
            int age = DateTime.Now.Year - BirthDate.Year;

            if (BirthDate > DateTime.Now.AddYears(-age)) --age;
            return age;
        }
    }

    public static Person Parse(string text)
    {
        string[] personData = text.Split(" ");
        return new Person(personData[0], personData[1], personData[2], personData[3] == "woman",
            float.Parse(personData[4], CultureInfo.InvariantCulture),
            DateTime.ParseExact(personData[5], "dd.MM.yyyy", CultureInfo.InvariantCulture));
    }

    public override string ToString()
    {
        return FormattableString.Invariant($"""
                Full name: {Surname} {Name} {Patronymic}
                Gender: {(gender ? "woman" : "man")}
                Height: {Height:F3}
                Birthday date: {BirthDate:dd/MM/yyyy}
                Age: {Age}

                """);
    }
}