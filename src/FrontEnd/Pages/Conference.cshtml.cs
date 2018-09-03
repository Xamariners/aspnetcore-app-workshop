﻿using System;
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

        public List<Guid> UserSessions { get; set; }

        public GlobalConference GlobalConference { get; set; }

        public List<Conference> Conferences { get; set; }

        public int CurrentDayOffset { get; set; }

        [TempData]
        public string Message { get; set; }

        public bool ShowMessage => !string.IsNullOrEmpty(Message);

        public string EventScheduleTitle => "EVENT SCHEDULE";

        public string EventScheduleDescription => "EVENT DESCRIPTION...";

        protected virtual Task<List<SessionResponse>> GetSessionsAsync()
        {
            return _apiClient.GetSessionsAsync();
        }

        protected virtual Task<List<Conference>> GetConferencesAsync()
        {
            return _apiClient.GetConferencesAsync();
        }

        protected virtual Task<GlobalConference> GetConferenceAsync()
        {
            return _apiClient.GetGlobalConferenceAsync();
        }

        public async Task OnGetAsync(int day = 0)
        {
            CurrentDayOffset = day;

            GlobalConference = await _apiClient.GetGlobalConferenceAsync();
            Conferences = await _apiClient.GetConferencesAsync();

            var userSessions = await _apiClient.GetSessionsByAttendeeAsync(User.Identity.Name);

            UserSessions = userSessions.Select(u => u.ID).ToList();

            var sessions = await GetSessionsAsync();

            var startDate = sessions.Min(s => s.StartTime?.Date);
            var endDate = sessions.Max(s => s.EndTime?.Date);

            var numberOfDays = ((endDate - startDate)?.Days) + 1;

            DayOffsets = Enumerable.Range(0, numberOfDays ?? 0)
                .Select(offset => (offset, (startDate?.AddDays(offset))?.DayOfWeek, startDate?.AddDays(offset)));

            var filterDate = startDate?.AddDays(day);

            Sessions = sessions.Where(s => s.StartTime?.Date == filterDate)
                               .OrderBy(s => s.TrackId)
                               .GroupBy(s => s.StartTime)
                               .OrderBy(g => g.Key);
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