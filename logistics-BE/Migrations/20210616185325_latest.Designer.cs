﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using logistics_BE.Data;

namespace logistics_BE.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20210616185325_latest")]
    partial class latest
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("logistics_BE.Domain.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tag")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("logistics_BE.Domain.LogisticsCenter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("LogisticsCenters");
                });

            modelBuilder.Entity("logistics_BE.Domain.Road", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Distance")
                        .HasColumnType("float");

                    b.Property<int>("EndCityId")
                        .HasColumnType("int");

                    b.Property<int>("StartCityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EndCityId");

                    b.HasIndex("StartCityId");

                    b.ToTable("Roads");
                });

            modelBuilder.Entity("logistics_BE.Domain.LogisticsCenter", b =>
                {
                    b.HasOne("logistics_BE.Domain.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("City");
                });

            modelBuilder.Entity("logistics_BE.Domain.Road", b =>
                {
                    b.HasOne("logistics_BE.Domain.City", "EndCity")
                        .WithMany()
                        .HasForeignKey("EndCityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("logistics_BE.Domain.City", "StartCity")
                        .WithMany()
                        .HasForeignKey("StartCityId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("EndCity");

                    b.Navigation("StartCity");
                });
#pragma warning restore 612, 618
        }
    }
}
