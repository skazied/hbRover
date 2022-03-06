using HbRover.Interfaces;
using System;

namespace HbRover
{
    /// <summary>
    /// this class holds pleateau's datas and functions
    /// </summary>
    public class Plateau : IPlateau
    {
        private int _maxX { get; set; }
        private int _maxY { get; set; }
        private int _minX { get; set; }
        private int _minY { get; set; }

        /// <summary>
        /// command should have two numeric values seperated by one space.
        /// example: "5 5" or "12 5"
        /// </summary>
        /// <param name="command"></param>
        public Plateau(string command)
        {
            var tempArray = command.Split(" ");
            _maxX = Convert.ToInt32(tempArray[0]);
            _maxY = Convert.ToInt32(tempArray[1]);
            _minX = 0;
            _minY = 0;
        }

        /// <summary>
        /// checks is given coordiantes are in the plateau
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns>
        /// true: coordinates are in plateau
        /// false: coordinates are out of plateau
        /// </returns>
        public bool IsInPlateau(int X, int Y)
        {
            return (X <= _maxX && X >= _minX && Y <= _maxY && Y >= _minY);
        }

        /// <summary>
        /// checks if set pleatea size set command string valid
        /// </summary>
        /// <param name="command"></param>
        /// <returns>
        /// true: sizes are valid
        /// false: sizes are not valid
        /// </returns>
        public static bool ValidateIncomingPlateauCommand(string command)
        {
            try
            {
                var tempArray = command.Split(" ");
                if (tempArray.Length != 2)
                {
                    throw new Exception();
                }
                Convert.ToInt32(tempArray[0]);
                Convert.ToInt32(tempArray[1]);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

