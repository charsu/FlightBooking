using System;
using System.Collections.Generic;
using System.Text;
using FlightBooking.Core.Models;

namespace FlightBooking.Core {
   public interface IPassengerSummary {
      FlightSummary Process(ScheduledFlight scheduledFlight, Passenger passenger);
   }
}
