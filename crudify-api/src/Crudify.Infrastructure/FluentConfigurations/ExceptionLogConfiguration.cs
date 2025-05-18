

namespace Crudify.Infrastructure.FluentConfigurations;

public class ExceptionLogConfiguration : IEntityTypeConfiguration<ExceptionLog>
{
    public void Configure(EntityTypeBuilder<ExceptionLog> builder)
    {
        builder.ToTable("ExceptionLogs");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .UseIdentityColumn(1);

        builder.Property(x => x.UserId)
            .HasMaxLength(100);

        builder.Property(x => x.ExceptionMessage)
            .IsRequired()
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.StackTrace)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.Source)
            .HasMaxLength(200);

        builder.Property(x => x.Path)
            .HasMaxLength(200);

        builder.Property(x => x.QueryString)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.Timestamp)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}
