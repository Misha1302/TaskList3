namespace TaskList2.Controllers;

public sealed record RTask(string Title, string Description, string Content, bool Archived, bool Finished);