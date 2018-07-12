using Dotnet.Services.SSO.Entities;
using System;
using System.Linq;

namespace Dotnet.Services.SSO
{
    public class AppInfoService : BaseService<AppInfo, int>, IAppInfoService
    {
        public void Create(AppInfo model)
        {
            Repository.Insert(model);
        }

        public AppInfo Get(string appKey)
        {
           return Repository.Single(p => p.AppKey == appKey);
        }
    }
}