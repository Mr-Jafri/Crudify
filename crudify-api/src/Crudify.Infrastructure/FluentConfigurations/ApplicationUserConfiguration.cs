
namespace Crudify.Infrastructure.FluentConfigurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.FirstName)
            .HasMaxLength(100);

        builder.Property(x => x.LastName)
            .HasMaxLength(100);

        builder.Property(x => x.Email)
            .HasMaxLength(256);

        builder.Property(x => x.UserName)
            .HasMaxLength(256);

        builder.HasMany(u => u.RefreshTokens)
           .WithOne(rt => rt.User)
           .HasForeignKey(rt => rt.UserId)
           .HasConstraintName("FK_RefreshTokens_AspNetUsers_ApplicationUserId")
           .OnDelete(DeleteBehavior.Cascade);
    }
}
