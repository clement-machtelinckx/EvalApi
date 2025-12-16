using EvalApi.Src.Models;

namespace EvalApi.Src.Core.Services.Interfaces;

public interface IPostService
{
    Task<Post> CreateForUserAsync(int routeUserId, Post post, CancellationToken ct = default);
    Task<IReadOnlyList<Post>> GetByUserIdAsync(int userId, CancellationToken ct = default);
    Task<Post> GetByIdAsync(int postId, CancellationToken ct = default);
    Task<Post> UpdateTitleBodyAsync(int postId, int dtoUserId, string title, string body, CancellationToken ct = default);
    Task DeleteAsync(int postId, CancellationToken ct = default);
}
