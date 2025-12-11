using Contacts.Core.Interfaces;
using Contacts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Contacts.Infrastructure.Repositories;


namespace Contacts.Infrastructure
{
    public static class InfrastructureService
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("ContactsDb");

            // Detect provider by environment
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
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
