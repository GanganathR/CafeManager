using CafeManager.Application.DTOs;
using MediatR;

namespace CafeManager.Application.Cafes.Queries;

public record GetCafesQuery(string? Location) : IRequest<List<CafeDto>>;
