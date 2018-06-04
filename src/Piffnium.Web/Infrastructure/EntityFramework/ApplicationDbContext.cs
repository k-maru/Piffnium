using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Piffnium.Web.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Piffnium.Web.Infrastructure.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<Session> Comparisons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>(ConfigureProject);
            modelBuilder.Entity<Session>(ConfigureSession);
            modelBuilder.Entity<Comparison>(ConfigureComparison);
            modelBuilder.Entity<Difference>(ConfigureDifferences);

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureProject(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects")
                .HasKey(p => p.ProjectId)
                .HasName("pk_projects");
            builder.HasAlternateKey(p => p.ProjectName)
                .HasName("uk_projects_projectName");

            builder.Property(p => p.ProjectId)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(p => p.ProjectName)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);
        }

        private void ConfigureSession(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("Sessions")
                .HasKey(s => s.SessionId)
                .HasName("pk_sessions");

            builder.Property(s => s.SessionId)
                .IsRequired()
                .ValueGeneratedOnAdd();
            builder.Property(s => s.ProjectId)
                .IsRequired();
            builder.Property(s => s.StartedAt)
                .IsRequired();

            builder.HasOne(s => s.Project)
                   .WithMany(p => p.Sessions)
                   .HasForeignKey(s => s.ProjectId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureComparison(EntityTypeBuilder<Comparison> builder)
        {
            builder.ToTable("Comparisons")
                .HasKey(c => c.ComparisonId)
                .HasName("pk_comparions");

            builder.Property(c => c.ComparisonId)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(c => c.ComparisonKey)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(100);

            builder.Property(c => c.SessionId)
                .IsRequired();

            builder.Property(c => c.DestinationFileName)
                .IsRequired()
                .IsUnicode();

            builder.HasOne(c => c.Session)
                   .WithMany(s => s.Comparisons)
                   .HasForeignKey(c => c.SessionId)
                   .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureDifferences(EntityTypeBuilder<Difference> builder)
        {
            builder.ToTable("Differences")
                .HasKey(d => d.ComparisonId)
                .HasName("pk_differences");

            builder.Property(d => d.ComparisonId)
                .IsRequired()
                .ValueGeneratedNever();

            builder.Property(d => d.DifferenceRate)
                .IsRequired();

            builder.Property(d => d.DifferenceFileName)
                .IsRequired()
                .IsUnicode();

            builder.Property(d => d.SourceFileName)
                .IsRequired()
                .IsUnicode();

            builder.HasOne(d => d.Comparison)
                   .WithOne(c => c.Difference)
                   .HasForeignKey<Difference>(d => d.ComparisonId)
                   .OnDelete(DeleteBehavior.Restrict);
        }


    }
}
