using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Archery.Models;

namespace Archery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilAPIController : ControllerBase
    {
        private readonly ArcheryVRContext _context;

        public ProfilAPIController(ArcheryVRContext context)
        {
            _context = context;
        }

        // GET: api/ProfilAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profil>>> GetProfil()
        {
            return await _context.Profil.ToListAsync();
        }

        // GET: api/ProfilAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profil>> GetProfil(int id)
        {
            var profil = await _context.Profil.FindAsync(id);

            if (profil == null)
            {
                return NotFound();
            }

            return profil;
        }

        // PUT: api/ProfilAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfil(int id, Profil profil)
        {
            if (id != profil.Id)
            {
                return BadRequest();
            }

            _context.Entry(profil).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfilExists(id))
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
        /*
        // POST: api/ProfilAPI
        [HttpPost]
        public async Task<ActionResult<Profil>> PostProfil(Profil profil)
        {
            _context.Profil.Add(profil);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfil", new { id = profil.Id }, profil);
        }

        // DELETE: api/ProfilAPI/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Profil>> DeleteProfil(int id)
        {
            var profil = await _context.Profil.FindAsync(id);
            if (profil == null)
            {
                return NotFound();
            }

            _context.Profil.Remove(profil);
            await _context.SaveChangesAsync();

            return profil;
        }*/

        private bool ProfilExists(int id)
        {
            return _context.Profil.Any(e => e.Id == id);
        }
    }
}
