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
using System.Windows.Input;
using FitnessDiary.Uwp.App.Models;
using FitnessDiary.Uwp.App.DataSource.ExceptionLogs;

namespace FitnessDiary.Uwp.App.ViewModels
{
    public class DeleteExerciseCommand : ICommand
    {
        private ExercisePageViewModel _viewModel;

        public DeleteExerciseCommand(ExercisePageViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => parameter != null;

        public async void Execute(object parameter)
        {
            Debug.WriteLine("Parameter: " + parameter);
            if (CanExecute(parameter))
            {
                try
                {
                    Views.Busy.SetBusy(true, "Deleting exercise...");
                    if (await DataSource.Exercises.AddedExercises.Instance.DeleteExercise((Exercise)parameter))
                        _viewModel.GotoMainPage();
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("Error occured when deleting exercise, see Error: " + ex.Message);
                    var log = new LogException();
                    log.Log(ex);
                }
                finally
                {
                    Views.Busy.SetBusy(false);
                }
            }
        }
    }

    public class ExercisePageViewModel : ViewModelBase
    {
        public ExercisePageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
            DeleteExerciseCommand = new DeleteExerciseCommand(this);
        }

        string _Value = "Gas";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        private Exercise _exercise;

        public Exercise Exercise { get { return _exercise; } set { Set(ref _exercise, value); } }

        ObservableCollection<Bodytype> _bodytypes;
        /// <summary>
        /// Gets or sets the bodytypes.
        /// </summary>
        /// <value>
        /// The bodytypes.
        /// </value>
        /// CA2227, This cant be read-only
        /// CA1704, Its spelled correctly
        public ObservableCollection<Bodytype> Bodytypes { get { return _bodytypes; } set { Set(ref _bodytypes, value); } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            try
            {
                Views.Busy.SetBusy(true, "Loading exercise...");
                if (parameter != null)
                {
                    Exercise exerciseParameter;
                    if (parameter is OnlineExercise)
                    {
                        exerciseParameter = ((OnlineExercise)parameter).Exercise;
                        ExerciseIsAddedByUser = false;
                    }
                    else
                    {
                        exerciseParameter = (Exercise)parameter;
                        ExerciseIsAddedByUser = true; 
                    }
                    Exercise = new Exercise
                    {
                        Title = exerciseParameter.Title,
                        ExerciseID = exerciseParameter.ExerciseID,
                        BodytypeID = exerciseParameter.BodytypeID,
                        Source = exerciseParameter.Source,
                        Directions = exerciseParameter.Directions,
                        CreationDate = exerciseParameter.CreationDate
                    };
                }
                if (Bodytypes == null)
                {
                    Bodytypes = new ObservableCollection<Bodytype>(await DataSource.Bodytypes.Bodytypes.Instance.GetBodytypes());
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error occured when loading exercises in ExercisePageViewModel, see Error: " + ex.Message);
                var log = new LogException();
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

        public ICommand DeleteExerciseCommand { get; set; }

        private bool _exerciseIsAddedByUser;
        public bool ExerciseIsAddedByUser
        {
            get => _exerciseIsAddedByUser;
            set => Set(ref _exerciseIsAddedByUser, value);
        }

        public bool ExerciseOnline { get => !ExerciseIsAddedByUser; }

        public void GotoMainPage() =>
            NavigationService.Navigate(typeof(Views.MainPage));

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
