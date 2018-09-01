using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ConferenceDTO
{
    public class ConferenceOrganiser : Speaker
    {
        [Required]
        [StringLength(200)]
        public string Email { get; set; }
    }
}
