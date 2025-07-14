using APIDB.Models;
using Microsoft.EntityFrameworkCore;

namespace DEAAPI.Services
{
    public class CountryApiService
    {
        private readonly ApplicationDbContext _context;

        public CountryApiService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Country>> GetAllAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country?> GetByIdAsync(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public async Task<bool> CreateAsync(Country country)
        {
            _context.Countries.Add(country);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(int id, Country country)
        {
            if (id != country.IdCountry) return false;
            _context.Entry(country).State = EntityState.Modified;
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
            var country = await _context.Countries.FindAsync(id);
            if (country == null) return false;
            _context.Countries.Remove(country);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
