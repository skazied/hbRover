using System;

namespace HbRover
{
    class Program
    {
        Rover rover;
        Plateau plateau;

        static void Main(string[] args)
        {
            Program program = new Program();
            program.createPlateau();
            program.getRoverCommands();
        }

        public void getRoverCommands()
        {
            while (true)
            {
                Console.WriteLine("Please enter command or rover position");
                var command = Console.ReadLine().ToUpper().TrimStart().TrimEnd();

                var commandType = detectIncomingCommandType(command);
                if (commandType == CommandType.UNKNOWN)
                {
                    Console.WriteLine("You entered unexpected values!");
                    continue;
                }
                else if (commandType == CommandType.ROVER_POSITION_COMMAND)
                {
                    rover = new Rover(command);
                }
                else if (commandType == CommandType.MOVEMENT_COMMAND)
                {
                    if (rover == null)
                    {
                        Console.WriteLine("You should enter rover's location data before move the rover!");
                        continue;
                    }
                    if (rover.MoveRover(command, plateau))
                    {
                        Console.WriteLine("{0} {1} {2}", rover.PositionX, rover.PositionY, rover.Rotation);
                    }
                    else
                    {
                        Console.WriteLine("Rover will move out the grid! check the movement command!");
                    }
                }
            }
        }

        public void createPlateau()
        {
            while (true)
            {
                Console.WriteLine("Please enter the size of plateau!");
                var upeerRightAsString = Console.ReadLine();
                if (Plateau.ValidateIncomingPlateauCommand(upeerRightAsString))
                {
                    plateau = new Plateau(upeerRightAsString);
                    break;
                }
                Console.WriteLine("You entered unexpected values!");
            };
        }

        public CommandType detectIncomingCommandType(string command)
        {
            if (Rover.ValidateMovementCommand(command))
            {
                return CommandType.MOVEMENT_COMMAND;
            }
            else if (Rover.ValidateSetRoverPositionCommand(command))
            {
                return CommandType.ROVER_POSITION_COMMAND;
            }
            return CommandType.UNKNOWN;
        }
    }
}
