using CafeManager.Domain.Entities;
using CafeManager.Infrastructure.Persistence;
using MediatR;

namespace CafeManager.Application.Cafes.Commands;

public class CreateCafeCommandHandler : IRequestHandler<CreateCafeCommand, Guid>
{
    private readonly CafeDbContext _context;

    public CreateCafeCommandHandler(CafeDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
    {
        var cafe = new Cafe
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            Logo = request.Logo,
            Location = request.Location
        };

        _context.Cafes.Add(cafe);
        await _context.SaveChangesAsync(cancellationToken);

        return cafe.Id;
    }
}
