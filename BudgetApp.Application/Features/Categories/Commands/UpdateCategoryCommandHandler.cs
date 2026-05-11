using BudgetApp.Application.Features.Categories.DTOs;
using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Categories.Commands;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);

        if (category == null)
            throw new Exception($"Category with id {request.Id} not found");

        category.Name = request.Name;
        category.AllocatedAmount = request.AllocatedAmount;

        var updated = await _categoryRepository.UpdateAsync(category);

        return new CategoryDto
        {
            Id = updated.Id,
            BudgetId = updated.BudgetId,
            Name = updated.Name,
            AllocatedAmount = updated.AllocatedAmount,
            CurrentBalance = updated.CurrentBalance,
            CreatedAt = updated.CreatedAt
        };
    }
}