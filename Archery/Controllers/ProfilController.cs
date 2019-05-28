using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Archery.Models;

namespace Archery.Controllers
{
    public class ProfilController : Controller
    {
        private readonly ArcheryVRContext _context;

        public ProfilController(ArcheryVRContext context)
        {
            _context = context;
        }

        // GET: Profil
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profil.ToListAsync());
        }

        // GET: Profil/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profil = await _context.Profil
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profil == null)
            {
                return NotFound();
            }

            return View(profil);
        }


        private bool ProfilExists(int id)
        {
            return _context.Profil.Any(e => e.Id == id);
        }
    }
}
