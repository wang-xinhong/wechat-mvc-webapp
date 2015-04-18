using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMAP.Interfaces;

namespace WMAP.Common
{
    /// <summary>
    /// Simple Implement for interface wechat configuration
    /// </summary>
    public class SimpleWechatConfig : IWechatConfig
    {
        public string Token
        {
            get
            {
                return @"{APPTOKEN}";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string AppID
        {
            get
            {
                return @"{APPID}";
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string EncodingAESKey
        {
            get
            {
                return @"{APPAESKEY}";
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
