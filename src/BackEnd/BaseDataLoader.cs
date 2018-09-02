using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using BackEnd.Data;

namespace BackEnd
{
    public abstract class BaseDataLoader
    {
        private readonly IServiceProvider _Services;
        protected const string DATA_DIR = "Data/Json";
        protected const string GLOBALCONFERENCE_FILE = "globalConference.json";
        protected const string CONFERENCE_FILE = "conference.json";
        protected const string SESSIONS_FILE = "sessions.json";
        protected const string SPEAKERS_FILE = "speakers.json";

        protected BaseDataLoader(IServiceProvider services)
        {
            _Services = services;
        }

        protected bool SaveData { get; set; } = true;

        public void LoadData()
        {
     

            using (var scope = _Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetService<ApplicationDbContext>();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                LoadFormattedData(db);

                if (SaveData)
                {
                    db.SaveChanges();
                }
            }
        }

        protected abstract void LoadFormattedData(ApplicationDbContext db);
    }
}
