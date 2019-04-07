using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Core {
   public static class TestData {

      /// <summary>
      /// setup test data 
      /// </summary>
      /// <returns></returns>
      public static ScheduledFlight SetupAirlineData() {
         var londonToParis = new FlightRoute("London", "Paris") {
            BaseCost = 50,
            BasePrice = 100,
            LoyaltyPointsGained = 5,
            MinimumTakeOffPercentage = 0.7
         };

         var _scheduledFlight = new ScheduledFlight(londonToParis);

         _scheduledFlight.SetAircraftForRoute(new Plane { Id = 123, Name = "Antonov AN-2", NumberOfSeats = 12 });

         return _scheduledFlight;
      }
   }
}
