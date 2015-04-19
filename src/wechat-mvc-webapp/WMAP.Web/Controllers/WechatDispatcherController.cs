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
using WMAP.Common.Consts;
using WMAP.Common.Security;
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
        /// 是否强制启用signature验证
        /// </summary>
        private const Boolean _FORCE_SIGNATURE_FLAG = false;

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

                logger.DebugFormat(@"The app [{1}] token is [{0}]", config.Token ?? @"Nu1l", AppID ?? @"Nu1l");

                String request = this.Request.Content.ReadAsStringAsync().Result;

                logger.DebugFormat(@"The post AppID is [{0}], signature is [{2}], timestamp is [{3}], request is [{1}]", AppID ?? @"Nu1l", request ?? @"Nu1l", signature ?? @"Nu1l", timestamp ?? @"Nu1l");

                if ( ( !String.IsNullOrEmpty(signature)
                        && !String.IsNullOrEmpty(timestamp)
                        && !String.IsNullOrEmpty(nonce))
                    || _FORCE_SIGNATURE_FLAG)
                {
                    MessageCryptErrorCode retCode = SignatureVerifier.VerifySignature(config.Token, timestamp, nonce, request, signature);

                    if (retCode != MessageCryptErrorCode.WXMsgCrypt_OK)
                        throw new ApplicationException(@"verify signature failed");
                }


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
