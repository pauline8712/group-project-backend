using BudgetApp.Application.Features.Categories.Commands;
using BudgetApp.Application.Features.Categories.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace BudgetApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CategoryController : ControllerBase
{
    // MediatR injiceras via konstruktorn — används för att skicka Commands och Queries
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{budgetId}")]
    public async Task<IActionResult> GetAll(Guid budgetId)
    {
        try
        {
            var result = await _mediator.Send(new GetCategoriesQuery { BudgetId = budgetId });
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Logga felet här
            return StatusCode(500, ex.Message);
        }

        // Hämtar en specifik kategori baserat på Id
        // Returnerar 404 om kategorin inte hittas
        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _mediator.Send(new GetCategoryByIdQuery { Id = id });
                if (result == null)
                    return NotFound($"Category with id {id} not found");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}