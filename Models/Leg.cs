using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Leg
    {
        public int Id { get; set; }

        public LegType Type { get; set; }

        public bool IsLegAvelable { get; set; }
    }

    public enum LegType
    {
        Enter = 1,
        PreLanding = 2,
        Landing = 3,
        Runaway = 4,
        Arrivals = 5,
        Terminal1 = 6,
        Terminal2 = 7,
        Departures = 8,
        Exit = 9
    }
}
