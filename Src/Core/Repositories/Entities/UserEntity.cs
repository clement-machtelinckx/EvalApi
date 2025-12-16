namespace EvalApi.Src.Core.Repositories.Entities;

public sealed class UserEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public List<PostEntity>? Posts { get; set; }
}
