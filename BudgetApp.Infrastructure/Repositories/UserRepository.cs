using BudgetApp.Application.Interfaces;
using BudgetApp.Domain.Entities;
using BudgetApp.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BudgetApp.Infrastructure.Repositories;

// UserRepository ärver från BaseRepository — får GetByIdAsync, AddAsync, UpdateAsync, DeleteAsync gratis
// Behåller sin egna GetAllAsync för att hämta alla användare
public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    // Hämtar alla användare från databasen
    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }
}