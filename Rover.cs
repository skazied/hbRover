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
                    switch (newRoverOrientation)
                    {
                        case 'E': newRoverX++; break;
                        case 'W': newRoverX--; break;
                        case 'N': newRoverY++; break;
                        case 'S': newRoverY--; break;
                    }
                }

                else if (command[i] == 'L')
                {
                    switch (newRoverOrientation)
                    {
                        case 'E': newRoverOrientation = 'N'; break;
                        case 'W': newRoverOrientation = 'S'; break;
                        case 'N': newRoverOrientation = 'W'; break;
                        case 'S': newRoverOrientation = 'E'; break;
                    }
                }

                else if (command[i] == 'R')
                {
                    switch (newRoverOrientation)
                    {
                        case 'E': newRoverOrientation = 'S'; break;
                        case 'W': newRoverOrientation = 'N'; break;
                        case 'N': newRoverOrientation = 'E'; break;
                        case 'S': newRoverOrientation = 'W'; break;
                    }
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
