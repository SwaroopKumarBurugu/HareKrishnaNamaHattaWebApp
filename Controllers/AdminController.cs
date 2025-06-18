using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using HareKrishnaNamaHattaWebApp.Models;
using System;
using HareKrishnaNamaHattaWebApp.Data;
using Microsoft.EntityFrameworkCore;
using HareKrishnaNamaHattaWebApp.Controllers;
using System.Drawing.Printing;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;

[AdminAuthorize]
public class AdminController : BaseController
{
    private readonly IConfiguration _config;
    private readonly ILogger<AdminController> _logger;
    private readonly AppDbContext _context;
    private readonly SmtpSettings _smtpSettings;

    public AdminController(IConfiguration config, ILogger<AdminController> logger, AppDbContext context, IOptions<SmtpSettings> smtpOptions):base(logger)
    {
        _config = config;
        _logger = logger;
        _context = context;
        _smtpSettings = smtpOptions.Value;
        ViewData["Layout"] = "_AdminLayout";
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View(new AdminCredentials());
    }

    [HttpPost]
    public IActionResult Login(AdminCredentials model)
    {
        try
        {
            var storedUsername = _config["AdminCredentials:Username"];
            var storedPasswordHash = _config["AdminCredentials:PasswordHash"];

            if (model.Username == storedUsername &&
                BCrypt.Net.BCrypt.Verify(model.PasswordHash, storedPasswordHash))
            {
                // Set session to simulate login success
                HttpContext.Session.SetString("AdminLoggedIn", "true");
                _logger.LogInformation("Admin successfully logged in at {time}", DateTime.UtcNow);
                return RedirectToAction("Dashboard");
            }

            model.ErrorMessage = "Invalid username or password.";
            _logger.LogWarning("Failed admin login attempt for user: {username} at {time}", model.Username, DateTime.UtcNow);
            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred during admin login.");
            model.ErrorMessage = "Something went wrong. Please try again.";
            return View(model);
        }
    }

    public IActionResult Dashboard()
    {
        if (!IsAdminLoggedIn()) return RedirectToAction("Login");
        return View();
    }
   
    public async Task<IActionResult> ContactMessages(int page = 1, int pageSize = 10)
    {
        var totalMessages= _context.ContactMessages.Count();
        var messages = await _context.ContactMessages
            .OrderByDescending(d => d.DateSent)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalMessages / pageSize);

        return View(messages);
    }

    [HttpPost]
    public async Task<IActionResult> ReplyToMessage(int id, string replyMessage)
    {
        var message = await _context.ContactMessages.FindAsync(id);
        if (message == null) return NotFound();

        // Send the receipt email
        var emailBody = $@"
        <h2 style='color: green;'>Hare Krishna!</h2>
        <p>Dear {message.Name},</p>
        <p>Thank you for reaching out to us.</p>
        <p>{replyMessage}</p>       
        <br />
        <p>With heartfelt gratitude,</p>
        <p><strong>Hare Krishna Nama Hatta Temple Team</strong></p>";

        var subject = "Hare Krishna Nama Hatta Temple - Query Response:";

        try
        {
            await HareKrishnaNamaHattaWebApp.Utility.EmailHelper.SendEmailAsync(message.Email, subject, emailBody, _smtpSettings.Username, _smtpSettings.Password, _smtpSettings.Host, _smtpSettings.Port, _smtpSettings.FromEmail);
            TempData["SuccessMessage"] = $"Reply sent to {message.Email}.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending reply for ID {DevoteeId}", message.Id);
            TempData["ErrorMessage"] = "Failed to send reply. Please try again.";
        }

        // Save reply in DB
        message.AdminReply = replyMessage;
        _context.Update(message);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Reply sent successfully!";
        return RedirectToAction("ContactMessages");
    }


    public async Task<IActionResult> Donations(int page = 1, int pageSize = 10)
    {
        var totalDonations = _context.Donations.Count();
        var donations = await _context.Donations
            .OrderByDescending(d => d.DonationDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalDonations / pageSize);

        return View(donations);
    }

    public async Task<IActionResult> Events()
    {
        if (!IsAdminLoggedIn()) return RedirectToAction("Login");

        var events = await _context.Events.OrderByDescending(e => e.Date).ToListAsync();
        return View(events);
    }

    [HttpGet]
    public async Task<IActionResult> SendReceipt(int id)
    {
        var donation = await _context.Donations.FirstOrDefaultAsync(d => d.Id == id);
        if (donation == null)
            return NotFound();

        // Send the receipt email
        var emailBody = $@"
        <h2 style='color: green;'>Hare Krishna!</h2>
        <p>Dear {donation.DonorName},</p>
        <p>Thank you for your generous donation of <strong>₹{donation.Amount:N2}</strong> to the Hare Krishna Nama Hatta Temple.</p>
        <p>Your support helps us continue our services including prasadam distribution, spiritual teachings, and festivals.</p>
        <p><strong>Donation Details:</strong></p>
        <ul>
            <li><strong>Receipt ID:</strong> HKNH-{donation.Id.ToString("D6")}</li>
            <li><strong>Amount:</strong> ₹{donation.Amount:N2}</li>
            <li><strong>Message:</strong> {donation.Message}</li>
            <li><strong>Date:</strong> {donation.DonationDate:dd MMM yyyy}</li>            
        </ul>
        <br />
        <p>With heartfelt gratitude,</p>
        <p><strong>Hare Krishna Nama Hatta Temple Team</strong></p>
        <p><small>This is an auto-generated email receipt.</small></p>
    ";

        var subject = "Thank you for your donation - Hare Krishna Nama Hatta Temple";        

        try
        {
            await HareKrishnaNamaHattaWebApp.Utility.EmailHelper.SendEmailAsync(donation.Email, subject, emailBody, _smtpSettings.Username, _smtpSettings.Password, _smtpSettings.Host, _smtpSettings.Port, _smtpSettings.FromEmail);
            TempData["SuccessMessage"] = $"Receipt sent to {donation.Email}.";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending donation receipt for ID {DonationId}", donation.Id);
            TempData["ErrorMessage"] = "Failed to send receipt. Please try again.";
        }

        return RedirectToAction("Donations");
    }

    public async Task<IActionResult> VolunteerDevotees(int page = 1, int pageSize = 10)
    {
        if (!IsAdminLoggedIn()) return RedirectToAction("Login");

        var volunteers = await _context.VolunteerDevotees
            .OrderByDescending(v => v.PreferredDate)
            .ToListAsync();
        var totalVolunteers = volunteers.Count();
        ViewBag.CurrentPage = page;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalPages = (int)Math.Ceiling((double)totalVolunteers / pageSize);

        return View(volunteers);
    }


    public IActionResult Logout()
    {
        HttpContext.Session.Remove("AdminLoggedIn");
        _logger.LogInformation("Admin logged out at {time}", DateTime.UtcNow);
        return RedirectToAction("Login");
    }

    private bool IsAdminLoggedIn()
    {
        return HttpContext.Session.GetString("AdminLoggedIn") == "true";
    }
}
