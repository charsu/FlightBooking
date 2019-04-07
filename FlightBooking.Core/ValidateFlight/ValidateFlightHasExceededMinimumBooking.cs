using System;
using System.Collections.Generic;
using System.Text;
using FlightBooking.Core.Models;

namespace FlightBooking.Core.ValidateFlight {
   public class ValidateFlightHasExceededMinimumBooking : IFlightValidator {
      public (bool isValid, List<string> messages) Validate(FlightSummary flightSummary, ScheduledFlight scheduledFlight)
         => (flightSummary.SeatsTaken / (double)scheduledFlight.Aircraft.NumberOfSeats > scheduledFlight.FlightRoute.MinimumTakeOffPercentage, null);
   }
}
