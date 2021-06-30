using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Hubs
{
    public interface IAirportHub
    {
        Task SendFlightToClient(IEnumerable<FlightOnRoute> flightsOnRoute);
    }
}
