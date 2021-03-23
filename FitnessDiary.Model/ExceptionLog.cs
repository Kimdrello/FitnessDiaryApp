using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessDiary.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionLog
    {
        /// <summary>
        /// Gets or sets the exception identifier.
        /// </summary>
        /// <value>
        /// The exception identifier.
        /// </value>
        /// CA1709, Correct ID to Id. I would if I hadnt set up the database yet. Since everything is up and running at this point, Id rather not delete the tables and create the database again just because it wants the ID to be Id.
        [Key]
        public Guid ExceptionID { get; set; }

        /// <summary>
        /// Gets or sets the exception message.
        /// </summary>
        /// <value>
        /// The exception message.
        /// </value>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Gets or sets the type of the exception.
        /// </summary>
        /// <value>
        /// The type of the exception.
        /// </value>
        public string ExceptionType { get; set; }

        /// <summary>
        /// Gets or sets the exception source.
        /// </summary>
        /// <value>
        /// The exception source.
        /// </value>
        public string ExceptionSource { get; set; }

        /// <summary>
        /// Gets or sets the date of exception.
        /// </summary>
        /// <value>
        /// The date of exception.
        /// </value>
        public DateTime DateOfException { get; set; }
    }
}
