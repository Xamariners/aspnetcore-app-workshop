using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ConferenceDTO;

namespace BackEnd.Data
{
    public class GlobalConference : ConferenceDTO.GlobalConference
    {
        public virtual ICollection<Conference> Conferences { get; set; }

        public virtual ICollection<Sponsor> Sponsors { get; set; }
    }
}
