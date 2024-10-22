using lab2.university.enums;
using System.Globalization;

namespace lab2.university.persons
{
    public record Teacher(string Name, string Patronymic, string LastName,
        DateTime BirthDate, Staff Post, int Seniority, string Department) : IPerson
    {
        public int Age => CalcAge.GetAge(BirthDate); 

        public static Teacher Parse(string text)
        {
            string[] teacherData = text.Split(" ", 7);
            return new Teacher(
                teacherData[0], teacherData[1], teacherData[2],
                DateTime.ParseExact(teacherData[3], "dd.MM.yyyy", CultureInfo.InvariantCulture),
                Enum.Parse<Staff>(teacherData[4]),
                int.Parse(teacherData[5]),
                teacherData[6]);
        }

        public override string ToString()
        {
            return FormattableString.Invariant($"""
                Full name: {LastName} {Name} {Patronymic}
                Birth date: {BirthDate:dd/MM/yyyy}
                Age: {Age}
                Department: {Department}
                Post: {Post}
                Seniority: {Seniority}

                """);
        }
    }
}