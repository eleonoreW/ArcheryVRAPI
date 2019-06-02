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
    public class ResultatController : Controller
    {
        private readonly ArcheryVRContext _context;

        public ResultatController(ArcheryVRContext context)
        {
            _context = context;
        }

        // GET: Resultat
        public async Task<IActionResult> Index()
        {
            var archeryVRContext = _context.Resultat.Include(r => r.Grade).Include(r => r.Profil);
            return View(await archeryVRContext.ToListAsync());
        }
        // GET: Resultat/Graph/1
        public async Task<IActionResult> Graph(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var archeryVRContext = _context.Resultat
                .Include(r => r.Grade)
                .Include(r => r.Profil)
                .Where(s => s.ProfilId.Equals(id));
            return View(await archeryVRContext.ToListAsync());
        }
        // GET: Resultat/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultat = await _context.Resultat
                .Include(r => r.Grade)
                .Include(r => r.Profil)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resultat == null)
            {
                return NotFound();
            }

            return View(resultat);
        }

        // GET: Resultat/Create
        public IActionResult Create()
        {
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Nom");
            ViewData["ProfilId"] = new SelectList(_context.Profil, "Id", "Nom");
            return View();
        }

        // POST: Resultat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProfilId,GradeId,DateResultat,DifficulteMaths,ResMaths,DifficulteFrancais,ResFrancais,DifficulteAnglais,ResAnglais")] Resultat resultat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resultat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Nom", resultat.GradeId);
            ViewData["ProfilId"] = new SelectList(_context.Profil, "Id", "Nom", resultat.ProfilId);
            return View(resultat);
        }

        // GET: Resultat/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultat = await _context.Resultat.FindAsync(id);
            if (resultat == null)
            {
                return NotFound();
            }
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Nom", resultat.GradeId);
            ViewData["ProfilId"] = new SelectList(_context.Profil, "Id", "Nom", resultat.ProfilId);
            return View(resultat);
        }

        // POST: Resultat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilId,GradeId,DateResultat,DifficulteMaths,ResMaths,DifficulteFrancais,ResFrancais,DifficulteAnglais,ResAnglais")] Resultat resultat)
        {
            if (id != resultat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resultat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultatExists(resultat.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Nom", resultat.GradeId);
            ViewData["ProfilId"] = new SelectList(_context.Profil, "Id", "Nom", resultat.ProfilId);
            return View(resultat);
        }

        // GET: Resultat/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultat = await _context.Resultat
                .Include(r => r.Grade)
                .Include(r => r.Profil)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resultat == null)
            {
                return NotFound();
            }

            return View(resultat);
        }

        // POST: Resultat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultat = await _context.Resultat.FindAsync(id);
            _context.Resultat.Remove(resultat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultatExists(int id)
        {
            return _context.Resultat.Any(e => e.Id == id);
        }
    }
}
