using Authentication.API.DTOs;
using Authentication.Common.Mediatr.Commands.Abstractions;
using Authentication.Common.Result;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Authentication.Core.Commands.Auth.Identity
{
    // Command for adding role-based claims
    public class AddRoleClaimsCommand : ICommand<Result<string>>
    {
        public AddRoleClaimsDto _request;

        public AddRoleClaimsCommand(AddRoleClaimsDto request)
        {
            _request = request;
        }
    }

    public class AddRoleClaimsCommandHandler(
RoleManager<IdentityRole<long>> _roleManager
    ) : ICommandHandler<AddRoleClaimsCommand, Result<string>>
    {
        public async Task<Result<string>> ExecuteAsync(AddRoleClaimsCommand command, CancellationToken cancellationToken = default)
        {
            try
            {
                var role = await _roleManager.Roles
                                         .FirstOrDefaultAsync(r => (r.Name == command._request.RoleName) || (r.NormalizedName == command._request.RoleName), cancellationToken);

                if (role == null)
                    throw new KeyNotFoundException($"Role '{command._request.RoleName}' not found.");
                
                        foreach (var claim in command._request.ClaimsOperation)
            {
                var existingClaims = await _roleManager.GetClaimsAsync(role);
                if (!existingClaims.Any(c => c.Type == $"{command._request.Claim}:{claim}"))
                {
                    await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim($"{command._request.Claim}:{claim}", "true"));
                }
            }

                return Result.Ok("Role claims added successfully.");
            }
            catch (System.Exception ex)
            {
                return Result.Invalid<string>($"Failed to add role claims: {ex.Message}");
            }
        }
    }
}
