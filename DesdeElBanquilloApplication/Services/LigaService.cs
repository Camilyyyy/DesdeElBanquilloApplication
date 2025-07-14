using DesdeElBanquilloApplication.Data;
using DesdeElBanquilloApplication.Models;
using DesdeElBanquilloApplication.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesdeElBanquilloApplication.Services
{
    public class LigaService : ILigaService
    {
        private readonly DesdeElBanquilloAppDBContext _ctx;
        public LigaService(DesdeElBanquilloAppDBContext ctx) => _ctx = ctx;

        public async Task<IEnumerable<LeagueViewModel>> GetAllAsync()
        {
            return await _ctx.League
                .Include(l => l.Country)
                .Select(l => new LeagueViewModel
                {
                    IdLeague = l.IdLeague,
                    Name = l.Name,
                    CreatedDate = l.CreatedDate,
                    IsActive = l.IsActive,
                    IdCountry = l.IdCountry,
                    CountryName = l.Country.Name
                })
                .ToListAsync();
        }

        public async Task<LeagueViewModel?> GetByIdAsync(int id)
        {
            var l = await _ctx.League
                              .Include(x => x.Country)
                              .FirstOrDefaultAsync(x => x.IdLeague == id);
            if (l == null) return null;
            return new LeagueViewModel
            {
                IdLeague = l.IdLeague,
                Name = l.Name,
                CreatedDate = l.CreatedDate,
                IsActive = l.IsActive,
                IdCountry = l.IdCountry,
                CountryName = l.Country.Name
            };
        }

        public async Task CreateAsync(LeagueViewModel vm)
        {
            var entity = new League
            {
                Name = vm.Name,
                CreatedDate = vm.CreatedDate,
                IsActive = vm.IsActive,
                IdCountry = vm.IdCountry
            };
            _ctx.League.Add(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task UpdateAsync(LeagueViewModel vm)
        {
            var entity = await _ctx.League.FindAsync(vm.IdLeague);
            if (entity == null) return;
            entity.Name = vm.Name;
            entity.CreatedDate = vm.CreatedDate;
            entity.IsActive = vm.IsActive;
            entity.IdCountry = vm.IdCountry;
            await _ctx.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _ctx.League.FindAsync(id);
            if (entity != null)
            {
                _ctx.League.Remove(entity);
                await _ctx.SaveChangesAsync();
            }
        }
    }
}