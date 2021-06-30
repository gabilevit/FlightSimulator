using Microsoft.AspNetCore.SignalR;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Hubs
{
    public class AirportHub : Hub, IAirportHub
    {
        protected IHubContext<AirportHub> _context;

        public AirportHub(IHubContext<AirportHub> context)
        {
            _context = context;
        }

        public async Task SendFlightToClient(IEnumerable<FlightOnRoute> flightsOnRoute)
        {
            await _context.Clients.All.SendAsync("SendFlight", flightsOnRoute);
        }

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("Conected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
