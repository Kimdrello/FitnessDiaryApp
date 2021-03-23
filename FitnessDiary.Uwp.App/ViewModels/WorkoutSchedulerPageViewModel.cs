using Template10.Mvvm;
using System.Collections.Generic;
using System;
using FitnessDiary.Model;
using FitnessDiary.Uwp.App.Models;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.NavigationService;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using System.Diagnostics;
using FitnessDiary.Uwp.App.DataSource.ExceptionLogs;

namespace FitnessDiary.Uwp.App.ViewModels
{
    public static class MyEnumerable
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            var result = new ObservableCollection<T>();
            foreach (var item in source)
                result.Add(item);
            return result;
        }
        //https://stackoverflow.com/questions/1565289/union-two-observablecollection-lists
    }

    public class WorkoutSchedulerPageViewModel : ViewModelBase
    {
        public WorkoutSchedulerPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
        }

        string _Value = "Gas";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        /// <summary>
        /// The exercises
        /// </summary>
        ObservableCollection<Exercise> _exercises;
        /// <summary>
        /// Gets or sets the exercises.
        /// </summary>
        /// <value>
        /// The exercises.
        /// </value>
        /// CA2227, This cant be read-only
        public ObservableCollection<Exercise> Exercises { get { return _exercises; } set { Set(ref _exercises, value); } }

        ObservableCollection<Session> _sessions;
        ///CA2227, This cant be read-only
        public ObservableCollection<Session> Sessions { get { return _sessions; } set { Set(ref _sessions, value); } }

        List<WorkoutEvent> _workoutEvents;
        ///CA2227, This cant be read-only
        public List<WorkoutEvent> WorkoutEvents { get { return _workoutEvents; } set { Set(ref _workoutEvents, value); } }

        public override async Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> suspensionState)
        {
            try
            {
                Views.Busy.SetBusy(true, "Loading workout events");
                if (Sessions == null)
                {
                    Sessions = new ObservableCollection<Session>(await DataSource.Sessions.Sessions.Instance.GetSessions());
                    CreateWorkoutEvents();
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Error occured when loading Sessions in WorkoutSchedulerPageViewModel, see Error: " + ex.Message);
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

        public async Task LoadExercises()
        {
            if(Exercises == null)
            {
                Exercises = new ObservableCollection<Exercise>();
                ObservableCollection<Exercise> addedExercises = new ObservableCollection<Exercise>(await DataSource.Exercises.AddedExercises.Instance.GetAddedExercises(""));
                ObservableCollection<Exercise> onlineApiExercises = new ObservableCollection<Exercise>(await DataSource.Exercises.Exercises.Instance.GetExercises(""));
                Exercises = addedExercises.Union(onlineApiExercises).ToObservableCollection();
            }
        }

        public void CreateWorkoutEvents()
        {
            WorkoutEvents = new List<WorkoutEvent>();
            DateTime eventDate = DateTime.Today;
            foreach (Session s in Sessions)
            {
                if (s.Date != eventDate)
                {
                    eventDate = s.Date;
                    WorkoutEvent workoutEvent = new WorkoutEvent();
                    workoutEvent.DateTime = eventDate;
                    workoutEvent.Title = eventDate.ToString("dd/MM/yyyy");
                    List<Session> eventSessions = new List<Session>();
                    foreach (Session ss in Sessions)
                    {
                        if (ss.Date == workoutEvent.DateTime)
                            eventSessions.Add(ss);
                    }
                    workoutEvent.Sessions = eventSessions;
                    WorkoutEvents.Add(workoutEvent);
                }
            }
            SortWorkoutEvents(WorkoutEvents);
        }
        /// <summary>
        /// Sorts the workout events.
        /// </summary>
        /// <param name="workoutEvents">The workout events.</param>
        /// CA1822 The "this" parameter isnt used here.
        public void SortWorkoutEvents(List<WorkoutEvent> workoutEvents)
        {
            if (workoutEvents != null)
                workoutEvents.Sort((x, y) => DateTime.Compare(x.DateTime, y.DateTime));

            DateTime today = DateTime.Today;
            foreach(WorkoutEvent we in workoutEvents)
            {
                if (we.DateTime > today) we.NotOld = false;
                else we.NotOld = true;
            }
        }

        public async Task UpdateSessions()
        {
            Sessions = new ObservableCollection<Session>(await DataSource.Sessions.Sessions.Instance.GetSessions());
            CreateWorkoutEvents();
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
