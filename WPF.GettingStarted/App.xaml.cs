namespace WPF.GettingStarted
{
    using System.Windows;
    using Catel.IoC;
    using Catel.Services;
    using Catel.Windows;
    using WPF.GettingStarted.Services;
    using WPF.GettingStarted.ViewModels;
    using WPF.GettingStarted.Views;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Application.Startup"/> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs"/> that contains the event data.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
#if DEBUG
            Catel.Logging.LogManager.AddDebugListener();
#endif

            StyleHelper.CreateStyleForwardersForDefaultStyles();

            var serviceLocator = ServiceLocator.Default;
            serviceLocator.RegisterType<IFamilyService, FamilyService>();

            var uiVisualizerService = serviceLocator.ResolveType<IUIVisualizerService>();
            uiVisualizerService.Register(typeof(PersonViewModel), typeof(PersonWindow));

            base.OnStartup(e);
        }
    }
}