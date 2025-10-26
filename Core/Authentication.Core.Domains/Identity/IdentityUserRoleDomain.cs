namespace Authentication.Core.Domain.Users
{
    /// <summary>
    /// Represents the domain model for mapping between a User and a Role.
    /// This acts as the junction between IdentityEntity (User) and IdentityRole (Role).
    /// </summary>
    public class IdentityUserRoleDomain
    {
        // ---------------- Core Keys ----------------
        public long UserId { get; private set; }      // FK → IdentityEntity
        public long RoleId { get; private set; }      // FK → IdentityRole

        // ---------------- Metadata (Optional) ----------------
        public DateTime AssignedOn { get; private set; }   // When role was assigned
        public string AssignedBy { get; private set; }     // Who assigned the role (optional, could be Admin UID)
        public bool IsActive { get; private set; }         // To soft-disable role link if needed

        // ---------------- Constructors ----------------
        private IdentityUserRoleDomain() { } // EF Core needs parameterless constructor

        public static IdentityUserRoleDomain Create(long userId, long roleId, string assignedBy)
        {
            return new IdentityUserRoleDomain
            {
                UserId = userId,
                RoleId = roleId,
                AssignedOn = DateTime.UtcNow,
                AssignedBy = assignedBy,
                IsActive = true
            };
        }

        // ---------------- Domain Behavior ----------------
        /// <summary>
        /// Marks this user-role relationship as inactive (soft delete).
        /// </summary>
        public void Deactivate()
        {
            IsActive = false;
        }

        /// <summary>
        /// Reactivates a previously inactive role mapping.
        /// </summary>
        public void Reactivate()
        {
            IsActive = true;
            AssignedOn = DateTime.UtcNow;
        }

        /// <summary>
        /// Updates who assigned the role (useful for audit).
        /// </summary>
        public void UpdateAssignedBy(string assignedBy)
        {
            AssignedBy = assignedBy;
        }
    }
}
