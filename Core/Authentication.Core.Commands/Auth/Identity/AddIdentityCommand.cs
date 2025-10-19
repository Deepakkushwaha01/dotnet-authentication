using Authentication.API.DTOs;
using Authentication.Common.Mediatr.Commands.Abstractions;
using Authentication.Common.Result;
using Authentication.Core.Domain.Users;
using Authentication.Core.Persistence.IdentityRepo.Repo;

namespace Authentication.Core.Commands.Auth.Identity
{
    public class AddIdentityCommand : ICommand<Result<IdentityResponseDto>>
    {
        public CreateIdentityRequestDto _request;

        public AddIdentityCommand(CreateIdentityRequestDto requests)
        {
            _request = requests;
        }

    }

    public class AddAdminCommandHandler(
        IIdentityRepo _identityRepo
    ) : ICommandHandler<AddIdentityCommand, Result<IdentityResponseDto>>
    {
        public async Task<Result<IdentityResponseDto>> ExecuteAsync(AddIdentityCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _identityRepo.AddIdentityAsync(IdentityDomain.Create(
                        email: command._request.Email,
                        firstName: command._request.FirstName,
                        phoneNumber: command._request.PhoneNumber,
                        adminLevel: command._request.AdminLevel,
                        lastName: command._request.LastName,
                        passwordHash: command._request.Password));
                if (result.IsFailure)
                {
                    return Result.FromError<IdentityResponseDto>(result);
                }

                return Result.Ok<IdentityResponseDto>(new IdentityResponseDto()
                {
                    Email = result.Value.Email,
                    FirstName = result.Value.FirstName,
                    LastName = result.Value.LastName,
                    Uid = result.Value.Uid,
                    CreatedAt = result.Value.CreatedOn
                });
            }
            catch (System.Exception ex)
            {

                return Result.Invalid<IdentityResponseDto>($"Failed to create user {ex.Message}");
            }
        }
    }
}