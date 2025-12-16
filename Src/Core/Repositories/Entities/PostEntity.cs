namespace EvalApi.Src.Core.Repositories.Entities;

public sealed class PostEntity
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;

    public UserEntity? User { get; set; }
}
