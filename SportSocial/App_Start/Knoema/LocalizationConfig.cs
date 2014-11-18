using System.Web.Mvc;
using Knoema.Localization;
using Knoema.Localization.Mvc;

namespace SportSocial.Knoema
{
    public class LocalizationConfig
    {
        public static void RegisterBinding()
        {
            #if DEBUG 
            LocalizationManager.Repository = new LocalizationRepository("EntityDbContextDebug");
            #endif

            #if !DEBUG 
            LocalizationManager.Repository = new LocalizationRepository("EntityDbContextRelease");
            #endif

            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new ValidationLocalizer());
            ModelMetadataProviders.Current = new MetadataLocalizer();
        }
    }
}