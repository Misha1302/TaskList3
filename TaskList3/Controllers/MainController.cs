namespace TaskList3.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]/[action]")]
public class MainController : ControllerBase
{
    private readonly IPersonRepository _personRepository;

    public MainController(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    [HttpPost]
    public IResult Login(string token) =>
        CanLogin(token, out var p)
            ? Results.Unauthorized()
            : CreateResult(p!);

    private bool CanLogin(string token, out Person? person)
    {
        person = null;
        if (token.TokenToPerson(out _, out _, out _) == null)
            return false;

        person = _personRepository.Get(token);

        return person != null;
    }

    [HttpPost]
    public IResult Authorize(Person p)
    {
        var person = _personRepository.Get(p.Id);

        if (person is not null)
            return Results.Problem();

        _personRepository.Create(p);

        return CreateResult(p);
    }

    private static IResult CreateResult(Person p) =>
        Results.Json(
            new
            {
                access_token = p.CreateToken().ToStrToken()
            }
        );

    [HttpPost]
    public IResult CreateTask(string token, RTask task)
    {
        if (!CanLogin(token, out _))
            return Results.Unauthorized();

        return null;
    }
}