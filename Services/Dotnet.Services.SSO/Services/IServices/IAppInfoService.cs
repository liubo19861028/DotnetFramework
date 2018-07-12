using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dotnet.Services.SSO.Entities;

namespace Dotnet.Services.SSO
{
    public interface IAppInfoService
    {
        void Create(AppInfo model);

        AppInfo Get(string appKey);
    }
}
