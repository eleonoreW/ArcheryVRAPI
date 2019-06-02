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
            Console.WriteLine("Display Res");
            return await _context.Resultat.ToListAsync();
        }
        
        // POST: api/Resultat
        [HttpPost]
        public async Task<ActionResult<Resultat>> PostResultat(Resultat resultat)
        {
            Console.WriteLine("Resultat reçu"+resultat.ToString());
            resultat.DateResultat = DateTime.Now;
            _context.Resultat.Add(resultat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetResultat", new { id = resultat.Id }, resultat);
        }

        private bool ResultatExists(int id)
        {
            return _context.Resultat.Any(e => e.Id == id);
        }
    }
}
