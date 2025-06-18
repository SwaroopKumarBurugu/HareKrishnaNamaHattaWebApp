using HareKrishnaNamaHattaWebApp.Data;
using HareKrishnaNamaHattaWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HareKrishnaNamaHattaWebApp.Services
{
    public class ManageEventService
    {
        private readonly AppDbContext _context;

        public ManageEventService(AppDbContext context)
        {
            _context = context;
        }

        // Get All Events
        public async Task<List<Event>> GetAllEventsAsync()
        {
            return await _context.Events.OrderBy(e => e.Date).ToListAsync();
        }

        // Get Event by ID
        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
        }

        // Create Event
        public async Task<bool> CreateEventAsync(Event evt)
        {
            _context.Events.Add(evt);
            await _context.SaveChangesAsync();
            return true;
        }

        // Update Event
        public async Task<bool> UpdateEventAsync(Event evt)
        {
            _context.Events.Update(evt);
            await _context.SaveChangesAsync();
            return true;
        }

        // Delete Event
        public async Task<bool> DeleteEventAsync(int id)
        {
            var evt = await _context.Events.FindAsync(id);
            if (evt == null) return false;

            _context.Events.Remove(evt);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ Get Special Events (e.g., marked or manually categorized)
        public async Task<List<Event>> GetSpecialEventsAsync()
        {
            return await _context.Events
                .Where(e => e.EventType=="Special")
                .OrderByDescending(e => e.Date)
                .ToListAsync();
        }

        // ✅ Get Everyday/Recurring Events (e.g., marked as recurring)
        public async Task<List<Event>> GetEverydayEventsAsync()
        {
            return await _context.Events
                .Where(e => e.EventType == "Everyday")
                .OrderBy(e => e.Date)
                .ToListAsync();
        }

        // Optional: Get Events By Date Range
        public async Task<List<Event>> GetEventsByDateRangeAsync(DateTime from, DateTime to)
        {
            return await _context.Events
                .Where(e => e.Date >= from && e.Date <= to)
                .OrderBy(e => e.Date)
                .ToListAsync();
        }
    }
}
