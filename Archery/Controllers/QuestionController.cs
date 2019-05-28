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
    public class QuestionController : Controller
    {
        private readonly ArcheryVRContext _context;

        public QuestionController(ArcheryVRContext context)
        {
            _context = context;
        }

        // GET: Question
        public async Task<IActionResult> Index()
        {
            var archeryVRContext = _context.Question.Include(q => q.Grade).Include(q => q.Sujet);
            return View(await archeryVRContext.ToListAsync());
        }

        // GET: Question/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                .Include(q => q.Grade)
                .Include(q => q.Sujet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Question/Create
        public IActionResult Create()
        {
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Id");
            ViewData["SujetId"] = new SelectList(_context.Sujet, "Id", "Id");
            return View();
        }

        // POST: Question/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GradeId,SujetId,QuestionText,Difficult,Explanation,GoodAnswers,BadAnswers")] Question question)
        {
            if (ModelState.IsValid)
            {
                _context.Add(question);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Id", question.GradeId);
            ViewData["SujetId"] = new SelectList(_context.Sujet, "Id", "Id", question.SujetId);
            return View(question);
        }

        // GET: Question/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question.FindAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Id", question.GradeId);
            ViewData["SujetId"] = new SelectList(_context.Sujet, "Id", "Id", question.SujetId);
            return View(question);
        }

        // POST: Question/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GradeId,SujetId,QuestionText,Difficult,Explanation,GoodAnswers,BadAnswers")] Question question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(question);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionExists(question.Id))
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
            ViewData["GradeId"] = new SelectList(_context.Grade, "Id", "Id", question.GradeId);
            ViewData["SujetId"] = new SelectList(_context.Sujet, "Id", "Id", question.SujetId);
            return View(question);
        }

        // GET: Question/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await _context.Question
                .Include(q => q.Grade)
                .Include(q => q.Sujet)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Question/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _context.Question.FindAsync(id);
            _context.Question.Remove(question);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuestionExists(int id)
        {
            return _context.Question.Any(e => e.Id == id);
        }
    }
}
