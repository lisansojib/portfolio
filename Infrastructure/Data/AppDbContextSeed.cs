using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext dbContext,
            ILoggerFactory loggerFactory, int? retry = 0)
        {
            int retryForAvailability = retry.Value;
            try
            {
                
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var log = loggerFactory.CreateLogger<AppDbContextSeed>();
                    log.LogError(ex.Message);
                    await SeedAsync(dbContext, loggerFactory, retryForAvailability);
                }
            }
        }
    }
}
