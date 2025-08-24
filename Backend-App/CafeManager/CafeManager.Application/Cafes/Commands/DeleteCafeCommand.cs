using MediatR;

namespace CafeManager.Application.Cafes.Commands;

public record DeleteCafeCommand(Guid Id) : IRequest<bool>;
