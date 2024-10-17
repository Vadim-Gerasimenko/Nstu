using lab2.university.enums;
using lab2.university.persons;

namespace lab2.university
{
    public class University : IUniversity
    {
        public IEnumerable<IPerson> Persons { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }

        public University(IEnumerable<Student> students, IEnumerable<Teacher> teachers)
        {
            Students = students;
            Teachers = teachers;
            Persons = GetUnion(students, teachers);
        }

        public void Add(IPerson newPerson)
        {
            Persons = Add(Persons, newPerson);

            if (newPerson is Student)
                Students = Add(Students, (Student)newPerson);
            else
                Teachers = Add(Teachers, (Teacher)newPerson);
        }

        public void Remove(IPerson person)
        {
            Persons = Remove(Persons, person);

            if (person is Student)
                Students = Remove(Students, (Student)person);
            else
                Teachers = Remove(Teachers, (Teacher)person);
        }

        public IEnumerable<Teacher> FindByDepartment(Departments department)
        {
            foreach (var teacher in Teachers)
                if (teacher.Department == department)
                    yield return teacher;
        }

        public IEnumerable<Student> FindByGpa(float gpa)
        {
            foreach (var student in Students)
                if (student.Gpa == gpa)
                    yield return student;
        }

        public IEnumerable<IPerson> FindByLastName(string lastName)
        {
            foreach (var person in Persons)
                if (person.LastName == lastName)
                    yield return person;
        }

        public void Sort(Comparison<IPerson> comparer)
        {
            Persons = Sort(Persons, comparer);
            Students = Sort(Students, (Comparison<Student>)comparer);
            Teachers = Sort(Teachers, (Comparison<Teacher>)comparer);
        }

        private static IEnumerable<T> Sort<T>(IEnumerable<T> persons, Comparison<T> comparer)
            where T : IPerson
        {
            var personsList = new List<T>(persons);
            personsList.Sort(comparer);
            return personsList;
        }

        private static IEnumerable<IPerson> GetUnion<T, K>(IEnumerable<T> persons1, IEnumerable<K> persons2)
            where T : IPerson
            where K : IPerson
        {
            foreach (var person in persons1)
                yield return person;

            foreach (var person in persons2)
                yield return person;
        }

        private static IEnumerable<T> Add<T>(IEnumerable<T> persons, T newPerson)
            where T : IPerson
        {
            foreach (var person in persons)
                yield return person;

            yield return newPerson;
        }

        private static IEnumerable<T> Remove<T>(IEnumerable<T> persons, T newPerson)
            where T : IPerson
        {
            foreach (var person in persons)
                if (!person.Equals(newPerson))
                    yield return person;
        }
    }
}