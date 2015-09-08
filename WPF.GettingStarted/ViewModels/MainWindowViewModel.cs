using System.Threading.Tasks;

namespace WPF.GettingStarted.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using Catel;
    using Catel.Collections;
    using Catel.Data;
    using Catel.IoC;
    using Catel.MVVM;
    using Catel.Services;
    using WPF.GettingStarted.Models;
    using WPF.GettingStarted.Services;

    /// <summary>
    /// MainWindow view model.
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IFamilyService _familyService;
        private readonly IUIVisualizerService _uiVisualizerService;
        private readonly IMessageService _messageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel(IFamilyService familyService, IUIVisualizerService uiVisualizerService, IMessageService messageService)
        {
            Argument.IsNotNull(() => familyService);
            Argument.IsNotNull(() => uiVisualizerService);
            Argument.IsNotNull(() => messageService);

            _familyService = familyService;
            _uiVisualizerService = uiVisualizerService;
            _messageService = messageService;

            AddFamily = new TaskCommand(OnAddFamilyExecuteAsync);
            EditFamily = new TaskCommand(OnEditFamilyExecute, OnEditFamilyCanExecute);
            RemoveFamily = new TaskCommand(OnRemoveFamilyExecute, OnRemoveFamilyCanExecute);
        }

        #region Properties
        /// <summary>
        /// Gets the title of the view model.
        /// </summary>
        /// <value>The title.</value>
        public override string Title { get { return "WPF Getting Started example"; } }

        /// <summary>
        /// Gets the families.
        /// </summary>
        public ObservableCollection<Family> Families
        {
            get { return GetValue<ObservableCollection<Family>>(FamiliesProperty); }
            private set { SetValue(FamiliesProperty, value); }
        }

        /// <summary>
        /// Register the Families property so it is known in the class.
        /// </summary>
        public static readonly PropertyData FamiliesProperty = RegisterProperty("Families", typeof(ObservableCollection<Family>), null);

        /// <summary>
        /// Gets the filtered families.
        /// </summary>
        public ObservableCollection<Family> FilteredFamilies
        {
            get { return GetValue<ObservableCollection<Family>>(FilteredFamiliesProperty); }
            private set { SetValue(FilteredFamiliesProperty, value); }
        }

        /// <summary>
        /// Register the FilteredFamilies property so it is known in the class.
        /// </summary>
        public static readonly PropertyData FilteredFamiliesProperty = RegisterProperty("FilteredFamilies", typeof(ObservableCollection<Family>));

        /// <summary>
        /// Gets or sets the search filter.
        /// </summary>
        public string SearchFilter
        {
            get { return GetValue<string>(SearchFilterProperty); }
            set { SetValue(SearchFilterProperty, value); }
        }

        /// <summary>
        /// Register the SearchFilter property so it is known in the class.
        /// </summary>
        public static readonly PropertyData SearchFilterProperty = RegisterProperty("SearchFilter", typeof(string), null, 
            (sender, e) => ((MainWindowViewModel)sender).UpdateSearchFilter());

        /// <summary>
        /// Gets or sets the selected family.
        /// </summary>
        public Family SelectedFamily
        {
            get { return GetValue<Family>(SelectedFamilyProperty); }
            set { SetValue(SelectedFamilyProperty, value); }
        }

        /// <summary>
        /// Register the SelectedFamily property so it is known in the class.
        /// </summary>
        public static readonly PropertyData SelectedFamilyProperty = RegisterProperty("SelectedFamily", typeof(Family), null);
        #endregion

        #region Commands
        /// <summary>
        /// Gets the AddFamily command.
        /// </summary>
        public TaskCommand AddFamily { get; private set; }

        /// <summary>
        /// Method to invoke when the AddFamily command is executed.
        /// </summary>
        private async Task OnAddFamilyExecuteAsync()
        {
            var family = new Family();

            // Note that we use the type factory here because it will automatically take care of any dependencies
            // that the FamilyWindowViewModel will add in the future
            var typeFactory = this.GetTypeFactory();
            var familyWindowViewModel = typeFactory.CreateInstanceWithParametersAndAutoCompletion<FamilyWindowViewModel>(family);
            if (await _uiVisualizerService.ShowDialogAsync(familyWindowViewModel) ?? false)
            {
                Families.Add(family);

                UpdateSearchFilter();
            }
        }

        /// <summary>
        /// Gets the EditFamily command.
        /// </summary>
        public TaskCommand EditFamily { get; private set; }

        /// <summary>
        /// Method to check whether the EditFamily command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnEditFamilyCanExecute()
        {
            return SelectedFamily != null;
        }

        /// <summary>
        /// Method to invoke when the EditFamily command is executed.
        /// </summary>
        private async Task OnEditFamilyExecute()
        {
            // Note that we use the type factory here because it will automatically take care of any dependencies
            // that the PersonViewModel will add in the future
            var typeFactory = this.GetTypeFactory();
            var familyWindowViewModel = typeFactory.CreateInstanceWithParametersAndAutoCompletion<FamilyWindowViewModel>(SelectedFamily);
            await _uiVisualizerService.ShowDialogAsync(familyWindowViewModel);
        }

        /// <summary>
        /// Gets the RemoveFamily command.
        /// </summary>
        public TaskCommand RemoveFamily { get; private set; }

        /// <summary>
        /// Method to check whether the RemoveFamily command can be executed.
        /// </summary>
        /// <returns><c>true</c> if the command can be executed; otherwise <c>false</c></returns>
        private bool OnRemoveFamilyCanExecute()
        {
            return SelectedFamily != null;
        }

        /// <summary>
        /// Method to invoke when the RemoveFamily command is executed.
        /// </summary>
        private async Task OnRemoveFamilyExecute()
        {
            if (await _messageService.ShowAsync(string.Format("Are you sure you want to delete the family '{0}'?", SelectedFamily),
                "Are you sure?", MessageButton.YesNo, MessageImage.Question) == MessageResult.Yes)
            {
                Families.Remove(SelectedFamily);
                SelectedFamily = null;
            }
        }

        /// <summary>
        /// Updates the filtered items.
        /// </summary>
        private void UpdateSearchFilter()
        {
            if (FilteredFamilies == null)
            {
                FilteredFamilies = new ObservableCollection<Family>();
            }

            if (string.IsNullOrWhiteSpace(SearchFilter))
            {
                FilteredFamilies.ReplaceRange(Families);
            }
            else
            {
                var lowerSearchFilter = SearchFilter.ToLower();

                FilteredFamilies.ReplaceRange(from family in Families
                                                where !string.IsNullOrWhiteSpace(family.FamilyName) && family.FamilyName.ToLower().Contains(lowerSearchFilter)
                                                select family);
            }
        }
        #endregion

        #region Methods

        protected override async Task InitializeAsync()
        {
            var families = _familyService.LoadFamilies();

            Families = new ObservableCollection<Family>(families);

            UpdateSearchFilter();
        }

        protected override async Task CloseAsync()
        {
            _familyService.SaveFamilies(Families);
        }

        #endregion
    }
}
