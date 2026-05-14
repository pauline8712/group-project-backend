using BudgetApp.Application.Features.Categories.DTOs;
using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Categories.Queries;
// Hanterar GetCategoriesQuery — hämtar alla kategorier för en specifik budget
public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;

    // ICategoryRepository injiceras via konstruktorn
    public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {

        // Hämtar alla kategorier för budgeten från databasen

        var categories = await _categoryRepository.GetAllAsync(request.BudgetId);

        // Mappar varje entitet till en DTO och returnerar listan
        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            BudgetId = c.BudgetId,
            Name = c.Name,
            AllocatedAmount = c.AllocatedAmount,
            CurrentBalance = c.CurrentBalance,
            CreatedAt = c.CreatedAt,
            IsWeekly = c.IsWeekly,
            WeeklyAmount = c.WeeklyAmount
        }).ToList();
    }
}