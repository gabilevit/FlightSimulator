using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class FlightOnRoute : Flight
    {
        public virtual Leg Leg { get; set; }
    }
}
