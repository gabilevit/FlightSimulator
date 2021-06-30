using System;
using System.Collections.Generic;
using System.Text;
using UI.Models;

namespace UI.Extentions
{
    public class PlaneUIGenerator
    {
        internal IEnumerable<Location> GetLocations()
        {
            var locations = new List<Location>();
            locations.Add(new Location { Row = 0, Column = 1 });
            locations.Add(new Location { Row = 1, Column = 0 });
            locations.Add(new Location { Row = 1, Column = 1 });
            locations.Add(new Location { Row = 1, Column = 2 });
            locations.Add(new Location { Row = 2, Column = 1 });
            return locations;
        }
    }
}
