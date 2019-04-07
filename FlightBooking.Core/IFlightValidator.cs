using System;
using System.Collections.Generic;
using System.Text;
using FlightBooking.Core.Models;

namespace FlightBooking.Core {
   public interface IFlightValidator {
      (bool isValid, List<string> messages) Validate(FlightSummary flightSummary, ScheduledFlight scheduledFlight);
   }
}
