using Microsoft.AspNetCore.Mvc;
using HareKrishnaNamaHattaWebApp.Data;
using HareKrishnaNamaHattaWebApp.Models;
using HareKrishnaNamaHattaWebApp.Utility;

namespace HareKrishnaNamaHattaWebApp.Controllers
{
   // [AuthorizeAdmin]
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(ContactMessage model)
        {
            if (ModelState.IsValid)
            {
                _context.ContactMessages.Add(model);
                _context.SaveChanges();
                TempData["Message"] = "Thank you for contacting us!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
