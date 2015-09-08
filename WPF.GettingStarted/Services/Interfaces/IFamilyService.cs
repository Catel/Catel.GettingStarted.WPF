namespace WPF.GettingStarted.Services
{
    using System.Collections.Generic;
    using WPF.GettingStarted.Models;

    public interface IFamilyService
    {
        IEnumerable<Family> LoadFamilies();

        void SaveFamilies(IEnumerable<Family> families);
    }
}
