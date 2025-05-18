
namespace Crudify.Infrastructure.Extensions;

public static class DbContextExtensions
{
    public static void AddApplicationContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationContext>(
                options =>
                {
                    options.UseSqlServer(
                        configuration.GetConnectionString("defaultConnectionString"),
                        x =>
                        {
                            x.MigrationsAssembly(typeof(ApplicationContext).Assembly.GetName().Name);
                        }).ConfigureWarnings(warnings => warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
                    options.EnableDetailedErrors();
                });
    }

    public static void SeedData(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        using var context = serviceScope.ServiceProvider.GetService<ApplicationContext>();

        context!.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
        context!.Database.Migrate();


        var assembly = typeof(ApplicationContext).Assembly;
        var files = assembly.GetManifestResourceNames();

        var executedSeedings = context.SeedingEntries?.ToList();
        var filePrefix = $"{assembly.GetName().Name}.Seedings.";
        foreach (var file in files.Where(f => f.StartsWith(filePrefix) && f.EndsWith(".sql"))
                .Select(
                    f => new
                    {
                        PhysicalFile = f,
                        LogicalFile = f.Replace(filePrefix, string.Empty),
                    })
                .OrderBy(f => f.LogicalFile))
        {
            if (executedSeedings.Exists(e => e.Name == file.LogicalFile))
            {
                continue;
            }

            var command = string.Empty;
            using (var stream = assembly.GetManifestResourceStream(file.PhysicalFile)!)
            {
                using StreamReader reader = new(stream);

                command = reader.ReadToEnd();
            }

            if (string.IsNullOrWhiteSpace(command))
            {
                continue;
            }

            using var transaction = context.Database.BeginTransaction();

            try
            {
                context.Database.ExecuteSqlRaw(command);
                context.SeedingEntries?.Add(new SeedingEntry { Name = file.LogicalFile });
                context.SaveChanges();
                transaction?.Commit();
            }
            catch
            {
                transaction?.Rollback();
                throw;
            }
        }

    }
}
