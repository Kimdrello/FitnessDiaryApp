using System;
using System.Collections.Generic;

namespace FitnessDiary.Uwp.App.DataSource.Exercises
{
    /// <summary>
    /// Generated trough json2csharp.com to access exercises in the GET Request to the REST API https://wger.de/en/software/api
    /// </summary>
    public class Result
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the license author.
        /// </summary>
        /// <value>
        /// The license author.
        /// </value>
        public string LicenseAuthor { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the name original.
        /// </summary>
        /// <value>
        /// The name original.
        /// </value>
        public string Name_Original { get; set; }

        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public string Creation_Date { get; set; }

        /// <summary>
        /// Gets or sets the UUID.
        /// </summary>
        /// <value>
        /// The UUID.
        /// </value>
        /// CA1704, Cant rename or remove this, it has to be spelled this way in order to get exercises from the API online.
        public Guid Uuid { get; set; }

        /// <summary>
        /// Gets or sets the license.
        /// </summary>
        /// <value>
        /// The license.
        /// </value>
        public int License { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public int Category { get; set; }

        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public int Language { get; set; }

        /// <summary>
        /// Gets or sets the muscles.
        /// </summary>
        /// <value>
        /// The muscles.
        /// </value>
        /// CA2227 This cant be read-only, some objects returns a list of muscles.
        public List<object> Muscles { get; set; }

        /// <summary>
        /// Gets or sets the muscles secondary.
        /// </summary>
        /// <value>
        /// The muscles secondary.
        /// </value>
        /// CA2227 This cant be read-only, some objects returns a list of muscles_secondary.
        public List<object> Muscles_Secondary { get; set; }

        /// <summary>
        /// Gets or sets the equipment.
        /// </summary>
        /// <value>
        /// The equipment.
        /// </value>
        /// CA2227 This cant be read-only, some objects returns a list of Equipments.
        public List<object> Equipment { get; set; }
    }
}
