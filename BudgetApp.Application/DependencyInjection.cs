using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BudgetApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        // Registrerar alla MediatR handlers i Application-lagret
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(
                typeof(DependencyInjection).Assembly));

        return services;
    }
}