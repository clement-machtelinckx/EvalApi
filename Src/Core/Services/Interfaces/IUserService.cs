using EvalApi.Src.Models;

namespace EvalApi.Src.Core.Services.Interfaces;

public interface IUserService
{
    Task<User> CreateAsync(User user, CancellationToken ct = default);
    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default);
    Task<bool> ExistsAsync(int userId, CancellationToken ct = default);
}
