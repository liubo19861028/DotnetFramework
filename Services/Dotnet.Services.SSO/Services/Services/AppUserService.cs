using Dotnet.Services.SSO.Entities;
using System;
using System.Linq;

namespace Dotnet.Services.SSO
{
    public class AppUserService : BaseService<AppUser, int>, IAppUserService
    {
        public AppUser Get(string username = "")
        {
            return Repository.Single(p => p.UserName == username);
        }
    }
}