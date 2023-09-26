namespace TaskList3.Controllers;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

public static class Extensions
{
    private const string Password = "password";
    private const string Name = "name";
    private const string Email = "email";
    
    private const string Title = "title";
    private const string Description = "description";
    private const string Content = "content";
    private const string Archived = "archived";
    private const string Finished = "finished";
    
    private static readonly JwtSecurityTokenHandler _tokenHandler = new();

    public static JwtSecurityToken CreateToken(this Person p) =>
        new(
            AuthOptions.Issuer,
            AuthOptions.Audience,
            new List<Claim>
            {
                new(Email, p.Email),
                new(Name, p.Name),
                new(Password, p.Password)
            },
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256)
        );

    public static JwtSecurityToken CreateToken(this RTask task) =>
        new(
            AuthOptions.Issuer,
            AuthOptions.Audience,
            new List<Claim>
            {
                new(Title, task.Title),
                new(Description, task.Description),
                new(Content, task.Content),
                new(Archived, task.Archived.ToString()),
                new(Finished, task.Finished.ToString())
            },
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256)
        );

    public static JwtSecurityToken? TokenToPerson(this string p, out string email, out string password, out string name)
    {
        var t = _tokenHandler.CanReadToken(p) ? _tokenHandler.ReadJwtToken(p) : null;

        email = t?.Claims.First(x => x.Type == Email).Value!;
        password = t?.Claims.First(x => x.Type == Password).Value!;
        name = t?.Claims.First(x => x.Type == Name).Value!;

        return t;
    }

    public static JwtSecurityToken? TokenToTask(this string p, out string title, out string description, out string content, out bool archived, out bool finished)
    {
        var t = _tokenHandler.CanReadToken(p) ? _tokenHandler.ReadJwtToken(p) : null;

        title = t?.Claims.First(x => x.Type == Title).Value!;
        description = t?.Claims.First(x => x.Type == Description).Value!;
        content = t?.Claims.First(x => x.Type == Content).Value!;
        archived = Convert.ToBoolean(t?.Claims.First(x => x.Type == Archived).Value!);
        finished = Convert.ToBoolean(t?.Claims.First(x => x.Type == Finished).Value!);

        return t;
    }

    public static string ToStrToken(this JwtSecurityToken jwt) => _tokenHandler.WriteToken(jwt);
}