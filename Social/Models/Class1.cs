using System;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.Validation;

namespace Social.Models
{
    public class PrefixlessBodyModelValidator : IBodyModelValidator
    {
        private readonly IBodyModelValidator _innerValidator;

        public PrefixlessBodyModelValidator()
        {
            _innerValidator = new DefaultBodyModelValidator();
        }

        public bool Validate(object model, Type type, ModelMetadataProvider metadataProvider, HttpActionContext actionContext, string keyPrefix)
        {
            // Remove the keyPrefix but otherwise let innerValidator do what it normally does.
            return _innerValidator.Validate(model, type, metadataProvider, actionContext, String.Empty);
        }
    }
}