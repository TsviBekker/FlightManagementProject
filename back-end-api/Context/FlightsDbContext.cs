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
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Flight>(e =>
        //    {
        //        e.HasOne(c => c.InStation).WithMany();
        //    });
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ArrivingFlight>(entity =>
        //    {
        //        entity.HasOne(d => d.Flight)
        //            .WithMany(p => p.ArrivingFlights)
        //            .HasForeignKey(d => d.FlightId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK__ArrivingF__Fligh__2B3F6F97");

        //        entity.HasOne(d => d.Station)
        //            .WithMany(p => p.ArrivingFlights)
        //            .HasForeignKey(d => d.StationId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK__ArrivingF__Stati__2C3393D0");
        //    });

        //    modelBuilder.Entity<DepartingFlight>(entity =>
        //    {
        //        entity.HasOne(d => d.Flight)
        //            .WithMany(p => p.DepartingFlights)
        //            .HasForeignKey(d => d.FlightId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK__Departing__Fligh__276EDEB3");

        //        entity.HasOne(d => d.Station)
        //            .WithMany(p => p.DepartingFlights)
        //            .HasForeignKey(d => d.StationId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK__Departing__Stati__286302EC");
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
