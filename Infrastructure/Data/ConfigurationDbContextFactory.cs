using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Reflection;

namespace Infrastructure.Data
{
    public class ConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
    {
        public ConfigurationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>();
            optionsBuilder.UseSqlServer("Server=192.168.10.198;Integrated Security=false;User Id=sa; Password=sa123;Initial Catalog=Portfolio;MultipleActiveResultSets=true",
                sql => {
                    sql.MigrationsAssembly(typeof(ConfigurationDbContextFactory).GetTypeInfo().Assembly.GetName().Name);
                    sql.MigrationsHistoryTable("_IdentityConfigurationMigrationHistory");
                });
            return new ConfigurationDbContext(optionsBuilder.Options, new ConfigurationStoreOptions());
        }
    }
}
