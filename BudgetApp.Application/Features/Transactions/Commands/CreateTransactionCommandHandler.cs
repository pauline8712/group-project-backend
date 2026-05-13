using BudgetApp.Application.Features.Transactions.DTOs;
using BudgetApp.Application.Interfaces;
using BudgetApp.Domain.Entities;
using MediatR;

namespace BudgetApp.Application.Features.Transactions.Commands;

// Hanterar CreateTransactionCommand — skapar en ny transaktion och uppdaterar Category.CurrentBalance
public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionDto>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICategoryRepository _categoryRepository;

    // Både ITransactionRepository och ICategoryRepository injiceras via konstruktorn
    public CreateTransactionCommandHandler(
        ITransactionRepository transactionRepository,
        ICategoryRepository categoryRepository)
    {
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
        // Hämtar kategorin för att uppdatera CurrentBalance
        var category = await _categoryRepository.GetByIdAsync(request.CategoryId);

        // Kastar ett undantag om kategorin inte hittas
        if (category == null)
            throw new Exception($"Category with id {request.CategoryId} not found");

        // Expense minskar saldot, Income ökar saldot
        if (request.Type == "Expense")
            category.CurrentBalance -= request.Amount;
        else if (request.Type == "Income")
            category.CurrentBalance += request.Amount;

        // Sparar uppdaterat saldo i databasen
        await _categoryRepository.UpdateAsync(category);

        // Skapar transaktionen i databasen
        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            CategoryId = request.CategoryId,
            Amount = request.Amount,
            Type = request.Type,
            Description = request.Description,
            Date = request.Date
        };

        var created = await _transactionRepository.AddAsync(transaction);

        // Returnerar en DTO med transaktionsdata
        return new TransactionDto
        {
            Id = created.Id,
            CategoryId = created.CategoryId,
            Amount = created.Amount,
            Type = created.Type,
            Description = created.Description,
            Date = created.Date
        };
    }
}