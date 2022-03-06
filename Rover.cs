using System;

namespace HbRover
{
    public class Rover
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public char Rotation { get; set; }

        public Rover(string command)
        {
            var splittedData = command.Split(" ");
            PositionX = Convert.ToInt32(splittedData[0]);
            PositionY = Convert.ToInt32(splittedData[1]);
            Rotation = Convert.ToChar(splittedData[2]);
        }

        public bool MoveRover(string command, Plateau plateau)
        {
            var newRoverX = PositionX;
            var newRoverY = PositionY;
            var newRoverOrientation = Rotation;

            for (int i = 0; i < command.Length; i++)
            {
                if (command[i] == 'M')
                {
                    if (newRoverOrientation == 'E') { newRoverX++; continue; }
                    if (newRoverOrientation == 'W') { newRoverX--; continue; }
                    if (newRoverOrientation == 'N') { newRoverY++; continue; }
                    if (newRoverOrientation == 'S') { newRoverY--; continue; }
                }

                else if (command[i] == 'L')
                {
                    if (newRoverOrientation == 'E') { newRoverOrientation = 'N'; continue; }
                    if (newRoverOrientation == 'W') { newRoverOrientation = 'S'; continue; }
                    if (newRoverOrientation == 'N') { newRoverOrientation = 'W'; continue; }
                    if (newRoverOrientation == 'S') { newRoverOrientation = 'E'; continue; }
                }

                else if (command[i] == 'R')
                {
                    if (newRoverOrientation == 'E') { newRoverOrientation = 'S'; continue; }
                    if (newRoverOrientation == 'W') { newRoverOrientation = 'N'; continue; }
                    if (newRoverOrientation == 'N') { newRoverOrientation = 'E'; continue; }
                    if (newRoverOrientation == 'S') { newRoverOrientation = 'W'; continue; }
                }
                if (!plateau.IsInPlateau(newRoverX, newRoverY)) return false;

            }
            PositionX = newRoverX;
            PositionY = newRoverY;
            Rotation = newRoverOrientation;
            return true;
        }

        public static bool ValidateMovementCommand(string command)
        {
            var splittedData = command.Split(" ");
            return splittedData.Length == 1 && splittedData[0].Replace("L", "").Replace("R", "").Replace("M", "").Length == 0;
        }

        public static bool ValidateSetRoverPositionCommand(string command)
        {
            try
            {
                var splittedData = command.Split(" ");
                Convert.ToInt32(splittedData[0]);
                Convert.ToInt32(splittedData[1]);
                return (splittedData[2].Length == 1 && splittedData[2].Replace("N", "").Replace("E", "").Replace("W", "").Replace("S", "").Length == 0);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
