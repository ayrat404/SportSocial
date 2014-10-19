using System.Web.Mvc;
using Knoema.Localization;
using Knoema.Localization.EFProvider;
using Knoema.Localization.Mvc;

namespace SportSocial
{
    public class LocalizationConfig
    {
        public static void RegisterBinding()
        {
            LocalizationManager.Repository = new LocalizationRepository("EntityDbContextDebug");
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new ValidationLocalizer());
            ModelMetadataProviders.Current = new MetadataLocalizer();
        }
    }
}