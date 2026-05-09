using BudgetApp.Application.Features.Categories.DTOs;
using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Categories.Queries;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto?>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByIdAsync(request.Id);

        if (category == null)
            return null;

        return new CategoryDto
        {
            Id = category.Id,
            BudgetId = category.BudgetId,
            Name = category.Name,
            AllocatedAmount = category.AllocatedAmount,
            CurrentBalance = category.CurrentBalance,
            CreatedAt = category.CreatedAt
        };
    }
}