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
    public class ProgressionAPIController : ControllerBase
    {
        private readonly ArcheryVRContext _context;

        public ProgressionAPIController(ArcheryVRContext context)
        {
            _context = context;
        }

        // GET: api/ProgressionAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Progression>>> GetProgression()
        {
            return await _context.Progression.ToListAsync();
        }

        // GET: api/ProgressionAPI/5
        [HttpGet("{idProfil}")]
        public async Task<ActionResult<Progression>> GetProgression(int idProfil)
        {
            var progression = await _context.Progression.FirstOrDefaultAsync(i => i.ProfilId == idProfil);

            if (progression == null)
            {
                return NotFound();
            }

            return progression;
        }

        // PUT: api/ProgressionAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProgression(int id, Progression progression)
        {
            if (id != progression.Id)
            {
                return BadRequest();
            }

            _context.Entry(progression).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProgressionExists(id))
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

        // POST: api/ProgressionAPI
        [HttpPost]
        public async Task<ActionResult<Progression>> PostProgression(Progression progression)
        {
            _context.Progression.Add(progression);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProgression", new { id = progression.Id }, progression);
        }

        private bool ProgressionExists(int id)
        {
            return _context.Progression.Any(e => e.Id == id);
        }
    }
}
