using CafeManager.Application.Cafes.Commands;
using CafeManager.Application.Cafes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CafeManager.Api.Controllers;

[ApiController]
[Route("cafes")]
public class CafesController : ControllerBase
{
    private readonly IMediator _mediator;
    public CafesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? location)
        => Ok(await _mediator.Send(new GetCafesQuery(location)));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCafeCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCafeCommand command)
        => (await _mediator.Send(command)) ? Ok() : NotFound();

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
        => (await _mediator.Send(new DeleteCafeCommand(id))) ? NoContent() : NotFound();
}
