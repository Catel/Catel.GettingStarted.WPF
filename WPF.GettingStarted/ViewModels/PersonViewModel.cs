namespace WPF.GettingStarted.ViewModels
{
    using Catel;
    using Catel.Data;
    using Catel.MVVM;
    using WPF.GettingStarted.Models;

    public class PersonViewModel : ViewModelBase
    {
        public PersonViewModel(Person person)
        {
            Argument.IsNotNull(() => person);

            Person = person;
        }

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        [Model]
        public Person Person
        {
            get { return GetValue<Person>(PersonProperty); }
            set { SetValue(PersonProperty, value); }
        }

        /// <summary>
        /// Register the Person property so it is known in the class.
        /// </summary>
        public static readonly PropertyData PersonProperty = RegisterProperty("Person", typeof(Person), null);

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [ViewModelToModel("Person")]
        public string FirstName
        {
            get { return GetValue<string>(FirstNameProperty); }
            set { SetValue(FirstNameProperty, value); }
        }

        /// <summary>
        /// Register the FirstName property so it is known in the class.
        /// </summary>
        public static readonly PropertyData FirstNameProperty = RegisterProperty("FirstName", typeof(string), null);

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [ViewModelToModel("Person")]
        public string LastName
        {
            get { return GetValue<string>(LastNameProperty); }
            set { SetValue(LastNameProperty, value); }
        }

        /// <summary>
        /// Register the LastName property so it is known in the class.
        /// </summary>
        public static readonly PropertyData LastNameProperty = RegisterProperty("LastName", typeof(string), null);
    }
}
