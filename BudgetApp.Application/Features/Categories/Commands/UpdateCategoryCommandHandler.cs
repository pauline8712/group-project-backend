using BudgetApp.Application.Features.Categories.DTOs;
using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Categories.Commands;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
{
    // Hanterar UpdateCategoryCommand — uppdaterar en befintlig kategori i databasen
    private readonly ICategoryRepository _categoryRepository;

    // ICategoryRepository injiceras via konstruktorn
    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {


        // Hämtar kategorin från databasen
        var category = await _categoryRepository.GetByIdAsync(request.Id);


        // Kastar ett undantag om kategorin inte hittas
        if (category == null)
            throw new Exception($"Category with id {request.Id} not found");

        // Uppdaterar kategorins fält med ny data från request
        category.Name = request.Name;
        category.AllocatedAmount = request.AllocatedAmount;

        // Uppdaterar vecko-inställningar
        category.IsWeekly = request.IsWeekly;
        category.WeeklyAmount = request.WeeklyAmount;

        // Sparar uppdateringen i databasen
        var updated = await _categoryRepository.UpdateAsync(category);

        // Returnerar en DTO med uppdaterad kategoridata
        return new CategoryDto
        {
            Id = updated.Id,
            BudgetId = updated.BudgetId,
            Name = updated.Name,
            AllocatedAmount = updated.AllocatedAmount,
            CurrentBalance = updated.CurrentBalance,
            CreatedAt = updated.CreatedAt,
            IsWeekly = updated.IsWeekly,
            WeeklyAmount = updated.WeeklyAmount
        };
    }
}