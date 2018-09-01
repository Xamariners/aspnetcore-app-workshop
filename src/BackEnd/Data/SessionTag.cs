using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Data
{
    public class SessionTag
    {
        public Guid SessionID { get; set; }

        public Session Session { get; set; }

        public Guid TagID { get; set; }

        public Tag Tag { get; set; }
    }
}
