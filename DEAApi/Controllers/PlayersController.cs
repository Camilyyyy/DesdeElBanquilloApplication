using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DEAApi.Data;
using DEAModels;

namespace DEAApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Players
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            return await _context.Players
                                 .Include(p => p.Team)
                                 .Include(p => p.Position)
                                 .Include(p => p.Country)
                                 .ToListAsync();
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _context.Players
                                       .Include(p => p.Team)
                                       .Include(p => p.Position)
                                       .Include(p => p.Country)
                                       .FirstOrDefaultAsync(p => p.IdPlayer == id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // PUT: api/Players/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            // ... (Este método probablemente necesitará un manejo de errores similar al de POST)
            if (id != player.IdPlayer)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id)) { return NotFound(); } else { throw; }
            }

            return NoContent();
        }

        // =========================================================================
        // POST: api/Players (¡MÉTODO MODIFICADO!)
        // =========================================================================
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            // Verificamos si el modelo que llega es válido según las anotaciones ([Required], etc.)
            if (!ModelState.IsValid)
            {
                // Si no es válido, devolvemos un error 400 con los detalles de la validación.
                return BadRequest(ModelState);
            }

            try
            {
                _context.Players.Add(player);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPlayer), new { id = player.IdPlayer }, player);
            }
            catch (DbUpdateException ex)
            {
                // Este bloque se activará si hay un error al guardar en la base de datos
                // (ej. una clave foránea que no existe, una restricción violada).
                // La 'InnerException' suele contener el mensaje de error real de SQL.
                var innerExceptionMessage = ex.InnerException?.Message ?? ex.Message;

                // Imprimimos el error en la consola de la API para verlo durante la depuración.
                Console.WriteLine($"DB UPDATE EXCEPTION: {innerExceptionMessage}");

                // Devolvemos un error 500 con un mensaje útil al cliente (la app MAUI).
                return StatusCode(500, $"Error de base de datos: {innerExceptionMessage}");
            }
            catch (Exception ex)
            {
                // Captura cualquier otro error inesperado.
                Console.WriteLine($"GENERIC EXCEPTION: {ex.Message}");
                return StatusCode(500, $"Un error inesperado ocurrió: {ex.Message}");
            }
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerExists(int id)
        {
            return _context.Players.Any(e => e.IdPlayer == id);
        }
    }
}