using System.Web.Mvc;
using BLL.Infrastructure.Localization;
using Knoema.Localization;
using Knoema.Localization.Mvc;

namespace SportSocial.Knoema
{
    public class LocalizationConfig
    {
        public static void RegisterBinding()
        {
            LocalizationManager.Repository = new LocalizationRepository();
            ModelValidatorProviders.Providers.Clear();
            ModelValidatorProviders.Providers.Add(new ValidationLocalizer());
            ModelMetadataProviders.Current = new MetadataLocalizer();
        }
    }
}