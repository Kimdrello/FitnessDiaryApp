using System;
using FitnessDiary.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using FitnessDiary.Uwp.App.DataSource.ExceptionLogs;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FitnessDiary.Uwp.App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.Page" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class ExercisePage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExercisePage"/> class.
        /// </summary>
        public ExercisePage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        /// <summary>
        /// Opens a editable list.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// CA1704, the parameter e is a suggested parameter which is why its being used.
        public void EditButtonOnClick(object sender, RoutedEventArgs e)
        {
            if(exerciseList.Visibility == Visibility.Visible && editExercise.Visibility == Visibility.Collapsed)
            {
                exerciseList.Visibility = Visibility.Collapsed;
                editExercise.Visibility = Visibility.Visible;
                cancelButton.IsEnabled = true;
                editButton.IsEnabled = false;
            }
        }

        /// <summary>
        /// Cancels editing.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// CA1704, the parameter e is a suggested parameter which is why its being used.
        public void CancelButtonOnClick(object sender, RoutedEventArgs e)
        {
            if(exerciseList.Visibility == Visibility.Collapsed && editExercise.Visibility == Visibility.Visible)
            {
                exerciseList.Visibility = Visibility.Visible;
                editExercise.Visibility = Visibility.Collapsed;
                cancelButton.IsEnabled = false;
                editButton.IsEnabled = true;
            }
        }

        /// <summary>
        /// Saves the exercise.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// CA1704, the parameter e is a suggested parameter which is why its being used.
        public async void SaveButtonOnClick(object sender, RoutedEventArgs e)
        {
            
            var exerciseOld = (Exercise)editExercise.DataContext;

            if(editBodyTypeComboBox.SelectedItem != null)
            {
                var newExercise = new Exercise()
                {
                    Title = editTitleTextBox.Text,
                    ExerciseID = exerciseOld.ExerciseID,
                    BodytypeID = ((Bodytype)editBodyTypeComboBox.SelectedItem).Id,
                    Source = editSourceTextBox.Text,
                    Directions = editDirectionsTextBox.Text,
                    CreationDate = exerciseOld.CreationDate
                };

                try
                {
                    Busy.SetBusy(true, "Updating exercise...");
                    if (await DataSource.Exercises.AddedExercises.Instance.UpdateExercise(newExercise))
                    {
                        ViewModel.Exercise = newExercise;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error occured when updating exercise, see Error: " + ex.Message);
                    var log = new LogException();
                    log.Log(ex);
                }
                finally
                {
                    Busy.SetBusy(false);
                    editButton.IsEnabled = true;
                    exerciseList.Visibility = Visibility.Visible;
                    editExercise.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Adds the exercise from online API to donau.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// CA1704, the parameter e is a suggested parameter which is why its being used.
        public async void AddButtonOnClick(object sender, RoutedEventArgs e)
        {
            var exercise = (Exercise)editExercise.DataContext;

            try
            {
                Busy.SetBusy(true, "Adding exercise...");
                if (await DataSource.Exercises.AddedExercises.Instance.AddExercise(exercise))
                    ViewModel.GotoMainPage();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error occured when adding a online exercise, see Error: " + ex.Message);
                var log = new LogException();
                log.Log(ex);
            }
            finally
            {
                Busy.SetBusy(false);
            }
        }
    }
}
