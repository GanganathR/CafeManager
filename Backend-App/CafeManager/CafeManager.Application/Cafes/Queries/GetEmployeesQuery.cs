using CafeManager.Application.DTOs;
using MediatR;

namespace CafeManager.Application.Employees.Queries;

public record GetEmployeesQuery(string? CafeName) : IRequest<List<EmployeeDto>>;
