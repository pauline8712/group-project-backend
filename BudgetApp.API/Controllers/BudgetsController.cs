using BudgetApp.Application.Features.Budgets.Commands;
using BudgetApp.Application.Features.Budgets.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BudgetApp.API.Controllers;

[ApiController]
[Route("api/budgets")]
public class BudgetsController : ControllerBase
{
    // MediatR injiceras via konstruktorn — används för att skicka Commands och Queries
    private readonly IMediator _mediator;

    public BudgetsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetAll(Guid userId)
    {
        try
        {
            var result = await _mediator.Send(new GetAllBudgetsQuery { UserId = userId });
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    //Hämtar en specifik budget baserat på Id
    //Returnerar 404 om budgeten inte hittas 
    [HttpGet("single/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetBudgetByIdQuery { Id = id });
            if (result == null)
                return NotFound($"Budget with id {id} not found");
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}