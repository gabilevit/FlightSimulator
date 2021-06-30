using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Dal.Repositories;
using Microsoft.AspNetCore.Mvc;
using Models;
using Server.Hubs;

namespace Server.Controllers
{
    public class AirportController : ControllerBase
    {
        private IRepository _repository;
        private IAirportManager _logic;
        private IAirportHub _airportHub;

        public AirportController(IRepository repository, IAirportManager logic, IAirportHub airportHub)
        {
            _repository = repository;
            _logic = logic;
            _airportHub = airportHub;
        }

        [HttpPost]
        public void NewFlight([FromBody] FlightHistory flight)
        {
            _repository.InsertFlight(flight);
            _logic.GetNewFlight(_repository.GetFlight(flight.Id));
            _airportHub.SendFlightToClient(_repository.GetFlightsOnRoute());
        }

        [HttpGet]
        public int Stam(int e)
        {
            return e;
        }
    }
}
