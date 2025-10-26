using Authentication.Core.Persistence.Identity.Entity;
using Microsoft.AspNetCore.Identity;
using System;

namespace Authentication.Core.Persistence.Admins.Entity
{
    public class IdentityUserRoleEntity : IdentityUserRole<long>
    {
        public long Id { get; set; }
        public DateTime AssignedOn { get; set; } = DateTime.UtcNow;
        public string AssignedBy { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
