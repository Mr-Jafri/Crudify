
namespace Crudify.Infrastructure.FluentConfigurations;

internal class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .IsRequired()
            .UseIdentityColumn(1);

        builder.Property(s => s.FullName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(s => s.DateOfBirth)
            .IsRequired();

        builder.Property(s => s.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(s => s.PhoneNumber)
            .HasMaxLength(20);

        builder.Property(s => s.CreatedOn)
            .IsRequired(false);

        builder.Property(s => s.CreatedBy)
            .IsRequired(false);

        builder.Property(s => s.ModifiedOn)
            .IsRequired(false);

        builder.Property(s => s.ModifiedBy)
            .IsRequired(false);
    }
}
