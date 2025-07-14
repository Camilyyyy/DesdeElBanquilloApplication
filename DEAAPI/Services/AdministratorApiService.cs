using APIDB.Models;
using Microsoft.EntityFrameworkCore;

namespace APIDB.Services
{
    public class AdministratorApiService
    {
        private readonly ApplicationDbContext _context;

        public AdministratorApiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Administrator>> GetAllAsync()
        {
            return await _context.Administrators.ToListAsync();
        }

        public async Task<Administrator?> GetByIdAsync(int id)
        {
            return await _context.Administrators.FindAsync(id);
        }

        public async Task<bool> CreateAsync(Administrator administrator)
        {
            _context.Administrators.Add(administrator);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, Administrator administrator)
        {
            if (id != administrator.IdAdministrator) return false;
            _context.Entry(administrator).State = EntityState.Modified;
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
            var administrator = await _context.Administrators.FindAsync(id);
            if (administrator == null) return false;
            _context.Administrators.Remove(administrator);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
