namespace Crudify.Infrastructure.FluentConfigurations;

public class SeedingEntryConfiguration : IEntityTypeConfiguration<SeedingEntry>
{
    public void Configure(EntityTypeBuilder<SeedingEntry> builder)
    {
        builder.ToTable("__SqlSeedingHistory");
        builder.HasKey(s => s.Name);
    }
}
