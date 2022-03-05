using System;

namespace HbRover
{
    class Program
    {
        int maxY = 0;
        int maxX = 0;

        static void Main(string[] args)
        {
            Program program = new Program();
            Rover rover = null;
            program.createPlateau();


            while (true)
            {
                Console.WriteLine("Please enter command or rover position");

                var command = Console.ReadLine().ToUpper().TrimStart().TrimEnd();

                var commandType = program.validateIncomingCommand(command);
                if (commandType == CommandType.UNKNOWN)
                {
                    Console.WriteLine("You entered unexpected values!");
                    continue;
                }
                else if (commandType == CommandType.ROVER_POSITION_COMMAND)
                {
                    rover = program.setRoverLocationData(command);
                }
                else if (commandType == CommandType.MOVEMENT_COMMAND)
                {
                    if (rover == null)
                    {
                        Console.WriteLine("You should enter rover's location data before move the rover!");
                        continue;
                    }
                    if (!program.moveRover(command, rover))
                    {
                        Console.WriteLine("Rover will move out the grid! check the movement command first!");
                    }
                    else
                    {
                        Console.WriteLine(rover.PositionX.ToString() + " " + rover.PositionY.ToString() + " " + rover.Rotation.ToString());
                    }
                }
            }
        }

        public Rover setRoverLocationData(string command)
        {
            var splittedData = command.Split(" ");
            return new Rover()
            {
                PositionX = Convert.ToInt32(splittedData[0]),
                PositionY = Convert.ToInt32(splittedData[1]),
                Rotation = Convert.ToChar(splittedData[2])
            };
        }

        public bool moveRover(string command, Rover rover)
        {
            var newRoverX = rover.PositionX;
            var newRoverY = rover.PositionY;
            var newRoverOrientation = rover.Rotation;

            for (int i = 0; i < command.Length; i++)
            {
                if (command[i] == 'M')
                {
                    if (newRoverOrientation == 'E') { newRoverX++; continue; }
                    if (newRoverOrientation == 'W') { newRoverY--; continue; }
                    if (newRoverOrientation == 'N') { newRoverX++; continue; }
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
                if (newRoverX < 0 || newRoverY < 0 || newRoverY > maxY || newRoverX > maxX)
                {
                    return false;
                }
            }
            rover.PositionX = newRoverX;
            rover.PositionY = newRoverY;
            rover.Rotation = newRoverOrientation;
            return true;
        }




        public CommandType validateIncomingCommand(string command)
        {
            var splittedData = command.Split(" ");
            if (splittedData.Length == 1 && splittedData[0].Replace("L", "").Replace("R", "").Replace("M", "").Length == 0)
            {
                return CommandType.MOVEMENT_COMMAND;
            }
            else if (splittedData.Length == 3)
            {
                try
                {
                    Convert.ToInt32(splittedData[0]);
                    Convert.ToInt32(splittedData[1]);
                    if (splittedData[2].Length == 1 && splittedData[2].Replace("N", "").Replace("E", "").Replace("W", "").Replace("S", "").Length == 0)
                        return CommandType.ROVER_POSITION_COMMAND;
                    return CommandType.UNKNOWN;
                }
                catch (Exception ex)
                {
                    return CommandType.UNKNOWN;
                }
            }
            return CommandType.UNKNOWN;
        }

        public void createPlateau()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter the size of plateau!");
                    var upeerRightAsString = Console.ReadLine();
                    var tempArray = upeerRightAsString.Split(" ");
                    if (tempArray.Length != 2)
                    {
                        throw new Exception();
                    }
                    maxX = Convert.ToInt32(tempArray[0]);
                    maxY = Convert.ToInt32(tempArray[1]);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("You entered unexpected values!");
                }
            }
        }

        public enum CommandType
        {
            ROVER_POSITION_COMMAND,
            MOVEMENT_COMMAND,
            UNKNOWN
        }
    }
}
