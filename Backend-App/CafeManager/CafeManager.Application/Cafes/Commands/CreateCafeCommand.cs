using CafeManager.Domain.Entities;
using MediatR;

namespace CafeManager.Application.Cafes.Commands;

public record CreateCafeCommand(string Name, string Description, string? Logo, string Location) : IRequest<Guid>;
