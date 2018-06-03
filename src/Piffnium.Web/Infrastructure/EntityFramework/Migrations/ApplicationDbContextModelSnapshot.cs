﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Piffnium.Web.Infrastructure.EntityFramework;

namespace Piffnium.Web.Infrastructure.EntityFramework.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rc1-32029");

            modelBuilder.Entity("Piffnium.Web.Domain.Entities.Project", b =>
                {
                    b.Property<long>("ProjectId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true);

                    b.HasKey("ProjectId")
                        .HasName("pk_projects");

                    b.HasAlternateKey("ProjectName")
                        .HasName("uk_projects_projectName");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Piffnium.Web.Domain.Entities.Session", b =>
                {
                    b.Property<long>("SessionId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ProjectId");

                    b.Property<DateTime>("StartedAt");

                    b.HasKey("SessionId")
                        .HasName("pk_sessions");

                    b.ToTable("Sessions");
                });
#pragma warning restore 612, 618
        }
    }
}