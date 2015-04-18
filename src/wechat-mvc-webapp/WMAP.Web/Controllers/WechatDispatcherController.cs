using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using WMAP.Common;

namespace WMAP.Web.Controllers
{
    /// <summary>
    /// Dispatcher For Wechat
    /// </summary>
    public class WechatDispatcherController : ApiController
    {
        public ActionResult Get()
        {
            return new JsonResult() { Data = 5 };
        }

        public ActionResult Post()
        {
            return new JsonResult() { ContentEncoding = new UTF8Encoding(false), Data = 6 };
        }

        public IHttpActionResult Get(String AppID)
        {
            SimpleWechatConfig config = new SimpleWechatConfig();

            return NotFound();
        }

        public IHttpActionResult Post(String AppID)
        {


            return NotFound();
        }
    }
}
