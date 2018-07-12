using Dotnet.Services.SSO.Entities;

namespace Dotnet.Services.SSO
{
    public class UserAuthOperateService : BaseService<UserAuthOperate, int>, IUserAuthOperateService
    {
        public void Create(UserAuthOperate model)
        {
            Repository.Insert(model);
        }
    }
}