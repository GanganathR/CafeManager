using CafeManager.Application.DTOs;
using CafeManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeManager.Application.Employees.Queries;

public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<EmployeeDto>>
{
    private readonly CafeDbContext _context;

    public GetEmployeesQueryHandler(CafeDbContext context) => _context = context;

    public async Task<List<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Employees
            .Include(e => e.Cafe)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.CafeName))
            query = query.Where(e => e.Cafe != null && e.Cafe.Name == request.CafeName);

var employees = await query
    .Select(e => new EmployeeDto
    {
        Id = e.Id,
        Name = e.Name,
        EmailAddress = e.EmailAddress,
        PhoneNumber = e.PhoneNumber,
        Gender = e.Gender,
        DaysWorked = EF.Functions.DateDiffDay(e.StartDate, DateTime.UtcNow),

        CafeId = e.CafeId, 
        CafeName = e.Cafe != null ? e.Cafe.Name : string.Empty 
    })
    .OrderByDescending(e => e.DaysWorked)
    .ToListAsync(cancellationToken);


        return employees;
    }
}
