using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMAP.Interfaces
{
    /// <summary>
    /// Interface wechatConfiguration
    /// </summary>
    public interface IWechatConfig
    {
        /// <summary>
        /// 微信配置Token信息
        /// </summary>
        string Token
        {
            set;
            get;
        }
        /// <summary>
        /// 微信AppID
        /// </summary>
        string AppID
        {
            set;
            get;
        }
        /// <summary>
        /// 微信EncodingAESKey
        /// </summary>
        string EncodingAESKey
        {
            set;
            get;
        }
    }
}
