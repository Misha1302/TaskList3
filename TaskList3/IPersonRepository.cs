namespace TaskList3;

using TaskList3.Controllers;

public interface IPersonRepository
{
    void Create(Person person);
    void Delete(string id);
    Person? Get(string id);
    void Update(Person person);
}