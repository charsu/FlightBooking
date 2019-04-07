using System;
using System.Collections.Generic;
using FlightBooking.Core;
using FlightBooking.Tests;
using NUnit.Framework;

namespace Tests {
   public class ScheduledFlightTests {

      [Test, Category(Config.Integration)]
      public void ScheduledFlight_AsItWas_OK() {
         var _scheduleFlightService = IoC.GetInstance<IScheduleFlightService>();
         var input = GetInitialExampleInput();

         input.ForEach(i => _scheduleFlightService.ProcessCommand(i));
         var output = _scheduleFlightService.GetSummary();

         Assert.AreEqual(GetInitialExampleOutput(), output);
      }

      [Test, Category(Config.Integration)]
      public void ScheduledFlight_WithDiscounted_OK() {
         var _scheduleFlightService = IoC.GetInstance<IScheduleFlightService>();
         var input = GetDiscountedExampleInput();

         input.ForEach(i => _scheduleFlightService.ProcessCommand(i));
         var output = _scheduleFlightService.GetSummary();

         Assert.AreEqual(GetDiscountedExampleOutput(), output);
      }

      [Test, Category(Config.Integration)]
      public void ScheduledFlight_OverbookedWithMessage_OK() {
         var _scheduleFlightService = IoC.GetInstance<IScheduleFlightService>();
         var input = GetOverbookedExampleInput();

         input.ForEach(i => _scheduleFlightService.ProcessCommand(i));
         var output = _scheduleFlightService.GetSummary();

         Assert.AreEqual(GetOverbookedExampleOutput(), output);
      }

      #region test data 

      internal List<string> GetInitialExampleInput()
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

      internal string GetInitialExampleOutput()
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

      internal List<string> GetDiscountedExampleInput()
         => new List<string>() {
               "add general Steve 30",
               "add general Mark 12",
               "add general Jane 32",
               "add discounted Mark 32",
               "add loyalty John 29 1000 true",
               "add loyalty Sarah 45 1250 false",
               "add loyalty Jack 60 50 false",
               "add discounted Trevor 30",
               "add airline Trevor 47",
               "add general Alan 34",
               "add general Suzy 21",
               "print summary"
         };

      internal string GetDiscountedExampleOutput()
         => string.Join(Environment.NewLine, new[]{
            "Flight summary for London to Paris",
            "",
            "Total passengers: 11",
            "    General sales: 5",
            "    Loyalty member sales: 3",
            "    Airline employee comps: 1",
            "    Discounted member sales: 2",
            "",
            "Total expected baggage: 12",
            "",
            "Total revenue from flight: 800",
            "Total costs from flight: 550",
            "Flight generating profit of: 250",
            "",
            "Total loyalty points given away: 10",
            "Total loyalty points redeemed: 100",
            "",
            "",
            "THIS FLIGHT MAY PROCEED"
         });

      internal List<string> GetOverbookedExampleInput()
         => new List<string>() {
               "add general Steve 30",
               "add general Mark 12",
               "add general James 36",
               "add general Jane 32",
               "add discounted Mark 32",
               "add general Steve 30",
               "add general Mark 12",
               "add general James 36",
               "add general Jane 32",
               "add discounted Mark 32",
               "add general Steve 30",
               "add general Mark 12",
               "add general James 36",
               "add general Jane 32",
               "add discounted Mark 32",
               "add general Steve 30",
               "add general Mark 12",
               "add general James 36",
               "add general Jane 32",
               "add discounted Mark 32",
               "add loyalty John 29 1000 true",
               "add loyalty Sarah 45 1250 false",
               "add loyalty Jack 60 50 false",
               "add discounted Trevor 30",
               "add airline Trevor 47",
               "add general Alan 34",
               "add loyalty Jack 60 50 false",
               "add discounted Trevor 30",
               "add airline Trevor 47",
               "add general Alan 34",
               "add general Suzy 21",
               "print summary"
         };

      internal string GetOverbookedExampleOutput()
         => string.Join(Environment.NewLine, new[]{
            "Flight summary for London to Paris",
            "",
            "Total passengers: 31",
            "    General sales: 19",
            "    Loyalty member sales: 4",
            "    Airline employee comps: 2",
            "    Discounted member sales: 6",
            "",
            "Total expected baggage: 29",
            "",
            "Total revenue from flight: 2500",
            "Total costs from flight: 1550",
            "Flight generating profit of: 950",
            "",
            "Total loyalty points given away: 15",
            "Total loyalty points redeemed: 100",
            "",
            "",
            "FLIGHT MAY NOT PROCEED",
            "Other more suitable aircraft are:",
            "Bombardier Q400 could handle this flight.",
            "ATR 640 could handle this flight."
         });

      #endregion
   }
}