using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConferenceDTO
{
    public class Sponsor : ObjectBase
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        
        [Required]
        public string Picture { get; set; }

        [Required]
        public Guid ParentID { get; set; }
    }
}
