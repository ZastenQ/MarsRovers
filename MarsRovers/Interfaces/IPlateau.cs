using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRovers.Interfaces
{
    interface IPlateau
    {
        RoverPosition MaxPosition { get; }
        List<IRover> Rovers { get; }

        Boolean ValidatePosition(RoverPosition roverPosition);
    }
}
