using System;
using System.Collections.Generic;
using System.Text;

namespace FlightBooking.Core {
   public interface IScheduleFlightService {
      (List<string>, ConsoleColor?) ProcessCommand(string command);
      string GetSummary();
   }
}
