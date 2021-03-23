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
    public class Exercise : INotifyPropertyChanged
    {
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
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
        /// The title
        /// </summary>
        [Required]
        private string title;
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get { return title; }
            set
            {
                if (SetField(ref title, value))
                {
                    OnPropertyChanged(nameof(Title));
                    OnPropertyChanged(nameof(IsValid));
                }
            }
        }

        /// <summary>
        /// The source
        /// </summary>
        private string source;
        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        /// <value>
        /// The source.
        /// </value>
        public string Source
        {
            get { return source; }
            set
            {
                if(SetField(ref source, value))
                {
                    OnPropertyChanged(nameof(Source));
                }
            }
        }

        /// <summary>
        /// The directions
        /// </summary>
        [Required]
        private string directions;
        /// <summary>
        /// Gets or sets the directions.
        /// </summary>
        /// <value>
        /// The directions.
        /// </value>
        public string Directions
        {
            get { return directions; }
            set
            {
                if (SetField(ref directions, value))
                {
                    OnPropertyChanged(nameof(Directions));
                    OnPropertyChanged(nameof(IsValid));
                }
            }
        }

        /// <summary>
        /// The bodytype identifier
        /// </summary>
        private int bodytypeID;
        /// <summary>
        /// Gets or sets the bodytype identifier.
        /// </summary>
        /// <value>
        /// The bodytype identifier.
        /// </value>
        ///CA1709, Correct ID to Id. I would if I hadnt set up the database yet. Since everything is up and running at this point, Id rather not delete the tables and create the database again just because it wants the ID to be Id.
        ///CA1074, Here it refers to Bodytype being spelled wrong again, its spelled correctly.
        public int BodytypeID
        {
            get { return bodytypeID; }
            set
            {
                if(SetField(ref bodytypeID, value))
                {
                    OnPropertyChanged(nameof(BodytypeID));
                }
            }
        }

        /// <summary>
        /// Gets or sets the exercise identifier.
        /// </summary>
        /// <value>
        /// The exercise identifier.
        /// </value>
        ///CA1709, Correct ID to Id.I would if I hadnt set up the database yet.Since everything is up and running at this point, Id rather not delete the tables and create the database again just because it wants the ID to be Id.
        [Key]
        public Guid ExerciseID { get; set; }

        /// <summary>
        /// The creation date
        /// </summary>
        private DateTime creationDate;
        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        /// <value>
        /// The creation date.
        /// </value>
        public DateTime CreationDate
        {
            get { return creationDate; }
            set
            {
                if(SetField(ref creationDate, value))
                {
                    OnPropertyChanged(nameof(creationDate));
                }
            }
        }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid { get => !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Directions); }
    }
}
