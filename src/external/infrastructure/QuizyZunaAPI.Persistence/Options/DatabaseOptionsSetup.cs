using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace QuizyZunaAPI.Persistence.Options;

public sealed class DatabaseOptionsSetup(IConfiguration configuration) : IConfigureOptions<DatabaseOptions>
{
    private const string ConfigurationSection = "DatabaseOptions";
    private readonly IConfiguration _configuration = configuration;

    public void Configure(DatabaseOptions options)
    {
        if(options is not null)
        {
            var connectionString = _configuration.GetConnectionString("Database");

            options.ConnectionString = connectionString!;

            _configuration.GetSection(ConfigurationSection).Bind(options);
        }
    }
}
