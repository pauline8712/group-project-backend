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
}