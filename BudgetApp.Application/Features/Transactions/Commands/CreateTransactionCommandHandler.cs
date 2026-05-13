using BudgetApp.Application.Features.Transactions.DTOs;
using BudgetApp.Application.Interfaces;
using BudgetApp.Domain.Entities;
using MediatR;

namespace BudgetApp.Application.Features.Transactions.Commands;

public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, TransactionDto>
{
    private readonly ITransactionRepository _transactionRepository;

    public CreateTransactionCommandHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<TransactionDto> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
    {
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