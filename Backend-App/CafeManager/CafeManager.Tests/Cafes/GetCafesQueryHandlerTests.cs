using CafeManager.Application.Cafes.Queries;
using CafeManager.Infrastructure.Persistence;
using CafeManager.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CafeManager.Tests.Cafes;

public class GetCafesQueryHandlerTests
{
    private readonly CafeDbContext _context;

    public GetCafesQueryHandlerTests()
    {
        var options = new DbContextOptionsBuilder<CafeDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new CafeDbContext(options);

        // Seed test data
        var cafe1 = new Cafe { Id = Guid.NewGuid(), Name = "Cafe A", Description = "Desc A", Location = "Singapore" };
        var cafe2 = new Cafe { Id = Guid.NewGuid(), Name = "Cafe B", Description = "Desc B", Location = "Penang" };
        _context.Cafes.AddRange(cafe1, cafe2);
        _context.Employees.Add(new Employee { Id = "UI0001", Name = "Alice", EmailAddress = "a@x.com", PhoneNumber = "91234567", Gender = "Female", StartDate = DateTime.UtcNow.AddDays(-10), CafeId = cafe1.Id });
        _context.SaveChanges();
    }

    [Fact]
    public async Task Handle_ShouldReturnCafes_SortedByEmployeeCount()
    {
        var handler = new GetCafesQueryHandler(_context);

        var result = await handler.Handle(new GetCafesQuery(null), CancellationToken.None);

        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.First().Name.Should().Be("Cafe A"); // has 1 employee
    }

    [Fact]
    public async Task Handle_ShouldFilterByLocation()
    {
        var handler = new GetCafesQueryHandler(_context);

        var result = await handler.Handle(new GetCafesQuery("Penang"), CancellationToken.None);

        result.Should().HaveCount(1);
        result.First().Name.Should().Be("Cafe B");
    }
}
