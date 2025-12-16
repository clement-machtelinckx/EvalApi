using EvalApi.Src.Core.Exceptions;
using EvalApi.Src.Core.Repositories.Interfaces;
using EvalApi.Src.Core.Services.Interfaces;
using EvalApi.Src.Models;

namespace EvalApi.Src.Core.Services.Implementations;

public sealed class PostService : IPostService
{
    private readonly IUserRepository _users;
    private readonly IPostRepository _posts;

    public PostService(IUserRepository users, IPostRepository posts)
    {
        _users = users;
        _posts = posts;
    }

    public async Task<Post> CreateForUserAsync(int routeUserId, Post post, CancellationToken ct = default)
    {
        var userExists = await _users.ExistsAsync(routeUserId, ct);
        if (!userExists)
            throw new NotFoundException($"User with id {routeUserId} not found");

        // route/body check is done in controller (per spec)
        return await _posts.CreateAsync(post, ct);
    }

    public async Task<IReadOnlyList<Post>> GetByUserIdAsync(int userId, CancellationToken ct = default)
    {
        var userExists = await _users.ExistsAsync(userId, ct);
        if (!userExists)
            throw new NotFoundException($"User with id {userId} not found");

        return await _posts.GetByUserIdAsync(userId, ct);
    }

    public async Task<Post> GetByIdAsync(int postId, CancellationToken ct = default)
    {
        var post = await _posts.GetByIdAsync(postId, ct);
        if (post is null)
            throw new NotFoundException($"Post with id {postId} not found");

        return post;
    }

    public async Task<Post> UpdateTitleBodyAsync(int postId, int dtoUserId, string title, string body, CancellationToken ct = default)
    {
        var existing = await _posts.GetByIdAsync(postId, ct);
        if (existing is null)
            throw new NotFoundException($"Post with id {postId} not found");

        if (existing.UserId != dtoUserId)
            throw new BadRequestException("userId does not match the post owner");

        // Update ONLY title/body (per spec)
        var updated = await _posts.UpdateTitleBodyAsync(postId, title, body, ct);
        if (updated is null)
            throw new NotFoundException($"Post with id {postId} not found");

        return updated;
    }

    public async Task DeleteAsync(int postId, CancellationToken ct = default)
    {
        var deleted = await _posts.DeleteAsync(postId, ct);
        if (!deleted)
            throw new NotFoundException($"Post with id {postId} not found");
    }
}

