using APIDB.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Services
{
    public class SeasonApiService
    {
        private readonly ApplicationDbContext _context;

        public SeasonApiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Season>> GetAllAsync()
        {
            return await _context.Seasons.ToListAsync();
        }

        public async Task<Season?> GetByIdAsync(int id)
        {
            return await _context.Seasons.FindAsync(id);
        }

        public async Task<bool> CreateAsync(Season season)
        {
            _context.Seasons.Add(season);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, Season season)
        {
            if (id != season.IdSeason) return false;
            _context.Entry(season).State = EntityState.Modified;
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
            var season = await _context.Seasons.FindAsync(id);
            if (season == null) return false;
            _context.Seasons.Remove(season);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
