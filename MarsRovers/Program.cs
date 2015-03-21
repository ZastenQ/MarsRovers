using MarsRoversLib.Concrete;
using MarsRoversLib.Interfaces;
using MarsRoversLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRovers
{
    class Program
    {
        static void Main(string[] args)
        {
            List<String> input = new List<String>();
            RoverPosition maxPosition;

            for (Int32 i = 0; i < 5; i++)
            {
                input.Add(Console.ReadLine());
            }

            maxPosition.X = Convert.ToInt32(input[0].Split(' ')[0]);
            maxPosition.Y = Convert.ToInt32(input[0].Split(' ')[1]);

            IPlateau plateau = new Plateau(maxPosition);

            for (Int32 i = 1; i < input.Count; i += 2)
            {
                RoverPosition roverPosition;
                roverPosition.X = Convert.ToInt32(input[i].Split(' ')[0]);
                roverPosition.Y = Convert.ToInt32(input[i].Split(' ')[1]);
                Direction direction = (Direction)Enum.Parse(typeof(Direction), input[i].Split(' ')[2]);
                IRover rover = new Rover(roverPosition, direction, plateau);
                plateau.Add(rover);

                foreach (char c in input[i + 1].ToCharArray())
                {
                    switch (c)
                    {
                        case 'L': rover.RotateLeft();
                            break;
                        case 'R': rover.RotateRight();
                            break;
                        case 'M': rover.MoveForward();
                            break;
                    }
                }
            }

            foreach (IRover r in plateau)
            {
                Console.WriteLine("{0} {1} {2}", r.Position.X, r.Position.Y, r.Direction);
            }

            Console.ReadKey();
        }
    }
}
