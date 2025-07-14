using APIDB.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Services
{
    public class PositionApiService
    {
        private readonly ApplicationDbContext _context;

        public PositionApiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Position>> GetAllAsync()
        {
            return await _context.Positions.ToListAsync();
        }

        public async Task<Position?> GetByIdAsync(int id)
        {
            return await _context.Positions.FindAsync(id);
        }

        public async Task<bool> CreateAsync(Position position)
        {
            _context.Positions.Add(position);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, Position position)
        {
            if (id != position.IdPosition) return false;
            _context.Entry(position).State = EntityState.Modified;
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
            var position = await _context.Positions.FindAsync(id);
            if (position == null) return false;
            _context.Positions.Remove(position);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
