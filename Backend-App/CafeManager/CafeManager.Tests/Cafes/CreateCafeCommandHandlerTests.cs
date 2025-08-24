using CafeManager.Application.Cafes.Commands;
using CafeManager.Infrastructure.Persistence;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace CafeManager.Tests.Cafes;

public class CreateCafeCommandHandlerTests
{
    private readonly CafeDbContext _context;

    public CreateCafeCommandHandlerTests()
    {
        var options = new DbContextOptionsBuilder<CafeDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _context = new CafeDbContext(options);
    }

    [Fact]
    public async Task Handle_ShouldCreateCafe()
    {
        var handler = new CreateCafeCommandHandler(_context);
        var command = new CreateCafeCommand("Test Cafe", "Description", null, "Singapore");

        var id = await handler.Handle(command, CancellationToken.None);

        var cafe = await _context.Cafes.FindAsync(id);
        cafe.Should().NotBeNull();
        cafe!.Name.Should().Be("Test Cafe");
    }
}
