using Microsoft.AspNetCore.SignalR.Client;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace UI.Services
{
    public class SignalRAirportService
    {
        private HubConnection _connection;

        public event Action<FlightOnRoute> FlightReceived;

        public SignalRAirportService(HubConnection connection)
        {
            _connection = connection;

            _connection.StartAsync();

            _connection.On<FlightOnRoute>("SendFlight", (flights) => FlightReceived?.Invoke(flights));
        }

        //public async Task Connect()
        //{
        //    await _connection.StartAsync();
        //}
    }
}
