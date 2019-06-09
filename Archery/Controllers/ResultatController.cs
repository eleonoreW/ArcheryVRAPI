using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Archery.Models;
using Newtonsoft.Json;

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

        // Resultat/Graph/1
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

            List<int> lvlInfos = new List<int>();
            List<List<double>> listRes = new List<List<double>>();
            List<List<string>> listDate = new List<List<string>>();

            List<string> listResJson = new List<string>();
            List<string> listDateJson = new List<string>();

            foreach (var item in archeryVRContext)
            {
                var lvl = item.DifficulteMaths;
                var x = item.ResMaths;
                var y = item.DateResultat.ToShortDateString();

                if (lvl == null)
                    lvl = 0;
                if (x == null)
                    x = 0.0;
                if (y == null)
                    y = "unknown";

                // verifie on a pas déja une liste pour ce lvl
                var index = lvlInfos.IndexOf((int)lvl);
                // si pas créée, on la créée
                if (index == -1)
                {
                    lvlInfos.Add((int)lvl);
                    listRes.Add(new List<double>());
                    listDate.Add(new List<string>());

                    index = lvlInfos.IndexOf((int)lvl);
                }

                listRes[index].Add((Math.Round((double)x, 2)));
                listDate[index].Add((string)y);
            }

            for (int i = 0; i < lvlInfos.Count; i++)
            {
                listResJson.Add(JsonConvert.SerializeObject(listRes[i]));
                listDateJson.Add(JsonConvert.SerializeObject(listDate[i]));
            }

            ViewBag.LVL = lvlInfos;
            ViewBag.RES = listResJson;
            ViewBag.DATE = listDateJson;

            return View(await archeryVRContext.ToListAsync());
        }

    }
}
