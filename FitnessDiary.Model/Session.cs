using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace FitnessDiary.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class Session : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// CA1026, I dont know how to change the method according to the warning
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets the field.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field">The field.</param>
        /// <param name="value">The value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        /// CA1026, CA1045, I dont know how to change the method according to the warning
        protected bool SetField<T>(ref T field, T value,
        [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// The date
        /// </summary>
        [Required]
        private DateTime date;
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date
        {
            get { return date; }
            set
            {
                if(SetField(ref date, value))
                {
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        /// <summary>
        /// The exercise identifier
        /// </summary>
        [Required]
        private Guid exerciseID;
        /// <summary>
        /// Gets or sets the exercise identifier.
        /// </summary>
        /// <value>
        /// The exercise identifier.
        /// </value>
        /// CA1709, Correct ID to Id. I would if I hadnt set up the database yet. Since everything is up and running at this point, Id rather not delete the tables and create the database again just because it wants the ID to be Id.
        public Guid ExerciseID
        {
            get { return exerciseID; }
            set
            {
                if(SetField(ref exerciseID, value))
                {
                    OnPropertyChanged(nameof(ExerciseID));
                }
            }
        }

        /// <summary>
        /// Gets or sets the session identifier.
        /// </summary>
        /// <value>
        /// The session identifier.
        /// </value>
        /// CA1709, Correct ID to Id. I would if I hadnt set up the database yet. Since everything is up and running at this point, Id rather not delete the tables and create the database again just because it wants the ID to be Id.
        [Key]
        public Guid SessionID { get; set; }

        /// <summary>
        /// The sets
        /// </summary>
        private int sets;
        /// <summary>
        /// Gets or sets the sets.
        /// </summary>
        /// <value>
        /// The sets.
        /// </value>
        public int Sets
        {
            get { return sets; }
            set
            {
                if(SetField(ref sets, value))
                {
                    OnPropertyChanged(nameof(Sets));
                }
            }
        }

        /// <summary>
        /// The repetitions
        /// </summary>
        private int repetitions;
        /// <summary>
        /// Gets or sets the repetitions.
        /// </summary>
        /// <value>
        /// The repetitions.
        /// </value>
        public int Repetitions
        {
            get { return repetitions; }
            set
            {
                if(SetField(ref repetitions, value))
                {
                    OnPropertyChanged(nameof(repetitions));
                }
            }
        }

        /// <summary>
        /// The notes
        /// </summary>
        private string notes;
        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>
        /// The notes.
        /// </value>
        public string Notes
        {
            get { return notes; }
            set
            {
                if(SetField(ref notes, value))
                {
                    OnPropertyChanged(nameof(notes));
                }
            }
        }

    }
}
