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
}