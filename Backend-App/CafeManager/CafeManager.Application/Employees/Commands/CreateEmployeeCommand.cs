using MediatR;

namespace CafeManager.Application.Employees.Commands;

public record CreateEmployeeCommand(
    string Name,
    string EmailAddress,
    string PhoneNumber,
    string Gender,
    DateTime StartDate,
    Guid? CafeId
) : IRequest<string>;
