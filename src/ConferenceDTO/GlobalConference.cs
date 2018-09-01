using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConferenceDTO
{
    public class GlobalConference : ObjectBase
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public string TagLine { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
