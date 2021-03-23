using System;
using FitnessDiary.Model;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using Template10.Services.NavigationService;
using FitnessDiary.Uwp.App.Models;
using FitnessDiary.Uwp.App.DataSource.ExceptionLogs;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FitnessDiary.Uwp.App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExerciseCategoryPage : Page
    {
        public ExerciseCategoryPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        /// <summary>
        /// Exercises the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ItemClickEventArgs"/> instance containing the event data.</param>
        /// CA1704, the parameter e is a suggested parameter which is why its being used.
        public void ExerciseButtonClick(object sender, ItemClickEventArgs e)
        {
            if (e != null)
            {
                var navService = NavigationService.GetForFrame(Frame);
                navService.Navigate(typeof(ExercisePage), (Exercise)e.ClickedItem);
            }
        }

        /// <summary>
        /// Called when [exercise on click].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ItemClickEventArgs"/> instance containing the event data.</param>
        /// CA1704, the parameter e is a suggested parameter which is why its being used.
        public void OnlineExerciseOnClick(object sender, ItemClickEventArgs e)
        {
            if (e != null)
            {
                OnlineExercise onlineExercise = new OnlineExercise { Exercise = (Exercise)e.ClickedItem };
                var navService = NavigationService.GetForFrame(Frame);
                navService.Navigate(typeof(ExercisePage), onlineExercise);
            }
        }
    }
}
