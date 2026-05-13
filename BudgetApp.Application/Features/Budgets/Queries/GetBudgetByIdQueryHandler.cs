using BudgetApp.Application.Features.Budgets.DTOs;
using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Budgets.Queries;

// Hanterar GetBudgetByIdQuery — hämtar en specifik budget från databasen
public class GetBudgetByIdQueryHandler : IRequestHandler<GetBudgetByIdQuery, BudgetDto?>
{
    private readonly IBudgetRepository _budgetRepository;

    // IBudgetRepository injiceras via konstruktorn
    public GetBudgetByIdQueryHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    public async Task<BudgetDto?> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken)
    {
        // Hämtar budgeten från databasen via repository
        var budget = await _budgetRepository.GetByIdAsync(request.Id);

        // Returnerar null om budgeten inte hittas
        if (budget == null)
            return null;

        // Returnerar en DTO med budgetdata
        return new BudgetDto
        {
            Id = budget.Id,
            UserId = budget.UserId,
            Name = budget.Name,
            Month = budget.Month,
            Year = budget.Year,
            TotalAmount = budget.TotalAmount,
            CreatedAt = budget.CreatedAt
        };
    }
}
