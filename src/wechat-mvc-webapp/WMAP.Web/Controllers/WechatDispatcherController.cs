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
using WMAP.Interfaces;
using WMAP.Web.HttpUtil;

namespace WMAP.Web.Controllers
{
    /// <summary>
    /// Dispatcher For Wechat
    /// </summary>
    public class WechatDispatcherController : ApiController
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(WechatDispatcherController));

        /// <summary>
        /// Get Request From Wechat
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="signature"></param>
        /// <param name="echostr"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public IHttpActionResult Get(String AppID, [FromUri] String signature, [FromUri] String echostr, [FromUri] String timestamp, [FromUri]String nonce)
        {
            try
            {
                IWechatConfig config = SimpleWechatConfigManager.GetInstance().FetchSelectedConfig(AppID);

                logger.DebugFormat(@"The get AppID is [{0}], echostr is [{1}]", AppID ?? @"Nu1l", echostr ?? @"Nu1l");

                //TODO: verify the sign

                return new TextHttpResult(echostr, this.Request);

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// POST Request From Wechat
        /// </summary>
        /// <param name="AppID"></param>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public IHttpActionResult Post(String AppID, [FromUri] String signature, [FromUri] String timestamp, [FromUri]String nonce)
        {
            try
            {
                IWechatConfig config = SimpleWechatConfigManager.GetInstance().FetchSelectedConfig(AppID);

                String request = this.Request.Content.ReadAsStringAsync().Result;

                logger.DebugFormat(@"The post AppID is [{0}], request is [{1}]", AppID ?? @"Nu1l", request ?? @"Nu1l");

                //TODO: verify the sign

                return NotFound();

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                return BadRequest();
            }
        }
    }
}
