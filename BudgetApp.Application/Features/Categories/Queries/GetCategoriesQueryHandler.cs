using BudgetApp.Application.Features.Categories.DTOs;
using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Categories.Queries;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;

    public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetAllAsync(request.BudgetId);

        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            BudgetId = c.BudgetId,
            Name = c.Name,
            AllocatedAmount = c.AllocatedAmount,
            CurrentBalance = c.CurrentBalance,
            CreatedAt = c.CreatedAt
        }).ToList();
    }
}