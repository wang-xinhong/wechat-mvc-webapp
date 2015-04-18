using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMAP.Interfaces;

namespace WMAP.Common
{
    /// <summary>
    /// 
    /// </summary>
    public class SimpleWechatConfigManager
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SimpleWechatConfigManager));


        private static SimpleWechatConfigManager instance;

        public SimpleWechatConfigManager()
        {
            if (instance == null)
                instance = new SimpleWechatConfigManager();
        }

        public static SimpleWechatConfigManager GetInstance()
        {
            return instance;
        }


        /// <summary>
        /// 获取指定的AppID对应的相关配置
        /// </summary>
        /// <param name="AppID"></param>
        /// <returns></returns>
        public IWechatConfig FetchSelectedConfig(String AppID)
        {
            if (String.IsNullOrWhiteSpace(AppID))
            {
                throw new ArgumentException(@"AppID");
            }

            return new SimpleWechatConfig();
        }
    }
}
