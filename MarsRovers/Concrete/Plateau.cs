using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRovers.Interfaces;

namespace MarsRovers.Concrete
{
    class Plateau : IPlateau
    {
        public Plateau(RoverPosition maxPosition)
        {
            MaxPosition = maxPosition;
            this.Rovers = new List<IRover>();
        }

        public RoverPosition MaxPosition { get; private set; }

        public IEnumerable<IRover> Rovers { get; private set; }

        public bool ValidatePosition(RoverPosition roverPosition)
        {
            return !(roverPosition.X < 0 ||
                    roverPosition.Y < 0 ||
                    roverPosition.X > MaxPosition.X ||
                    roverPosition.Y > MaxPosition.Y);
        }

        public void AddRover(IRover rover)
        {
            ((List<IRover>)Rovers).Add(rover);
        }
    }
}
