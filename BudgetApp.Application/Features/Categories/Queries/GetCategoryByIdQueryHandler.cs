using BudgetApp.Application.Features.Categories.DTOs;
using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Categories.Queries;

//Hanterar GetCategoryByIdQuery — hämtar en specifik kategori från databasen
public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto?>
{
    private readonly ICategoryRepository _categoryRepository;

    // ICategoryRepository injiceras via konstruktorn
    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto?> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        // Hämtar kategorin från databasen baserat på Id
        var category = await _categoryRepository.GetByIdAsync(request.Id);

        // Returnerar null om kategorin inte hittas
        if (category == null)
            return null;

        // Mappar entiteten till en DTO och returnerar
        return new CategoryDto
        {
            Id = category.Id,
            BudgetId = category.BudgetId,
            Name = category.Name,
            AllocatedAmount = category.AllocatedAmount,
            CurrentBalance = category.CurrentBalance,
            CreatedAt = category.CreatedAt,
            IsWeekly = category.IsWeekly,
            WeeklyAmount = category.WeeklyAmount
        };
    }
}