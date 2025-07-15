using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResourceBookingSystem.Models;

namespace ResourceBookingSystem.Controllers;

// ✅ Primary constructor used (C# 12 feature)
public class BookingsController(ApplicationDbContext _context) : Controller
{
    // GET: Bookings
    public async Task<IActionResult> Index()
    {
        var bookings = _context.Bookings
            .Include(b => b.Resource)
            .OrderBy(b => b.StartTime);

        return View(await bookings.ToListAsync());
    }

    // GET: Bookings/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var booking = await _context.Bookings
            .Include(b => b.Resource)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (booking == null) return NotFound();

        return View(booking);
    }

    // GET: Bookings/Create
    public IActionResult Create()
    {
        ViewBag.ResourceId = new SelectList(_context.Resources.Where(r => r.IsAvailable), "Id", "Name");
        return View();
    }

    // POST: Bookings/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Booking booking)
    {
        if (booking.EndTime <= booking.StartTime)
            ModelState.AddModelError("EndTime", "End Time must be after Start Time.");

        if (string.IsNullOrWhiteSpace(booking.BookedBy))
            ModelState.AddModelError("BookedBy", "Booked By is required.");

        if (string.IsNullOrWhiteSpace(booking.Purpose))
            ModelState.AddModelError("Purpose", "Purpose is required.");

        if (ModelState.IsValid)
        {
            bool conflict = _context.Bookings.Any(b =>
                b.ResourceId == booking.ResourceId &&
                b.StartTime < booking.EndTime &&
                booking.StartTime < b.EndTime
            );

            if (conflict)
            {
                ModelState.AddModelError("", "This resource is already booked during the selected time.");
            }
            else
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        ViewBag.ResourceId = new SelectList(_context.Resources.Where(r => r.IsAvailable), "Id", "Name", booking.ResourceId);
        return View(booking);
    }

    // GET: Bookings/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null) return NotFound();

        ViewBag.ResourceId = new SelectList(_context.Resources.Where(r => r.IsAvailable), "Id", "Name", booking.ResourceId);
        return View(booking);
    }

    // POST: Bookings/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Booking booking)
    {
        if (id != booking.Id) return NotFound();

        if (booking.EndTime <= booking.StartTime)
            ModelState.AddModelError("EndTime", "End Time must be after Start Time.");

        if (string.IsNullOrWhiteSpace(booking.BookedBy))
            ModelState.AddModelError("BookedBy", "Booked By is required.");

        if (string.IsNullOrWhiteSpace(booking.Purpose))
            ModelState.AddModelError("Purpose", "Purpose is required.");

        if (ModelState.IsValid)
        {
            bool conflict = _context.Bookings.Any(b =>
                b.Id != booking.Id &&
                b.ResourceId == booking.ResourceId &&
                b.StartTime < booking.EndTime &&
                booking.StartTime < b.EndTime
            );

            if (conflict)
            {
                ModelState.AddModelError("", "This resource is already booked during the selected time.");
            }
            else
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Bookings.Any(e => e.Id == booking.Id))
                        return NotFound();
                    else
                        throw;
                }
            }
        }

        ViewBag.ResourceId = new SelectList(_context.Resources.Where(r => r.IsAvailable), "Id", "Name", booking.ResourceId);
        return View(booking);
    }

    // GET: Bookings/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var booking = await _context.Bookings
            .Include(b => b.Resource)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (booking == null) return NotFound();

        return View(booking);
    }

    // POST: Bookings/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking != null)
        {
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }
}

