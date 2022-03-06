using System;

namespace HbRover
{
    class Program
    {        
        private static readonly string UNEXPECTED_VALUE_MSG = "You entered unexpected values!";
        private static readonly string ENTER_COMMAND_MSG = "Please enter rover movement command or set rover position command";
        private static readonly string ENTER_ROVER_POSITION_FIRST_MSG = "You should enter rover's location data before move the rover!";
        private static readonly string ROVER_MOVEMENT_RESPONSE_MSG = "{0} {1} {2}";
        private static readonly string PLATEAU_SIZE_MSG = "Please enter the size of plateau! (X Y)";
        private static readonly string ROVER_OUT_MSG = "Rover will move out the grid! check the movement command!";


        private Rover rover;
        private Plateau plateau;

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
                Console.WriteLine(ENTER_COMMAND_MSG);
                var command = Console.ReadLine().ToUpper().TrimStart().TrimEnd();

                var commandType = detectIncomingCommandType(command);
                if (commandType == CommandType.UNKNOWN)
                {
                    Console.WriteLine(UNEXPECTED_VALUE_MSG);
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
                        Console.WriteLine(ENTER_ROVER_POSITION_FIRST_MSG);
                        continue;
                    }
                    if (rover.MoveRover(command, plateau))
                    {
                        Console.WriteLine(ROVER_MOVEMENT_RESPONSE_MSG, rover.PositionX, rover.PositionY, rover.Rotation);
                    }
                    else
                    {
                        Console.WriteLine(ROVER_OUT_MSG);
                    }
                }
            }
        }

        public void createPlateau()
        {
            while (true)
            {
                Console.WriteLine(PLATEAU_SIZE_MSG);
                var upeerRightAsString = Console.ReadLine();
                if (Plateau.ValidateIncomingPlateauCommand(upeerRightAsString))
                {
                    plateau = new Plateau(upeerRightAsString);
                    break;
                }
                Console.WriteLine(UNEXPECTED_VALUE_MSG);
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
