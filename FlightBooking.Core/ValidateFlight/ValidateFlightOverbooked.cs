using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlightBooking.Core.Models;

namespace FlightBooking.Core.ValidateFlight {
   public class ValidateFlightOverbooked : IFlightValidator {
      private readonly IPlaneRepository _planeRepository;
      public ValidateFlightOverbooked(IPlaneRepository planeRepository) {
         _planeRepository = planeRepository;
      }
      public (bool isValid, List<string> messages) Validate(FlightSummary flightSummary, ScheduledFlight scheduledFlight) {
         var isValid = flightSummary.SeatsTaken < scheduledFlight.Aircraft.NumberOfSeats;
         var messages = new List<string>();
         if (!isValid) {
            var possiblePlanes = _planeRepository.GetSet()
                  .Where(x => x.NumberOfSeats > flightSummary.SeatsTaken)
                  .ToList();

            if (possiblePlanes.Count > 0) {
               messages.Add("Other more suitable aircraft are:");

               possiblePlanes.ForEach(p => {
                  messages.Add($"{p.Name} could handle this flight.");
               });
            }
         }

         return (isValid, messages);
      }
   }
}
