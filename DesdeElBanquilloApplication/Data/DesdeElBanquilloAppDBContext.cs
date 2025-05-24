using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DesdeElBanquilloApplication.Models;

    public class DesdeElBanquilloAppDBContext : DbContext
    {
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
