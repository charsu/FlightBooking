using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightBooking.Core {
   public interface IPlaneRepository {
      IQueryable<Plane> GetSet();
   }
}
