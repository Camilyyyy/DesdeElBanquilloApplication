using APIDB.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Services
{
    public class CompetitionApiService
    {
        private readonly ApplicationDbContext _context;

        public CompetitionApiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Competition>> GetAllAsync()
        {
            return await _context.Competitions.ToListAsync();
        }

        public async Task<Competition?> GetByIdAsync(int id)
        {
            return await _context.Competitions.FindAsync(id);
        }

        public async Task<bool> CreateAsync(Competition competition)
        {
            _context.Competitions.Add(competition);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, Competition competition)
        {
            if (id != competition.IdCompetition) return false;
            _context.Entry(competition).State = EntityState.Modified;
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
            var competition = await _context.Competitions.FindAsync(id);
            if (competition == null) return false;
            _context.Competitions.Remove(competition);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
