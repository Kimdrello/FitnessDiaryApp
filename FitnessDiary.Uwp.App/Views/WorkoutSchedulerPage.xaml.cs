using FitnessDiary.Model;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using FitnessDiary.Uwp.App.DataSource.ExceptionLogs;
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FitnessDiary.Uwp.App.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.Page" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class WorkoutSchedulerPage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WorkoutSchedulerPage"/> class.
        /// </summary>
        public WorkoutSchedulerPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        /// <summary>
        /// Adds the session.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// CA1704, the parameter e is a suggested parameter which is why its being used.
        public async void AddSession(object sender, RoutedEventArgs e)
        {
            Session session = new Session() { SessionID = Guid.NewGuid() };
            datePicker.MinDate = DateTime.Today;
            addSessionContentDialog.PrimaryButtonText = "Add";
            try
            {
                Busy.SetBusy(true, "Preparing template...");
                addSessionContentDialog.DataContext = session;
                await ViewModel.LoadExercises();
                exercisePicker.ItemsSource = ViewModel.Exercises;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error occured when preparing the template, see Error: " + ex.Message);
                var log = new LogException();
                log.Log(ex);
            }
            finally
            {
                Busy.SetBusy(false);
            }

            var result = await addSessionContentDialog.ShowAsync();
            if (result == ContentDialogResult.Primary && exercisePicker.SelectedItem != null)
            {
                try
                {
                    Busy.SetBusy(true, "Adding session...");
                    session.ExerciseID = ((Exercise)exercisePicker.SelectedItem).ExerciseID;
                    if (await DataSource.Sessions.Sessions.Instance.AddSession(session))
                    {
                        ViewModel.Sessions.Add(session);
                        ViewModel.CreateWorkoutEvents();
                        sessionList.ItemsSource = ViewModel.WorkoutEvents;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error occured while trying to add session, see Error: " + ex.Message);
                    var log = new LogException();
                    log.Log(ex);
                }
                finally
                {
                    Busy.SetBusy(false);
                }
            }
        }

        /// <summary>
        /// Edits the session.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// CA1704, the parameter e is a suggested parameter which is why its being used.
        public async void EditSession(object sender, RoutedEventArgs e)
        {
            if(sender != null)
            {
                Button editButton = (Button)sender;
                Session sessionToBeEdited = new Session() { SessionID = (Guid)editButton.CommandParameter };
                datePicker.MinDate = DateTime.Today;
                addSessionContentDialog.PrimaryButtonText = "Save";
                try
                {
                    Busy.SetBusy(true, "Preparing template...");
                    addSessionContentDialog.DataContext = sessionToBeEdited;
                    await ViewModel.LoadExercises();
                    exercisePicker.ItemsSource = ViewModel.Exercises;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error occured when preparing the template, see Error: " + ex.Message);
                    var log = new LogException();
                    log.Log(ex);
                }
                finally
                {
                    Busy.SetBusy(false);
                }
                var result = await addSessionContentDialog.ShowAsync();
                if (result == ContentDialogResult.Primary && exercisePicker.SelectedItem != null)
                {
                    try
                    {
                        sessionToBeEdited.ExerciseID = ((Exercise)exercisePicker.SelectedItem).ExerciseID;
                        Busy.SetBusy(true, "Updating session...");
                        if (await DataSource.Sessions.Sessions.Instance.UpdateSession(sessionToBeEdited))
                        {
                            await ViewModel.UpdateSessions();
                            sessionList.ItemsSource = ViewModel.WorkoutEvents;
                        }

                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Error occured while trying to update session, see Error: " + ex.Message);
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

        /// <summary>
        /// Deletes the session.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        /// CA1704, the parameter e is a suggested parameter which is why its being used.
        public async void DeleteSession(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                Button deleteButton = (Button)sender;
                Session sessionToBeDeleted = new Session() { SessionID = (Guid)deleteButton.CommandParameter };
                try
                {
                    Busy.SetBusy(true, "Deleting session...");
                    if (await DataSource.Sessions.Sessions.Instance.DeleteSession(sessionToBeDeleted))
                    {
                        await ViewModel.UpdateSessions();
                        sessionList.ItemsSource = ViewModel.WorkoutEvents;
                    }    
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error occured when deleting session, see Error: " + ex.Message);
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
}
