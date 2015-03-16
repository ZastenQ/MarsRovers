using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRovers.Interfaces
{
    public interface IPlateau: ICollection<IRover>
    {
        RoverPosition MaxPosition { get; }

        Boolean ValidatePosition(RoverPosition roverPosition);
    }
}
