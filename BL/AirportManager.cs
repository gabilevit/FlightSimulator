using Dal.Repositories;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL
{
    public class AirportManager : IAirportManager
    {
        public Flight CurrentFlight { get; set; }
        public IEnumerable<Leg> Legs { get; set; }
        public List<FlightOnRoute> FlightsOnRoutes { get; set; }
        //public Leg[] Legs { get; set; }
        //public List< MyProperty { get; set; }
        public Queue<Flight> ArrivalFlightsQueue { get; set; }
        public Queue<Flight> DepartureFlightsQueue { get; set; }
        //public event Action<int> IslegAvalableCheker;
        //private System.Timers.Timer _timer = new System.Timers.Timer(5000);
        private IRepository _repository;

        public AirportManager(IRepository repository)
        {
            ArrivalFlightsQueue = new Queue<Flight>();
            DepartureFlightsQueue = new Queue<Flight>();
            _repository = repository;
        }

        public void GetNewFlight(FlightHistory flight)
        {
            //_timer.Start();
            if (CurrentFlight.IsArrival)
            {
                ArrivalFlightsQueue.Enqueue(flight);
                //SwitchcaseForLeg(1);
                //TimerListener(1);              
                EnterLeg(1);

            }
            else if (CurrentFlight.IsDeparture) 
            {
                DepartureFlightsQueue.Enqueue(flight);
                TerminalLeg(6);
            }
           
        }

        public void StoryRoute()
        {
            DeleteFlightOnRoute();
            ExitLeg(9);
            TerminalLeg(6);
            //TerminalLeg(7);
            RunWayLeg(4);
            DeparturesLeg(8);
            ArrivalsLeg(5);
            LandingLeg(3);
            LandingLeg(2);
            EnterLeg(1);
        }
        //private void TimerListener(int i)
        //{
        //    _timer.Elapsed += (s, e) => IslegAvalableCheker.Invoke(i);

        //}

        private void EnterLeg(int currentLeg)
        {
            if (LegGayver(currentLeg).IsLegAvelable && ArrivalFlightsQueue.Count != 0)
            {
                //IslegAvalableCheker -= EnterLeg;
                CreateFlightOnRoute(ArrivalFlightsQueue.Dequeue(), currentLeg);
                //TimerListener(2);
                //SwitchcaseForLeg(2);
                //the UI doing a sleep to show wait between legs
            }
            //else SwitchcaseForLeg(currentLeg);
        }

        private void LandingLeg(int currentLeg)
        {
            if (LegGayver(currentLeg).IsLegAvelable && !LegGayver(currentLeg - 1).IsLegAvelable)
            {
                //IslegAvalableCheker -= LandingLeg;
                TranferFlightToNextLeg(currentLeg - 1, currentLeg, GetPlaneOnRoute(currentLeg));
                //the UI doing a sleep to show wait between legs
            }
            //else SwitchcaseForLeg(currentLeg);
        }

        private void RunWayLeg(int currentLeg)
        {
            if (LegGayver(currentLeg).IsLegAvelable)
            {
                if (!LegGayver(3).IsLegAvelable && (LegGayver(8).IsLegAvelable || GetPlaneOnRoute(3).Date < GetPlaneOnRoute(8).Date))
                {
                    TranferFlightToNextLeg(3, 4, GetPlaneOnRoute(3));
                    //IslegAvalableCheker -= RunWayLeg;
                }
                else if (!LegGayver(8).IsLegAvelable)
                {
                    TranferFlightToNextLeg(8, 4, GetPlaneOnRoute(8));
                    //IslegAvalableCheker -= RunWayLeg;
                }
            }
            //else SwitchcaseForLeg(currentLeg);
        }

        private void ArrivalsLeg(int currentLeg)
        {
            if (LegGayver(currentLeg).IsLegAvelable)
            {
                if (!LegGayver(4).IsLegAvelable && GetPlaneOnRoute(4).IsArrival)
                {
                    TranferFlightToNextLeg(4, 5, GetPlaneOnRoute(4));
                    //IslegAvalableCheker -= ArrivalsLeg;
                }
            }
            //else SwitchcaseForLeg(currentLeg);
        }

        private void TerminalLeg(int currentLeg)
        {
            if (LegGayver(currentLeg).IsLegAvelable)
            {
                if (LegGayver(5).IsLegAvelable)
                {
                    CreateFlightOnRoute(DepartureFlightsQueue.Dequeue(), currentLeg);
                    //IslegAvalableCheker -= TerminalLeg;
                }
                else if (GetPlaneOnRoute(5).Date < DepartureFlightsQueue.Peek().Date)
                {
                    TranferFlightToNextLeg(5, currentLeg, GetPlaneOnRoute(5));
                    DeleteFlightOnRoute();
                }
            }
            else if(LegGayver(currentLeg + 1).IsLegAvelable)
            {
                if (LegGayver(5).IsLegAvelable)
                {
                    CreateFlightOnRoute(DepartureFlightsQueue.Dequeue() ,currentLeg + 1);
                    //IslegAvalableCheker -= TerminalLeg;
                }
                else if (GetPlaneOnRoute(5).Date < DepartureFlightsQueue.Peek().Date)
                {
                    TranferFlightToNextLeg(5, currentLeg + 1, GetPlaneOnRoute(5));
                    DeleteFlightOnRoute();
                }
            }
            //else SwitchcaseForLeg(currentLeg);
        }

        private void DeparturesLeg(int currentLeg)
        {
            if (LegGayver(currentLeg).IsLegAvelable)
            {
                if (!LegGayver(6).IsLegAvelable)
                {
                    if (!LegGayver(7).IsLegAvelable)
                    {
                        if (GetPlaneOnRoute(6).Date < GetPlaneOnRoute(7).Date) TranferFlightToNextLeg(6, 8, GetPlaneOnRoute(6));
                        else TranferFlightToNextLeg(7, 8, GetPlaneOnRoute(7));
                    }
                    TranferFlightToNextLeg(6, 8, GetPlaneOnRoute(6));
                }
                else if(!LegGayver(7).IsLegAvelable) TranferFlightToNextLeg(7, 8, GetPlaneOnRoute(7));
            }
        }

        private void ExitLeg(int currentLeg)
        {
            if (LegGayver(currentLeg).IsLegAvelable)
            {
                if (!LegGayver(4).IsLegAvelable)
                {
                    TranferFlightToNextLeg(4, 9, GetPlaneOnRoute(4));
                    DeleteFlightOnRoute();
                }
            }
        }

        private void DeleteFlightOnRoute()
        {
            if (!GetPlaneOnRoute(9).Leg.IsLegAvelable)
            {
                _repository.DeleteFlightOnRoute(GetPlaneOnRoute(9).Id);
                FlightsOnRoutes.Remove(GetPlaneOnRoute(9));
            }
            else if(!GetPlaneOnRoute(6).Leg.IsLegAvelable && GetPlaneOnRoute(6).IsArrival)
            {
                _repository.DeleteFlightOnRoute(GetPlaneOnRoute(6).Id);
                FlightsOnRoutes.Remove(GetPlaneOnRoute(6));
            }
            else if (!GetPlaneOnRoute(7).Leg.IsLegAvelable && GetPlaneOnRoute(7).IsArrival)
            {
                _repository.DeleteFlightOnRoute(GetPlaneOnRoute(7).Id);
                FlightsOnRoutes.Remove(GetPlaneOnRoute(7));
            }
        }

        public void CreateFlightOnRoute(Flight flight, int num)
        {
            FlightOnRoute flightOnRoute = new FlightOnRoute
            {
                FlightNum = flight.FlightNum,
                Contry = flight.Contry,
                IsArrival = flight.IsArrival,
                IsDeparture = flight.IsDeparture,
                Date = flight.Date,
                Leg = new Leg
                {
                    IsLegAvelable = false,
                    Type = (LegType)num
                }
            };
            _repository.InsertFlightOnRoute(flightOnRoute);
            FlightsOnRoutes.Add(flightOnRoute);
        }

        //public void CreateDeparturingFlightOnRoute(int num)
        //{
        //    FlightOnRoute flightOnRoute = new FlightOnRoute
        //    {
        //        FlightNum = DepartureFlightsQueue.Peek().FlightNum,
        //        Contry = DepartureFlightsQueue.Peek().Contry,
        //        IsArrival = DepartureFlightsQueue.Peek().IsArrival,
        //        IsDeparture = DepartureFlightsQueue.Peek().IsDeparture,
        //        Date = DepartureFlightsQueue.Peek().Date,
        //        Leg = new Leg
        //        {
        //            IsLegAvelable = false,
        //            Type = (LegType)num
        //        }
        //    };
        //    _repository.InsertFlightOnRoute(flightOnRoute);
        //    FlightsOnRoutes.Add(flightOnRoute);
        //}

        public FlightOnRoute GetPlaneOnRoute(int num)
        {           
            return FlightsOnRoutes.Where(f => f.Leg.Id.Equals(num)).FirstOrDefault();    
        }

        public void TranferFlightToNextLeg(int currentLeg, int nextLeg, FlightOnRoute flightOnRoute)
        {
            Legs.First(L => L.Id.Equals(nextLeg)).IsLegAvelable = false;
            flightOnRoute.Leg = Legs.First(L => L.Id.Equals(nextLeg));
            Legs.First(L => L.Id.Equals(currentLeg)).IsLegAvelable = true;
        }

        public Leg LegGayver(int legNum)
        {
            return Legs.First(L => L.Id.Equals(legNum));
        }

        //private void SwitchcaseForLeg(int i)
        //{
        //    switch (i)
        //    {
        //        case 1:
        //            IslegAvalableCheker += EnterLeg;
        //            break;
        //        case 2:
        //            IslegAvalableCheker += LandingLeg;
        //            break;
        //        case 3:
        //            IslegAvalableCheker += LandingLeg;
        //            break;
        //        case 4:
        //            IslegAvalableCheker += RunWayLeg;
        //            break;
        //        case 5:
        //            IslegAvalableCheker += ArrivalsLeg;
        //            break;
        //        case 6:
        //            IslegAvalableCheker += TerminalLeg;
        //            break;
        //        case 7:
        //            IslegAvalableCheker += TerminalLeg;
        //            break;
        //        case 8:
        //            IslegAvalableCheker += DeparturesLeg;
        //            break;
        //        case 9:
        //            IslegAvalableCheker += ExitLeg;
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }
}
