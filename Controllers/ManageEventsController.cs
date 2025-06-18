using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HareKrishnaNamaHattaWebApp.Data;
using HareKrishnaNamaHattaWebApp.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HareKrishnaNamaHattaWebApp.Controllers
{
    public class ManageEventsController : BaseController
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ManageEventsController> _logger;

        public ManageEventsController(AppDbContext context, ILogger<ManageEventsController> logger):base(logger)
        {
            _context = context;
            _logger = logger;
            ViewData["Layout"] = "_AdminLayout";
        }

        public async Task<IActionResult> Index()
        {
            if (!IsAdminLoggedIn()) return RedirectToAction("Login", "Admin");

            var events = await _context.Events.OrderByDescending(e => e.Date).ToListAsync();
            return View(events);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (!IsAdminLoggedIn()) return RedirectToAction("Login", "Admin");

            if (id == null) return NotFound();

            var evt = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (evt == null) return NotFound();

            return View(evt);
        }

        [HttpGet]
        public IActionResult Create()
        { 
            if (!IsAdminLoggedIn()) return RedirectToAction("Login", "Admin");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event evt)
        {
            if (!IsAdminLoggedIn()) return RedirectToAction("Login", "Admin");

            if (ModelState.IsValid)
            {
                _context.Events.Add(evt);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Event created successfully!";
                _logger.LogInformation("Event created: {EventName} on {Date}", evt.Title, evt.Date);
                return RedirectToAction("Index");
            }

            return View(evt);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (!IsAdminLoggedIn()) return RedirectToAction("Login", "Admin");

            if (id == null) return NotFound();

            var evt = await _context.Events.FindAsync(id);
            if (evt == null) return NotFound();

            return View(evt);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Event evt)
        {
            if (!IsAdminLoggedIn()) return RedirectToAction("Login", "Admin");

            if (id != evt.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Events.Update(evt);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Event updated successfully!";
                    _logger.LogInformation("Event updated: {EventName} (ID: {EventId})", evt.Title, evt.Id);
                }
                catch (DbUpdateException ex)
                {
                    _logger.LogError(ex, "Error updating event ID: {EventId}", id);
                    TempData["ErrorMessage"] = "An error occurred while updating the event.";
                }

                return RedirectToAction("Index");
            }

            return View(evt);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (!IsAdminLoggedIn()) return RedirectToAction("Login", "Admin");

            if (id == null) return NotFound();

            var evt = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            if (evt == null) return NotFound();

            return View(evt);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!IsAdminLoggedIn()) return RedirectToAction("Login", "Admin");

            var evt = await _context.Events.FindAsync(id);
            if (evt != null)
            {
                _context.Events.Remove(evt);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Event deleted successfully!";
                _logger.LogInformation("Event deleted: {EventName} (ID: {EventId})", evt.Title, evt.Id);
            }
            else
            {
                TempData["ErrorMessage"] = "Event not found.";
                _logger.LogWarning("Attempt to delete non-existent event ID: {EventId}", id);
            }

            return RedirectToAction("Index");
        }
    }
}
