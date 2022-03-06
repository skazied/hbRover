using HbRover.Interfaces;
using System;

namespace HbRover
{
    /// <summary>
    /// This class holds a rover's position data and helps to move rover.
    /// </summary>
    public class Rover : IRover
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public char Rotation { get; set; }

        /// <summary>
        /// command should have two spaces and 3 words
        /// </summary>
        /// <param name="command"></param>
        public Rover(string command)
        {
            var splittedData = command.Split(" ");
            PositionX = Convert.ToInt32(splittedData[0]);
            PositionY = Convert.ToInt32(splittedData[1]);
            Rotation = Convert.ToChar(splittedData[2]);
        }

        /// <summary>
        /// this function moves the rover.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="plateau"></param>
        /// <returns>
        /// false: rover will out of the grid
        /// true: rover moved successfully
        /// </returns>
        public bool MoveRover(string command, IPlateau plateau)
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

        /// <summary>
        /// this fucntion check if the movement command is valid.
        /// </summary>
        /// <param name="command"></param>
        /// <returns>
        /// true: command is valid
        /// false: command is not valid
        /// </returns>
        public static bool ValidateMovementCommand(string command)
        {
            var splittedData = command.Split(" ");
            return splittedData.Length == 1 && splittedData[0].Replace("L", "").Replace("R", "").Replace("M", "").Length == 0 && !String.IsNullOrEmpty(command);
        }

        /// <summary>
        /// this fucntion helps to check if the rover position set command is valid
        /// </summary>
        /// <param name="command"></param>
        /// <returns>
        /// true: command is valid
        /// false: command is not valid
        /// </returns>
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
