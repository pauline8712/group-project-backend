using BudgetApp.Application.Features.Categories.DTOs;
using MediatR;

namespace BudgetApp.Application.Features.Categories.Commands;

public class CreateCategoryCommand : IRequest<CategoryDto>
{
    public Guid BudgetId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal AllocatedAmount { get; set; }
}