using EvalApi.Src.Core.Repositories.Entities;
using EvalApi.Src.Core.Repositories.Interfaces;
using EvalApi.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace EvalApi.Src.Core.Repositories.Implementations;

public sealed class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<User> CreateAsync(User user, CancellationToken ct = default)
    {
        var entity = new UserEntity
        {
            Name = user.Name,
            Username = user.Username,
            Email = user.Email
        };

        _db.Users.Add(entity);
        await _db.SaveChangesAsync(ct);

        return MapToModel(entity);
    }

    public async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken ct = default)
    {
        var entities = await _db.Users
            .AsNoTracking()
            .OrderBy(u => u.Id)
            .ToListAsync(ct);

        return entities.Select(MapToModel).ToList();
    }

    public async Task<User?> GetByIdAsync(int id, CancellationToken ct = default)
    {
        var entity = await _db.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, ct);

        return entity is null ? null : MapToModel(entity);
    }

    public Task<bool> ExistsAsync(int id, CancellationToken ct = default)
    {
        return _db.Users.AnyAsync(u => u.Id == id, ct);
    }

    private static User MapToModel(UserEntity entity)
    {
        return new User
        {
            Id = entity.Id,
            Name = entity.Name,
            Username = entity.Username,
            Email = entity.Email
        };
    }
}
