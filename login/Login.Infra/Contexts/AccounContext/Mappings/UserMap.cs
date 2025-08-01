using Login.Core.Contexts.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Login.Infra.Contexts.AccounContext.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(u => u.Name)
                .HasColumnName("Name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(120)
                .IsRequired();

            builder.Property(u => u.Image)
                .HasColumnName("Image")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(255)
                .IsRequired();

            builder.OwnsOne(u => u.Email, email =>
            {
                email.Property(e => e.Address)
                    .HasColumnName("Email")
                    .HasColumnType("NVARCHAR")
                    .HasMaxLength(255)
                    .IsRequired();

                email.OwnsOne(e => e.Verification, verification =>
                {
                    verification.Property(v => v.Code)
                        .HasColumnName("EmailVerificationCode")
                        .IsRequired();

                    verification.Property(v => v.ExpiresAt)
                        .HasColumnName("EmailVerificationExpiresAt")
                        .IsRequired(false);

                    verification.Property(v => v.VerifiedAt)
                        .HasColumnName("EmailVerificationVerifiedAt")
                        .IsRequired(false);

                    verification.Ignore(v => v.IsActive);
                });
            });

            builder.OwnsOne(u => u.Password, password =>
            {
                password.Property(p => p.Hash)
                    .HasColumnName("PasswordHash")
                    .HasColumnType("NVARCHAR")
                    .HasMaxLength(255)
                    .IsRequired();

                password.OwnsOne(p => p.ResetCode, reset =>
                {
                    reset.Property(r => r.Code)
                        .HasColumnName("PasswordResetCode")
                        .IsRequired(false);

                    reset.Property(r => r.ExpiresAt)
                        .HasColumnName("PasswordResetCodeExpiresAt")
                        .IsRequired(false);

                    reset.Property(r => r.VerifiedAt)
                        .HasColumnName("PasswordResetCodeVerifiedAt")
                        .IsRequired(false);
                });
            });
            
            builder
                .HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRole",
                    role => role
                        .HasOne<Role>()
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade),
                    user => user
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade));
        }
    }
}
