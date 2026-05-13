using BudgetApp.Application.Features.Transactions.DTOs;
using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Transactions.Commands;

// Hanterar UpdateTransactionCommand — uppdaterar en befintlig transaktion i databasen
public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, TransactionDto>
{
    private readonly ITransactionRepository _transactionRepository;

    // ITransactionRepository injiceras via konstruktorn
    public UpdateTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }


    public async Task<TransactionDto?> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
    {
        // Hämtar transaktionen från databasen
        var transaction = await _transactionRepository.GetByIdAsync(request.Id);

        // Returnerar null om transaktionen inte hittas
        if (transaction == null)
            return null;

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