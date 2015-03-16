using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRovers.Interfaces;

namespace MarsRovers.Concrete
{
    public class Rover : IRover
    {
        public Rover(RoverPosition position, Direction direction, IPlateau currentPlateau)
        {
            Position = position;
            Direction = direction;
            CurrentPlateau = currentPlateau;
        }

        public RoverPosition Position { get; private set; }

        public Direction Direction { get; private set; }

        private IPlateau CurrentPlateau { get; set; }

        public void RotateRight()
        {
            this.Direction = (Direction)((((Int32)this.Direction) + 1) % 4);
        }

        public void RotateLeft()
        {
            this.Direction = (Direction)((((Int32)this.Direction) + 3) % 4);
        }

        public void MoveForward()
        {
            RoverPosition newPosition = Position;
            switch (this.Direction)
            {
                case Direction.N:
                    newPosition.Y += 1;
                    break;
                case Direction.E:
                    newPosition.X += 1;
                    break;
                case Direction.S:
                    newPosition.Y -= 1;
                    break;
                case Direction.W:
                    newPosition.X -= 1;
                    break;
            }

            if (CurrentPlateau.ValidatePosition(newPosition))
            {
                Position = newPosition;
            }
        }
    }
}
