using Microsoft.EntityFrameworkCore.Design;

namespace Crudify.Infrastructure;

public class AplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
{
    public ApplicationContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .Build();

        var builder = new DbContextOptionsBuilder<ApplicationContext>();
        var connectionString = configuration.GetConnectionString("defaultConnectionString");

        builder.UseSqlServer(connectionString);

        return new ApplicationContext(builder.Options);
    }
}
