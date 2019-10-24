using ApplicationCore.Entities.Portfolio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Portfolio
{
    public class ProjectsConfiguration : IEntityTypeConfiguration<Projects>
    {
        public void Configure(EntityTypeBuilder<Projects> builder)
        {
            builder.Property(e => e.AddedOn).HasColumnType("datetime");

            builder.Property(e => e.Description).IsUnicode(false);

            builder.Property(e => e.Libraries)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(false);

            builder.Property(e => e.Tools)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.UpdatedBy).HasColumnType("datetime");

            builder.Property(e => e.UpdatedOn).HasColumnType("datetime");
        }
    }
}
