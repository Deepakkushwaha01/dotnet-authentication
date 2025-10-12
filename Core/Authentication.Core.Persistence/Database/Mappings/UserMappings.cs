namespace Authentication.Core.Persistence.Database.Mappings
{
       using Authentication.Core.Persistence.Users.Entity;
       using Microsoft.EntityFrameworkCore;
       using Microsoft.EntityFrameworkCore.Metadata.Builders;
       public class UserMappings : IEntityTypeConfiguration<Users>
       {
              public void Configure(EntityTypeBuilder<Users> builder)
              {
                     // Table name override (default AspNetUsers hota, hum "Users" kar rahe hain)
                     builder.ToTable("Admins");

                     // Primary Key
                     builder.HasKey(u => u.Id);

                     // ---------------- IdentityUser Default Fields ----------------

                     // Unique identifier for the user (GUID/string)
                     builder.Property(u => u.Id)
                            .HasColumnName("Id")
                            .HasColumnType("varchar")
                            .IsRequired();

                     builder.Property(x => x.Uid)
                            .HasColumnName("Uid")
                            .HasColumnType("uuid")
                            .IsRequired();

                     builder.Property(x => x.CreatedOn)
                            .HasColumnName("CreatedOn")
                            .HasColumnType("timestamp")
                            .IsRequired();

                     builder.Property(x => x.DeletedOn)
                            .HasColumnName("DeletedOn")
                            .HasColumnType("timestamp")
                            .IsRequired(false);

                     // Username entered by the user (login name)
                     builder.Property(u => u.UserName)
                            .HasColumnName("UserName")
                            .HasMaxLength(256);

                     // Normalized (uppercase) version of UserName (for case-insensitive searches)
                     builder.Property(u => u.NormalizedUserName)
                            .HasColumnName("NormalizedUserName")
                            .HasMaxLength(256);

                     // User's email address (required in most apps)
                     builder.Property(u => u.Email)
                            .HasColumnName("Email")
                            .HasMaxLength(256)
                            .IsRequired();

                     // Normalized (uppercase) version of Email (used for indexing/search)
                     builder.Property(u => u.NormalizedEmail)
                            .HasColumnName("NormalizedEmail")
                            .HasMaxLength(256);

                     // Whether the email is confirmed (true after user verifies email)
                     builder.Property(u => u.EmailConfirmed)
                            .HasColumnName("EmailConfirmed")
                            .HasColumnType("bool")
                            .HasDefaultValue(false);

                     // Hashed password (never plain text, stored using Identity hashing)
                     builder.Property(u => u.PasswordHash)
                            .HasColumnName("PasswordHash").HasMaxLength(256).IsRequired();

                     // Random string used for extra security checks
                     builder.Property(u => u.SecurityStamp)
                            .HasColumnName("SecurityStamp");

                     // Used for optimistic concurrency check (row versioning)
                     builder.Property(u => u.ConcurrencyStamp)
                            .HasColumnName("ConcurrencyStamp")
                            .IsConcurrencyToken();

                     // User's phone number
                     builder.Property(u => u.PhoneNumber)
                            .HasColumnName("PhoneNumber")
                            .HasMaxLength(20);

                     // Whether phone number is confirmed
                     builder.Property(u => u.PhoneNumberConfirmed)
                            .HasColumnName("PhoneNumberConfirmed")
                            .HasColumnType("bool")
                            .HasDefaultValue(false);

                     // Two-factor authentication enabled/disabled
                     builder.Property(u => u.TwoFactorEnabled)
                            .HasColumnName("TwoFactorEnabled")
                            .HasColumnType("bool")
                            .HasDefaultValue(false);

                     // Lockout end time (if user is locked out due to failed login attempts)
                     builder.Property(u => u.LockoutEnd)
                            .HasColumnName("LockoutEnd");

                     // Whether account lockout is enabled for this user
                     builder.Property(u => u.LockoutEnabled)
                            .HasColumnName("LockoutEnabled")
                            .HasColumnType("bool")
                            .HasDefaultValue(false);

                     // Number of failed login attempts
                     builder.Property(u => u.AccessFailedCount)
                            .HasColumnName("AccessFailedCount")
                            .HasColumnType("int")
                            .HasDefaultValue(0);

                     // ---------------- Custom Fields ----------------

                     // User's first name
                     builder.Property(u => u.FirstName)
                            .HasColumnName("FirstName")
                            .HasMaxLength(100)
                            .IsRequired();

                     // User's last name
                     builder.Property(u => u.LastName)
                            .HasColumnName("LastName")
                            .HasMaxLength(100);

                     // FullName is a calculated property (not stored in DB)
                     builder.Ignore(u => u.FullName);

                     // ---------------- Indexes ----------------

                     // Unique index for NormalizedUserName (ensures phoneNumber are unique, case-insensitive)
                     builder.HasIndex(u => u.PhoneNumber)
                            .IsUnique()
                            .HasDatabaseName("PhoneNumberIndex");

                     // Index for NormalizedEmail (faster lookup by email, case-insensitive)
                     builder.HasIndex(u => u.NormalizedEmail)
                            .HasDatabaseName("EmailIndex");
              }
       }
}
