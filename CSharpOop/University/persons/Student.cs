using System.Globalization;

namespace lab2.university.persons
{
    public record Student(string Name, string Patronymic, string LastName,
        DateTime BirthDate, int Course, string Group, float Gpa) : IPerson
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

        public static Student Parse(string text)
        {
            string[] studentData = text.Split(" ");
            return new Student(
                studentData[0], studentData[1], studentData[2],
                DateTime.ParseExact(studentData[3], "dd.MM.yyyy", CultureInfo.InvariantCulture),
                int.Parse(studentData[4]), studentData[5],
                float.Parse(studentData[6], CultureInfo.InvariantCulture)
                );
        }

        public override string ToString()
        {
            return FormattableString.Invariant($"""
                Full name: {LastName} {Name} {Patronymic}
                Birth date: {BirthDate:dd/MM/yyyy}
                Age: {Age}
                Course: {Course}
                Group: {Group}
                Gpa: {Gpa:F2}

                """);
        }
    }
}