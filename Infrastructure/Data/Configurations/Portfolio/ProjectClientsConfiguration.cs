using ApplicationCore.Entities.Portfolio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Portfolio
{
    public class ProjectClientsConfiguration : IEntityTypeConfiguration<ProjectClient>
    {
        public void Configure(EntityTypeBuilder<ProjectClient> builder)
        {
            builder.ToTable("ProjectClients");

            builder.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.Organization)
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.HasOne(d => d.Master)
                .WithMany(p => p.ProjectClients)
                .HasForeignKey(d => d.MasterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectClients_Projects");
        }
    }
}
