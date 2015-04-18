using log4net;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using WMAP.Common;
using WMAP.Web.HttpUtil;

namespace WMAP.Web.Controllers
{
    /// <summary>
    /// Dispatcher For Wechat
    /// </summary>
    public class WechatDispatcherController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(WechatDispatcherController));

        public ActionResult Get()
        {
            return new JsonResult() { Data = 5 };
        }

        public ActionResult Post()
        {
            return new JsonResult() { ContentEncoding = new UTF8Encoding(false), Data = 6 };
        }

        public IHttpActionResult Get(String AppID, [FromUri] String signature, [FromUri] String echostr, [FromUri] String timestamp, [FromUri]String nonce)
        {
            SimpleWechatConfig config = new SimpleWechatConfig();

            logger.DebugFormat(@"The get AppID is [{0}], echostr is [{1}]", AppID ?? @"Nu1l", echostr ?? @"Nu1l");
            Debug.Print(@"The get AppID is [{0}], echostr is [{1}]", AppID ?? @"Nu1l", echostr ?? @"Nu1l");

            //TODO: verify the sign

            return new TextHttpResult(echostr, this.Request);

            

            //return NotFound();
        }

        public IHttpActionResult Post(String AppID, [FromBody] String request)
        {
            SimpleWechatConfig config = new SimpleWechatConfig();

            logger.DebugFormat(@"The post AppID is [{0}], request is [{1}]", AppID ?? @"Nu1l", request ?? @"Nu1l");
            Debug.Print(@"The post AppID is [{0}], request is [{1}]", AppID ?? @"Nu1l", request ?? @"Nu1l");

            return NotFound();
        }
    }
}
