namespace lab2.university.persons
{
    public interface IPerson
    {
        string Name { get; }
        string Patronymic { get; }
        string LastName { get; }
        DateTime BirthDate { get; }
        int Age { get; }
    }

    public static class CalcAge
    {
        public static int GetAge(DateTime birthDate)
        {
            var dateTimeNow = DateTime.Now;
            var age = dateTimeNow.Year - birthDate.Year;

            if (birthDate > dateTimeNow.AddYears(-age)) --age;
            return age;
        }
    }
}