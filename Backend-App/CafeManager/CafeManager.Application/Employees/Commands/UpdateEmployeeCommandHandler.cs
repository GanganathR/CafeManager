using CafeManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeManager.Application.Employees.Commands;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
{
    private readonly CafeDbContext _context;
    public UpdateEmployeeCommandHandler(CafeDbContext context) => _context = context;

    public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
        if (employee == null) return false;

        employee.Name = request.Name;
        employee.EmailAddress = request.EmailAddress;
        employee.PhoneNumber = request.PhoneNumber;
        employee.Gender = request.Gender;
        employee.StartDate = request.StartDate;
        employee.CafeId = request.CafeId;

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
