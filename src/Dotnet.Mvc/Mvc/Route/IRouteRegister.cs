
using System.Collections.Generic;

namespace Dotnet.Mvc.Route
{
    public interface IRouteRegister
    {
        IEnumerable<RouteDescriptor> RegistRoute();
    }
}
