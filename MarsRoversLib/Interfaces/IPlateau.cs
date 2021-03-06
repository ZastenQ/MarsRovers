﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversLib.Interfaces
{
    public interface IPlateau : ICollection<IRover>
    {
        RoverPosition MaxPosition { get; }

        Boolean ValidateMoving(RoverPosition roverPosition);

        Boolean ValidatePosition(RoverPosition roverPosition);
    }
}
