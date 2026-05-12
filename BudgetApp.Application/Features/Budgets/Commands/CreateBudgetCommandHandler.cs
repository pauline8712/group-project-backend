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

}