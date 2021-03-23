using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FitnessDiary.Model;
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
    public sealed partial class AddExercisePage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddExercisePage"/> class.
        /// </summary>
        public AddExercisePage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        /// <summary>
        /// Refreshes the button on click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// CA1704, the parameter e is a suggested parameter which is why its being used.
        public void RefreshButtonOnClick(object sender, RoutedEventArgs e)
        {
            webViewWindow.Navigate(new Uri("https://www.google.com/search?q=" + URLTextBox.Text));
        }

        /// <summary>
        /// Adds the exercise.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// CA1704, the parameter e is a suggested parameter which is why its being used.
        public async void AddExercise(object sender, RoutedEventArgs e)
        {
            if (bodyTypeComboBox.SelectedItem != null)
            {
                var newExercise = new Exercise()
                {
                    Title = titleTextBox.Text,
                    Source = sourceTextBox.Text,
                    BodytypeID = ((Bodytype)bodyTypeComboBox.SelectedItem).Id,
                    CreationDate = DateTime.Today,
                    Directions = directionsTextBox.Text,
                    ExerciseID = Guid.NewGuid(),
                };
                try
                {
                    Busy.SetBusy(true, "Adding exercise...");
                    if (await DataSource.Exercises.AddedExercises.Instance.AddExercise(newExercise))
                        ViewModel.GotoMainPage();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error occured when adding a new exercise, see Error: " + ex.Message);
                    LogException log = new LogException();
                    log.Log(ex);
                }
                finally
                {
                    Busy.SetBusy(false);
                }
            } 
        }
    }
}
