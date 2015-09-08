using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.GettingStarted.Models
{
    using System.Collections.ObjectModel;
    using Catel.Data;

    public class Family : ModelBase
    {
        /// <summary>
        /// Gets or sets the family name.
        /// </summary>
        public string FamilyName
        {
            get { return GetValue<string>(FamilyNameProperty); }
            set { SetValue(FamilyNameProperty, value); }
        }

        /// <summary>
        /// Register the FamilyName property so it is known in the class.
        /// </summary>
        public static readonly PropertyData FamilyNameProperty = RegisterProperty("FamilyName", typeof(string), null);

        /// <summary>
        /// Gets or sets the list of persons in this family.
        /// </summary>
        public ObservableCollection<Person> Persons
        {
            get { return GetValue<ObservableCollection<Person>>(PersonsProperty); }
            set { SetValue(PersonsProperty, value); }
        }

        /// <summary>
        /// Register the Persons property so it is known in the class.
        /// </summary>
        public static readonly PropertyData PersonsProperty = RegisterProperty("Persons", typeof(ObservableCollection<Person>), () => new ObservableCollection<Person>());

        protected override void ValidateFields(List<IFieldValidationResult> validationResults)
        {
            if (string.IsNullOrWhiteSpace(FamilyName))
            {
                validationResults.Add(FieldValidationResult.CreateError(FamilyNameProperty, "The family name is required"));
            }
        }

        public override string ToString()
        {
            return FamilyName;
        }
    }
}
