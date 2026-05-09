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

    public  CategoryController (IMediator mediator)
    {
        _mediator = mediator;
    }
}