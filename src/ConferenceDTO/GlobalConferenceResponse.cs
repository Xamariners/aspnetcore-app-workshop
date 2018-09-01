using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceDTO
{
    public class GlobalConferenceResponse : GlobalConference
    {
        public ICollection<Conference> Conferences { get; set; } = new List<Conference>();
    }
}
