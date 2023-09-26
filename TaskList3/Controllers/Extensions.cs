namespace TaskList3.Controllers;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

public static class Extensions
{
    private const string Password = "password";
    private const string Name = "name";
    private const string Email = "email";
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

    public static JwtSecurityToken? FromStrToken(this string p, out string email, out string password, out string name)
    {
        var t = _tokenHandler.CanReadToken(p) ? _tokenHandler.ReadJwtToken(p) : null;

        email = t?.Claims.First(x => x.Type == Email).Value!;
        password = t?.Claims.First(x => x.Type == Password).Value!;
        name = t?.Claims.First(x => x.Type == Name).Value!;

        return t;
    }

    public static string ToStrToken(this JwtSecurityToken jwt) => _tokenHandler.WriteToken(jwt);
}