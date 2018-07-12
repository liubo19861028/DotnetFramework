using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;
using DapperExtensions;
using DapperExtensions.Mapper;
using Dotnet.Data;

namespace Dotnet.Services.SSO.Entities
{
    public class UserAuthOperate : IEntity<int>
    {
        public int Id { get; set; }
        public string SessionKey { get; set; }
        public string Remark { get; set; }
        public string IpAddress { get; set; }
        public DateTime CreateTime { get; set; }
    }

    public sealed class UserAuthOperateMap : ClassMapper<UserAuthOperate>
    {
        public UserAuthOperateMap()
        {
            Table("UserAuthOperate");
            Map(x => x.Id).Key(KeyType.Identity);
            AutoMap();
        }
    }
}