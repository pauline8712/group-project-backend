using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Transactions.Commands;

// Hanterar DeleteTransactionCommand — tar bort en transaktion från databasen
public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, bool>
{
    private readonly ITransactionRepository _transactionRepository;

    // ITransactionRepository injiceras via konstruktorn
    public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        // Hämtar transaktionen från databasen
        var transaction = await _transactionRepository.GetByIdAsync(request.Id);

        // Returnerar false om transaktionen inte hittas
        if (transaction == null)
            return false;

        // Tar bort transaktionen från databasen
        await _transactionRepository.DeleteAsync(request.Id);
        return true;
    }
}