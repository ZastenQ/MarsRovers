using MarsRoversLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRoversLib.Concrete
{
    public class HunterRover : Rover
    {
        public HunterRover(RoverPosition position, Direction direction, IPlateau currentPlateau)
            : base(position, direction, currentPlateau)
        {
            IEnumerable<Direction> directions = new[] { Direction.E, Direction.S, Direction.W, Direction.N };

            Graph = Enumerable
                .Range(0, currentPlateau.MaxPosition.X)
                .SelectMany(x => Enumerable.Range(0, currentPlateau.MaxPosition.Y)
                                           .Select(y => new RoverPosition() { X = x, Y = y }))
                .SelectMany(p => directions.Select(d => GetNewPosition(p, d))
                                           .Where(m => currentPlateau.ValidatePosition(m))
                                           .Select(m => new KeyValuePair<RoverPosition, RoverPosition>(p, m)))
                .Select(p => new DijkstraAlgorithm.Path<RoverPosition>()
                                {
                                    Cost = 1,
                                    Destination = p.Value,
                                    Source = p.Key
                                })
                .ToList();
        }

        public RoverPosition Target { get; set; }

        private IEnumerable<DijkstraAlgorithm.Path<RoverPosition>> Graph { get; set; }

        public override void AutoMove()
        {
            if (!(Position.X == Target.X && Position.Y == Target.Y))
            {
                LinkedList<DijkstraAlgorithm.Path<RoverPosition>> res = DijkstraAlgorithm.Engine.CalculateShortestPathBetween(Position, Target, Graph);

                if (res.Any())
                {
                    Position = res.First.Value.Destination;
                }
                else
                {
                    base.AutoMove();
                    Target = Position;
                }
            }
            else
            {
                base.AutoMove();
                Target = Position;
            }

        }
    }
}
