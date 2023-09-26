namespace TaskList3.Controllers;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
    public IResult Login(string token)
    {
        if (token.FromStrToken(out _, out var password, out _) == null) 
            return Results.Unauthorized();
        
        var person = _personRepository.Get(token);

        return person?.Password != password
            ? Results.Unauthorized()
            : CreateResult(person);
    }

    [HttpPost]
    public IResult Authorize(Person p)
    {
        var person = _personRepository.Get(p.Id);

        if (person is not null) return Results.Problem();

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
        var t = DecomposeToken(token);
        if (t == null)
            return Results.Unauthorized();

        var value = t.Claims.First(x => x.Type == ClaimTypes.Name).Value;
        if (_personRepository.Get(value) != null)
        {
        }

        return null;
    }

    private static JwtSecurityToken? DecomposeToken(string token) => new JwtSecurityTokenHandler().ReadJwtToken(token);
}