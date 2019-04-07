using System;
using System.Collections.Generic;
using System.Text;
using FlightBooking.Core.Models;

namespace FlightBooking.Core.PassengerSummary {
   public class LoyalityMemberSummary : IPassengerSummary {
      public FlightSummary Process(ScheduledFlight scheduledFlight, Passenger passenger) {
         var output = new FlightSummary();

         if (passenger.Type == PassengerType.LoyaltyMember) {
            if (passenger.IsUsingLoyaltyPoints) {
               var loyaltyPointsRedeemed = Convert.ToInt32(Math.Ceiling(scheduledFlight.FlightRoute.BasePrice));
               passenger.LoyaltyPoints -= loyaltyPointsRedeemed;
               output.TotalLoyaltyPointsRedeemed += loyaltyPointsRedeemed;
            }
            else {
               output.TotalLoyaltyPointsAccrued += scheduledFlight.FlightRoute.LoyaltyPointsGained;
               output.ProfitFromFlight += scheduledFlight.FlightRoute.BasePrice;
            }

            output.TotalExpectedBaggage = 2;
         }

         return output;
      }
   }
}
