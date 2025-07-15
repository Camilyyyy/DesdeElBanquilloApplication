using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DesdeElBanquilloApplication.Models;

    public class DesdeElBanquilloAppDBContext : DbContext
    {
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Siempre llamar al base

        // Configuración para evitar múltiples cascadas

        modelBuilder.Entity<Team>()
                    .HasOne(t => t.Competition)
                    .WithMany(c => c.Teams)
                    .HasForeignKey(t => t.IdCompetition)
                    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Team>()
            .HasOne(t => t.Country)
            .WithMany(c => c.Teams)
            .HasForeignKey(t => t.IdCountry)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Team>()
            .HasOne(t => t.League)
            .WithMany(l => l.Teams)
            .HasForeignKey(t => t.IdLeague)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<League>()
            .HasOne(l => l.Country)
            .WithMany(c => c.Leagues)
            .HasForeignKey(l => l.IdCountry)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Competition>()
            .HasOne(c => c.Country)
            .WithMany(cn => cn.Competitions)
            .HasForeignKey(c => c.IdCountry)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Competition>()
            .HasOne(c => c.Federation)
            .WithMany(f => f.Competitions)
            .HasForeignKey(c => c.IdFederation)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Stadium>()
            .HasOne(s => s.Team)
            .WithOne(t => t.Stadium)
            .HasForeignKey<Stadium>(s => s.IdTeam)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Player>()
            .HasOne(p => p.Team)
            .WithMany(t => t.Players)
            .HasForeignKey(p => p.IdTeam)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Match>()
        .HasOne(m => m.HomeTeam)
        .WithMany(t => t.HomeMatches)
        .HasForeignKey(m => m.IdHomeTeam)
       .OnDelete(DeleteBehavior.Cascade); // Uno con cascade

        modelBuilder.Entity<Match>()
            .HasOne(m => m.AwayTeam)
            .WithMany(t => t.AwayMatches)
            .HasForeignKey(m => m.IdAwayTeam)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
        .HasOne(m => m.HomeTeam)
        .WithMany(t => t.HomeMatches)
        .HasForeignKey(m => m.IdHomeTeam)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Match>()
            .HasOne(m => m.AwayTeam)
            .WithMany(t => t.AwayMatches)
            .HasForeignKey(m => m.IdAwayTeam)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MatchPlayer>()
        .HasOne(mp => mp.Match)
        .WithMany(m => m.MatchPlayers)
        .HasForeignKey(mp => mp.IdMatch)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<MatchPlayer>()
            .HasOne(mp => mp.Player)
            .WithMany(p => p.MatchPlayers)
            .HasForeignKey(mp => mp.IdPlayer)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MatchPlayer>()
            .HasOne(mp => mp.Position)
            .WithMany(pos => pos.MatchPlayers)
            .HasForeignKey(mp => mp.IdPosition)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Competition>()
       .HasOne(c => c.Season)
       .WithMany(s => s.Competitions)
       .HasForeignKey(c => c.IdSeason)
       .OnDelete(DeleteBehavior.Restrict);

    }

    public DesdeElBanquilloAppDBContext (DbContextOptions<DesdeElBanquilloAppDBContext> options)
            : base(options)
    {
        }

        public DbSet<DesdeElBanquilloApplication.Models.Match> Match { get; set; } = default!;

public DbSet<DesdeElBanquilloApplication.Models.Team> Team { get; set; } = default!;

public DbSet<DesdeElBanquilloApplication.Models.Player> Player { get; set; } = default!;

public DbSet<DesdeElBanquilloApplication.Models.Competition> Competition { get; set; } = default!;

public DbSet<DesdeElBanquilloApplication.Models.User> User { get; set; } = default!;

public DbSet<DesdeElBanquilloApplication.Models.Federation> Federation { get; set; } = default!;

public DbSet<DesdeElBanquilloApplication.Models.League> League { get; set; } = default!;

public DbSet<DesdeElBanquilloApplication.Models.Country> Country { get; set; } = default!;

public DbSet<DesdeElBanquilloApplication.Models.Position> Position { get; set; } = default!;

public DbSet<DesdeElBanquilloApplication.Models.Season> Season { get; set; } = default!;

public DbSet<DesdeElBanquilloApplication.Models.Stadium> Stadium { get; set; } = default!;

public DbSet<DesdeElBanquilloApplication.Models.Administrator> Administrator { get; set; } = default!;
    }
