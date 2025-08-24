using CafeManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeManager.Application.Cafes.Commands;

public class UpdateCafeCommandHandler : IRequestHandler<UpdateCafeCommand, bool>
{
    private readonly CafeDbContext _context;
    public UpdateCafeCommandHandler(CafeDbContext context) => _context = context;

    public async Task<bool> Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
    {
        var cafe = await _context.Cafes.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (cafe == null) return false;

        cafe.Name = request.Name;
        cafe.Description = request.Description;
        cafe.Logo = request.Logo;
        cafe.Location = request.Location;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
