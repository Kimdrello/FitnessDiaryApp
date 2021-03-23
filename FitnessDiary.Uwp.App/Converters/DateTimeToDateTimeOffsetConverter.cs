using System;
using Windows.UI.Xaml.Data;
using System.Diagnostics;
using FitnessDiary.Uwp.App.DataSource.ExceptionLogs;

namespace FitnessDiary.Uwp.App.Converters
{
    //Source: http://bretstateham.com/binding-to-the-new-xaml-datepicker-and-timepicker-controls-to-the-same-datetime-value/
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Data.IValueConverter" />
    public class DateTimeToDateTimeOffsetConverter : IValueConverter
    {

        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            try
            {
                DateTime date = (DateTime)value;
                return new DateTimeOffset(date);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error occured in DateTimeToDateTimeOffsetConverter, see Error: " + ex.Message);
                var log = new LogException();
                log.Log(ex);
                return DateTimeOffset.MinValue;
            }
        }

        /// <summary>
        /// Converts back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            try
            {
                DateTimeOffset dto = (DateTimeOffset)value;
                return dto.DateTime;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error occured in DateTimeToDateTimeOffsetConverter, see Error: " + ex.Message);
                var log = new LogException();
                log.Log(ex);
                return DateTime.MinValue;
            }
        }
    }
}
