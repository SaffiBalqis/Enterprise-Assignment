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
    public class MentorController : Controller
    {
        private readonly AppDbContext _context;

        public MentorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Mentor
        public async Task<IActionResult> Index()
        {
            return View(await _context.Mentors.ToListAsync());
        }

        // GET: Mentor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentor = await _context.Mentors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mentor == null)
            {
                return NotFound();
            }

            return View(mentor);
        }

        // GET: Mentor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mentor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,Expertise")] Mentor mentor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mentor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mentor);
        }

        // GET: Mentor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentor = await _context.Mentors.FindAsync(id);
            if (mentor == null)
            {
                return NotFound();
            }
            return View(mentor);
        }

        // POST: Mentor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Email,Expertise")] Mentor mentor)
        {
            if (id != mentor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mentor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MentorExists(mentor.Id))
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
            return View(mentor);
        }

        // GET: Mentor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mentor = await _context.Mentors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mentor == null)
            {
                return NotFound();
            }

            return View(mentor);
        }

        // POST: Mentor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mentor = await _context.Mentors.FindAsync(id);
            if (mentor != null)
            {
                _context.Mentors.Remove(mentor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MentorExists(int id)
        {
            return _context.Mentors.Any(e => e.Id == id);
        }
    }
}
