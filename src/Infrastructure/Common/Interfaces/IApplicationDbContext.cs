using ComeTogether.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ComeTogether.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Category> Categories { get; set; }

     
        public DbSet<Deal> Deals { get; set; }

      
        public DbSet<Region> Region { get; set; }

      
        public DbSet<Territory> Territories { get; set; }

       // public DbSet<Founder> Founders { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectDetails> ProjectDetails { get; set; }

      
        public DbSet<PartnerTerritory> PartnerTerritories { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
