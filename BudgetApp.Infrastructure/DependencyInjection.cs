using BudgetApp.Application.Interfaces;
using BudgetApp.Infrastructure.Database;
using BudgetApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetApp.Infrastructure;

public static class DependencyInjection
{
    //Registrerar alla Infrastructure-tjänster - anropas från program.cs
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        //Konfiguerar EF Core med SQL Server och connection string från appsettings.sjon
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        //Registrerar repositories - kopplar interface till implementationen. 
        services.AddScoped<ICategoryRepository, CategoryRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IBudgetRepository, BudgetRepository>();

        return services;
    }
}