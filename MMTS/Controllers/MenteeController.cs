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
    public class MenteeController : Controller
    {
        private readonly AppDbContext _context;

        public MenteeController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Mentee
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mentees.ToListAsync());
        }

        // GET: Mentee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentee = await _context.Mentees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mentee == null)
            {
                return NotFound();
            }

            return View(mentee);
        }

        // GET: Mentee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mentee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email")] Mentee mentee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mentee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mentee);
        }

        // GET: Mentee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentee = await _context.Mentees.FindAsync(id);
            if (mentee == null)
            {
                return NotFound();
            }
            return View(mentee);
        }

        // POST: Mentee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email")] Mentee mentee)
        {
            if (id != mentee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mentee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenteeExists(mentee.Id))
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
            return View(mentee);
        }

        // GET: Mentee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentee = await _context.Mentees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mentee == null)
            {
                return NotFound();
            }

            return View(mentee);
        }

        // POST: Mentee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mentee = await _context.Mentees.FindAsync(id);
            if (mentee != null)
            {
                _context.Mentees.Remove(mentee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MenteeExists(int id)
        {
            return _context.Mentees.Any(e => e.Id == id);
        }
    }
}
