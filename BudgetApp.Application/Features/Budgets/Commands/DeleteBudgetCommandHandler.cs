using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Budgets.Commands;

// Hanterar DeleteBudgetCommand — tar bort en budget från databasen
public class DeleteBudgetCommandHandler : IRequestHandler<DeleteBudgetCommand, bool>
{
    private readonly IBudgetRepository _budgetRepository;

    // IBudgetRepository injiceras via konstruktorn
    public DeleteBudgetCommandHandler(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }
}
