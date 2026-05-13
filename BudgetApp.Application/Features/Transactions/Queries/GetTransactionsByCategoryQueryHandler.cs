using BudgetApp.Application.Features.Transactions.DTOs;
using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Transactions.Queries;

// Hanterar GetTransactionsByCategoryQuery — hämtar alla transaktioner för en specifik kategori
public class GetTransactionsByCategoryQueryHandler : IRequestHandler<GetTransactionsByCategoryQuery, List<TransactionDto>>
{
    private readonly ITransactionRepository _transactionRepository;

    // ITransactionRepository injiceras via konstruktorn
    public GetTransactionsByCategoryQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<List<TransactionDto>> Handle(GetTransactionsByCategoryQuery request, CancellationToken cancellationToken)
    {
        // Hämtar alla transaktioner för kategorin från databasen
        var transactions = await _transactionRepository.GetAllByCategoryIdAsync(request.CategoryId);

        // Mappar varje entitet till en DTO och returnerar listan
        return transactions.Select(t => new TransactionDto
        {
            Id = t.Id,
            CategoryId = t.CategoryId,
            Amount = t.Amount,
            Type = t.Type,
            Description = t.Description,
            Date = t.Date
        }).ToList();
    }
}