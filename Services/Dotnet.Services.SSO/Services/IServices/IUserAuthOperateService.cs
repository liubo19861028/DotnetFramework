using Dotnet.Services.SSO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Services.SSO
{
    public interface IUserAuthOperateService
    {
        void Create(UserAuthOperate model);
    }
}
