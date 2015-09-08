namespace WPF.GettingStarted.ViewModels
{
    using System.Collections.ObjectModel;
    using Catel;
    using Catel.Data;
    using Catel.MVVM;
    using WPF.GettingStarted.Models;

    public class FamilyViewModel : ViewModelBase
    {
        public FamilyViewModel(Family family)
        {
            Argument.IsNotNull(() => family);

            Family = family;
        }

        /// <summary>
        /// Gets the family.
        /// </summary>
        [Model]
        public Family Family
        {
            get { return GetValue<Family>(FamilyProperty); }
            private set { SetValue(FamilyProperty, value); }
        }

        /// <summary>
        /// Register the Family property so it is known in the class.
        /// </summary>
        public static readonly PropertyData FamilyProperty = RegisterProperty("Family", typeof(Family), null);

        /// <summary>
        /// Gets the family members.
        /// </summary>
        [ViewModelToModel("Family")]
        public ObservableCollection<Person> Persons
        {
            get { return GetValue<ObservableCollection<Person>>(PersonsProperty); }
            private set { SetValue(PersonsProperty, value); }
        }

        /// <summary>
        /// Register the Persons property so it is known in the class.
        /// </summary>
        public static readonly PropertyData PersonsProperty = RegisterProperty("Persons", typeof(ObservableCollection<Person>), null);

        /// <summary>
        /// Gets or sets the family name.
        /// </summary>
        [ViewModelToModel("Family")]
        public string FamilyName
        {
            get { return GetValue<string>(FamilyNameProperty); }
            set { SetValue(FamilyNameProperty, value); }
        }

        /// <summary>
        /// Register the FamilyName property so it is known in the class.
        /// </summary>
        public static readonly PropertyData FamilyNameProperty = RegisterProperty("FamilyName", typeof(string));
    }
}
