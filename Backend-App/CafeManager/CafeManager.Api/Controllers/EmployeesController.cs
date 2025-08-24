using CafeManager.Application.Employees.Commands;
using CafeManager.Application.Employees.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CafeManager.Api.Controllers;

[ApiController]
[Route("employees")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;
    public EmployeesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? cafe)
        => Ok(await _mediator.Send(new GetEmployeesQuery(cafe)));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id }, id);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommand command)
        => (await _mediator.Send(command)) ? Ok() : NotFound();

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
        => (await _mediator.Send(new DeleteEmployeeCommand(id))) ? NoContent() : NotFound();
}
