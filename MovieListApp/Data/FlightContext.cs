// File: Data/FlightContext.cs
using Microsoft.EntityFrameworkCore;
using MovieListApp.Models;

namespace MovieListApp.Data
{
    public class FlightContext : DbContext
    {
        public FlightContext(DbContextOptions<FlightContext> options) : base(options) { }

        public DbSet<Flight> Flights => Set<Flight>();
    }
}