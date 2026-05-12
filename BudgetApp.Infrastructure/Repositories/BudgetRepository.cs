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
}