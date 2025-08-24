using CafeManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeManager.Application.Cafes.Commands;

public class DeleteCafeCommandHandler : IRequestHandler<DeleteCafeCommand, bool>
{
    private readonly CafeDbContext _context;
    public DeleteCafeCommandHandler(CafeDbContext context) => _context = context;

    public async Task<bool> Handle(DeleteCafeCommand request, CancellationToken cancellationToken)
    {
        var cafe = await _context.Cafes
            .Include(c => c.Employees)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        if (cafe == null) return false;

        _context.Employees.RemoveRange(cafe.Employees);
        _context.Cafes.Remove(cafe);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
