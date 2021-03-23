using System;
using FitnessDiary.Model;
using System.Diagnostics;

namespace FitnessDiary.Uwp.App.DataSource.ExceptionLogs
{
    /// <summary>
    /// 
    /// </summary>
    public class LogException
    {
        /// <summary>
        /// Logs the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public async void Log(Exception exception)
        {
            ExceptionLog exceptionLog = new ExceptionLog()
            {
                ExceptionMessage = exception.Message.ToString(),
                ExceptionSource = exception.StackTrace.ToString(),
                ExceptionType = exception.Source.GetType().Name.ToString(),
                ExceptionID = Guid.NewGuid(),
                DateOfException = DateTime.Today
            };

            try
            {
                if (await ExceptionLogs.Instance.AddExceptionLog(exceptionLog))
                    Debug.WriteLine("Exception added to log");
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Exception was not logged, see Error: " + ex.Message);
            }
        }
    }
}
