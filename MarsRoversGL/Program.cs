using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;
using MarsRoversLib.Concrete;
using MarsRoversLib.Interfaces;
using MarsRoversLib;


namespace MarsRoversGL
{
    class Program
    {
        private const float DEG2RAD = 3.14159f / 180f;
        private static Random Randomizer = new Random();

        [STAThread]
        static void Main(string[] args)
        {
            List<RoverPosition> obstacles = new List<RoverPosition>();
            RoverPosition maxPosition;
            maxPosition.X = 10;
            maxPosition.Y = 10;

            obstacles.AddRange(Enumerable.Range(0, maxPosition.X*2)
                                         .Select(m => new RoverPosition()
                                {
                                    X = Randomizer.Next(0, maxPosition.X),
                                    Y = Randomizer.Next(0, maxPosition.Y)
                                })
                                         .Where(r => r.X != 0 && r.Y != 0));

            FreeFormPlateau plateau = new FreeFormPlateau(maxPosition, obstacles);

            plateau.Add(new Rover(new RoverPosition(), Direction.E, plateau));
         //   plateau.Add(new Rover(new RoverPosition(), Direction.N, plateau));
            plateau.Add(new HunterRover(new RoverPosition() { X = 5, Y = 5 }, Direction.N, plateau)
            {
                Target = new RoverPosition() { X = 7, Y = 9 }
            });

            
            using (GameWindow game = new GameWindow(800, 800))
            {
                game.Load += (sender, e) =>
                {
                    // setup settings, load textures, sounds
                    game.VSync = VSyncMode.On;
                };

                game.Resize += (sender, e) =>
                {
                    GL.Viewport(0, 0, game.Width, game.Height);
                };

                game.UpdateFrame += (sender, e) =>
                {
                    // add game logic, input handling
                    if (game.Keyboard[Key.Escape])
                    {
                        game.Exit();
                    }
                };

                game.RenderFrame += (sender, e) =>
                {
                    // render graphics
                    GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                    GL.MatrixMode(MatrixMode.Projection);
                    GL.LoadIdentity();
                    GL.Ortho(-1, maxPosition.X + 1, -1, maxPosition.Y + 1, 0.0, 4.0);
                    GL.ClearColor(Color.Ivory);

                    foreach (RoverPosition obst in plateau.Obstacles)
                    {
                        DrawObstacles(obst);
                    }

                    DrawCluster(maxPosition);

                    foreach (Rover rover in plateau)
                    {
                        DrawRover(0.2f, rover.Position.X + 0.5f, rover.Position.Y + 0.5f, Color.Red);
                        rover.AutoMove();
                        DrawTrace(rover.RoverTrace);
                    }

                    game.SwapBuffers();
                    System.Threading.Thread.Sleep(500);
                };

                // Run the game at 60 updates per second
                game.Run(60.0);
            }
        }

        static void DrawRover(float radius, float pX, float pY, Color color)
        {
            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(color);

            for (int i = 0; i <= 360; i++)
            {
                float degInRad = i * DEG2RAD;
                GL.Vertex2(Math.Cos(degInRad) * radius + pX, Math.Sin(degInRad) * radius + pY);
            }

            GL.End();
        }

        static void DrawObstacles(RoverPosition obstacle)
        {
            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(Color.Black);

            GL.Vertex2(obstacle.X, obstacle.Y);
            GL.Vertex2(obstacle.X + 1, obstacle.Y);
            GL.Vertex2(obstacle.X + 1, obstacle.Y + 1);
            GL.Vertex2(obstacle.X, obstacle.Y + 1);

            GL.End();
        }

        static void DrawCluster(RoverPosition maxPosition)
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Gray);
            for (Int32 i = 0; i <= Math.Max(maxPosition.X, maxPosition.Y); i++)
            {
                GL.Vertex2(i, 0.0f);
                GL.Vertex2(i, maxPosition.Y);
                GL.Vertex2(0.0f, i);
                GL.Vertex2(maxPosition.X, i);
            }

            GL.End();
        }

        static void DrawTrace(IEnumerable<RoverPosition> trace)
        {
            for (Int32 i = 0; i < trace.Count(); i++)
            {
                Color traceColor = Color.FromArgb(0, 255, 255 - ((int)(255 / trace.Count()) * i), 255 - ((int)(255 / trace.Count()) * i));
                DrawRover(0.05f, trace.ToList()[i].X + 0.5f, trace.ToList()[i].Y + 0.5f, traceColor);
            }
        }
    }
}
