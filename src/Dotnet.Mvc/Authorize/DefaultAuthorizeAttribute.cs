using Microsoft.AspNetCore.Authorization;

namespace Dotnet.Mvc.Authorize
{
    public class DefaultAuthorizeAttribute : AuthorizeAttribute
    {
        public const string DefaultAuthenticationScheme = "DefaultAuthenticationScheme";
        public DefaultAuthorizeAttribute()
        {
            this.AuthenticationSchemes = DefaultAuthenticationScheme;
        }
    }
}