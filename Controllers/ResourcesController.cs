using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceBookingSystem.Models;

namespace ResourceBookingSystem.Controllers;


public class ResourcesController(ApplicationDbContext _context) : Controller
{
    // GET: Resources
    public async Task<IActionResult> Index()
    {
        return View(await _context.Resources.ToListAsync());
    }

    // GET: Resources/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var resource = await _context.Resources
            .Include(r => r.Bookings)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (resource == null) return NotFound();

        return View(resource);
    }

    // GET: Resources/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Resources/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Resource resource)
    {
        if (resource.Capacity <= 0)
            ModelState.AddModelError("Capacity", "Capacity must be a positive number.");

        if (ModelState.IsValid)
        {
            _context.Add(resource);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(resource);
    }

    // GET: Resources/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var resource = await _context.Resources.FindAsync(id);
        if (resource == null) return NotFound();

        return View(resource);
    }

    // POST: Resources/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Resource resource)
    {
        if (id != resource.Id) return NotFound();

        if (resource.Capacity <= 0)
            ModelState.AddModelError("Capacity", "Capacity must be a positive number.");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(resource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Resources.Any(e => e.Id == resource.Id))
                    return NotFound();
                else
                    throw;
            }
        }

        return View(resource);
    }

    // GET: Resources/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var resource = await _context.Resources.FirstOrDefaultAsync(m => m.Id == id);
        if (resource == null) return NotFound();

        return View(resource);
    }

    // POST: Resources/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var resource = await _context.Resources.FindAsync(id);
        if (resource != null)
        {
            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}
