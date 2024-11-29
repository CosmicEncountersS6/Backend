using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CEBackend;
using CEBackend.Database;

namespace CEBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AliensController : ControllerBase
    {
        private readonly Context _context;

        public AliensController(Context context)
        {
            _context = context;
        }

        // GET: api/Aliens
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alien>>> GetAliens()
        {
            return await _context.Aliens.ToListAsync();
        }

        // GET: api/Aliens/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alien>> GetAlien(int id)
        {
            var alien = await _context.Aliens.FindAsync(id);

            if (alien == null)
            {
                return NotFound();
            }

            return alien;
        }

        // PUT: api/Aliens/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlien(int id, Alien alien)
        {
            if (id != alien.ID)
            {
                return BadRequest();
            }

            _context.Entry(alien).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlienExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Aliens
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Alien>> PostAlien(Alien alien)
        {
            _context.Aliens.Add(alien);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAlien", new { id = alien.ID }, alien);
        }

        // DELETE: api/Aliens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlien(int id)
        {
            var alien = await _context.Aliens.FindAsync(id);
            if (alien == null)
            {
                return NotFound();
            }

            _context.Aliens.Remove(alien);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlienExists(int id)
        {
            return _context.Aliens.Any(e => e.ID == id);
        }
    }
}
