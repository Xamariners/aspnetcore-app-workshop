using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ConferenceDTO
{
    public class Speaker : ObjectBase
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(10000)]
        public string Bio { get; set; }

        [StringLength(200)]
        public string TagLine { get; set; }

        public int Order { get; set; }

        [StringLength(1000)]
        public virtual string WebSite { get; set; }

        [StringLength(100)]
        public virtual string Twitter { get; set; }

        [StringLength(100)]
        public virtual string LinkedIn { get; set; }

        public virtual string Picture { get; set; }

    }
}
