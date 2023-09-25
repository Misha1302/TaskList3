namespace TaskList3;

using TaskList3.Controllers;

public interface IPersonRepository
{
    void Create(Person person);
    void Delete(int id);
    Person? Get(int id);
    void Update(Person person);
}