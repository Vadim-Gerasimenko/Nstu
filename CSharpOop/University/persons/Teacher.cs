using lab2.university.enums;
using System.Globalization;

namespace lab2.university.persons
{
    public record Teacher(string Name, string Patronymic, string LastName,
        DateTime BirthDate, Departments Department, Staff Post, int Seniority) : IPerson
    {
        public int Age
        {
            get
            {
                var dateTimeNow = DateTime.Now;
                var age = dateTimeNow.Year - BirthDate.Year;

                if (BirthDate > dateTimeNow.AddYears(-age)) --age;
                return age;
            }
        }

        public static Teacher Parse(string text)
        {
            string[] teacherData = text.Split(' ');
            return new Teacher(
                teacherData[0], teacherData[1], teacherData[2],
                DateTime.ParseExact(teacherData[3], "dd.MM.yyyy", CultureInfo.InvariantCulture),
                (Departments)Enum.Parse(typeof(Departments), teacherData[4]),
                (Staff)Enum.Parse(typeof(Staff), teacherData[5]),
                int.Parse(teacherData[6])
                );
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