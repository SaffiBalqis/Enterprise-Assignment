using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MMTS.Data;
using MMTS.Models;
using Microsoft.AspNetCore.Http;

namespace MMTS.Controllers
{
    public class ForumPostController : Controller
    {
        private readonly AppDbContext _context;

        public ForumPostController(AppDbContext context)
        {
            _context = context;
        }

        // GET: ForumPost
        public async Task<IActionResult> Index()
        {
            return View(await _context.ForumPosts.ToListAsync());
        }

        // GET: ForumPost/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var forumPost = await _context.ForumPosts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumPost == null) return NotFound();

            return View(forumPost);
        }

        // GET: ForumPost/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ForumPost/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content")] ForumPost forumPost, IFormFile FileUpload)
        {
            if (ModelState.IsValid)
            {
                if (FileUpload != null && FileUpload.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(FileUpload.FileName);
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await FileUpload.CopyToAsync(fileStream);
                    }

                    forumPost.FilePath = "/uploads/" + uniqueFileName;
                }

                forumPost.PostedOn = DateTime.Now;

                _context.Add(forumPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(forumPost);
        }

        // GET: ForumPost/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var forumPost = await _context.ForumPosts.FindAsync(id);
            if (forumPost == null) return NotFound();

            return View(forumPost);
        }

        // POST: ForumPost/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,PostedOn,FilePath")] ForumPost forumPost)
        {
            if (id != forumPost.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forumPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumPostExists(forumPost.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(forumPost);
        }

        // GET: ForumPost/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var forumPost = await _context.ForumPosts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (forumPost == null) return NotFound();

            return View(forumPost);
        }

        // POST: ForumPost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var forumPost = await _context.ForumPosts.FindAsync(id);
            if (forumPost != null)
            {
                _context.ForumPosts.Remove(forumPost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumPostExists(int id)
        {
            return _context.ForumPosts.Any(e => e.Id == id);
        }
    }
}
