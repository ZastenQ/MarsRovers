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

        private List<IRover> Rovers { get; set; }

        public bool ValidatePosition(RoverPosition roverPosition)
        {
            return !(roverPosition.X < 0 ||
                    roverPosition.Y < 0 ||
                    roverPosition.X > MaxPosition.X ||
                    roverPosition.Y > MaxPosition.Y);
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
