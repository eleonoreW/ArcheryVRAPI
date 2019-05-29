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
    public class ProgressionController : Controller
    {
        private readonly ArcheryVRContext _context;

        public ProgressionController(ArcheryVRContext context)
        {
            _context = context;
        }

        // GET: Progression
        public async Task<IActionResult> Index()
        {
            var archeryVRContext = _context.Progression.Include(p => p.Grade).Include(p => p.Profil);
            return View(await archeryVRContext.ToListAsync());
        }

        // GET: Progression/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progression = await _context.Progression
                .Include(p => p.Grade)
                .Include(p => p.Profil)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (progression == null)
            {
                return NotFound();
            }

            return View(progression);
        }
        // GET: Progression/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progression = await _context.Progression.FindAsync(id);
            if (progression == null)
            {
                return NotFound();
            }
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Nom", progression.GradeId);
            ViewData["ProfilId"] = new SelectList(_context.Profil, "Id", "Nom", progression.ProfilId);
            return View(progression);
        }

        // POST: Progression/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GradeId,ProfilId,DifficulteMaths,Xpmaths,DifficulteFrancais,Xpfrancais,DifficulteAnglais,Xpanglais")] Progression progression)
        {
            if (id != progression.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(progression);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgressionExists(progression.Id))
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
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Nom", progression.GradeId);
            ViewData["ProfilId"] = new SelectList(_context.Profil, "Id", "Nom", progression.ProfilId);
            return View(progression);
        }

        // GET: Progression/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var progression = await _context.Progression
                .Include(p => p.Grade)
                .Include(p => p.Profil)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (progression == null)
            {
                return NotFound();
            }

            return View(progression);
        }

        // POST: Progression/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var progression = await _context.Progression.FindAsync(id);
            _context.Progression.Remove(progression);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgressionExists(int id)
        {
            return _context.Progression.Any(e => e.Id == id);
        }
    }
}
