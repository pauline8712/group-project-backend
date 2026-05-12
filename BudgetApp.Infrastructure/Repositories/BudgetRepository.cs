using BudgetApp.Application.Interfaces;
using BudgetApp.Domain.Entities;
using BudgetApp.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Infrastructure.Repositories;

// Implementerar IBudgetRepository — hanterar databasoperationer för Budget via EF Core
public class BudgetRepository : IBudgetRepository
{
    private readonly AppDbContext _context;

    // AppDbContext injiceras via konstruktorn
    public BudgetRepository(AppDbContext context)
    {
        _context = context;
    }

    // Hämtar alla budgetar för en specifik användare
    public async Task<List<Budget>> GetAllAsync(Guid userId)
    {
        return await _context.Budgets
            .Where(b => b.UserId == userId)
            .ToListAsync();
    }

    // Hämtar en specifik budget baserat på Id
    public async Task<Budget?> GetByIdAsync(Guid id)
    {
        return await _context.Budgets
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    // Skapar en ny budget i databasen
    public async Task<Budget> AddAsync(Budget budget)
    {
        _context.Budgets.Add(budget);
        await _context.SaveChangesAsync();
        return budget;
    }
}