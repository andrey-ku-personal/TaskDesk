using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TaskDesk.Domain.Entities;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Project");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName(@"Id")
            .HasColumnType("int")
            .IsRequired()
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasColumnName(@"Name")
            .HasColumnType("nvarchar")
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(x => x.Description)
            .HasColumnName(@"Description")
            .HasColumnType("nvarchar")
            .IsRequired(false)
            .HasMaxLength(2048);
    }
}