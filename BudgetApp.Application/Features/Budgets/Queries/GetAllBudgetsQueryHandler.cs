using BudgetApp.Application.Features.Budgets.DTOs;
using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Budgets.Queries;

// Hanterar GetAllBudgetsQuery — hämtar alla budgetar för en specifik användare
public class GetAllBudgetsQueryHandler : IRequestHandler<GetAllBudgetsQuery, List<BudgetDto>>
{
    private readonly IBudgetRepository _budgetRepository;

    // IBudgetRepository injiceras via konstruktorn
    public GetAllBudgetsQueryHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }


    public async Task<List<BudgetDto>> Handle(GetAllBudgetsQuery request, CancellationToken cancellationToken)
    {
        // Hämtar alla budgetar för användaren från databasen via repository
        var budgets = await _budgetRepository.GetAllAsync(request.UserId);

        // Mappar varje budget till en DTO
        return budgets.Select(budget => new BudgetDto
        {
            Id = budget.Id,
            UserId = budget.UserId,
            Name = budget.Name,
            Month = budget.Month,
            Year = budget.Year,
            TotalAmount = budget.TotalAmount,
            CreatedAt = budget.CreatedAt
        }).ToList();
    }
}