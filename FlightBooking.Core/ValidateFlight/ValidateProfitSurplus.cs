using System;
using System.Collections.Generic;
using System.Text;
using FlightBooking.Core.Models;

namespace FlightBooking.Core.ValidateFlight {
   public class ValidateProfitSurplus : IFlightValidator {
      public (bool isValid, List<string> messages) Validate(FlightSummary flightSummary, ScheduledFlight scheduledFlight)
         => (flightSummary.ProfitSurplus > 0, null);
   }
}
