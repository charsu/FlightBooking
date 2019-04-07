using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Core.Models {
   public class FlightSummary {
      public double CostOfFlight { get; set; } = 0;
      public double ProfitFromFlight { get; set; } = 0;
      public int TotalLoyaltyPointsAccrued { get; set; } = 0;
      public int TotalLoyaltyPointsRedeemed { get; set; } = 0;
      public int TotalExpectedBaggage { get; set; } = 0;
      public int SeatsTaken { get; set; } = 0;
      public double ProfitSurplus => ProfitFromFlight - CostOfFlight;

      public static FlightSummary operator +(FlightSummary left, FlightSummary right) {
         var a = left ?? new FlightSummary();
         var b = right ?? new FlightSummary();
         return new FlightSummary() {
            CostOfFlight = a.CostOfFlight + b.CostOfFlight,
            ProfitFromFlight = a.ProfitFromFlight + b.ProfitFromFlight,
            TotalLoyaltyPointsAccrued = a.TotalLoyaltyPointsAccrued + b.TotalLoyaltyPointsAccrued,
            TotalLoyaltyPointsRedeemed = a.TotalLoyaltyPointsRedeemed + b.TotalLoyaltyPointsRedeemed,
            TotalExpectedBaggage = a.TotalExpectedBaggage + b.TotalExpectedBaggage,
            SeatsTaken = a.SeatsTaken + b.SeatsTaken,
         };
      }
   }
}
