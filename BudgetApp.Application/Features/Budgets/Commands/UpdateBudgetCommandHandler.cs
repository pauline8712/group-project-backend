using BudgetApp.Application.Features.Budgets.DTOs;
using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Budgets.Commands;

// Hanterar UpdateBudgetCommand — uppdaterar en befintlig budget i databasen
public class UpdateBudgetCommandHandler : IRequestHandler<UpdateBudgetCommand, BudgetDto>
{
    private readonly IBudgetRepository _budgetRepository;

    // IBudgetRepository injiceras via konstruktorn
    public UpdateBudgetCommandHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }


    public async Task<BudgetDto> Handle(UpdateBudgetCommand request, CancellationToken cancellationToken)
    {
        // Hämtar budgeten från databasen
        var budget = await _budgetRepository.GetByIdAsync(request.Id);

        // Returnerar null om budgeten inte hittas
        if (budget == null)
            return null!;

        // Uppdaterar budgetens properties
        budget.Name = request.Name;
        budget.Month = request.Month;
        budget.Year = request.Year;
        budget.TotalAmount = request.TotalAmount;

        // Sparar ändringarna i databasen via repository
        var updated = await _budgetRepository.UpdateAsync(budget);

        // Returnerar uppdaterad DTO
        return new BudgetDto
        {
            Id = updated.Id,
            UserId = updated.UserId,
            Name = updated.Name,
            Month = updated.Month,
            Year = updated.Year,
            TotalAmount = updated.TotalAmount,
            CreatedAt = updated.CreatedAt
        };
    }
}