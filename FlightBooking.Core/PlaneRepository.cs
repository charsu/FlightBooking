using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightBooking.Core {
   public class PlaneRepository : IPlaneRepository {
      private readonly List<Plane> _planes;
      public PlaneRepository(List<Plane> planes) {
         _planes = planes;
      }

      public IQueryable<Plane> GetSet()
         => _planes.AsQueryable();
   }
}
