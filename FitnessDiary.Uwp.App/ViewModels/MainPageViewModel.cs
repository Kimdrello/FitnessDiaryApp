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

namespace FitnessDiary.Uwp.App.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
            {
                Value = "Designtime value";
            }
        }

        string _Value = "Gas";
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

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
                if (Bodytypes == null)
                {
                    Views.Busy.SetBusy(true, "Loading bodytypes...");
                    Bodytypes = new ObservableCollection<Bodytype>(await DataSource.Bodytypes.Bodytypes.Instance.GetBodytypes());
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
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

        private bool _isInProgress;
        public bool IsInProgress
        {
            get => _isInProgress;
            set => Set(ref _isInProgress, value);
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

