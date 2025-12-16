using EvalApi.Src.Models;

namespace EvalApi.Src.Core.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> CreateAsync(User user, CancellationToken ct = default);
    Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default);
    Task<User?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<bool> ExistsAsync(int id, CancellationToken ct = default);
}
