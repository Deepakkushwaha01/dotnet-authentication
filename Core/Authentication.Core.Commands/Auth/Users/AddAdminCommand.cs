using Authentication.API.DTOs;
using Authentication.Common.Mediatr.Commands.Abstractions;
using Authentication.Common.Result;
using Authentication.Core.Persistence.Users.Repo;

namespace Authentication.Core.Commands.Auth.Users
{
    public class AddAdminCommand : ICommand<Result<UserResponseDto>>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public AddAdminCommand(CreateAdminRequestDto requests)
        {
            Email = requests.Email;
            Password = requests.Password;
            PhoneNumber = requests.PhoneNumber;
            FirstName = requests.FirstName;
            LastName = requests.LastName;
        }

    }

    public class AddAdminCommandHandler(
        IUsersRepo _usersRepo
    ) : ICommandHandler<AddAdminCommand, Result<UserResponseDto>>
    {
        public async Task<Result<UserResponseDto>> ExecuteAsync(AddAdminCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                await _usersRepo.AddAsync(Domain.Users.User.Create(
                        userName: command.Email,
                        email: command.Email,
                        firstName: command.FirstName,
                        lastName: command.LastName,
                        passwordHash: command.Password));
                return Result.Ok<UserResponseDto>(new UserResponseDto()
                {
                    Email = command.Email,
                    FirstName = command.FirstName,
                    LastName = command.LastName
                });
            }
            catch (System.Exception ex)
            {

                return Result.Invalid<UserResponseDto>($"Failed to create user {ex.Message}");
            }
        }
    }
}