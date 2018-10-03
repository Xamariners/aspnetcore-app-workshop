using BackEnd.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            var gcFile = File.ReadAllText($"{DATA_DIR}/{GLOBALCONFERENCE_FILE}");
            GlobalConference = JsonConvert.DeserializeObject<GlobalConference>(gcFile);
            db.GlobalConferences.Add(GlobalConference);
            
            var dirs = Directory.EnumerateDirectories("Data/Json");
            foreach (var dir in dirs)
            {
                var conferenceJson = File.ReadAllText($"{dir}/{CONFERENCE_FILE}");
                var conference = JsonConvert.DeserializeObject<Conference>(conferenceJson);
                db.Conferences.Add(conference);

                var sessionsFile = File.OpenText($"{dir}/{SESSIONS_FILE}");
                var seReader = new JsonTextReader(sessionsFile);

                var speakersJson = File.ReadAllText($"{dir}/{SPEAKERS_FILE}");
                var speakers = JsonConvert.DeserializeObject<List<Speaker>>(speakersJson);

                foreach (var speaker in speakers)
                    speaker.Picture = GetBase64StringForImage($"{dir}/images/speakers/{speaker.ID}.png");

                db.Speakers.AddRange(speakers);
                
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

