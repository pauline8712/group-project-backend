using BudgetApp.Application.Features.Transactions.DTOs;
using BudgetApp.Application.Interfaces;
using MediatR;
using System.Globalization;

namespace BudgetApp.Application.Features.Transactions.Queries;

// Hanterar GetTransactionsByWeekQuery — hämtar transaktioner för en specifik kategori och vecka
public class GetTransactionsByWeekQueryHandler : IRequestHandler<GetTransactionsByWeekQuery, List<TransactionDto>>
{
    private readonly ITransactionRepository _transactionRepository;

    // ITransactionRepository injiceras via konstruktorn
    public GetTransactionsByWeekQueryHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<List<TransactionDto>> Handle(GetTransactionsByWeekQuery request, CancellationToken cancellationToken)
    {
        // Hämtar alla transaktioner för kategorin från databasen
        var transactions = await _transactionRepository.GetAllByCategoryIdAsync(request.CategoryId);

        // Filtrerar transaktioner på rätt år och veckonummer
        var filtered = transactions.Where(t =>
            t.Date.Year == request.Year &&
            ISOWeek.GetWeekOfYear(t.Date) == request.WeekNumber
        ).ToList();

        // Mappar varje entitet till en DTO och returnerar listan
        return filtered.Select(t => new TransactionDto
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