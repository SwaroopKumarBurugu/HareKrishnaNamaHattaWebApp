using HareKrishnaNamaHattaWebApp.Data;
using HareKrishnaNamaHattaWebApp.Models;
using HareKrishnaNamaHattaWebApp.ViewModels;
using HareKrishnaNamaHattaWebApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HareKrishnaNamaHattaWebApp.Services;

namespace HareKrishnaNamaHattaWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;
        private readonly ManageEventService _eventService;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, ManageEventService eventService)
        {
            _logger = logger;
            _context = context;
            _eventService = eventService;
        }

        public async Task<IActionResult> Index()
        {
            var specialEvents = await _eventService.GetSpecialEventsAsync();
            var everydayEvents = await _eventService.GetEverydayEventsAsync();

            var viewModel = new HomePageEventsViewModel
            {
                SpecialEvents = specialEvents,
                EverydayEvents = everydayEvents
            };

            return View(viewModel);
        }
        public IActionResult About() => View();

        public IActionResult Privacy()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Contact() => View(new ContactMessage());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactMessage model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.ContactMessages.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("ContactConfirmation");
                }
            }

            catch (Exception ex)
            {
                // Log or inspect ex.Message
                throw;
            }

            return View(model);
        }

        public IActionResult ContactConfirmation() => View();

        public IActionResult Events()
        {
            var allEvents = _context.Events
                .Where(e => e.Date >= DateTime.Today)
                .OrderBy(e => e.Date)
                .ToList();
            return View(allEvents);
        }
        
        public IActionResult EventDetails(int id)
        {
            var evt = _context.Events.FirstOrDefault(e => e.Id == id);
            if (evt == null) return NotFound();
            return View(evt);
        }

        [HttpGet]
        public IActionResult Donate() => View(new Donation());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Donate(Donation model)
        {
            if (ModelState.IsValid)
            {
                _context.Donations.Add(model);
                _context.SaveChanges();
                return RedirectToAction("DonateConfirmation");
            }
            return View(model);
        }

        public IActionResult DonateConfirmation() => View();

        [HttpGet]
        public IActionResult VolunteerService()
        {
            return View(new VolunteerDevotees());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VolunteerService(VolunteerDevotees model)
        {
            if (ModelState.IsValid)
            {
                // Here you'd normally send an email or store to DB
                // For now, just simulate success
                TempData["SuccessMessage"] = "Thank you for your voluntary service! Admin has been notified.";
                return RedirectToAction("VolunteerService");
            }

            return View(model);
        }
        public IActionResult Gallery()
        {
            string galleryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "gallery");

            var imageFiles = Directory.GetFiles(galleryPath)
                .Select(f => "/uploads/gallery/" + Path.GetFileName(f))
                .ToList();

            return View(imageFiles);
        }

        //Temparary Method to generate hash code need to remove later
        public IActionResult GenerateHash()
        {
            string plainPassword = "Dasanudasa@130525";
            string hash = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            ViewBag.PlainPassword = plainPassword;
            ViewBag.HashedPassword = hash;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
