using Microsoft.AspNetCore.Mvc;
using HareKrishnaNamaHattaWebApp.Models;
using HareKrishnaNamaHattaWebApp.Data;
using System.Threading.Tasks;

public class VolunteerController : Controller
{
    private readonly AppDbContext _context;

    public VolunteerController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new VolunteerDevotees());
    }

    [HttpPost]
    public async Task<IActionResult> Index(VolunteerDevotees model)
    {
        if (ModelState.IsValid)
        {
            await _context.VolunteerDevotees.AddAsync(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Thank you for volunteering! We’ll contact you soon.";
            return RedirectToAction("ThankYou");
        }

        return View(model);
    }

    public IActionResult ThankYou()
    {
        return View();
    }
}
