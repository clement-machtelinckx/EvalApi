using EvalApi.Src.Core.Repositories.Entities;
using EvalApi.Src.Core.Repositories.Interfaces;
using EvalApi.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace EvalApi.Src.Core.Repositories.Implementations;

public sealed class PostRepository : IPostRepository
{
    private readonly AppDbContext _db;

    public PostRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Post> CreateAsync(Post post, CancellationToken ct = default)
    {
        var entity = new PostEntity
        {
            UserId = post.UserId,
            Title = post.Title,
            Body = post.Body
        };

        _db.Posts.Add(entity);
        await _db.SaveChangesAsync(ct);

        return MapToModel(entity);
    }

    public async Task<IReadOnlyList<Post>> GetByUserIdAsync(int userId, CancellationToken ct = default)
    {
        var entities = await _db.Posts
            .AsNoTracking()
            .Where(p => p.UserId == userId)
            .OrderBy(p => p.Id)
            .ToListAsync(ct);

        return entities.Select(MapToModel).ToList();
    }

    public async Task<Post?> GetByIdAsync(int postId, CancellationToken ct = default)
    {
        var entity = await _db.Posts
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == postId, ct);

        return entity is null ? null : MapToModel(entity);
    }

    public async Task<Post?> UpdateTitleBodyAsync(int postId, string title, string body, CancellationToken ct = default)
    {
        var entity = await _db.Posts.FirstOrDefaultAsync(p => p.Id == postId, ct);
        if (entity is null) return null;

        entity.Title = title;
        entity.Body = body;

        await _db.SaveChangesAsync(ct);

        return MapToModel(entity);
    }

    public async Task<bool> DeleteAsync(int postId, CancellationToken ct = default)
    {
        var entity = await _db.Posts.FirstOrDefaultAsync(p => p.Id == postId, ct);
        if (entity is null) return false;

        _db.Posts.Remove(entity);
        await _db.SaveChangesAsync(ct);
        return true;
    }

    private static Post MapToModel(PostEntity entity)
    {
        return new Post
        {
            Id = entity.Id,
            UserId = entity.UserId,
            Title = entity.Title,
            Body = entity.Body
        };
    }
}
