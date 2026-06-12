using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace clslLibShared
{
    public static class clsEventLog
    {
        private const string SourceName = "DVLD";
        private const string LogName = "Application";
        private static bool _IsLoggerAvailable = false;
        static clsEventLog()
        {
            Initialized();
        }
        static void Initialized()
        {
            try
            {
                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, LogName);
                }
                _IsLoggerAvailable = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Logger Initialized Failed: {ex.Message}");
                _IsLoggerAvailable = false;
            }
        }
        public static void LogInformation(string message)
        {
            if (!_IsLoggerAvailable)
            {
                return;
            }
            EventLog.WriteEntry(SourceName, message, EventLogEntryType.Information);
        }
        public static void LogWarning(string message)
        {
            if (!_IsLoggerAvailable)
            {
                return;
            }
            EventLog.WriteEntry(SourceName, message, EventLogEntryType.Warning);
        }
        public static void LogError(string message)
        {
            if (!_IsLoggerAvailable)
            {
                return;
            }
            EventLog.WriteEntry(SourceName, message, EventLogEntryType.Error);
        }
    }
}
