using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConferenceDTO;

namespace FrontEnd.Services
{
    public interface IApiClient
    {
        Task<List<SessionResponse>> GetSessionsByAttendeeAsync(string name);
        Task<List<SessionResponse>> GetSessionsAsync();
        Task<SessionResponse> GetSessionAsync(Guid id);
        Task<List<SpeakerResponse>> GetSpeakersAsync();
        Task<SpeakerResponse> GetSpeakerAsync(Guid id);
        Task PutSessionAsync(Session session);
        Task<List<SearchResult>> SearchAsync(string query);
        Task AddAttendeeAsync(Attendee attendee);
        Task<AttendeeResponse> GetAttendeeAsync(string name);
        Task DeleteSessionAsync(Guid id);
        Task AddSessionToAttendeeAsync(string name, Guid sessionId);
        Task RemoveSessionFromAttendeeAsync(string name, Guid sessionId);
        Task<List<Conference>> GetConferencesAsync();
        Task<GlobalConference> GetGlobalConferenceAsync();
    }
}
