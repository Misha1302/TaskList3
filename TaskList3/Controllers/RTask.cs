namespace TaskList3.Controllers;

using Microsoft.EntityFrameworkCore;

[PrimaryKey(nameof(Id))]
public sealed record RTask(string Title, string Description, string Content, bool Archived, bool Finished)
{
    public string Id
    {
        get => this.CreateToken().ToStrToken();
        // ReSharper disable once ValueParameterNotUsed
        set { }
    }
}