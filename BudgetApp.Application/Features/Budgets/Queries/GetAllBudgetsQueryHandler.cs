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
}