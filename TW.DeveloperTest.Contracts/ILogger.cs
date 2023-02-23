using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TW.DeveloperTest.Contracts.Enums;

namespace TW.DeveloperTest.Contracts
{
    public interface ILogger
    {
        void DisplayInterface();
        void LogMessage(string message, LoggingLevel loggingLevel);
        void LogErrorMessage(string message);
        // TODO: Impliment a unique method for each logging level that we wanted.  (E.g. LogWarningMessage, LogInfoMessage)
    }
}
