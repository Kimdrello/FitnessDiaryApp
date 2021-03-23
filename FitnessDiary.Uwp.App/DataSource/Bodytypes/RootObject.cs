using System.Collections.Generic;

namespace FitnessDiary.Uwp.App.DataSource.Bodytypes
{
    /// <summary>
    /// Generated trough json2csharp.com to access RootObject of the GET Request to the REST API https://wger.de/en/software/api
    /// </summary>
    public class RootObject
    {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the next.
        /// </summary>
        /// <value>
        /// The next.
        /// </value>
        public object Next { get; set; }

        /// <summary>
        /// Gets or sets the previous.
        /// </summary>
        /// <value>
        /// The previous.
        /// </value>
        public object Previous { get; set; }

        /// <summary>
        /// Gets or sets the results.
        /// </summary>
        /// <value>
        /// The results.
        /// </value>
        /// CA2227, The get is being used when data is being pulled from the online API
        public List<Result> Results { get; set; }
    }
}
