using Application.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(GetConnectionString(defaultConnectionString));
        });
        services.AddScoped<IApplicationDbContext>(
            provider => provider.GetRequiredService<ApplicationDbContext>());

        return services;
    }

    private static string GetConnectionString(string defaultConnectionString)
    {
        var dbHost = Environment.GetEnvironmentVariable(EnvironmentVariable.DbHost);
        if (string.IsNullOrEmpty(dbHost))
            return defaultConnectionString;

        var dbUser = Environment.GetEnvironmentVariable(EnvironmentVariable.DbUser);
        var dbPass = Environment.GetEnvironmentVariable(EnvironmentVariable.DbPass);
        var dbPort = Environment.GetEnvironmentVariable(EnvironmentVariable.DbPort);
        var dbName = Environment.GetEnvironmentVariable(EnvironmentVariable.DbName);

        return $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPass}";
    }

    /// <summary>
    ///     EnvironmentVariable
    /// </summary>
    private static class EnvironmentVariable
    {
        /// <summary>
        ///     DbHost
        /// </summary>
        public const string DbHost = "DBHOST";

        /// <summary>
        ///     DbUser
        /// </summary>
        public const string DbUser = "DBUSER";

        /// <summary>
        ///     DbPass
        /// </summary>
        public const string DbPass = "DBPASS";

        /// <summary>
        ///     DbPort
        /// </summary>
        public const string DbPort = "DBPORT";

        /// <summary>
        ///     DbName
        /// </summary>
        public const string DbName = "DBNAME";
    }
}
