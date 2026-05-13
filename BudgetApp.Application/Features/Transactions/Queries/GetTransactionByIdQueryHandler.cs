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
}