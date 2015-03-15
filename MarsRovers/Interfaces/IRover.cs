using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRovers.Interfaces
{
    public interface IRover
    {
        RoverPosition Position { get; }
        Direction Direction { get; }

        void RotateRight();
        void RotateLeft();
        void MoveForward();
    }
}
