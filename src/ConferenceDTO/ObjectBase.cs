using System;
using System.Collections.Generic;
using System.Text;

namespace ConferenceDTO
{
    public abstract class ObjectBase
    {
        public Guid ID { get; set; }

        protected ObjectBase()
        {
            ID = Guid.NewGuid();
        }
    }
}
