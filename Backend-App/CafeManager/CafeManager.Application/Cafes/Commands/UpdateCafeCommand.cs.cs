using MediatR;

namespace CafeManager.Application.Cafes.Commands;

public record UpdateCafeCommand(Guid Id, string Name, string Description, string? Logo, string Location) : IRequest<bool>;
