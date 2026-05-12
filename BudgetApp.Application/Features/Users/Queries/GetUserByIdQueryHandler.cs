using BudgetApp.Application.Features.Users.DTOs;
using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Users.Queries;

// Hanterar GetUserByIdQuery — hämtar en specifik användare från databasen
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        // Hämtar användaren från databasen via repository
        var user = await _userRepository.GetByIdAsync(request.Id);

        // Returnerar null om användaren inte hittas
        if (user == null)
            return null;

        // Returnerar en DTO med användardata — aldrig PasswordHash
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        };
    }
}