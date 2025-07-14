using APIDB.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Services
{
    public class LeagueApiService
    {
        private readonly ApplicationDbContext _context;

        public LeagueApiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<League>> GetAllAsync()
        {
            return await _context.Leagues.ToListAsync();
        }

        public async Task<League?> GetByIdAsync(int id)
        {
            return await _context.Leagues.FindAsync(id);
        }

        public async Task<bool> CreateAsync(League league)
        {
            _context.Leagues.Add(league);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, League league)
        {
            if (id != league.IdLeague) return false;
            _context.Entry(league).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var league = await _context.Leagues.FindAsync(id);
            if (league == null) return false;
            _context.Leagues.Remove(league);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
