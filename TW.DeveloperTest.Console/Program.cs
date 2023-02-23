using System;
using System.Threading;
using TW.DeveloperTest.Contracts;
using TW.DeveloperTest.Contracts.Enums;

namespace TW.DeveloperTest.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool run = true;
            IWorker worker = Ioc.Resolve<IWorker>();
            ILogger logger = Ioc.Resolve<ILogger>();

            do
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();

                    if (key.Modifiers.HasFlag(ConsoleModifiers.Control)
                        && key.Key == ConsoleKey.X)
                    {
                        run = false;
                    }
                    else if (key.Key == ConsoleKey.C)
                    {
                        logger.DisplayInterface();
                    }
                }

                try
                {
                    var result = worker.GetResult();

                    // Setting a random logging level (excluding error) for demonstration purposes
                    Random rand = new Random();
                    var val = rand.Next(1, 4);

                    logger.LogMessage(result, (LoggingLevel)val);
                }
                catch (Exception e)
                {
                    logger.LogErrorMessage(e.Message);
                }

                Thread.Sleep(500);
            } while (run);
        }
    }
}
