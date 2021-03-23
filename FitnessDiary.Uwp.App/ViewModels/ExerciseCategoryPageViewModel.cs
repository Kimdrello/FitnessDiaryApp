using Template10.Mvvm;
using System.Collections.Generic;
using System;
using FitnessDiary.Model;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.Diagnostics;
using FitnessDiary.Uwp.App.DataSource.ExceptionLogs;

namespace FitnessDiary.Uwp.App.ViewModels
{
    public class ExerciseCategoryPageViewModel : ViewModelBase
    {
        public ExerciseCategoryPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
        }

        string _Value = "Gas";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        ObservableCollection<Exercise> _exercises;
        /// <summary>
        /// Gets or sets the exercises.
        /// </summary>
        /// <value>
        /// The exercises.
        /// </value>
        /// CA2227, This cant be read-only
        public ObservableCollection<Exercise> Exercises { get { return _exercises; } set { Set(ref _exercises, value); } }

        ObservableCollection<Exercise> _addedExercises;
        /// <summary>
        /// Gets or sets the added exercises.
        /// </summary>
        /// <value>
        /// The added exercises.
        /// </value>
        /// CA2227, This cant be read-only
        public ObservableCollection<Exercise> AddedExercises { get { return _addedExercises; } set { Set(ref _addedExercises, value); } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            try
            {
                if (parameter != null)
                {
                    Views.Busy.SetBusy(true, "Loading exercises...");
                    AddedExercises = new ObservableCollection<Exercise>(await DataSource.Exercises.AddedExercises.Instance.GetAddedExercises(parameter.ToString()));
                    Exercises = new ObservableCollection<Exercise>(await DataSource.Exercises.Exercises.Instance.GetExercises(parameter.ToString()));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error occured when loading Exercises in ExerciseCategoryPageViewModel, see Error: " + ex.Message);
                LogException log = new LogException();
                log.Log(ex);
            }
            finally
            {
                Views.Busy.SetBusy(false);
            }
            if (suspensionState.Any())
            {
                Value = suspensionState[nameof(Value)]?.ToString();
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> suspensionState, bool suspending)
        {
            if (suspending)
            {
                suspensionState[nameof(Value)] = Value;
            }
            await Task.CompletedTask;
        }

        public override async Task OnNavigatingFromAsync(NavigatingEventArgs args)
        {
            args.Cancel = false;
            await Task.CompletedTask;
        }

        public void GotoDetailsPage() =>
            NavigationService.Navigate(typeof(Views.DetailPage), Value);

        public void GotoSettings() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 0);

        public void GotoPrivacy() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);

        public void GotoAbout() =>
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);

    }
}

