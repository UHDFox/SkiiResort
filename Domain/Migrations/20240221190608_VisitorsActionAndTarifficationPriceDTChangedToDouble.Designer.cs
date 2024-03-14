﻿// <auto-generated />
using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Domain.Migrations
{
    [DbContext(typeof(SkiiResortContext))]
    [Migration("20240221190608_VisitorsActionAndTarifficationPriceDTChangedToDouble")]
    partial class VisitorsActionAndTarifficationPriceDTChangedToDouble
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "operation_type", new[] { "positive", "negative" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Location.LocationRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Domain.Entities.Skipass.SkipassRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Balance")
                        .HasColumnType("double precision");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.Property<Guid>("TariffId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("VisitorId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TariffId");

                    b.HasIndex("VisitorId");

                    b.ToTable("Skipasses");
                });

            modelBuilder.Entity("Domain.Entities.Tariff.TariffRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("IsVip")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("PriceModifier")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Tariffs");
                });

            modelBuilder.Entity("Domain.Entities.Tariffication.TarifficationRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uuid");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.Property<Guid>("TariffId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("TariffId");

                    b.ToTable("Tariffications");
                });

            modelBuilder.Entity("Domain.Entities.Visitor.VisitorRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Passport")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Visitors");
                });

            modelBuilder.Entity("Domain.Entities.VisitorsAction.VisitorActionsRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("BalanceChange")
                        .HasColumnType("integer");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SkipassId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TransactionType")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("SkipassId");

                    b.ToTable("VisitorActions");
                });

            modelBuilder.Entity("Domain.Entities.Skipass.SkipassRecord", b =>
                {
                    b.HasOne("Domain.Entities.Tariff.TariffRecord", "Tariff")
                        .WithMany("Skipasses")
                        .HasForeignKey("TariffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Visitor.VisitorRecord", "Visitor")
                        .WithMany("Skipasses")
                        .HasForeignKey("VisitorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tariff");

                    b.Navigation("Visitor");
                });

            modelBuilder.Entity("Domain.Entities.Tariffication.TarifficationRecord", b =>
                {
                    b.HasOne("Domain.Entities.Location.LocationRecord", "Location")
                        .WithMany("Tariffications")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Tariff.TariffRecord", "Tariff")
                        .WithMany("Tariffications")
                        .HasForeignKey("TariffId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Tariff");
                });

            modelBuilder.Entity("Domain.Entities.VisitorsAction.VisitorActionsRecord", b =>
                {
                    b.HasOne("Domain.Entities.Location.LocationRecord", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Skipass.SkipassRecord", "Skipass")
                        .WithMany("VisitorActions")
                        .HasForeignKey("SkipassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Location");

                    b.Navigation("Skipass");
                });

            modelBuilder.Entity("Domain.Entities.Location.LocationRecord", b =>
                {
                    b.Navigation("Tariffications");
                });

            modelBuilder.Entity("Domain.Entities.Skipass.SkipassRecord", b =>
                {
                    b.Navigation("VisitorActions");
                });

            modelBuilder.Entity("Domain.Entities.Tariff.TariffRecord", b =>
                {
                    b.Navigation("Skipasses");

                    b.Navigation("Tariffications");
                });

            modelBuilder.Entity("Domain.Entities.Visitor.VisitorRecord", b =>
                {
                    b.Navigation("Skipasses");
                });
#pragma warning restore 612, 618
        }
    }
}
