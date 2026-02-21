using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieListApp.Data;
using MovieListApp.Models;

namespace MovieListApp.Controllers
{
    public class FlightsController : Controller
    {
        private readonly FlightContext _context;

        // Predefined cities for dropdowns (you can add more if you want)
        private static readonly string[] Cities = new[]
        {
            "Chicago", "New York", "Dubai", "London",
            "Hong Kong", "San Francisco", "Los Angeles",
            "Toronto", "Tokyo", "Paris"
        };

        public FlightsController(FlightContext context) => _context = context;

        // LIST
        public async Task<IActionResult> Index()
        {
            var flights = await _context.Flights.OrderBy(f => f.Date).ToListAsync();
            return View(flights);
        }

        // CREATE
        public IActionResult Create()
        {
            ViewBag.Cities = Cities;
            return View(new Flight { Date = DateTime.Today });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Flight flight)
        {
            if (flight.FromCity == flight.ToCity)
                ModelState.AddModelError("ToCity", "Destination must be different from origin.");

            if (!ModelState.IsValid)
            {
                ViewBag.Cities = Cities;   // re-provide options when redisplaying validation errors
                return View(flight);
            }

            _context.Add(flight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // EDIT
        public async Task<IActionResult> Edit(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight == null) return NotFound();

            ViewBag.Cities = Cities;       // provide options for edit form
            return View(flight);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Flight flight)
        {
            if (id != flight.Id) return BadRequest();

            if (flight.FromCity == flight.ToCity)
                ModelState.AddModelError("ToCity", "Destination must be different from origin.");

            if (!ModelState.IsValid)
            {
                ViewBag.Cities = Cities;   // re-provide options when redisplaying
                return View(flight);
            }

            _context.Update(flight);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // DELETE confirmation page
        public async Task<IActionResult> Delete(int id)
        {
            var flight = await _context.Flights.FirstOrDefaultAsync(f => f.Id == id);
            if (flight == null) return NotFound();
            return View(flight);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flight = await _context.Flights.FindAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}