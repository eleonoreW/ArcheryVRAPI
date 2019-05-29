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
        [HttpPut("{idProfil}")]
        public async Task<IActionResult> PutProgression(int idProfil, Progression progression)
        {
            using (var ctx = _context)
            {
                var existingProg = ctx.Progression.Where(s => s.ProfilId == idProfil)
                                                        .FirstOrDefault<Progression>();

                if (existingProg != null)
                {
                    existingProg.GradeId = progression.GradeId;
                    existingProg.DifficulteAnglais = progression.DifficulteAnglais;
                    existingProg.DifficulteAnglais = progression.DifficulteAnglais;
                    existingProg.DifficulteMaths = progression.DifficulteMaths;
                    existingProg.Xpanglais = progression.Xpanglais;
                    existingProg.Xpfrancais = progression.Xpfrancais;
                    existingProg.Xpmaths = progression.Xpmaths;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();

                }
                return NoContent(); 
            }
        }
        private bool ProgressionExists(int id)
        {
            return _context.Progression.Any(e => e.Id == id);
        }
    }
}
