using System;
using FlightBooking.Core;

namespace FlightBooking.Console {
   internal class Program {
      private static IScheduleFlightService _scheduleFlightService;
      private static void Main(string[] args) {
         _scheduleFlightService = IoC.GetInstance<IScheduleFlightService>();

         string command;
         do {
            System.Console.WriteLine("Please enter command.");
            command = System.Console.ReadLine() ?? "";

            var enteredText = command.ToLower();

            if (enteredText.Contains("exit")) {
               // special case , do we need it ?!?
               Environment.Exit(1);
            }

            var (messages, color) = _scheduleFlightService.ProcessCommand(enteredText);

            if (color.HasValue) {
               System.Console.ForegroundColor = color.Value;
            }

            messages?.ForEach(m => System.Console.WriteLine(m));
            System.Console.ResetColor();

         } while (command != "exit");
      }
      //private static void Main(string[] args) {
      //   _scheduledFlight = TestData.SetupAirlineData();

      //   string command;
      //   do {
      //      System.Console.WriteLine("Please enter command.");
      //      command = System.Console.ReadLine() ?? "";
      //      var enteredText = command.ToLower();
      //      if (enteredText.Contains("print summary")) {
      //         System.Console.WriteLine();
      //         System.Console.WriteLine(_scheduledFlight.GetSummary());
      //      }
      //      else if (enteredText.Contains("add general")) {
      //         var passengerSegments = enteredText.Split(' ');
      //         _scheduledFlight.AddPassenger(new Passenger {
      //            Type = PassengerType.General,
      //            Name = passengerSegments[2],
      //            Age = Convert.ToInt32(passengerSegments[3])
      //         });
      //      }
      //      else if (enteredText.Contains("add loyalty")) {
      //         var passengerSegments = enteredText.Split(' ');
      //         _scheduledFlight.AddPassenger(new Passenger {
      //            Type = PassengerType.LoyaltyMember,
      //            Name = passengerSegments[2],
      //            Age = Convert.ToInt32(passengerSegments[3]),
      //            LoyaltyPoints = Convert.ToInt32(passengerSegments[4]),
      //            IsUsingLoyaltyPoints = Convert.ToBoolean(passengerSegments[5]),
      //         });
      //      }
      //      else if (enteredText.Contains("add airline")) {
      //         var passengerSegments = enteredText.Split(' ');
      //         _scheduledFlight.AddPassenger(new Passenger {
      //            Type = PassengerType.AirlineEmployee,
      //            Name = passengerSegments[2],
      //            Age = Convert.ToInt32(passengerSegments[3]),
      //         });
      //      }
      //      else if (enteredText.Contains("exit")) {
      //         Environment.Exit(1);
      //      }
      //      else {
      //         System.Console.ForegroundColor = ConsoleColor.Red;
      //         System.Console.WriteLine("UNKNOWN INPUT");
      //         System.Console.ResetColor();
      //      }
      //   } while (command != "exit");
      //}
   }
}
