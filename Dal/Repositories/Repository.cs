using Dal.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dal.Repositories
{
    public class Repository : IRepository
    {
        private DataContext _data;

        public Repository(DataContext data)
        {
            _data = data;
        }

        public void InsertFlight(FlightHistory flight)
        {
            _data.FlightsHistory.Add(flight);
            _data.SaveChanges();
        }

        public IEnumerable<FlightHistory> GetFlights()
        {
            return _data.FlightsHistory.ToList().AsEnumerable();
        }

        public FlightHistory GetFlight(int id)
        {
            var flightInDb = _data.FlightsHistory.SingleOrDefault(f => f.Id == id);
            return flightInDb;
        }

        public void UpdateFlight(int id, FlightHistory flight)
        {
            var tmp = _data.FlightsHistory.First(f => f.Id == id);
            if (flight.FlightNum != null) tmp.FlightNum = flight.FlightNum;
            if (flight.Contry != null) tmp.Contry = flight.Contry;
            if (flight.Date != null) tmp.Date = flight.Date;
            if (flight.IsArrival) tmp.IsArrival = flight.IsArrival;
            else tmp.IsArrival = flight.IsArrival;
            if (flight.IsDeparture) tmp.IsDeparture = flight.IsDeparture;
            else tmp.IsDeparture = flight.IsDeparture;
            _data.FlightsHistory.Update(tmp);
            _data.SaveChanges();
        }

        public void DeleteFlight(int id)
        {
            var flightInDb = _data.FlightsHistory.SingleOrDefault(f => f.Id == id);
            _data.FlightsHistory.Remove(flightInDb);
            _data.SaveChanges();
        }

        public void InsertLeg(Leg leg)
        {
            _data.Legs.Add(leg);
            _data.SaveChanges();
        }

        public IEnumerable<Leg> GetLegs()
        {
            return _data.Legs.ToList().AsEnumerable();
        }

        public Leg GetLeg(int id)
        {
            var legInDb = _data.Legs.SingleOrDefault(l => l.Id == id);
            return legInDb;
        }

        public void UpdateLeg(int id, Leg leg)
        {
            var tmp = _data.Legs.First(l => l.Id == id);
            tmp.Type = leg.Type;
            if (leg.IsLegAvelable) tmp.IsLegAvelable = leg.IsLegAvelable;
            else tmp.IsLegAvelable = leg.IsLegAvelable;
            _data.Legs.Update(tmp);
            _data.SaveChanges();
        }

        public void DeleteLeg(int id)
        {
            var legInDb = _data.Legs.SingleOrDefault(l => l.Id == id);
            _data.Legs.Remove(legInDb);
            _data.SaveChanges();
        }

        public void InsertFlightOnRoute(FlightOnRoute flightOnRoute)
        {
            _data.FlightsOnRoute.Add(flightOnRoute);
            _data.SaveChanges();
        }

        public IEnumerable<FlightOnRoute> GetFlightsOnRoute()
        {
            return _data.FlightsOnRoute.ToList().AsEnumerable();
        }

        public FlightOnRoute GetFlightOnRouteViaLeg(int legNum)
        {
            var flightOnRouteInDb = _data.FlightsOnRoute.SingleOrDefault(f => (int)f.Leg.Type == legNum);
            return flightOnRouteInDb;
        }

        public void UpdateFlightOnRoute(int id, FlightOnRoute flightOnRoute)
        {
            var tmp = _data.FlightsOnRoute.First(f => f.Id == id);
            if (flightOnRoute.FlightNum != null) tmp.FlightNum = flightOnRoute.FlightNum;
            if (flightOnRoute.Contry != null) tmp.Contry = flightOnRoute.Contry;
            if (flightOnRoute.Date != null) tmp.Date = flightOnRoute.Date;
            if (flightOnRoute.IsArrival) tmp.IsArrival = flightOnRoute.IsArrival;
            else tmp.IsArrival = flightOnRoute.IsArrival;
            if (flightOnRoute.IsDeparture) tmp.IsDeparture = flightOnRoute.IsDeparture;
            else tmp.IsDeparture = flightOnRoute.IsDeparture;
            if (flightOnRoute.Leg != null) tmp.Leg = flightOnRoute.Leg;
            _data.FlightsOnRoute.Update(tmp);
            _data.SaveChanges();
        }

        public void DeleteFlightOnRoute(int id)
        {
            var flightOnRouteInDb = _data.FlightsOnRoute.SingleOrDefault(f => f.Id == id);
            _data.FlightsOnRoute.Remove(flightOnRouteInDb);
            _data.SaveChanges();
        }
    }
}
