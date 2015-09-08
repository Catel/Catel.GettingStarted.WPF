namespace WPF.GettingStarted.Views
{
    using Catel.Windows;

    using ViewModels;

    /// <summary>
    /// Interaction logic for PersonWindow.xaml.
    /// </summary>
    public partial class PersonWindow : DataWindow
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonWindow"/> class.
        /// </summary>
        public PersonWindow()
            : this(null) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonWindow"/> class.
        /// </summary>
        /// <param name="viewModel">The view model to inject.</param>
        /// <remarks>
        /// This constructor can be used to use view-model injection.
        /// </remarks>
        public PersonWindow(PersonViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
