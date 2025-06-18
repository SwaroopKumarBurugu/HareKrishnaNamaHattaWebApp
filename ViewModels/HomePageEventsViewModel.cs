using HareKrishnaNamaHattaWebApp.Models;
using System.Collections.Generic;

namespace HareKrishnaNamaHattaWebApp.ViewModels
{
    public class HomePageEventsViewModel
    {
        public List<Event> SpecialEvents { get; set; }
        public List<Event> EverydayEvents { get; set; }
    }
}
