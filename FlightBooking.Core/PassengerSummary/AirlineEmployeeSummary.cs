using System;
using System.Collections.Generic;
using System.Text;
using FlightBooking.Core.Models;

namespace FlightBooking.Core.PassengerSummary {
   public class AirlineEmployeeSummary : IPassengerSummary {
      public FlightSummary Process(ScheduledFlight scheduledFlight, Passenger passenger) {
         var output = new FlightSummary();

         if (passenger.Type == PassengerType.AirlineEmployee) {
            output.TotalExpectedBaggage = 1;
         }

         return output;
      }
   }
}
