using HareKrishnaNamaHattaWebApp.Data;
using HareKrishnaNamaHattaWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

public class VolunteerController : Controller
{
    private readonly AppDbContext _context;

    public VolunteerController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        ViewBag.Categories = new SelectList(await _context.ServiceCategories.ToListAsync(), "Id", "Name");
        ViewBag.Services = new SelectList(Enumerable.Empty<SelectListItem>());
        return View(new VolunteerDevotees());
    }

    [HttpPost]
    public async Task<IActionResult> Index(VolunteerDevotees model)
    {
        if (model.PreferredDate == null)
        {
            ModelState.AddModelError("PreferredDate", "Preferred date is required.");
        }
        if (ModelState.IsValid)
        {
            await _context.VolunteerDevotees.AddAsync(model);
            await _context.SaveChangesAsync();
            return RedirectToAction("ThankYou");
        }

        var selectedCategoryId = await _context.VolunteerServices
            .Where(s => s.Id == model.VolunteerServiceId)
            .Select(s => s.ServiceCategoryId)
            .FirstOrDefaultAsync();

        ViewBag.Categories = new SelectList(await _context.ServiceCategories.ToListAsync(), "Id", "Name");
        ViewBag.SelectedCategoryId = selectedCategoryId;

        ViewBag.Services = new SelectList(
            await _context.VolunteerServices
                .Where(s => s.ServiceCategoryId == selectedCategoryId)
                .ToListAsync(),
            "Id", "Name");

        return View(model);
    }

    [HttpGet]
    public async Task<JsonResult> GetServicesByCategory(int categoryId)
    {
        var services = await _context.VolunteerServices
            .Where(s => s.ServiceCategoryId == categoryId)
            .Select(s => new { s.Id, s.Name })
            .ToListAsync();

        return Json(services);
    }

    public IActionResult ThankYou()
    {
        return View();
    }
}
