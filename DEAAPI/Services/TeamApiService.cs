using APIDB.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Services
{
    public class TeamApiService
    {
        private readonly ApplicationDbContext _context;

        public TeamApiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Team>> GetAllAsync()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team?> GetByIdAsync(int id)
        {
            return await _context.Teams.FindAsync(id);
        }

        public async Task<bool> CreateAsync(Team team)
        {
            _context.Teams.Add(team);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, Team team)
        {
            if (id != team.IdTeam) return false;
            _context.Entry(team).State = EntityState.Modified;
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
            var team = await _context.Teams.FindAsync(id);
            if (team == null) return false;
            _context.Teams.Remove(team);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
