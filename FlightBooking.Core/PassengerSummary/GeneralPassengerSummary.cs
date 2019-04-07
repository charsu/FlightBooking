using System;
using System.Collections.Generic;
using System.Text;
using FlightBooking.Core.Models;

namespace FlightBooking.Core.PassengerSummary {
   public class GeneralPassengerSummary : IPassengerSummary {
      public FlightSummary Process(ScheduledFlight scheduledFlight, Passenger passenger) {
         var output = new FlightSummary();

         if (passenger.Type == PassengerType.General) {
            output.ProfitFromFlight = scheduledFlight.FlightRoute.BasePrice;
            output.TotalExpectedBaggage = 1;
         }

         return output;
      }
   }
}
