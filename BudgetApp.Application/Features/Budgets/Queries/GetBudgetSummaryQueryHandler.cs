using BudgetApp.Application.Features.Budgets.DTOs;
using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Budgets.Queries;

// Hanterar GetBudgetSummaryQuery — hämtar budgetsammanfattning med kategorier och beräknade saldo
public class GetBudgetSummaryQueryHandler : IRequestHandler<GetBudgetSummaryQuery, BudgetSummaryDto?>
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly ICategoryRepository _categoryRepository;

    // Både IBudgetRepository och ICategoryRepository injiceras via konstruktorn
    public GetBudgetSummaryQueryHandler(
        IBudgetRepository budgetRepository,
        ICategoryRepository categoryRepository)
    {
        _budgetRepository = budgetRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<BudgetSummaryDto?> Handle(GetBudgetSummaryQuery request, CancellationToken cancellationToken)
    {
        // Hämtar budgeten från databasen
        var budget = await _budgetRepository.GetByIdAsync(request.BudgetId);

        // Returnerar null om budgeten inte hittas
        if (budget == null)
            return null;

        // Hämtar alla kategorier som tillhör budgeten
        var categories = await _categoryRepository.GetAllAsync(request.BudgetId);

        // Mappar varje kategori till CategorySummaryDto och beräknar AmountSpent
        var categoryDtos = categories.Select(c => new CategorySummaryDto
        {
            Id = c.Id,
            Name = c.Name,
            AllocatedAmount = c.AllocatedAmount,
            CurrentBalance = c.CurrentBalance,
            // Spenderat = AllocatedAmount minus CurrentBalance
            AmountSpent = c.AllocatedAmount - c.CurrentBalance,
            // Mappar vecko-inställningar
            IsWeekly = c.IsWeekly,
            WeeklyAmount = c.WeeklyAmount
        }).ToList();

        // Beräknar totalt spenderat — summan av alla kategoriers AmountSpent
        var totalSpent = categoryDtos.Sum(c => c.AmountSpent);

        // Returnerar BudgetSummaryDto med all samlad information
        return new BudgetSummaryDto
        {
            Id = budget.Id,
            Name = budget.Name,
            Month = budget.Month,
            Year = budget.Year,
            TotalAmount = budget.TotalAmount,
            Categories = categoryDtos,
            TotalSpent = totalSpent,
            // Återstående = TotalAmount minus TotalSpent
            RemainingAmount = budget.TotalAmount - totalSpent
        };
    }
}