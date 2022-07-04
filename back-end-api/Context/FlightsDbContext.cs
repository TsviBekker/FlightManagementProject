using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using back_end_api.Repository.Models;

namespace back_end_api.Context
{
    public partial class FlightsDbContext : DbContext
    {
        public FlightsDbContext()
        {
        }

        public FlightsDbContext(DbContextOptions<FlightsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ArrivingFlight> ArrivingFlights { get; set; } = null!;
        public virtual DbSet<DepartingFlight> DepartingFlights { get; set; } = null!;
        public virtual DbSet<Flight> Flights { get; set; } = null!;
        public virtual DbSet<Station> Stations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FlightManagement;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArrivingFlight>(entity =>
            {
                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.ArrivingFlights)
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArrivingF__Fligh__2B3F6F97");

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.ArrivingFlights)
                    .HasForeignKey(d => d.StationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ArrivingF__Stati__2C3393D0");
            });

            modelBuilder.Entity<DepartingFlight>(entity =>
            {
                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.DepartingFlights)
                    .HasForeignKey(d => d.FlightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Departing__Fligh__276EDEB3");

                entity.HasOne(d => d.Station)
                    .WithMany(p => p.DepartingFlights)
                    .HasForeignKey(d => d.StationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Departing__Stati__286302EC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
