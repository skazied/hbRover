using System;

namespace HbRover
{
    class Program
    {
        static int maxY = 0;
        static int maxX = 0;

        static int roverX = 0;
        static int roverY = 0;
        static char roverOrientation;

        static void Main(string[] args)
        {
            takeSizesOfGrid();
            while (true)
            {
                Console.WriteLine("Please enter command or rover position");

                var command = Console.ReadLine();
                command = command.ToUpper().TrimStart().TrimEnd();


                var commandType = checkIncomingCommand(command);
                if (commandType == CommandType.UNKNOWN)
                {
                    Console.WriteLine("You entered unexpected values!");
                    continue;
                }
                else if (commandType == CommandType.ROVER_POSITION_COMMAND)
                {
                    var splittedData = command.Split(" ");
                    roverX = Convert.ToInt32(splittedData[0]);
                    roverY = Convert.ToInt32(splittedData[1]);
                    roverOrientation = Convert.ToChar(splittedData[2]);
                }
                else if (commandType == CommandType.MOVEMENT_COMMAND)
                {
                    moveRover(command);
                }
            }







        }

        private static void moveRover(string command)
        {
            var newRoverX = roverX;
            var newRoverY = roverY;
            var newRoverOrientation = roverOrientation;

            for (int i = 0; i < command.Length; i++)
            {
                if (command[i] == 'M')
                {
                    if (newRoverOrientation == 'E') roverY++;
                    if (newRoverOrientation == 'W') roverY--;
                    if (newRoverOrientation == 'N') roverX++;
                    if (newRoverOrientation == 'S') roverY--;
                }

                if (command[i] == 'L')
                {
                    if (newRoverOrientation == 'E') newRoverOrientation = 'N';
                    if (newRoverOrientation == 'W') newRoverOrientation = 'S';
                    if (newRoverOrientation == 'N') newRoverOrientation = 'W';
                    if (newRoverOrientation == 'S') newRoverOrientation = 'E';
                }

                if (command[i] == 'R')
                {
                    if (newRoverOrientation == 'E') newRoverOrientation = 'S';
                    if (newRoverOrientation == 'W') newRoverOrientation = 'N';
                    if (newRoverOrientation == 'N') newRoverOrientation = 'E';
                    if (newRoverOrientation == 'S') newRoverOrientation = 'W';
                }
            }
        }

        


        public static CommandType checkIncomingCommand(string command)
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

        public static void takeSizesOfGrid()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter the upper right coordinates!");
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
