using Dotnet.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dotnet.Test.AutoMapper
{
    [AutoMap(typeof(TestOrder))]
    public class TestUser
    {
        public int UserId { get; set; }


        public string UserName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
