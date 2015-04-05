using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace WMAP.Web.Controllers
{
    /// <summary>
    /// Default Method For Wechat Server Calls
    /// 
    /// </summary>
    public class WechatDefaultController : ApiController
    {

        public IHttpActionResult Get()
        {
            return this.NotFound();
        }

        public IHttpActionResult Post()
        {
            return this.NotFound();
        }
    }
}
