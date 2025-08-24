using CafeManager.Application.DTOs;
using CafeManager.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CafeManager.Application.Cafes.Queries;

public class GetCafesQueryHandler : IRequestHandler<GetCafesQuery, List<CafeDto>>
{
    private readonly CafeDbContext _context;

    public GetCafesQueryHandler(CafeDbContext context) => _context = context;

    public async Task<List<CafeDto>> Handle(GetCafesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Cafes
            .Include(c => c.Employees)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Location))
            query = query.Where(c => c.Location == request.Location);

        var cafes = await query
            .Select(c => new CafeDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                Logo = c.Logo,
                Location = c.Location,
                Employees = c.Employees.Count
            })
            .OrderByDescending(c => c.Employees)
            .ToListAsync(cancellationToken);

        return cafes;
    }
}
