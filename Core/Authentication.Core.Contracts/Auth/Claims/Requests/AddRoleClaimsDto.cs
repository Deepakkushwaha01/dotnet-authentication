using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Authentication.Core.SharedKernel.Enums;
using Authentication.Core.SharedKernel.Enums.IdentityRoleEnum;

namespace Authentication.API.DTOs
{
    public class AddRoleClaimsDto
    {
        [Required(ErrorMessage = "RoleName is required.")]
        public string RoleName { get; set; } = default!;

        [Required(ErrorMessage = "Claim is required.")]
        public required string Claim { get; set; }

        [Required(ErrorMessage = "ClaimsOperation is required.")]
        public List<RoleClaimOperationType> ClaimsOperation { get; set; } = new();
    }

}
