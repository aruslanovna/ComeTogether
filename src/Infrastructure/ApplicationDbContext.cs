﻿
using ComeTogether.Application.Common.Interfaces;
using ComeTogether.Domain.Entities;
using ComeTogether.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VotingApplication.Data.Model;

using System.Linq;
using Microsoft.EntityFrameworkCore.Design;

namespace ComeTogether.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Partner> Partners { get; set; }
        public DbSet<Deal> Deals { get; set; }


        public DbSet<Region> Region { get; set; }


        public DbSet<Territory> Territories { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<ProjectDetails> ProjectDetails { get; set; }



        public DbSet<PollOption> PollOptions { get; set; }

        //  public DbSet<PartnerTerritory> PartnerTerritories { get; set; }

        public DbSet<Article> Articles { get; set; }


        public DbSet<Comment> Comments { get; set; }
        public DbSet<Follower> Followers { get; set; }


        public DbSet<Following> Followings { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Ballot> Ballots { get; set; }
        public DbSet<Metric> Metrics { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
          //  builder.Entity<Deal>()
          //.HasKey(t => new { t.ProjectId, t.PartnerId });
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
    public class EventoDBContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionBuilder.UseSqlServer("Server=DESKTOP-2A033PF\\SQLEXPRESS;Initial Catalog=ComeTogetherDB;Persist Security Info=False;User ID=user;Password=user;MultipleActiveResultSets=True;Encrypt=False;TrustServerCertificate=False");
            return new ApplicationDbContext(optionBuilder.Options);
        }
    }


}