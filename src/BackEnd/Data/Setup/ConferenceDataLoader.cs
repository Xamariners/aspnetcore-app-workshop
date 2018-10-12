using BackEnd.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ConferenceDTO;

namespace BackEnd.Data.Setup
{
    public class ConferenceDataLoader : BaseDataLoader
    {
        public List<Conference> Conferences { get; private set; }
        public GlobalConference GlobalConference { get; private set; }

        public ConferenceDataLoader(IServiceProvider services) : base(services)
        {
            // this.SaveData = false;
        }

        protected override void LoadFormattedData(ApplicationDbContext db)
        {
            // GLOBAL CONFERENCE
            var gcFile = File.ReadAllText($"{DATA_DIR}/Global/{GLOBALCONFERENCE_FILE}");
            GlobalConference = JsonConvert.DeserializeObject<GlobalConference>(gcFile);
            db.GlobalConferences.Add(GlobalConference);

            // GLOBAL SPONSORS
            var globalSponsors = new List<Sponsor>();
            var globalSponsorFiles = Directory.GetFiles($"{DATA_DIR}/Global/Images/Sponsors");
            
            foreach (var globalSponsorFile in globalSponsorFiles)
                globalSponsors.Add(new Sponsor{ParentID =  GlobalConference.ID, Name = globalSponsorFile.Split('\\').LastOrDefault()?.Split('.').FirstOrDefault(), Picture = GetBase64StringForImage(globalSponsorFile)});

            db.Sponsors.AddRange(globalSponsors);
            
            // CONFERENCES
            var dirs = Directory.GetDirectories("Data/Json/Conferences", "*", SearchOption.TopDirectoryOnly);
            foreach (var dir in dirs)
            {
                // CONFERENCE
                var conferenceJson = File.ReadAllText($"{dir}/{CONFERENCE_FILE}");
                var conference = JsonConvert.DeserializeObject<Conference>(conferenceJson);
                db.Conferences.Add(conference);

                // CONFERENCE SPONSORS
                var sponsors = new List<Sponsor>();
                var sponsorFiles = Directory.GetFiles($"{dir}/Images/Sponsors");
                
                foreach (var sponsorFile in sponsorFiles)
                    sponsors.Add(new Sponsor{ParentID = conference.ID, Name = sponsorFile.Split('\\').LastOrDefault()?.Split('.').FirstOrDefault(), Picture = GetBase64StringForImage(sponsorFile)});
                
                db.Sponsors.AddRange(sponsors);

                // SESSIONS
                var sessionsFile = File.OpenText($"{dir}/{SESSIONS_FILE}");
                var seReader = new JsonTextReader(sessionsFile);

                // SPEAKERS
                var speakersJson = File.ReadAllText($"{dir}/{SPEAKERS_FILE}");
                var speakers = JsonConvert.DeserializeObject<List<Speaker>>(speakersJson);

                foreach (var speaker in speakers)
                    speaker.Picture = GetBase64StringForImage($"{dir}/Images/Speakers/{speaker.ID}.jpg");

                db.Speakers.AddRange(speakers);

                // ORGANISERS
                var organisersJson = File.ReadAllText($"{dir}/{ORGANISERS_FILE}");
                var organisers = JsonConvert.DeserializeObject<List<ConferenceOrganiser>>(organisersJson);

                conference.ConferenceOrganisers = new List<ConferenceOrganiser>();
                foreach (var organiser in organisers)
                {
                    organiser.Picture = GetBase64StringForImage($"{dir}/Images/Organisers/{organiser.ID}.jpg");
                    organiser.ConferenceID = conference.ID;
                    conference.ConferenceOrganisers.Add(organiser);
                }

                // TRACKS
                var tracks = new Dictionary<string, Track>();

                JArray doc = JArray.Load(seReader);

                foreach (JObject item in doc)
                {
                    var theseTracks = new List<Track>();
                    foreach (var trackName in item["trackNames"])
                    {
                        if (!tracks.ContainsKey(trackName.Value<string>()))
                        {
                            var thisTrack = new Track { Name = trackName.Value<string>(), Conference = conference };
                            db.Tracks.Add(thisTrack);
                            tracks.Add(trackName.Value<string>(), thisTrack);
                        }
                        theseTracks.Add(tracks[trackName.Value<string>()]);
                    }

                    var session = new Session
                    {
                        Conference = conference,
                        ConferenceID = conference.ID,
                        Title = item["title"].Value<string>(),
                        StartTime = item["startTime"].Value<DateTime>(),
                        EndTime = item["endTime"].Value<DateTime>(),
                        Track = theseTracks[0],
                        Abstract = item["abstract"].Value<string>()
                    };

                    session.SessionSpeakers = new List<SessionSpeaker>();
                    foreach (var speakerId in item["speakers"])
                    {
                        session.SessionSpeakers.Add(new SessionSpeaker
                        {
                            Session = session,
                            Speaker = speakers.FirstOrDefault(x => x.ID == Guid.Parse(speakerId.Value<string>()))
                        });
                    }

                    db.Sessions.Add(session);
                }
            }
        }

        protected static string GetBase64StringForImage(string path)
        {
            byte[] imageBytes = File.ReadAllBytes(path);
            return Convert.ToBase64String(imageBytes);
        }
    }

}

