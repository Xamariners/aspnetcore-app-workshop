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
    public class ConferenceModel : PageModel
    {
        // https://www.bing.com/api/maps/sdkrelease/mapcontrol/isdk/displayinfoboxonclickpushpin

        protected readonly IApiClient _apiClient;

        public ConferenceModel(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public IEnumerable<IGrouping<DateTimeOffset?, SessionResponse>> Sessions { get; set; }

        public IEnumerable<(int Offset, DayOfWeek? DayofWeek, DateTime? Date)> DayOffsets { get; set; }

        public List<SpeakerResponse> Speakers { get; set; }

        public List<Guid> UserSessions { get; set; }

        public GlobalConference GlobalConference { get; set; }

        public ConferenceResponse Conference { get; set; }

        public List<Sponsor> Sponsors { get; set; }

        public int CurrentDayOffset { get; set; }

        [TempData]
        public string Message { get; set; }

        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        public int SessionCount { get; set; }

        protected virtual Task<List<SessionResponse>> GetSessionsAsync()
        {
            return _apiClient.GetSessionsAsync();
        }
        
        public async Task OnGetAsync(string id, int day = 0)
        {
            CurrentDayOffset = day;
            
            GlobalConference = await _apiClient.GetGlobalConferenceAsync();

            Conference = await _apiClient.GetConferenceBySlugAsync(id);

            var userSessions = await _apiClient.GetSessionsByAttendeeAsync(User.Identity.Name);

            UserSessions = userSessions.Select(u => u.ID).ToList();

            var sessions = await GetSessionsAsync();
            
            var startDate = sessions.Min(s => s.StartTime?.Date);
            var endDate = sessions.Max(s => s.EndTime?.Date);

            var numberOfDays = ((endDate - startDate)?.Days) + 1;

            DayOffsets = Enumerable.Range(0, numberOfDays ?? 0)
                .Select(offset => (offset, (startDate?.AddDays(offset))?.DayOfWeek, startDate?.AddDays(offset)));

            var filterDate = startDate?.AddDays(day);

            var speakers = await _apiClient.GetConferenceSpeakersAsync(Conference.ID);

            foreach (var session in sessions)
            {
                foreach (var speaker in session.Speakers)
                    speaker.Picture = speakers.FirstOrDefault(x => x.ID == speaker.ID)?.Picture;
            }

            Speakers = speakers.Where(x => x.Order != -1).ToList();

            Sessions = sessions.Where(s => s.StartTime?.Date == filterDate)
                               .OrderBy(s => s.TrackId)
                               .GroupBy(s => s.StartTime)
                               .OrderBy(g => g.Key);

            SessionCount = sessions.Count;

            Sponsors = await _apiClient.GetConferenceSponsorsAsync(Conference.ID);

            ViewData["Title"] = Conference.Name;
        }

        public async Task<IActionResult> OnPostAsync(Guid sessionId)
        {
            await _apiClient.AddSessionToAttendeeAsync(User.Identity.Name, sessionId);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveAsync(Guid sessionId)
        {
            await _apiClient.RemoveSessionFromAttendeeAsync(User.Identity.Name, sessionId);

            return RedirectToPage();
        }
    }
}
