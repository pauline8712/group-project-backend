using BudgetApp.Application.Features.Transactions.DTOs;
using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Transactions.Commands;

// Hanterar UpdateTransactionCommand — uppdaterar en befintlig transaktion och justerar Category.CurrentBalance
public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, TransactionDto>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly ICategoryRepository _categoryRepository;

    // Både ITransactionRepository och ICategoryRepository injiceras via konstruktorn
    public UpdateTransactionCommandHandler(
        ITransactionRepository transactionRepository,
        ICategoryRepository categoryRepository)
    {
        _transactionRepository = transactionRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<TransactionDto?> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        // Hämtar transaktionen från databasen
        var transaction = await _transactionRepository.GetByIdAsync(request.Id);

        // Returnerar null om transaktionen inte hittas
        if (transaction == null)
            return null;

        // Hämtar kategorin för att justera CurrentBalance
        var category = await _categoryRepository.GetByIdAsync(transaction.CategoryId);

        // Kastar ett undantag om kategorin inte hittas
        if (category == null)
            throw new Exception($"Category with id {transaction.CategoryId} not found");

        // Ångrar det gamla beloppets effekt på CurrentBalance
        if (transaction.Type == "Expense")
            category.CurrentBalance += transaction.Amount;
        else if (transaction.Type == "Income")
            category.CurrentBalance -= transaction.Amount;

        // Applicerar det nya beloppets effekt på CurrentBalance
        if (request.Type == "Expense")
            category.CurrentBalance -= request.Amount;
        else if (request.Type == "Income")
            category.CurrentBalance += request.Amount;

        // Sparar uppdaterat saldo i databasen
        await _categoryRepository.UpdateAsync(category);

        // Uppdaterar transaktionens fält med ny data från request
        transaction.Amount = request.Amount;
        transaction.Type = request.Type;
        transaction.Description = request.Description;
        transaction.Date = request.Date;

        // Sparar uppdateringen i databasen
        var updated = await _transactionRepository.UpdateAsync(transaction);

        // Returnerar en DTO med uppdaterad transaktionsdata
        return new TransactionDto
        {
            Id = updated.Id,
            CategoryId = updated.CategoryId,
            Amount = updated.Amount,
            Type = updated.Type,
            Description = updated.Description,
            Date = updated.Date
        };
    }
}