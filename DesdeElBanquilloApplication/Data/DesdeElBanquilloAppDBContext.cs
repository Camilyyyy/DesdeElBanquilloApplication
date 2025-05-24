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
    }
