namespace TaskList3.Controllers;

using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
public sealed record Person(string Name, string Email, string Password)
{
    public string Id
    {
        get => this.CreateToken().ToStrToken();
        // ReSharper disable once ValueParameterNotUsed
        set { }
    }
}