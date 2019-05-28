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
    public class GradeAPIController : ControllerBase
    {
        private readonly ArcheryVRContext _context;

        public GradeAPIController(ArcheryVRContext context)
        {
            _context = context;
        }

        // GET: api/GradeAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Grade>>> GetGrade()
        {
            return await _context.Grade.ToListAsync();
        }

        // GET: api/GradeAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Grade>> GetGrade(int id)
        {
            var grade = await _context.Grade.FindAsync(id);

            if (grade == null)
            {
                return NotFound();
            }

            return grade;
        }
        private bool GradeExists(int id)
        {
            return _context.Grade.Any(e => e.Id == id);
        }
    }
}
