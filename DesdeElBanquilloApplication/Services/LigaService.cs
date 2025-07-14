using DesdeElBanquilloApplication.Data;
using DesdeElBanquilloApplication.Models;
using DesdeElBanquilloApplication.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesdeElBanquilloApplication.Services
{
    public class LigaService : ILigaService
    {
        private readonly DesdeElBanquilloAppDBContext _context;

        public LigaService(DesdeElBanquilloAppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LeagueViewModel>> GetAllAsync()
        {
            return await _context.League
                .Include(l => l.Country)
                .Select(l => new LeagueViewModel
                {
                    IdLeague = l.IdLeague,
                    Name = l.Name,
                    CreatedDate = l.CreatedDate,
                    IsActive = l.IsActive,
                    IdCountry = l.IdCountry,
                    CountryName = l.Country!.Name
                })
                .ToListAsync();
        }

        public async Task<LeagueViewModel?> GetByIdAsync(int id)
        {
            var league = await _context.League
                .Include(l => l.Country)
                .FirstOrDefaultAsync(l => l.IdLeague == id);

            if (league == null) return null;

            return new LeagueViewModel
            {
                IdLeague = league.IdLeague,
                Name = league.Name,
                CreatedDate = league.CreatedDate,
                IsActive = league.IsActive,
                IdCountry = league.IdCountry,
                CountryName = league.Country?.Name
            };
        }

        public async Task CreateAsync(LeagueViewModel vm)
        {
            var league = new League
            {
                Name = vm.Name,
                CreatedDate = vm.CreatedDate,
                IsActive = vm.IsActive,
                IdCountry = vm.IdCountry
            };

            _context.League.Add(league);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(LeagueViewModel vm)
        {
            var league = await _context.League.FindAsync(vm.IdLeague);
            if (league == null)
                throw new KeyNotFoundException($"Liga con ID {vm.IdLeague} no encontrada");

            league.Name = vm.Name;
            league.IsActive = vm.IsActive;
            league.IdCountry = vm.IdCountry;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var league = await _context.League.FindAsync(id);
            if (league != null)
            {
                _context.League.Remove(league);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetCountriesAsync()
        {
            return await _context.Country
                .Select(c => new SelectListItem
                {
                    Value = c.IdCountry.ToString(),
                    Text = c.Name
                })
                .ToListAsync();
        }
    }
}