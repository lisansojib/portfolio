using ApplicationCore.Entities.Portfolio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Portfolio
{
    public class ProjectImagesConfiguration : IEntityTypeConfiguration<ProjectImages>
    {
        public void Configure(EntityTypeBuilder<ProjectImages> builder)
        {
            builder.Property(e => e.Caption)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

            builder.Property(e => e.Path)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(false);

            builder.HasOne(d => d.Master)
                .WithMany(p => p.ProjectImages)
                .HasForeignKey(d => d.MasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectImages_Projects");
        }
    }
}
