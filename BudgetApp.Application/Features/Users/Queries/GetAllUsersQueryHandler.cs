using BudgetApp.Application.Features.Users.DTOs;
using BudgetApp.Application.Interfaces;
using MediatR;

namespace BudgetApp.Application.Features.Users.Queries;

// Hanterar GetAllUsersQuery — hämtar alla användare från databasen
public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        // Hämtar alla användare från databasen via repository
        var users = await _userRepository.GetAllAsync();

        // Mappar varje användare till en DTO — aldrig PasswordHash
        return users.Select(user => new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            Role = user.Role,
            CreatedAt = user.CreatedAt
        }).ToList();
    }
}
