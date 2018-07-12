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
    public class AppInfo:  IEntity<int>
    {
        public int Id { get; set; }

        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        public string Title { get; set; }

        public string Remark { get; set; }

        public string Icon { get; set; }

        public string ReturnUrl { get; set; }

        public bool IsEnable { get; set; }

        public DateTime CreateTime { get; set; }
    }

    public sealed class AppInfoMap : ClassMapper<AppInfo>
    {
        public AppInfoMap()
        {
            Table("AppInfo");
            Map(x => x.Id).Key(KeyType.Identity);
            AutoMap();
        }
    }
}