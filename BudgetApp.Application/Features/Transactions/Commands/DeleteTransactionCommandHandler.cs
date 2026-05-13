using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Transactions.Commands
{
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, bool>
    {
        private readonly ITransactionRepository _transactionRepository;
        public DeleteTransactionCommandHandler(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public async Task<bool> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            // Anropa repository
        }
