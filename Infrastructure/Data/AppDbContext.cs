using ApplicationCore.Entities.Portfolio;
using Infrastructure.Data.Configurations.Portfolio;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ProjectClients> ProjectClients { get; set; }
        public virtual DbSet<ProjectImages> ProjectImages { get; set; }
        public virtual DbSet<Projects> Projects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectsConfiguration).Assembly);
        }
    }
}
