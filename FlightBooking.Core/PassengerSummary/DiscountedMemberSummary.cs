using System;
using System.Collections.Generic;
using System.Text;
using FlightBooking.Core.Models;

namespace FlightBooking.Core.PassengerSummary {
   public class DiscountedMemberSummary : IPassengerSummary {
      public FlightSummary Process(ScheduledFlight scheduledFlight, Passenger passenger) {
         var output = new FlightSummary();

         if (passenger.Type == PassengerType.Discounted) {
            output.ProfitFromFlight = scheduledFlight.FlightRoute.BasePrice / 2;
         }

         return output;
      }
   }
}
