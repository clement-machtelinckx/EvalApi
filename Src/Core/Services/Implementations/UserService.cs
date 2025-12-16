using EvalApi.Src.Core.Repositories.Interfaces;
using EvalApi.Src.Core.Services.Interfaces;
using EvalApi.Src.Models;

namespace EvalApi.Src.Core.Services.Implementations;

public sealed class UserService : IUserService
{
    private readonly IUserRepository _users;

    public UserService(IUserRepository users)
    {
        _users = users;
    }

    public Task<User> CreateAsync(User user, CancellationToken ct = default)
        => _users.CreateAsync(user, ct);

    public Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default)
        => _users.GetAllAsync(ct);

    public Task<bool> ExistsAsync(int userId, CancellationToken ct = default)
        => _users.ExistsAsync(userId, ct);
}
