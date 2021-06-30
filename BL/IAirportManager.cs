using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL
{
    public interface IAirportManager
    {
        void GetNewFlight(FlightHistory flight);
    }
}
