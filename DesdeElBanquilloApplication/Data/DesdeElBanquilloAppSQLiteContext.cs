using DesdeElBanquilloApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace DesdeElBanquilloApplication.Data
{
    public class DesdeElBanquilloAppSQLiteContext : DbContext
    {
        // Constructor que recibe las opciones de configuración
        public DesdeElBanquilloAppSQLiteContext(DbContextOptions<DesdeElBanquilloAppSQLiteContext> options)
            : base(options)
        {
        }

        // Definir los DbSets para cada entidad
        public DbSet<Match> Match { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Competition> Competition { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Federation> Federation { get; set; }
        public DbSet<League> League { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Season> Season { get; set; }
        public DbSet<Stadium> Stadium { get; set; }
        public DbSet<Administrator> Administrator { get; set; }
    }

}
