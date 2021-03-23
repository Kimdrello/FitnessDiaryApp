using System;
using FitnessDiary.Model;
using System.Collections.Generic;

namespace FitnessDiary.Uwp.App.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkoutEvent
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the date time title.
        /// </summary>
        /// <value>
        /// The date time title.
        /// </value>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets the sessions.
        /// </summary>
        /// <value>
        /// The sessions.
        /// </value>
        /// CA2227, This cant be readonly, the set is being used.
        public virtual List<Session> Sessions { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [not old].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [not old]; otherwise, <c>false</c>.
        /// </value>
        public bool NotOld { get; set; }
    }
}
