using Microsoft.AspNetCore.Mvc;
using HareKrishnaNamaHattaWebApp.Data;
using HareKrishnaNamaHattaWebApp.Models;
namespace HareKrishnaNamaHattaWebApp.Controllers
{
    //[AuthorizeAdmin]
    public class DonationController : Controller
    {
        private readonly AppDbContext _context;

        public DonationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Donation model)
        {
            if (ModelState.IsValid)
            {
                _context.Donations.Add(model);
                _context.SaveChanges();
                TempData["Message"] = "Thank you for your donation!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}

