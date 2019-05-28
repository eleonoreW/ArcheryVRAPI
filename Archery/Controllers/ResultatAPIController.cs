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
    public class ResultatAPIController : ControllerBase
    {
        private readonly ArcheryVRContext _context;

        public ResultatAPIController(ArcheryVRContext context)
        {
            _context = context;
        }

        // GET: api/Resultat
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resultat>>> GetResultat()
        {
            return await _context.Resultat.ToListAsync();
        }

        // GET: api/Resultat/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Resultat>> GetResultat(int id)
        {
            var resultat = await _context.Resultat.FindAsync(id);

            if (resultat == null)
            {
                return NotFound();
            }

            return resultat;
        }

        // PUT: api/Resultat/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutResultat(int id, Resultat resultat)
        {
            if (id != resultat.Id)
            {
                return BadRequest();
            }

            _context.Entry(resultat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ResultatExists(id))
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

        // POST: api/Resultat
        [HttpPost]
        public async Task<ActionResult<Resultat>> PostResultat(Resultat resultat)
        {
            _context.Resultat.Add(resultat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResultat", new { id = resultat.Id }, resultat);
        }

        // DELETE: api/Resultat/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Resultat>> DeleteResultat(int id)
        {
            var resultat = await _context.Resultat.FindAsync(id);
            if (resultat == null)
            {
                return NotFound();
            }

            _context.Resultat.Remove(resultat);
            await _context.SaveChangesAsync();

            return resultat;
        }

        private bool ResultatExists(int id)
        {
            return _context.Resultat.Any(e => e.Id == id);
        }
    }
}
