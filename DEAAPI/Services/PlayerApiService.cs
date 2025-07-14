using APIDB.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Services
{
    public class PlayerApiService
    {
        private readonly ApplicationDbContext _context;

        public PlayerApiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Player>> GetAllAsync()
        {
            return await _context.Players.ToListAsync();
        }

        public async Task<Player?> GetByIdAsync(int id)
        {
            return await _context.Players.FindAsync(id);
        }

        public async Task<bool> CreateAsync(Player player)
        {
            _context.Players.Add(player);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, Player player)
        {
            if (id != player.IdPlayer) return false;
            _context.Entry(player).State = EntityState.Modified;
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
            var player = await _context.Players.FindAsync(id);
            if (player == null) return false;
            _context.Players.Remove(player);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
