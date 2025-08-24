using MediatR;

namespace CafeManager.Application.Employees.Commands;

public record UpdateEmployeeCommand(
    string Id,
    string Name,
    string EmailAddress,
    string PhoneNumber,
    string Gender,
    DateTime StartDate,
    Guid? CafeId
) : IRequest<bool>;
