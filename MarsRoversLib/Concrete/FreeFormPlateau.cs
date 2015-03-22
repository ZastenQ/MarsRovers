using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarsRoversLib.Interfaces;

namespace MarsRoversLib.Concrete
{
    public class FreeFormPlateau: IPlateau
    {
        public FreeFormPlateau(RoverPosition maxPosition, IEnumerable<RoverPosition> obstacles)
        {
            MaxPosition = maxPosition;
            this.Rovers = new List<IRover>();
            this.Obstacles = obstacles;
        }

        public RoverPosition MaxPosition { get; private set; }

        private List<IRover> Rovers { get; set; }

        public IEnumerable<RoverPosition> Obstacles { get; private set; }

        public Boolean ValidatePosition(RoverPosition roverPosition)
        {
            return !(roverPosition.X < 0 ||
                    roverPosition.Y < 0 ||
                    roverPosition.X >= MaxPosition.X ||
                    roverPosition.Y >= MaxPosition.Y||
                    Obstacles.Contains(roverPosition) );
        }

        public Boolean ValidateMoving(RoverPosition roverPosition)
        {
            return ValidatePosition(roverPosition) &&
                   !(this.Rovers.Any(ro => ro.Position.X == roverPosition.X && ro.Position.Y == roverPosition.Y));
        }

        public void Add(IRover item)
        {
            Rovers.Add(item);
        }

        public void Clear()
        {
            Rovers.Clear();
        }

        public bool Contains(IRover item)
        {
            return Rovers.Contains(item);
        }

        public void CopyTo(IRover[] array, int arrayIndex)
        {
            Rovers.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return Rovers.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(IRover item)
        {
            return Rovers.Remove(item);
        }

        public IEnumerator<IRover> GetEnumerator()
        {
            return Rovers.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Rovers.GetEnumerator();
        }
    }
}
