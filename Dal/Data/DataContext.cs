using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<FlightHistory> FlightsHistory { get; set; }
        public DbSet<FlightOnRoute> FlightsOnRoute { get; set; }
        public DbSet<Leg> Legs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Leg>().HasData(
                new Leg { Id = 1, IsLegAvelable = true, Type = LegType.Enter },
                new Leg { Id = 2, IsLegAvelable = true, Type = LegType.PreLanding },
                new Leg { Id = 3, IsLegAvelable = true, Type = LegType.Landing },
                new Leg { Id = 4, IsLegAvelable = true, Type = LegType.Runaway },
                new Leg { Id = 5, IsLegAvelable = true, Type = LegType.Arrivals },
                new Leg { Id = 6, IsLegAvelable = true, Type = LegType.Terminal1 },
                new Leg { Id = 7, IsLegAvelable = true, Type = LegType.Terminal2 },
                new Leg { Id = 8, IsLegAvelable = true, Type = LegType.Departures },
                new Leg { Id = 9, IsLegAvelable = true, Type = LegType.Exit }
            );
        }
    }
}
