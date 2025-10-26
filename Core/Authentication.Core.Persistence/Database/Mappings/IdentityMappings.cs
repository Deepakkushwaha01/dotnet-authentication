using Authentication.Core.Persistence.Admins.Entity;
using Authentication.Core.Persistence.Identity.Entity;
using Authentication.Core.SharedKernel.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Authentication.Core.Persistence.Database.Mappings
{
    public class IdentityMappings : IEntityTypeConfiguration<IdentityEntity>
    {
        public void Configure(EntityTypeBuilder<IdentityEntity> builder)
        {
            // ---------------- Table ----------------
            builder.ToTable(IdentityTable.Identity.ToString());

            // ---------------- Primary Key ----------------
            builder.HasKey(u => u.Id);

            // ---------------- IdentityUser Base Fields ----------------
            builder.Property(u => u.Id)
                .HasColumnName("Id")
                .HasColumnType("bigint") // ✅ Correct SQL type
                .ValueGeneratedOnAdd()   // ✅ Auto increment
                .IsRequired();

            builder.Property(u => u.Email)
                .HasColumnName("Email")
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(u => u.NormalizedEmail)
                .HasColumnName("NormalizedEmail")
                .HasMaxLength(256);

            builder.Property(u => u.EmailConfirmed)
                .HasColumnName("EmailConfirmed")
                .HasColumnType("bit")
                .HasDefaultValue(false);

            builder.Property(u => u.PasswordHash)
                .HasColumnName("PasswordHash")
                .HasMaxLength(512)
                .IsRequired();

            builder.Property(u => u.SecurityStamp)
                .HasColumnName("SecurityStamp")
                .HasMaxLength(256);

            builder.Property(u => u.ConcurrencyStamp)
                .HasColumnName("ConcurrencyStamp")
                .IsConcurrencyToken();

            builder.Property(u => u.PhoneNumber)
                .HasColumnName("PhoneNumber")
                .HasMaxLength(15).IsRequired();

            builder.Property(u => u.PhoneNumberConfirmed)
                .HasColumnName("PhoneNumberConfirmed")
                .HasColumnType("bit")
                .HasDefaultValue(false);

            builder.Property(u => u.TwoFactorEnabled)
                .HasColumnName("TwoFactorEnabled")
                .HasColumnType("bit")
                .HasDefaultValue(false);

            builder.Property(u => u.LockoutEnd)
                .HasColumnName("LockoutEnd");

            builder.Property(u => u.LockoutEnabled)
                .HasColumnName("LockoutEnabled")
                .HasColumnType("bit")
                .HasDefaultValue(false);

            builder.Property(u => u.AccessFailedCount)
                .HasColumnName("AccessFailedCount")
                .HasColumnType("int")
                .HasDefaultValue(0);

            // ---------------- Custom Domain Fields ----------------
            builder.Property(u => u.FirstName)
                .HasColumnName("FirstName")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(u => u.LastName)
                .HasColumnName("LastName")
                .HasMaxLength(100);

            builder.Property(u => u.Uid)
                .HasColumnName("Uid")
                .HasColumnType("uniqueidentifier")
                .IsRequired();

            builder.Property(u => u.CreatedOn)
                .HasColumnName("CreatedOn")
                .HasColumnType("datetime2")
                .IsRequired();

            builder.Property(u => u.UpdatedOn)
                .HasColumnName("UpdatedOn")
                .HasColumnType("datetime2");

            builder.Property(u => u.IsVerified)
                .HasColumnName("IsVerified")
                .HasColumnType("bit")
                .HasDefaultValue(false);

            builder.Property(u => u.BusinessAddress)
                .HasColumnName("BusinessAddress")
                .HasMaxLength(500)
                .HasDefaultValue(string.Empty);

            // ---------------- Indexes ----------------
            builder.HasIndex(u => u.NormalizedEmail).HasDatabaseName("IX_Admins_NormalizedEmail");
            builder.HasIndex(u => u.Uid).IsUnique();

            // ---------------- Relationships (optional, if using roles/claims) ----------------
            builder.HasMany(x => x.UserRoles).WithOne().HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
