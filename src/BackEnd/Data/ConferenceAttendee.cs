using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Data
{
    public class ConferenceAttendee
    {
        public Guid ConferenceID { get; set; }

        public Conference Conference { get; set; }

        public Guid AttendeeID { get; set; }

        public Attendee Attendee { get; set; }
    }
}
