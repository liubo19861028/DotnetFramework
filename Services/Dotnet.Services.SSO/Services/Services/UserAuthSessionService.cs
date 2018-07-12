using System;
using System.Diagnostics;
using System.Linq;
using Dotnet.Dependency;
using Dotnet.Runtime.Caching;
using Dotnet.Services.SSO.Entities;
using SmartSSO.Services.Models;

namespace Dotnet.Services.SSO
{
    public class UserAuthSessionService : BaseService<UserAuthSession, int>, IUserAuthSessionService
    {
        
        public ICache Cache { get; set; }


        public UserAuthSessionService()
        {
            Cache = IocManager.GetContainer().Resolve<ICache>();
        }

        public UserAuthSession ExistsByValid(string appKey, string userName)
        {
            var currentTime = DateTime.Now;
            return Repository.Single(p => p.AppKey == appKey && p.UserName == userName && p.InvalidTime >= currentTime);
        }

        public void ExtendValid(string sessionkey)
        {
            var model = Repository.Single(p => p.SessionKey == sessionkey);
            if (model != null)
            {
                //延长一年
                model.InvalidTime = DateTime.Now.AddYears(1);
                Repository.Update(model);

                //设置缓存
                Cache.Set(model.SessionKey, new SessionCacheItem
                {
                    AppKey = model.AppKey,
                    InvalidTime = model.InvalidTime,
                    UserName = model.UserName
                });
            }
        }

        public void Create(UserAuthSession model)
        {

            //添加Session
            Repository.Insert(model);

            //设置缓存
            Cache.Set(model.SessionKey, new SessionCacheItem
            {
                AppKey = model.AppKey,
                InvalidTime = model.InvalidTime,
                UserName = model.UserName
            });
        }

        public UserAuthSession Get(string sessionKey)
        {
            return Repository.Single(p => p.SessionKey == sessionKey);
        }

        public bool GetCache(string sessionKey)
        {
            var obj = Cache.GetOrDefault(sessionKey);

            SessionCacheItem sessionCacheItem = (SessionCacheItem)obj;

            if (sessionCacheItem == null) return false;

            if (sessionCacheItem.InvalidTime > DateTime.Now)
            {
                return true;
            }

            //移除无效Session缓存
            Cache.Remove(sessionKey);

            return false;
        }
    }
}