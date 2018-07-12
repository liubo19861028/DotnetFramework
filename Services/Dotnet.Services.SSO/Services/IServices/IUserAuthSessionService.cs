using Dotnet.Services.SSO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Services.SSO
{
    public interface IUserAuthSessionService
    {
        void Create(UserAuthSession model);

        UserAuthSession Get(string sessionKey);

        UserAuthSession ExistsByValid(string appKey, string userName);

        void ExtendValid(string sessionkey);

        bool GetCache(string sessionKey);
    }
}
