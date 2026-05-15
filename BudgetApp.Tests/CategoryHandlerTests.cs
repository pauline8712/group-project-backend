using BudgetApp.Application.Features.Categories.Commands;
using BudgetApp.Application.Features.Categories.Queries;
using BudgetApp.Domain.Entities;
using BudgetApp.Infrastructure.Database;
using BudgetApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BudgetApp.Tests;

// Enhetstester för Category CRUD handlers i Application-lagret
[TestFixture]
public class CategoryHandlerTests
{
    private AppDbContext _context;
    private CategoryRepository _categoryRepository;

    // Sätter upp en ny in-memory databas innan varje test
    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _categoryRepository = new CategoryRepository(_context);
    }

    // Rensar upp efter varje test
    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    // Testar att CreateCategoryCommandHandler skapar en kategori korrekt
    [Test]
    public async Task CreateCategory_ShouldReturnCategoryDto_WithCorrectValues()
    {
        var handler = new CreateCategoryCommandHandler(_categoryRepository);
        var command = new CreateCategoryCommand
        {
            BudgetId = Guid.NewGuid(),
            Name = "Mat",
            AllocatedAmount = 3000,
            IsWeekly = false,
            WeeklyAmount = null
        };

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Mat"));
        Assert.That(result.AllocatedAmount, Is.EqualTo(3000));
        Assert.That(result.CurrentBalance, Is.EqualTo(3000));
    }

    // Testar att GetCategoriesQueryHandler returnerar alla kategorier för en budget
    [Test]
    public async Task GetCategories_ShouldReturnAllCategoriesForBudget()
    {
        var budgetId = Guid.NewGuid();

        // Lägger till två kategorier i databasen
        _context.Categories.AddRange(
            new Category { Id = Guid.NewGuid(), BudgetId = budgetId, Name = "Mat", AllocatedAmount = 3000, CurrentBalance = 3000, CreatedAt = DateTime.UtcNow },
            new Category { Id = Guid.NewGuid(), BudgetId = budgetId, Name = "Hyra", AllocatedAmount = 6500, CurrentBalance = 6500, CreatedAt = DateTime.UtcNow }
        );
        await _context.SaveChangesAsync();

        var handler = new GetCategoriesQueryHandler(_categoryRepository);
        var query = new GetCategoriesQuery { BudgetId = budgetId };

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result[0].Name, Is.EqualTo("Mat"));
        Assert.That(result[1].Name, Is.EqualTo("Hyra"));
    }

    // Testar att GetCategoryByIdQueryHandler returnerar rätt kategori
    [Test]
    public async Task GetCategoryById_ShouldReturnCorrectCategory()
    {
        var categoryId = Guid.NewGuid();
        _context.Categories.Add(new Category
        {
            Id = categoryId,
            BudgetId = Guid.NewGuid(),
            Name = "Mat",
            AllocatedAmount = 3000,
            CurrentBalance = 3000,
            CreatedAt = DateTime.UtcNow
        });
        await _context.SaveChangesAsync();

        var handler = new GetCategoryByIdQueryHandler(_categoryRepository);
        var query = new GetCategoryByIdQuery { Id = categoryId };

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(categoryId));
        Assert.That(result.Name, Is.EqualTo("Mat"));
    }

    // Testar att UpdateCategoryCommandHandler uppdaterar en kategori korrekt
    [Test]
    public async Task UpdateCategory_ShouldReturnUpdatedCategoryDto()
    {
        var categoryId = Guid.NewGuid();
        _context.Categories.Add(new Category
        {
            Id = categoryId,
            BudgetId = Guid.NewGuid(),
            Name = "Mat",
            AllocatedAmount = 3000,
            CurrentBalance = 3000,
            CreatedAt = DateTime.UtcNow
        });
        await _context.SaveChangesAsync();

        var handler = new UpdateCategoryCommandHandler(_categoryRepository);
        var command = new UpdateCategoryCommand
        {
            Id = categoryId,
            Name = "Mat uppdaterad",
            AllocatedAmount = 4000,
            IsWeekly = true,
            WeeklyAmount = 1000
        };

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Name, Is.EqualTo("Mat uppdaterad"));
        Assert.That(result.AllocatedAmount, Is.EqualTo(4000));
        Assert.That(result.IsWeekly, Is.True);
        Assert.That(result.WeeklyAmount, Is.EqualTo(1000));
    }

    // Testar att DeleteCategoryCommandHandler tar bort en kategori korrekt
    [Test]
    public async Task DeleteCategory_ShouldReturnTrue_WhenCategoryExists()
    {
        var categoryId = Guid.NewGuid();
        _context.Categories.Add(new Category
        {
            Id = categoryId,
            BudgetId = Guid.NewGuid(),
            Name = "Mat",
            AllocatedAmount = 3000,
            CurrentBalance = 3000,
            CreatedAt = DateTime.UtcNow
        });
        await _context.SaveChangesAsync();

        var handler = new DeleteCategoryCommandHandler(_categoryRepository);
        var command = new DeleteCategoryCommand { Id = categoryId };

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.True);
    }

    // Testar att DeleteCategoryCommandHandler returnerar false om kategorin inte finns
    [Test]
    public async Task DeleteCategory_ShouldReturnFalse_WhenCategoryDoesNotExist()
    {
        var handler = new DeleteCategoryCommandHandler(_categoryRepository);
        var command = new DeleteCategoryCommand { Id = Guid.NewGuid() };

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.That(result, Is.False);
    }
}