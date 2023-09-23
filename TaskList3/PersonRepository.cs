namespace TaskList3;

using TaskList2;
using TaskList2.Controllers;

public class PersonRepository : IPersonRepository, IDisposable
{
    private readonly NpgsqlContext _context;

    public PersonRepository(NpgsqlContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _context.Dispose();
    }

    public void Delete(int id)
    {
        _context.Persons.Remove(_context.Persons.Find(id) ?? throw new InvalidOperationException());
        _context.SaveChanges();
    }

    public Person? Get(int id) => _context.Persons.Find(id);

    public void Create(Person person)
    {
        _context.Persons.Add(person);
        _context.SaveChanges();
    }

    public void Update(Person person)
    {
        _context.Persons.Update(Get(person.Id) ?? throw new InvalidOperationException());
        _context.SaveChanges();
    }

    public List<Person> GetPersons() => _context.Persons.ToList();
}