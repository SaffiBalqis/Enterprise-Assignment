using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MMTS.Data;
using MMTS.Models;

namespace MMTS.Controllers
{
    public class SessionController : Controller
    {
        private readonly AppDbContext _context;

        public SessionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Session
        public async Task<IActionResult> Index()
        {
            var sessions = _context.Sessions
                .Include(s => s.Mentor)
                .Include(s => s.Mentee);
            return View(await sessions.ToListAsync());
        }

        // GET: Session/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var session = await _context.Sessions
                .Include(s => s.Mentor)
                .Include(s => s.Mentee)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (session == null) return NotFound();

            return View(session);
        }

        // GET: Session/Create
        public IActionResult Create()
        {
            PopulateDropdowns();
            return View();
        }

        // POST: Session/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MentorId,MenteeId,ScheduledTime")] Session session)
        {
            if (ModelState.IsValid)
            {
                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdowns(session.MentorId, session.MenteeId);
            return View(session);
        }

        // GET: Session/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var session = await _context.Sessions.FindAsync(id);
            if (session == null) return NotFound();

            PopulateDropdowns(session.MentorId, session.MenteeId);
            return View(session);
        }

        // POST: Session/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MentorId,MenteeId,ScheduledTime")] Session session)
        {
            if (id != session.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            PopulateDropdowns(session.MentorId, session.MenteeId);
            return View(session);
        }

        // GET: Session/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var session = await _context.Sessions
                .Include(s => s.Mentor)
                .Include(s => s.Mentee)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (session == null) return NotFound();

            return View(session);
        }

        // POST: Session/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool SessionExists(int id)
        {
            return _context.Sessions.Any(e => e.Id == id);
        }

        private void PopulateDropdowns(int? selectedMentorId = null, int? selectedMenteeId = null)
        {
            ViewData["MentorId"] = new SelectList(_context.Mentors, "Id", "FullName", selectedMentorId);
            ViewData["MenteeId"] = new SelectList(_context.Mentees, "Id", "FullName", selectedMenteeId);
        }
    }
}
