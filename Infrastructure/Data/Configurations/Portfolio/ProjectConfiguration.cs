﻿using ApplicationCore.Entities.Portfolio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations.Portfolio
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");

            builder.Property(e => e.AddedOn).HasColumnType("datetime");

            builder.Property(e => e.Description).IsUnicode(false);

            builder.Property(e => e.Libraries)
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(256)
                .IsUnicode(false);

            builder.Property(e => e.Languages)
                .HasMaxLength(500);

            builder.Property(e => e.Status)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Tools)
                .HasMaxLength(500)
                .IsUnicode(false);
        }
    }
}
