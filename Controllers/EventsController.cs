using Microsoft.AspNetCore.Mvc;
using HareKrishnaNamaHattaWebApp.Data;
using System.Linq;

public class EventsController : Controller
{
    private readonly AppDbContext _context;

    public EventsController(AppDbContext context)
    {
        _context = context;
    }

    // Public page
    public IActionResult Index()
    {
        return View();
    }

    // JSON for FullCalendar
    [HttpGet]
    public JsonResult GetEvents()
    {
        var events = _context.Events
            .Select(e => new {
                title = e.Title,
                start = e.Date.ToString("yyyy-MM-dd"),
                // You can add e.end if you have end dates
                 end = e.EndDate.Value.ToString("yyyy-MM-dd")
            }).ToList();

        return Json(events);
    }
}
