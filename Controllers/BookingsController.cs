using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InternalResourceBookingSystem.Models;
using System.Linq;
using System.Threading.Tasks;

namespace InternalResourceBookingSystem.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = _context.Bookings.Include(b => b.Resource);
            return View(await bookings.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["ResourceId"] = new SelectList(_context.Resources, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (booking.EndTime <= booking.StartTime)
            {
                ModelState.AddModelError("", "End time must be after start time.");
            }

            var conflict = _context.Bookings.Any(b =>
                b.ResourceId == booking.ResourceId &&
                ((booking.StartTime >= b.StartTime && booking.StartTime < b.EndTime) ||
                 (booking.EndTime > b.StartTime && booking.EndTime <= b.EndTime) ||
                 (booking.StartTime <= b.StartTime && booking.EndTime >= b.EndTime)));

            if (conflict)
            {
                ModelState.AddModelError("", "This resource is already booked during the selected time.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ResourceId"] = new SelectList(_context.Resources, "Id", "Name", booking.ResourceId);
            return View(booking);
        }
    }
}