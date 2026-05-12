using BudgetApp.Application.Features.Budgets.DTOs;
using BudgetApp.Application.Interfaces;
using BudgetApp.Domain.Entities;
using MediatR;

namespace BudgetApp.Application.Features.Budgets.Commands;

// Hanterar CreateBudgetCommand — skapar en ny budget i databasen
public class CreateBudgetCommandHandler : IRequestHandler<CreateBudgetCommand, BudgetDto>
{
    private readonly IBudgetRepository _budgetRepository;

    // IBudgetRepository injiceras via konstruktorn
    public CreateBudgetCommandHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }


    public async Task<BudgetDto> Handle(CreateBudgetCommand request, CancellationToken cancellationToken)
    {
        // Skapar ett nytt Budget-objekt från command-datan
        var budget = new Budget
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Name = request.Name,
            Month = request.Month,
            Year = request.Year,
            TotalAmount = request.TotalAmount,
            CreatedAt = DateTime.UtcNow
        };

        // Sparar budgeten i databasen via repository
        var created = await _budgetRepository.AddAsync(budget);

        // Returnerar en DTO med budgetdata
        return new BudgetDto
        {
            Id = created.Id,
            UserId = created.UserId,
            Name = created.Name,
            Month = created.Month,
            Year = created.Year,
            TotalAmount = created.TotalAmount,
            CreatedAt = created.CreatedAt
        };
    }

}