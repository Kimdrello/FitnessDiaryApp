using System;
using FitnessDiary.Model;
using Windows.UI.Xaml.Controls;
using System.Diagnostics;
using Template10.Services.NavigationService;
using FitnessDiary.Uwp.App.DataSource.ExceptionLogs;

namespace FitnessDiary.Uwp.App.Views
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.Page" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ItemClickEventArgs"/> instance containing the event data.</param>
        /// CA1704, Bodytype is correctly spelled
        /// CA1704, the parameter e is a suggested parameter which is why its being used.
        public void BodytypeButtonClick(object sender, ItemClickEventArgs e)
        {
            if (e != null)
            {
                var navService = NavigationService.GetForFrame(Frame);
                navService.Navigate(typeof(ExerciseCategoryPage), ((Bodytype)e.ClickedItem).Id.ToString());
            }
        }
    }
}