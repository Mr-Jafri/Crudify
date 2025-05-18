
namespace Crudify.Infrastructure.FluentConfigurations;

public class ActivityLogConfiguration : IEntityTypeConfiguration<ActivityLog>
{
    public void Configure(EntityTypeBuilder<ActivityLog> builder)
    {
        builder.ToTable("ActivityLogs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn(1);

        builder.Property(x => x.UserId)
            .HasMaxLength(100);

        builder.Property(x => x.Action)
            .HasMaxLength(200);

        builder.Property(x => x.Endpoint)
            .HasMaxLength(200);

        builder.Property(x => x.HttpMethod)
            .HasMaxLength(10);

        builder.Property(x => x.RequestBody)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.ResponseBody)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.StatusCode);

        builder.Property(x => x.IPAddress)
            .HasMaxLength(50);

        builder.Property(x => x.Timestamp)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}
