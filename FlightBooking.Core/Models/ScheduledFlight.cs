using System;
using System.Linq;
using System.Collections.Generic;

namespace FlightBooking.Core {
   public class ScheduledFlight {
      public ScheduledFlight(FlightRoute flightRoute) {
         FlightRoute = flightRoute;
         Passengers = new List<Passenger>();
      }

      public FlightRoute FlightRoute { get; }
      public Plane Aircraft { get; private set; }
      public List<Passenger> Passengers { get; }

      public void AddPassenger(Passenger passenger) {
         Passengers.Add(passenger);
      }

      public void SetAircraftForRoute(Plane aircraft) {
         Aircraft = aircraft;
      }
   }
}
