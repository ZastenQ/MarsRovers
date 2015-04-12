using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRoversLib.Interfaces;

namespace MarsRoversLib.Concrete
{
    public class Rover : IRover
    {
        public Rover(RoverPosition position, Direction direction, IPlateau currentPlateau)
        {
            Position = position;
            Direction = direction;
            CurrentPlateau = currentPlateau;
            IsKilled = false; // default value on creation == false or null
            Priority = new Dictionary<RoverPosition, int>() { { position, 1 } };
            RoverTrace = new Queue<RoverPosition>();
        }

        public RoverPosition Position { get; protected set; }

        public Direction Direction { get; protected set; }

        public Boolean IsKilled { get; protected set; }

        protected IPlateau CurrentPlateau { get; set; }

        private Dictionary<RoverPosition, Int32> Priority { get; set; }

        public Queue<RoverPosition> RoverTrace { get; set; }

        protected static RoverPosition GetNewPosition(RoverPosition position, Direction direction)
        {
            RoverPosition newPosition = position;
            switch (direction)
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
            return newPosition;
        }

        public void Kill()
        {
            this.IsKilled = true;
        }

        public void RotateRight()
        {
            this.Direction = (Direction)((((Int32)this.Direction) + 1) % 4);
        }

        public void RotateLeft()
        {
            this.Direction = (Direction)((((Int32)this.Direction) + 3) % 4);
        }

        public Boolean MoveForward()
        {
            RoverPosition newPosition = GetNewPosition(this.Position, this.Direction);

            if (CurrentPlateau.ValidateMoving(newPosition))
            {
                RoverTrace.Enqueue(Position);

                if (RoverTrace.Count > 10)
                {
                    RoverTrace.Dequeue();
                }

                Position = newPosition;
                if (Priority.ContainsKey(newPosition))
                {
                    Priority[newPosition] += 1;
                }
                else
                {
                    Priority.Add(newPosition, 1);
                }
                return true;
            }
            return false;
        }

        public virtual void AutoMove()
        {
            // берем все направления
            // преобразуем в пары направление-точка
            // отбираем валидные пары
            // сортируем по приоритету
            // сортируем по направлению; текущее направление - приоритетно!
            // отбираем только направления
            if (this.IsKilled == false)
            {
                this.Direction = (new List<Direction>() { Direction.E, Direction.S, Direction.W, Direction.N })
                    .Select(dir => new KeyValuePair<Direction, RoverPosition>(dir, GetNewPosition(this.Position, dir)))
                    .Where(dir => CurrentPlateau.ValidateMoving(dir.Value))
                    .OrderBy(dir => Priority.ContainsKey(dir.Value) ? Priority[dir.Value] : 0)
                    .ThenBy(dir => dir.Key == Direction ? 0 : 1)
                    .Select(dir => dir.Key)
                    .First();

                MoveForward();
            }
        }
    }
}
