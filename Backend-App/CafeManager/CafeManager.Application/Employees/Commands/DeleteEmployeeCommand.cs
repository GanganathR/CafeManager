using MediatR;

namespace CafeManager.Application.Employees.Commands;

public record DeleteEmployeeCommand(string Id) : IRequest<bool>;
