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

        private bool ProfilExists(int id)
        {
            return _context.Profil.Any(e => e.Id == id);
        }
    }
}
