using lab2.university.persons;

namespace lab2.university
{
    public class University : IUniversity
    {
        private List<IPerson> _persons;
        public IEnumerable<IPerson> Persons => _persons.OrderBy(p => p.LastName); 
        public IEnumerable<Student> Students => _persons.OfType<Student>().OrderBy(p => p.LastName); 
        public IEnumerable<Teacher> Teachers => _persons.OfType<Teacher>().OrderBy(p => p.LastName); 

        public University(List<IPerson> persons) =>  _persons = persons;

        public void Add(IPerson newPerson) => _persons.Add(newPerson);

        public void Remove(IPerson person) => _persons.Remove(person);

        public IEnumerable<IPerson> FindByLastName(string lastName)
            => _persons.Where(p => p.LastName.Contains(lastName, StringComparison.InvariantCultureIgnoreCase));

        public IEnumerable<Teacher> FindByDepartment(string department)
            => _persons.OfType<Teacher>().Where(p => p.Department.Contains(department, StringComparison.InvariantCultureIgnoreCase)).OrderBy(p => p.Post);
    }
}