using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using TW.DeveloperTest.Contracts;
using TW.DeveloperTest.Contracts.Enums;

namespace TW.DeveloperTest.WorkLibrary
{
    public class Logger : ILogger
    {

        private readonly Dictionary<LoggingLevel, ConsoleColor> LOGGING_LEVEL_COLOR_DICT =
            new Dictionary<LoggingLevel, ConsoleColor>()
            {
                {LoggingLevel.DEBUG, ConsoleColor.Green},
                {LoggingLevel.INFO, ConsoleColor.Blue },
                {LoggingLevel.WARN, ConsoleColor.Yellow },
                {LoggingLevel.ERROR, ConsoleColor.Red },
            };
        private bool DisplayDateTime;
        private bool DisplayLoggingLevel;
        private bool UseColors;
        private LoggingLevel MinimumLoggingLevel;

        public Logger(bool displayDateTime = true, bool useColors = true, bool displayLoggingLevel = true, LoggingLevel minimumLoggingLevel = LoggingLevel.DEBUG)
        {
            DisplayDateTime = displayDateTime;
            UseColors = useColors;
            DisplayLoggingLevel = displayLoggingLevel;
            MinimumLoggingLevel = minimumLoggingLevel;
        }

        public void DisplayInterface()
        {
            // Adding in a blank line so the character that was typed does not end up on same line as questions
            Console.WriteLine("");

            // Configure Date/Time Preferences
            string response = "";
            while (response.ToLower() != "y" && response.ToLower() != "n")
            {
                Console.WriteLine("Do you want to display date/time information as a part of the logs? (y/n) ");
                response = Console.ReadLine();
            }
            this.DisplayDateTime = (response == "y");

            // Configure UseColors Preferences
            response = "";
            while (response.ToLower() != "y" && response.ToLower() != "n")
            {
                Console.WriteLine("Do you want to use colors? (y/n) ");
                response = Console.ReadLine();
            }
            this.UseColors = (response == "y");

            // Configure Display Logging Level Preferences
            response = "";
            while (response.ToLower() != "y" && response.ToLower() != "n")
            {
                Console.WriteLine("Do you want to display the logging level? (y/n) ");
                response = Console.ReadLine();
            }
            this.DisplayLoggingLevel = (response == "y");

            var keepLooping = true;
            LoggingLevel level;
            while (keepLooping)
            {
                Console.WriteLine("What is the minimum level of logging you'd like displayed?");
                foreach (var loggingLevel in Enum.GetValues(typeof(LoggingLevel)))
                {
                    Console.WriteLine($"    For {loggingLevel.ToString()}, type {(int)loggingLevel}");
                }

                response = Console.ReadLine();
                // TODO: This will still allow users to input logging levels higher than exist, for example
                // could input 9 and even though there's for logging levels.  Need to imliment some sort of
                // check to make sure the input isn't above the set amount of enum values;
                keepLooping = !Enum.TryParse<LoggingLevel>(response, out level);
                if (!keepLooping) this.MinimumLoggingLevel = level;
            }
        }

        public void LogErrorMessage(string message)
        {
            LogMessage(message, LoggingLevel.ERROR);
        }

        public void LogMessage(string message, LoggingLevel loggingLevel)
        {
            Console.ForegroundColor = UseColors ? LOGGING_LEVEL_COLOR_DICT[loggingLevel] : ConsoleColor.Gray;

            string log = $"{(DisplayDateTime ? DateTime.Now.ToString() + " - " : "")}" +
                $"{(DisplayLoggingLevel ? loggingLevel.ToString() + " - " : "")}" +
                message;

            if (loggingLevel >= MinimumLoggingLevel) Console.WriteLine(log);
        }
    }
}
