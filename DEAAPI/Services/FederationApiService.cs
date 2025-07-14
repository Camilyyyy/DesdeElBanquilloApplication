using APIDB.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Services
{
    public class FederationApiService
    {
        private readonly ApplicationDbContext _context;

        public FederationApiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Federation>> GetAllAsync()
        {
            return await _context.Federations.ToListAsync();
        }

        public async Task<Federation?> GetByIdAsync(int id)
        {
            return await _context.Federations.FindAsync(id);
        }

        public async Task<bool> CreateAsync(Federation federation)
        {
            _context.Federations.Add(federation);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, Federation federation)
        {
            if (id != federation.IdFederation) return false;
            _context.Entry(federation).State = EntityState.Modified;
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
            var federation = await _context.Federations.FindAsync(id);
            if (federation == null) return false;
            _context.Federations.Remove(federation);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
