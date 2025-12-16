using EvalApi.Src.Core.Repositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvalApi.Src.Core.Repositories;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<UserEntity> Users => Set<UserEntity>();
    public DbSet<PostEntity> Posts => Set<PostEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Name).IsRequired();
            entity.Property(u => u.Username).IsRequired();
            entity.Property(u => u.Email).IsRequired();

            entity.HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<PostEntity>(entity =>
        {
            entity.ToTable("Posts");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Title).IsRequired();
            entity.Property(p => p.Body).IsRequired();

            entity.HasIndex(p => p.UserId);
        });
    }
}
