namespace Crudify.Infrastructure.Database;

public class ApplicationContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IApplicationContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
       : base(options)
    {

    }

    DatabaseFacade IApplicationContext.Database => this.Database;

    public new DbSet<T> Set<T>()
        where T : BaseEntity
    {
        return base.Set<T>();
    }

    public override Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry Update(object entity)
    {
        return base.Update(entity);
    }

    public Task<int> SaveChangesAsync()
    {
        return base.SaveChangesAsync();
    }

    public virtual DbSet<SeedingEntry>? SeedingEntries { get; set; }
    public virtual DbSet<ActivityLog>? ActivityLogs { get; set; }
    public virtual DbSet<ExceptionLog>? ExceptionLogs { get; set; }
    public virtual DbSet<RefreshToken>? RefreshTokens { get; set; }
    public virtual DbSet<Student>? Students { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}
