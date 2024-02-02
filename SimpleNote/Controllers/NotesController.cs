using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleNote.Data;
using SimpleNote.Models;

namespace SimpleNote.Controllers
{
    public class NotesController : Controller
    {
        private readonly Context _context;

        public NotesController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Notes.ToListAsync());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null) { return NotFound(); }
            var note = await _context.Notes.FirstOrDefaultAsync(m => m.Id == id);
            if (note == null) { return NotFound(); }
            return View(note);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Content")] Note note)
        {
            if (ModelState.IsValid)
            {
                _context.Add(note);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) {  return NotFound(); }
            var note = await _context.Notes.FindAsync(id);
            if (note == null) { return NotFound(); }
            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Content")] Note note)
        {
            if (id != note.Id) { return NotFound(); }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(note);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(note.Id)) { return NotFound(); }
                    else { throw; }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(note);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) { return NotFound(); }
            var note = await _context.Notes.FirstOrDefaultAsync(m => m.Id == id);
            if (note == null) { return NotFound(); }
            return View(note);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note != null) { _context.Notes.Remove(note); }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteExists(string id)
        {
            return _context.Notes.Any(e => e.Id == id);
        }
    }
}