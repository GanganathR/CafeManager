using CafeManager.Domain.Entities;
using CafeManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeManager.Application.Employees.Commands;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, string>
{
    private readonly CafeDbContext _context;

    public CreateEmployeeCommandHandler(CafeDbContext context) => _context = context;

    public async Task<string> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        // 🔹 Generate a new unique ID in required format: UIXXXXXXX
        string newId;
        do
        {
            newId = GenerateEmployeeId();
        } 
        while (await _context.Employees.AnyAsync(e => e.Id == newId, cancellationToken));

        // 🔹 Create new Employee entity
        var employee = new Employee
        {
            Id = newId,
            Name = request.Name,
            EmailAddress = request.EmailAddress,
            PhoneNumber = request.PhoneNumber,
            Gender = request.Gender,
            StartDate = request.StartDate,
            CafeId = request.CafeId
        };

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync(cancellationToken);

        return employee.Id;
    }

    // 🔹 Helper: Generate Employee ID in format "UIXXXXXXX"
    private static string GenerateEmployeeId()
    {
        // Example: UI123AB4
        var random = Guid.NewGuid().ToString("N").Substring(0, 7).ToUpper();
        return $"UI{random}";
    }
}
