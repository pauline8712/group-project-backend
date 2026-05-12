using BudgetApp.Application.Features.Users.DTOs;
using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Users.Commands;

// Hanterar UpdateUserCommand — uppdaterar en befintlig användare i databasen
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Hämtar användaren från databasen
        var user = await _userRepository.GetByIdAsync(request.Id);

        // Returnerar null om användaren inte hittas
        if (user == null)
            return null!;

        // Uppdaterar användarens properties
        user.Email = request.Email;
        user.Role = request.Role;

        // Sparar ändringarna i databasen via repository
        var updated = await _userRepository.UpdateAsync(user);

        // Returnerar uppdaterad DTO — aldrig PasswordHash
        return new UserDto
        {
            Id = updated.Id,
            Email = updated.Email,
            Role = updated.Role,
            CreatedAt = updated.CreatedAt
        };
    }
}
