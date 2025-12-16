using EvalApi.Src.Models;

namespace EvalApi.Src.Core.Repositories.Interfaces;

public interface IPostRepository
{
    Task<Post> CreateAsync(Post post, CancellationToken ct = default);
    Task<IReadOnlyList<Post>> GetByUserIdAsync(int userId, CancellationToken ct = default);
    Task<Post?> GetByIdAsync(int postId, CancellationToken ct = default);
    Task<Post?> UpdateTitleBodyAsync(int postId, string title, string body, CancellationToken ct = default);
    Task<bool> DeleteAsync(int postId, CancellationToken ct = default);
}
