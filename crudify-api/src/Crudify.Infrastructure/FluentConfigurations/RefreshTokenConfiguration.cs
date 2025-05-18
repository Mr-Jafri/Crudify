
namespace Crudify.Infrastructure.FluentConfigurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
               .UseIdentityColumn(1, 1);

        builder.Property(x => x.UserId)
                .HasColumnType("uniqueidentifier")
               .IsRequired(false);

        builder.Property(x => x.Token)
               .HasMaxLength(500)
               .IsRequired(false);

        builder.Property(x => x.JwtId)
               .HasMaxLength(500)
               .IsRequired(false);

        builder.Property(x => x.IsUsed)
               .IsRequired()
               .HasDefaultValue(false);

        builder.Property(x => x.IsRevoked)
               .IsRequired()
               .HasDefaultValue(false);

        builder.Property(x => x.CreatedAt)
               .IsRequired()
               .HasColumnType("datetime");

        builder.Property(x => x.ExpiredAt)
               .IsRequired()
               .HasColumnType("datetime");

        builder.HasOne(x => x.User)
               .WithMany(x => x.RefreshTokens)
               .HasForeignKey(x => x.UserId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

