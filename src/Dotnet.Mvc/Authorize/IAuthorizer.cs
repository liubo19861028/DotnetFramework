using Dotnet.Models;
using Dotnet.Modules.User.Models;

namespace Dotnet.Mvc.Authorize
{
    public interface IAuthorizer
    {
        bool Authorize(string permission);
        bool Authorize(string permission, IUser user);
    }
}