namespace WPF.GettingStarted.Services
{
    using System.Collections.Generic;
    using System.IO;
    using Catel.Collections;
    using Catel.Data;
    using WPF.GettingStarted.Models;

    public class FamilyService : IFamilyService
    {
        private readonly string _path;

        public FamilyService()
        {
            string directory = Catel.IO.Path.GetApplicationDataDirectory("CatenaLogic", "WPF.GettingStarted");
            Directory.CreateDirectory(directory);

            _path = Path.Combine(directory, "family.xml");
        }

        public IEnumerable<Family> LoadFamilies()
        {
            if (!File.Exists(_path))
            {
                return new Family[] { };
            }

            using (var fileStream = File.Open(_path, FileMode.Open))
            {
                var settings = Settings.Load(fileStream, SerializationMode.Xml);
                return settings.Families;
            }
        }

        public void SaveFamilies(IEnumerable<Family> families)
        {
            var settings = new Settings();
            settings.Families.ReplaceRange(families);
            settings.Save(_path, SerializationMode.Xml);
        }
    }
}
