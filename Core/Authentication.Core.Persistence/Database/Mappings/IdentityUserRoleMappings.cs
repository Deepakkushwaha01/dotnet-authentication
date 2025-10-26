using Authentication.Core.Persistence.Admins.Entity;
using Authentication.Core.Persistence.Identity.Entity;
using Authentication.Core.SharedKernel.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Core.Persistence.Database.Mappings
{
    public class IdentityUserRoleMappings : IEntityTypeConfiguration<IdentityUserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRoleEntity> builder)
        {
            // ---------------- Table ----------------
            builder.ToTable(IdentityTable.IdentityUserRoles.ToString());

            // ---------------- Composite Primary Key ----------------
            builder.HasKey(ur => ur.Id);

            // ---------------- Columns ----------------
            
            builder.Property(u => u.Id)
                .HasColumnName("Id")
                .HasColumnType("bigint") // ✅ Correct SQL type
                .ValueGeneratedOnAdd()   // ✅ Auto increment
                .IsRequired();

            
            builder.Property(ur => ur.UserId)
                .HasColumnName("UserId")
                .HasColumnType("bigint")
                .IsRequired();

            

            builder.Property(ur => ur.RoleId)
                .HasColumnName("RoleId")
                .HasColumnType("bigint")
                .IsRequired();


            builder.Property(ur => ur.AssignedOn)
                .HasColumnName("AssignedOn")
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(ur => ur.AssignedBy)
                .HasColumnName("AssignedBy")
                .HasMaxLength(256)
                .HasDefaultValue(string.Empty);

            builder.Property(ur => ur.IsActive)
                .HasColumnName("IsActive")
                .HasColumnType("bit")
                .HasDefaultValue(true);


            // ✅ Ye define karta hai ki ek `Role` (IdentityRole<long>) ke paas multiple users ho sakte hain.
            //    Har mapping record ek user-role relationship ko represent karta hai.
            builder.HasOne<IdentityRole<long>>()          // This side = "one" role (e.g. Admin, Manager, User)
                .WithMany()                               // A role can belong to many users (many rows in mapping table)
                .HasForeignKey(ur => ur.RoleId)           // Foreign key column in this table (AdminUserRoles.RoleId)
                .OnDelete(DeleteBehavior.Cascade);        // Agar role delete ho jaye → us role ke sare users ke links bhi delete ho jaayein

            // ---------------- Index ----------------
            builder.HasIndex(ur => ur.RoleId)
                .HasDatabaseName("IX_AdminUserRoles_RoleId");
            
            builder.HasIndex(ur => ur.Id)
                .HasDatabaseName("IX_AdminUserRoles_Id");
            
            builder.HasIndex(ur => ur.UserId)
                .HasDatabaseName("IX_AdminUserRoles_UserId");
        }
    }
}
