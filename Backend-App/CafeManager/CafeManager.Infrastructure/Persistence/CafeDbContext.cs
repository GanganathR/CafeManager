using CafeManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CafeManager.Infrastructure.Persistence;

public class CafeDbContext : DbContext
{
    public CafeDbContext(DbContextOptions<CafeDbContext> options) : base(options) { }

    public DbSet<Cafe> Cafes { get; set; }
    public DbSet<Employee> Employees { get; set; }

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    var cafe1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
    var cafe2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
    var cafe3Id = Guid.Parse("33333333-3333-3333-3333-333333333333");

    modelBuilder.Entity<Cafe>().HasData(
        new Cafe { Id = cafe1Id, Name = "Cafe Mocha", Description = "Best coffee in town", Location = "Singapore" },
        new Cafe { Id = cafe2Id, Name = "Latte House", Description = "Cosy cafe for friends", Location = "Singapore" },
        new Cafe { Id = cafe3Id, Name = "Brew & Bite", Description = "Quick bites and coffee", Location = "Penang" }
    );
}
}
