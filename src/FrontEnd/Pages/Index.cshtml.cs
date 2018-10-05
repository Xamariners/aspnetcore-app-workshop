using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceDTO;
using FrontEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace FrontEnd.Pages
{
    public class IndexModel : PageModel
    {
        // https://www.bing.com/api/maps/sdkrelease/mapcontrol/isdk/displayinfoboxonclickpushpin

        protected readonly IApiClient _apiClient;

        public IndexModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
        
        public GlobalConference GlobalConference { get; set; }

        public List<Conference> Conferences { get; set; }

        public List<Sponsor> Sponsors { get; set; }

        public int CurrentDayOffset { get; set; }

        [TempData]
        public string Message { get; set; }

        public bool ShowMessage => !string.IsNullOrEmpty(Message);
        
        public async Task OnGetAsync(int day = 0)
        {
            CurrentDayOffset = day;

            GlobalConference = await _apiClient.GetGlobalConferenceAsync();
            Conferences = await _apiClient.GetConferencesAsync();
            Sponsors = await _apiClient.GetConferenceSponsorsAsync(GlobalConference.ID);

            ViewData["Title"] = GlobalConference.Name;
        }
    }
}
