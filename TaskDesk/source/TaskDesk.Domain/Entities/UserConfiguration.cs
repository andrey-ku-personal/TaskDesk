using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskDesk.Domain.Entities;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName(@"Id")
            .HasColumnType("int")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.UserId)
            .HasColumnName(@"UserId")
            .HasColumnType("nvarchar")
            .IsRequired()
            .HasMaxLength(128);

        builder.HasIndex(x => x.UserId)
               .IsUnique()
               .HasDatabaseName("UX_User_UserId");

        builder.Property(x => x.FirstName)
            .HasColumnName(@"FirstName")
            .HasColumnType("nvarchar")
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.LastName)
            .HasColumnName(@"LastName")
            .HasColumnType("nvarchar")
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.Email)
            .HasColumnName(@"Email")
            .HasColumnType("nvarchar")
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.PasswordHash)
            .HasColumnName(@"PasswordHash")
            .HasColumnType("nvarchar")
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.CreateTime)
            .HasColumnName(@"CreateTime")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(x => x.LastLoginTime)
            .HasColumnName(@"LastLoginTime")
            .HasColumnType("datetime")
            .IsRequired();

        builder.Property(x => x.Website)
            .HasColumnName(@"Website")
            .HasColumnType("nvarchar")
            .IsRequired(false)
            .HasMaxLength(1024);

        builder.Property(x => x.Description)
            .HasColumnName(@"Description")
            .HasColumnType("nvarchar")
            .IsRequired(false)
            .HasMaxLength(2048);

        builder.Ignore(x => x.UserName);
        builder.Ignore(x => x.AccessFailedCount);
        builder.Ignore(x => x.ConcurrencyStamp);
        builder.Ignore(x => x.EmailConfirmed);
        builder.Ignore(x => x.LockoutEnabled);
        builder.Ignore(x => x.LockoutEnd);
        builder.Ignore(x => x.PhoneNumber);
        builder.Ignore(x => x.PhoneNumberConfirmed);
        builder.Ignore(x => x.SecurityStamp);
        builder.Ignore(x => x.TwoFactorEnabled);
        builder.Ignore(x => x.NormalizedEmail);
        builder.Ignore(x => x.NormalizedUserName);
    }
}