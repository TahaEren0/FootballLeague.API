using FootballLeague.API.models;
using FootballLeague.API.Models;
using Microsoft.EntityFrameworkCore;

namespace FootballLeague.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Match> Matches { get; set; }
       
    }
}