using System;
using System.Collections.Generic;
using System.Text;
using FlightBooking.Core.PassengerSummary;

namespace FlightBooking.Core {
   public static class IoC {

      // raw dependecy injection that assures that propper objects are setup and service instances in the right order
      // .. to be replaced by a proper framework 
      private static Dictionary<Type, Func<object>> _rawDependencyInjection = new Dictionary<Type, Func<object>>() {
         [typeof(IScheduleFlightService)] = ()
            => new ScheduleFlightService(
                     GetInstance<ScheduledFlight>(),
                     new List<IPassengerSummary>() {
                        new GeneralPassengerSummary(),
                        new LoyalityMemberSummary(),
                        new AirlineEmployeeSummary()
                     }),

         [typeof(ScheduledFlight)] = ()
            => SetupAirlineData(),
      };

      public static T GetInstance<T>() {
         var key = typeof(T);
         return (T)(_rawDependencyInjection.ContainsKey(key) ? _rawDependencyInjection[key].Invoke() : default(T));
      }

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
