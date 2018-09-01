﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConferenceDTO
{
    public class Speaker : ObjectBase
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(4000)]
        public string Bio { get; set; }

        [StringLength(30)]
        public string TagLine { get; set; }

        [StringLength(1000)]
        public virtual string WebSite { get; set; }

        [StringLength(1000)]
        public virtual string Picture { get; set; }

    }
}
