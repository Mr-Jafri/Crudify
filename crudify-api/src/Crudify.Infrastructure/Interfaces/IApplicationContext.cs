
namespace Crudify.Infrastructure.Interfaces
{
    public interface IApplicationContext : IDisposable
    {
        DatabaseFacade Database { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        DbSet<T> Set<T>()
            where T : BaseEntity;

        EntityEntry Entry(object entity);

        DbSet<SeedingEntry>? SeedingEntries { get; set; }
        DbSet<ActivityLog>? ActivityLogs { get; set; }
        DbSet<ExceptionLog>? ExceptionLogs { get; set; }
        DbSet<RefreshToken>? RefreshTokens { get; set; }
        DbSet<Student>? Students { get; set; }
    }
}
