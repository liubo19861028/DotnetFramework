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
    public class UserAuthSession : IEntity<int>
    {
        public int Id { get; set; }
        public string SessionKey { get; set; }

        public string AppKey { get; set; }

        public string UserName { get; set; }
        
        public string IpAddress { get; set; }

        public DateTime InvalidTime { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public sealed class UserAuthSessionMap : ClassMapper<UserAuthSession>
    {
        public UserAuthSessionMap()
        {
            Table("AppInfo");
            Map(x => x.Id).Key(KeyType.Identity);
            AutoMap();
        }
    }
}