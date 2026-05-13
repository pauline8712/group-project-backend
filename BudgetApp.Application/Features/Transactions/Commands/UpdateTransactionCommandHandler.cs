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
}