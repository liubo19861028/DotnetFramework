using System;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dotnet.Services.Base;
using System.Web.Mvc;
using Dotnet.Services.SSO.Entities;
using Dotnet.Utility;
using Dotnet.Encrypt;
using Microsoft.AspNetCore.HttpOverrides;
using Dotnet.Data;

namespace Dotnet.Services.SSO
{
    public class PassportController : SsoBaseController
    {
        public IAppInfoService _appInfoService { get; set; }
        public IAppUserService _appUserService { get; set; }
        public IUserAuthSessionService _authSessionService { get; set; }
        public IUserAuthOperateService _userAuthOperateService { get; set; }

        //授权登录
        [HttpPost]
        public ActionResult Login(PassportLoginRequest model)
        {
            //获取应用信息
            var appInfo = _appInfoService.Get(model.AppKey);
            if (appInfo == null)
            {
                //应用不存在
                return View(model);
            }
           

            if (ModelState.IsValid == false)
            {
                //实体验证失败
                return View(model);
            }

            //过滤字段无效字符
            model.Trim();

            //获取用户信息
            var userInfo = _appUserService.Get(model.UserName);
            if (userInfo == null)
            {
                //用户不存在
                return View(model);
            }

            if (userInfo.UserPwd != Md5Encryptor.GetMd5(model.Password))
            {
                //密码不正确
                return View(model);
            }
            
            //获取当前未到期的Session
            var currentSession = _authSessionService.ExistsByValid(appInfo.AppKey, userInfo.UserName);
            if (currentSession == null)
            {
                //构建Session
                currentSession = new UserAuthSession
                {
                    AppKey = appInfo.AppKey,
                    CreateTime = DateTime.Now,
                    InvalidTime = DateTime.Now.AddYears(1),
                  //  IpAddress = Request.UserHostAddress,
                    SessionKey = Md5Encryptor.GetMd5(Guid.NewGuid().ToString()),
                    UserName = userInfo.UserName
                };

                //创建Session
                _authSessionService.Create(currentSession);
            }
            else
            {
                //延长有效期，默认一年
                _authSessionService.ExtendValid(currentSession.SessionKey);
            }

            //记录用户授权日志
            _userAuthOperateService.Create(new UserAuthOperate
            {
                CreateTime = DateTime.Now,
                //IpAddress = Request.UserHostAddress, 
                Remark = string.Format("{0} 登录 {1} 授权成功", currentSession.UserName, appInfo.Title),
                SessionKey = currentSession.SessionKey
            });

            var redirectUrl = string.Format("{0}?SessionKey={1}&SessionUserName={2}",
                appInfo.ReturnUrl,
                currentSession.SessionKey,
                userInfo.UserName);
           

            //跳转默认回调页面
            return Redirect(redirectUrl);
        }

        public bool Get(string sessionKey = "", string remark = "")
        {
            if (_authSessionService.GetCache(sessionKey))
            {
                _userAuthOperateService.Create(new UserAuthOperate
                {
                    CreateTime = DateTime.Now,
                   // IpAddress = Request.RequestUri.Host,
                    Remark = string.Format("验证成功-{0}", remark),
                    SessionKey = sessionKey
                });

                return true;
            }

            _userAuthOperateService.Create(new UserAuthOperate
            {
                CreateTime = DateTime.Now,
               // IpAddress = Request.RequestUri.Host,
                Remark = string.Format("验证失败-{0}", remark),
                SessionKey = sessionKey
            });

            return false;
        }

        #region 注销操作
        /* [UserAuthorization]
         public ActionResult LoginOut()
         {
             if (Request.Cookies[UserAuthorizationAttribute.CookieUserName] != null &&
                 !string.IsNullOrWhiteSpace(Request.Cookies[UserAuthorizationAttribute.CookieUserName].Value))
             {
                 //退出登录日志记录操作
                 _manageService.Logout(Request.Cookies[UserAuthorizationAttribute.CookieUserName].Value);

                 Response.Cookies.Add(new HttpCookie(UserAuthorizationAttribute.CookieUserName, string.Empty));
             }

             return RedirectToAction("Login");
         }*/

        #endregion

    }
}
