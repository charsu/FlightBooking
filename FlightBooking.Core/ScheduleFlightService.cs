using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightBooking.Core {
   public class ScheduleFlightService : IScheduleFlightService {
      private readonly string _verticalWhiteSpace = Environment.NewLine + Environment.NewLine;
      private readonly string _newLine = Environment.NewLine;
      private const string Indentation = "    ";

      private readonly ScheduledFlight _scheduledFlight;

      public ScheduleFlightService(ScheduledFlight scheduledFlight) {
         _scheduledFlight = scheduledFlight;
      }

      public (List<string>, ConsoleColor?) ProcessCommand(string enteredText) {
         ConsoleColor? color = null;
         List<string> messages = new List<string>();

         if (enteredText.Contains("print summary")) {
            messages.Add(_newLine);
            messages.Add(GetSummary());
         }
         else if (enteredText.Contains("add general")) {
            var passengerSegments = enteredText.Split(' ');
            _scheduledFlight.AddPassenger(new Passenger {
               Type = PassengerType.General,
               Name = passengerSegments[2],
               Age = Convert.ToInt32(passengerSegments[3])
            });
         }
         else if (enteredText.Contains("add loyalty")) {
            var passengerSegments = enteredText.Split(' ');
            _scheduledFlight.AddPassenger(new Passenger {
               Type = PassengerType.LoyaltyMember,
               Name = passengerSegments[2],
               Age = Convert.ToInt32(passengerSegments[3]),
               LoyaltyPoints = Convert.ToInt32(passengerSegments[4]),
               IsUsingLoyaltyPoints = Convert.ToBoolean(passengerSegments[5]),
            });
         }
         else if (enteredText.Contains("add airline")) {
            var passengerSegments = enteredText.Split(' ');
            _scheduledFlight.AddPassenger(new Passenger {
               Type = PassengerType.AirlineEmployee,
               Name = passengerSegments[2],
               Age = Convert.ToInt32(passengerSegments[3]),
            });
         }
         else {
            color = ConsoleColor.Red;
            messages.Add("UNKNOWN INPUT");
         }

         return (messages, color);
      }

      public string GetSummary() {
         double costOfFlight = 0;
         double profitFromFlight = 0;
         var totalLoyaltyPointsAccrued = 0;
         var totalLoyaltyPointsRedeemed = 0;
         var totalExpectedBaggage = 0;
         var seatsTaken = 0;

         var result = new StringBuilder();
         result.Append("Flight summary for " + _scheduledFlight.FlightRoute.Title);

         foreach (var passenger in _scheduledFlight.Passengers) {
            switch (passenger.Type) {
               case (PassengerType.General): {
                     profitFromFlight += _scheduledFlight.FlightRoute.BasePrice;
                     totalExpectedBaggage++;
                     break;
                  }
               case (PassengerType.LoyaltyMember): {
                     if (passenger.IsUsingLoyaltyPoints) {
                        var loyaltyPointsRedeemed = Convert.ToInt32(Math.Ceiling(_scheduledFlight.FlightRoute.BasePrice));
                        passenger.LoyaltyPoints -= loyaltyPointsRedeemed;
                        totalLoyaltyPointsRedeemed += loyaltyPointsRedeemed;
                     }
                     else {
                        totalLoyaltyPointsAccrued += _scheduledFlight.FlightRoute.LoyaltyPointsGained;
                        profitFromFlight += _scheduledFlight.FlightRoute.BasePrice;
                     }
                     totalExpectedBaggage += 2;
                     break;
                  }
               case (PassengerType.AirlineEmployee): {
                     totalExpectedBaggage += 1;
                     break;
                  }
               default:
                  throw new ArgumentOutOfRangeException();
            }
            costOfFlight += _scheduledFlight.FlightRoute.BaseCost;
            seatsTaken++;
         }

         result.Append(_verticalWhiteSpace);

         result.Append("Total passengers: " + seatsTaken);
         result.Append(_newLine);
         result.Append(Indentation + "General sales: " + _scheduledFlight.Passengers.Count(p => p.Type == PassengerType.General));
         result.Append(_newLine);
         result.Append(Indentation + "Loyalty member sales: " + _scheduledFlight.Passengers.Count(p => p.Type == PassengerType.LoyaltyMember));
         result.Append(_newLine);
         result.Append(Indentation + "Airline employee comps: " + _scheduledFlight.Passengers.Count(p => p.Type == PassengerType.AirlineEmployee));

         result.Append(_verticalWhiteSpace);
         result.Append("Total expected baggage: " + totalExpectedBaggage);

         result.Append(_verticalWhiteSpace);

         result.Append("Total revenue from flight: " + profitFromFlight);
         result.Append(_newLine);
         result.Append("Total costs from flight: " + costOfFlight);
         result.Append(_newLine);

         var profitSurplus = profitFromFlight - costOfFlight;

         result.Append((profitSurplus > 0 ? "Flight generating profit of: " : "Flight losing money of: ") + profitSurplus);

         result.Append(_verticalWhiteSpace);

         result.Append("Total loyalty points given away: " + totalLoyaltyPointsAccrued + _newLine);
         result.Append("Total loyalty points redeemed: " + totalLoyaltyPointsRedeemed + _newLine);

         result.Append(_verticalWhiteSpace);

         if (profitSurplus > 0 &&
             seatsTaken < _scheduledFlight.Aircraft.NumberOfSeats &&
             seatsTaken / (double)_scheduledFlight.Aircraft.NumberOfSeats > _scheduledFlight.FlightRoute.MinimumTakeOffPercentage) {
            result.Append("THIS FLIGHT MAY PROCEED");
         }
         else {
            result.Append("FLIGHT MAY NOT PROCEED");
         }

         return result.ToString();
      }
   }
}
