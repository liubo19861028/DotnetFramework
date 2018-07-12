using System;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dotnet.Services.Base;

namespace Dotnet.Services.SSO
{
    [RoutePrefix("api/Account")]
    public class AccountController : BaseApiController
    {

        [HttpPost]
        [Route("VerifyMobile")]
        public IHttpActionResult VerifyMobile()
        {
            return Ok();
        }

       

    }
}
