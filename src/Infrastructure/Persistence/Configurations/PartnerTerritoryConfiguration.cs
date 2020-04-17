using ComeTogether.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComeTogether.Infrastructure.Persistence.Configurations
{
    public class PartnerTerritoryConfiguration : IEntityTypeConfiguration<PartnerTerritory>
    {
        public void Configure(EntityTypeBuilder<PartnerTerritory> builder)
        {
            builder.HasKey(e => new { e.PartnerId, e.TerritoryId })
                .IsClustered(false);

            builder.Property(e => e.PartnerId).HasColumnName("PartnerID");

            builder.Property(e => e.TerritoryId)
                .HasColumnName("TerritoryID")
                .HasMaxLength(20);

            builder.HasOne(d => d.Partner)
                .WithMany(p => p.PartnerTerritories)
                .HasForeignKey(d => d.PartnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartnerTerritories_Partners");

            builder.HasOne(d => d.Territory)
                .WithMany(p => p.PartnerTerritories)
                .HasForeignKey(d => d.TerritoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PartnerTerritories_Territories");
        }
    }
}
