using System;
using System.Collections.Generic;
using System.Text;
using Windows.Services.Maps;

namespace Serveza.Model
{
    public class Direction
    {
        public string direction { get; private set; }
        public double lenght { get; private set; }
        public Direction()
        {

        }

        public void Load(MapRouteManeuver maneuver)
        {
            lenght = maneuver.LengthInMeters;
            direction = maneuver.InstructionText;
        }
    }
}
