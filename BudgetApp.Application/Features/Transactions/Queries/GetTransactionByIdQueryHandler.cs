using BudgetApp.Application.Features.Transactions.DTOs;
using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Transactions.Queries;

// Hanterar GetTransactionByIdQuery — hämtar en specifik transaktion från databasen
public class GetTransactionByIdQueryHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto?>
{
    private readonly ITransactionRepository _transactionRepository;

    // ITransactionRepository injiceras via konstruktorn
    public GetTransactionByIdQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<TransactionDto?> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        // Hämtar transaktionen från databasen baserat på Id
        var transaction = await _transactionRepository.GetByIdAsync(request.Id);

        // Returnerar null om transaktionen inte hittas
        if (transaction == null)
            return null;

        // Mappar entiteten till en DTO och returnerar
        return new TransactionDto
        {
            Id = transaction.Id,
            CategoryId = transaction.CategoryId,
            Amount = transaction.Amount,
            Type = transaction.Type,
            Description = transaction.Description,
            Date = transaction.Date
        };
    }
}