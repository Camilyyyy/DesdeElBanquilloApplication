using DEAModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Numerics;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace DEAApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Federation> Federations { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<LeagueTable> LeagueTables { get; set; }
        public DbSet<DEAModels.Match> Matches { get; set; }
        public DbSet<MatchPlayer> MatchPlayers { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Stadium> Stadiums { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<User> Users { get; set; }

    }
}