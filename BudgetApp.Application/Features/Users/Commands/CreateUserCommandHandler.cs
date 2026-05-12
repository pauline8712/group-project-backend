using BudgetApp.Application.Features.Users.DTOs;
using BudgetApp.Application.Interfaces;
using BudgetApp.Domain.Entities;
using MediatR;

namespace BudgetApp.Application.Features.Users.Commands;

// Hanterar CreateUserCommand — skapar en ny användare i databasen
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Skapar ett nytt User-objekt från command-datan
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            PasswordHash = request.PasswordHash,
            Role = request.Role,
            CreatedAt = DateTime.UtcNow
        };

        // Sparar användaren i databasen via repository
        var created = await _userRepository.AddAsync(user);

        // Returnerar en DTO med användardata — aldrig PasswordHash
        return new UserDto
        {
            Id = created.Id,
            Email = created.Email,
            Role = created.Role,
            CreatedAt = created.CreatedAt
        };
    }
} 