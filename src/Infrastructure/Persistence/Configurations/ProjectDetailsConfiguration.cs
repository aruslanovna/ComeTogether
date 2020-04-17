using ComeTogether.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ComeTogether.Infrastructure.Persistence.Configurations
{
    public class ProjectDetailConfiguration : IEntityTypeConfiguration<ProjectDetails>
    {
        public void Configure(EntityTypeBuilder<ProjectDetails> builder)
        {
            builder.HasKey(e => new { e.ProjectId });

            builder.ToTable("Project Details");

            builder.Property(e => e.ProjectId).HasColumnName("ProjectID");

            //builder.Property(e => e.ProductId).HasColumnName("ProductID");

            //builder.Property(e => e.Quantity).HasDefaultValueSql("((1))");

            //builder.Property(e => e.UnitPrice).HasColumnType("money");

            //builder.HasOne(d => d.Order)
            //    .WithMany(p => p.OrderDetails)
            //    .HasForeignKey(d => d.OrderId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_Order_Details_Orders");

            builder.HasOne(d => d.Project)
                .WithMany(p => p.ProjectDetails)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Project_Details_Products");
        }
    }
}
