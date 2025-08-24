using CafeManager.Application.Employees.Queries;
using CafeManager.Infrastructure.Persistence;
using CafeManager.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CafeManager.Tests.Employees;

public class GetEmployeesQueryHandlerTests
{
    private readonly CafeDbContext _context;

    public GetEmployeesQueryHandlerTests()
    {
        var options = new DbContextOptionsBuilder<CafeDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new CafeDbContext(options);

        var cafe = new Cafe { Id = Guid.NewGuid(), Name = "Cafe A", Description = "Desc A", Location = "Singapore" };
        _context.Cafes.Add(cafe);
        _context.Employees.Add(new Employee { Id = "UI0001", Name = "Alice", EmailAddress = "alice@x.com", PhoneNumber = "91234567", Gender = "Female", StartDate = DateTime.UtcNow.AddDays(-30), CafeId = cafe.Id });
        _context.SaveChanges();
    }

    [Fact]
    public async Task Handle_ShouldReturnEmployeesWithDaysWorked()
    {
        var handler = new GetEmployeesQueryHandler(_context);

        var result = await handler.Handle(new GetEmployeesQuery("Cafe A"), CancellationToken.None);

        result.Should().HaveCount(1);
        result.First().DaysWorked.Should().BeGreaterThan(0);
    }
}
