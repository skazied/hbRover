using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HbRover
{
    public class Plateau
    {
        private int _maxX { get; set; }
        private int _maxY { get; set; }
        private int _minX { get; set; }
        private int _minY { get; set; }

        public Plateau(string command)
        {
            var tempArray = command.Split(" ");
            _maxX = Convert.ToInt32(tempArray[0]);
            _maxY = Convert.ToInt32(tempArray[1]);
            _minX = 0;
            _minY = 0;
        }

        public bool IsInPlateau(int X, int Y)
        {
            return (X <= _maxX && X >= _minX && Y <= _maxY && Y >= _minY);
        }

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

