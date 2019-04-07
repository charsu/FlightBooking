using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlightBooking.Core.Models;

namespace FlightBooking.Core {
   public class ScheduleFlightService : IScheduleFlightService {
      private readonly string _verticalWhiteSpace = Environment.NewLine + Environment.NewLine;
      private readonly string _newLine = Environment.NewLine;
      private const string Indentation = "    ";

      private readonly ScheduledFlight _scheduledFlight;
      private readonly List<IPassengerSummary> _passengerSummaries;
      private readonly List<IFlightValidator> _flightValidators;

      public ScheduleFlightService(ScheduledFlight scheduledFlight, List<IPassengerSummary> passengerSummaries, List<IFlightValidator> flightValidators) {
         _scheduledFlight = scheduledFlight;
         _passengerSummaries = passengerSummaries;
         _flightValidators = flightValidators;
      }

      public (List<string>, ConsoleColor?) ProcessCommand(string enteredText) {
         /// note : should require refactoring and be split into a separate service 
         /// but for now we have to make do with it here
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
         else if (enteredText.Contains("add discounted")) {
            var passengerSegments = enteredText.Split(' ');
            _scheduledFlight.AddPassenger(new Passenger {
               Type = PassengerType.Discounted,
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
         var flightsummary = new FlightSummary();

         var result = new StringBuilder();
         result.Append("Flight summary for " + _scheduledFlight.FlightRoute.Title);

         foreach (var p in _scheduledFlight.Passengers) {
            foreach (var s in _passengerSummaries) {
               var output = s.Process(_scheduledFlight, p);
               flightsummary = flightsummary + output;
            }

            // we add them for all types ?!
            flightsummary.CostOfFlight += _scheduledFlight.FlightRoute.BaseCost;
            flightsummary.SeatsTaken++;
         }

         result.Append(_verticalWhiteSpace);

         result.Append("Total passengers: " + flightsummary.SeatsTaken);
         result.Append(_newLine);
         result.Append(Indentation + "General sales: " + _scheduledFlight.Passengers.Count(p => p.Type == PassengerType.General));
         result.Append(_newLine);
         result.Append(Indentation + "Loyalty member sales: " + _scheduledFlight.Passengers.Count(p => p.Type == PassengerType.LoyaltyMember));
         result.Append(_newLine);
         result.Append(Indentation + "Airline employee comps: " + _scheduledFlight.Passengers.Count(p => p.Type == PassengerType.AirlineEmployee));

         // we don't want to change the output in case we don't have any discounted members
         var discountedPassengers = _scheduledFlight.Passengers.Count(p => p.Type == PassengerType.Discounted);
         if (discountedPassengers > 0) {
            result.Append(_newLine);
            result.Append(Indentation + "Discounted member sales: " + discountedPassengers);
         }

         result.Append(_verticalWhiteSpace);
         result.Append("Total expected baggage: " + flightsummary.TotalExpectedBaggage);

         result.Append(_verticalWhiteSpace);

         result.Append("Total revenue from flight: " + flightsummary.ProfitFromFlight);
         result.Append(_newLine);
         result.Append("Total costs from flight: " + flightsummary.CostOfFlight);
         result.Append(_newLine);

         var surplusMessage = (flightsummary.ProfitSurplus > 0 ?
            "Flight generating profit of: " : "Flight losing money of: ");
         result.Append(surplusMessage + flightsummary.ProfitSurplus);

         result.Append(_verticalWhiteSpace);

         result.Append("Total loyalty points given away: " + flightsummary.TotalLoyaltyPointsAccrued + _newLine);
         result.Append("Total loyalty points redeemed: " + flightsummary.TotalLoyaltyPointsRedeemed + _newLine);

         result.Append(_verticalWhiteSpace);

         var validationMessage = new List<string>();
         var isValid = true;
         foreach (var v in _flightValidators) {
            var (vIsValid, vMessage) = v.Validate(flightsummary, _scheduledFlight);
            isValid = isValid && vIsValid;
            if (vMessage != null) {
               validationMessage.AddRange(vMessage);
            }
         }

         result.Append(isValid ? "THIS FLIGHT MAY PROCEED" : "FLIGHT MAY NOT PROCEED");
         validationMessage?.ForEach(m => {
            result.Append(_newLine);
            result.Append(m);
         });

         return result.ToString();
      }
   }
}
