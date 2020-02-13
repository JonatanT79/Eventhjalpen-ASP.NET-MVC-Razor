using System;
using System.Collections.Generic;

namespace Eventhjälpen.Models
{
    public partial class Events
    {
        public Events()
        {
            EventDetails = new HashSet<EventDetails>();
        }

        public int Id { get; set; }
        public string EventName { get; set; }
        public string EventType { get; set; }

        public virtual ICollection<EventDetails> EventDetails { get; set; }
    }
}
