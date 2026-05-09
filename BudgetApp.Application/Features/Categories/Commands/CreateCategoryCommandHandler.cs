using BudgetApp.Application.Features.Categories.DTOs;
using BudgetApp.Application.Interfaces;
using BudgetApp.Domain.Entities;
using MediatR;

namespace BudgetApp.Application.Features.Categories.Commands;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category
         {
            Id = Guid.NewGuid(),
            BudgetId = request.BudgetId,
            Name = request.Name,
            AllocatedAmount = request.AllocatedAmount,
            CurrentBalance = request.AllocatedAmount,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _categoryRepository.AddAsync(category);

        return new CategoryDto
        {
            Id = created.Id,
            BudgetId = created.BudgetId,
            Name = created.Name,
            AllocatedAmount = created.AllocatedAmount,
            CurrentBalance = created.CurrentBalance,
            CreatedAt = created.CreatedAt
        };
    }
}