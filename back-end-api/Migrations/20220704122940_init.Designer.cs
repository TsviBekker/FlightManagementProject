﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using back_end_api.Context;

#nullable disable

namespace back_end_api.Migrations
{
    [DbContext(typeof(FlightsDbContext))]
    [Migration("20220704122940_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("back_end_api.Repository.Models.ArrivingFlight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("FlightId")
                        .HasColumnType("int");

                    b.Property<bool>("HasArrived")
                        .HasColumnType("bit");

                    b.Property<int>("StationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.HasIndex("StationId");

                    b.ToTable("ArrivingFlights");
                });

            modelBuilder.Entity("back_end_api.Repository.Models.DepartingFlight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("FlightId")
                        .HasColumnType("int");

                    b.Property<bool>("HasDeparted")
                        .HasColumnType("bit");

                    b.Property<int>("StationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.HasIndex("StationId");

                    b.ToTable("DepartingFlights");
                });

            modelBuilder.Entity("back_end_api.Repository.Models.Flight", b =>
                {
                    b.Property<int>("FlightId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FlightId"), 1L, 1);

                    b.Property<string>("Airline")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<int>("PrepTime")
                        .HasColumnType("int");

                    b.HasKey("FlightId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("back_end_api.Repository.Models.Station", b =>
                {
                    b.Property<int>("StationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StationId"), 1L, 1);

                    b.Property<bool>("IsAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("StationId");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("back_end_api.Repository.Models.ArrivingFlight", b =>
                {
                    b.HasOne("back_end_api.Repository.Models.Flight", "Flight")
                        .WithMany("ArrivingFlights")
                        .HasForeignKey("FlightId")
                        .IsRequired()
                        .HasConstraintName("FK__ArrivingF__Fligh__2B3F6F97");

                    b.HasOne("back_end_api.Repository.Models.Station", "Station")
                        .WithMany("ArrivingFlights")
                        .HasForeignKey("StationId")
                        .IsRequired()
                        .HasConstraintName("FK__ArrivingF__Stati__2C3393D0");

                    b.Navigation("Flight");

                    b.Navigation("Station");
                });

            modelBuilder.Entity("back_end_api.Repository.Models.DepartingFlight", b =>
                {
                    b.HasOne("back_end_api.Repository.Models.Flight", "Flight")
                        .WithMany("DepartingFlights")
                        .HasForeignKey("FlightId")
                        .IsRequired()
                        .HasConstraintName("FK__Departing__Fligh__276EDEB3");

                    b.HasOne("back_end_api.Repository.Models.Station", "Station")
                        .WithMany("DepartingFlights")
                        .HasForeignKey("StationId")
                        .IsRequired()
                        .HasConstraintName("FK__Departing__Stati__286302EC");

                    b.Navigation("Flight");

                    b.Navigation("Station");
                });

            modelBuilder.Entity("back_end_api.Repository.Models.Flight", b =>
                {
                    b.Navigation("ArrivingFlights");

                    b.Navigation("DepartingFlights");
                });

            modelBuilder.Entity("back_end_api.Repository.Models.Station", b =>
                {
                    b.Navigation("ArrivingFlights");

                    b.Navigation("DepartingFlights");
                });
#pragma warning restore 612, 618
        }
    }
}