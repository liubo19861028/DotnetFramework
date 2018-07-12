using Dotnet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using DapperExtensions;
using DapperExtensions.Mapper;

namespace Dotnet.Services.SSO.Entities
{
    public partial class AppUser : IEntity<int>
    {
        public int Id { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string UserPwd { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string Nick { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    public sealed class AppUserMap : ClassMapper<AppUser>
    {
        public AppUserMap()
        {
            Table("AppUser");
            Map(x => x.Id).Key(KeyType.Identity);
            AutoMap();
        }
    }
}