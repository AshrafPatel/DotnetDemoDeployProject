using Contacts.Core.Interfaces;
using Contacts.Infrastructure.Data;
using Contacts.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace Contacts.Infrastructure
{
    public static class InfrastructureService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");

            // Detect provider by environment
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Connection string 'Default' was not found. Check environment variables.");
            }

            if (env == "Development")
            {
                // Local development (SQL Server LocalDB)
                services.AddDbContext<ContactDbContext>(options =>
                    options.UseSqlServer(connectionString));
            }
            else
            {
                // Production (PostgreSQL on Neon.tech)
                services.AddDbContext<ContactDbContext>(options => 
                    options.UseNpgsql(connectionString));

            }

            services.AddScoped<IContactRepository, ContactRepository>();
            return services;
        }
    }
}
