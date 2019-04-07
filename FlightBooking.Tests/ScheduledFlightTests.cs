using System;
using System.Collections.Generic;
using FlightBooking.Core;
using FlightBooking.Tests;
using NUnit.Framework;

namespace Tests {
   public class ScheduledFlightTests {

      [Test, Category(Config.Integration)]
      public void ScheduledFlight_OK() {
         var _scheduleFlightService = new ScheduleFlightService(TestData.SetupAirlineData());
         var input = GetExampleInput();

         input.ForEach(i => _scheduleFlightService.ProcessCommand(i));
         var output = _scheduleFlightService.GetSummary();

         Assert.AreEqual(GetExampleOutput(), output);
      }

      #region test data 

      internal List<string> GetExampleInput()
         => new List<string>() {
               "add general Steve 30",
               "add general Mark 12",
               "add general James 36",
               "add general Jane 32",
               "add loyalty John 29 1000 true",
               "add loyalty Sarah 45 1250 false",
               "add loyalty Jack 60 50 false",
               "add airline Trevor 47",
               "add general Alan 34",
               "add general Suzy 21",
               "print summary"
         };

      internal string GetExampleOutput()
         => string.Join(Environment.NewLine, new[]{
            "Flight summary for London to Paris",
            "",
            "Total passengers: 10",
            "    General sales: 6",
            "    Loyalty member sales: 3",
            "    Airline employee comps: 1",
            "",
            "Total expected baggage: 13",
            "",
            "Total revenue from flight: 800",
            "Total costs from flight: 500",
            "Flight generating profit of: 300",
            "",
            "Total loyalty points given away: 10",
            "Total loyalty points redeemed: 100",
            "",
            "",
            "THIS FLIGHT MAY PROCEED"
         });


      #endregion
   }
}