
namespace FitnessDiary.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// CA1704, Correct the spelling of Bodytype in type name Bodytype. Dont really know what it means, Bodytype is spelled correctly?
    public class Bodytype
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the exercise count.
        /// </summary>
        /// <value>
        /// The exercise count.
        /// </value>
        public int ExerciseCount { get; set; }
    }
}
