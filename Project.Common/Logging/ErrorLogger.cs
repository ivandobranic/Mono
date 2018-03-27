using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Project.Common.Logging
{
    public class ErrorLogger: IErrorLogger
    {
        private ErrorLogger()
        {

        }
        private static readonly Lazy<ErrorLogger> instance = new Lazy<ErrorLogger>(() => new ErrorLogger());
        public static ErrorLogger GetInstance
        {
            get
            {
                return instance.Value;
            }
        }
        public void LogError(Exception ex)
        {

            StringBuilder message = new StringBuilder();
            message.Append(string.Format("Time: ", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
            message.Append(Environment.NewLine);
            message.Append("-----------------------------------------------------------");
            message.Append(Environment.NewLine);
            message.Append("Exception Type:" + Environment.NewLine);
            message.Append(ex.GetType().Name);
            message.Append(Environment.NewLine);
            message.Append("Message:" + Environment.NewLine);
            message.Append(ex.Message);
            message.Append(Environment.NewLine);
            message.Append("Stack Trace:" + Environment.NewLine);
            message.Append(ex.StackTrace);
            message.Append(Environment.NewLine);
            message.Append("Source:" + Environment.NewLine);
            message.Append(ex.Source);

            Exception innerException = ex.InnerException;


            while (innerException != null)
            {

                message.Append(string.Format("Time: ", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
                message.Append(Environment.NewLine);
                message.Append("-----------------------------------------------------------");
                message.Append(Environment.NewLine);
                message.Append("Exception Type:" + Environment.NewLine);
                message.Append(ex.GetType().Name);
                message.Append(Environment.NewLine);
                message.Append("Message:" + Environment.NewLine);
                message.Append(ex.Message);
                message.Append(Environment.NewLine);
                message.Append("Stack Trace:" + Environment.NewLine);
                message.Append(ex.StackTrace);
                message.Append(Environment.NewLine);
                message.Append("Source:" + Environment.NewLine);
                message.Append(ex.Source);

                innerException = innerException.InnerException;
            }
            if (!EventLog.SourceExists("Vehicle"))
            {
                EventLog.CreateEventSource("Vehicle", "VehicleLogs");
                EventLog log = new EventLog("VehicleLogs");
                log.Source = "Vehicle";
                log.WriteEntry(message.ToString(), EventLogEntryType.Error);
            }
            else
            {
                EventLog log = new EventLog("VehicleLogs");
                log.Source = "Vehicle";
                log.WriteEntry(message.ToString(), EventLogEntryType.Error);
            }
        }

    }
}
