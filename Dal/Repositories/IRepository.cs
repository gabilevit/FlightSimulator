using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dal.Repositories
{
    public interface IRepository
    {
        void InsertFlight(FlightHistory flight);

        IEnumerable<FlightHistory> GetFlights();

        FlightHistory GetFlight(int id);

        void UpdateFlight(int id, FlightHistory flight);

        void DeleteFlight(int id);

        void InsertLeg(Leg leg);

        IEnumerable<Leg> GetLegs();

        Leg GetLeg(int id);

        void UpdateLeg(int id, Leg leg);

        void DeleteLeg(int id);

        void InsertFlightOnRoute(FlightOnRoute flightOnRoute);

        IEnumerable<FlightOnRoute> GetFlightsOnRoute();

        FlightOnRoute GetFlightOnRouteViaLeg(int legNum);

        void UpdateFlightOnRoute(int id, FlightOnRoute flightOnRoute);

        void DeleteFlightOnRoute(int id);
    }
}
