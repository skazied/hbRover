using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HbRover.Interfaces
{
    public interface IRover
    {
        bool MoveRover(string command, IPlateau plateau);
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public char Rotation { get; set; }
    }
}
