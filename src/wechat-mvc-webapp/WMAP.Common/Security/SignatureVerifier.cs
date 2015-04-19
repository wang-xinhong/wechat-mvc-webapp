using log4net;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMAP.Common.Consts;

namespace WMAP.Common.Security
{
    /// <summary>
    /// verify the signature
    /// </summary>
    public static class SignatureVerifier
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly ILog logger = LogManager.GetLogger(typeof(SignatureVerifier));

        /// <summary>
        /// computer signature
        /// </summary>
        /// <param name="sToken"></param>
        /// <param name="sTimeStamp"></param>
        /// <param name="sNonce"></param>
        /// <param name="sMsgEncrypt"></param>
        /// <param name="sMsgSignature"></param>
        /// <returns></returns>
        public static MessageCryptErrorCode GenarateSinature(string sToken, string sTimeStamp, string sNonce, string sMsgEncrypt, ref string sMsgSignature)
        {
            ArrayList AL = new ArrayList();
            AL.Add(sToken);
            AL.Add(sTimeStamp);
            AL.Add(sNonce);
            AL.Add(sMsgEncrypt);
            AL.Sort(new DictionarySort());
            StringBuilder raw = new StringBuilder();
            for (int i = 0; i < AL.Count; ++i)
            {
                raw.Append(AL[i]);
            }

            try
            {
                var hash = raw.ToString().ComputeSHA1();

                sMsgSignature = hash;

                return MessageCryptErrorCode.WXMsgCrypt_OK;
            }
            catch (Exception)
            {
                return MessageCryptErrorCode.WXMsgCrypt_ComputeSignature_Error;
            }
        }

        public class DictionarySort : System.Collections.IComparer
        {
            public int Compare(object oLeft, object oRight)
            {
                string sLeft = oLeft as string;
                string sRight = oRight as string;
                int iLeftLength = sLeft.Length;
                int iRightLength = sRight.Length;
                int index = 0;
                while (index < iLeftLength && index < iRightLength)
                {
                    if (sLeft[index] < sRight[index])
                        return -1;
                    else if (sLeft[index] > sRight[index])
                        return 1;
                    else
                        index++;
                }
                return iLeftLength - iRightLength;

            }
        }

        /// <summary>
        /// Verify the request signature
        /// </summary>
        /// <param name="sToken"></param>
        /// <param name="sTimeStamp"></param>
        /// <param name="sNonce"></param>
        /// <param name="sMsgEncrypt"></param>
        /// <param name="sSigture"></param>
        /// <returns></returns>
        public static MessageCryptErrorCode VerifySignature(string sToken, string sTimeStamp, string sNonce, string sMsgEncrypt, string sSigture)
        {
            string hash = String.Empty;
            MessageCryptErrorCode ret = MessageCryptErrorCode.WXMsgCrypt_OK;
            ret = GenarateSinature(sToken, sTimeStamp, sNonce, sMsgEncrypt, ref hash);
            if (ret != MessageCryptErrorCode.WXMsgCrypt_OK)
                return ret;

            logger.DebugFormat(@"The Message [{0}] Hash is [{1}]", sTimeStamp ?? @"Nu1l", hash ?? @"Nu1l");
            
            if (hash == sSigture)
                return MessageCryptErrorCode.WXMsgCrypt_OK;
            else
            {
                return MessageCryptErrorCode.WXMsgCrypt_ValidateSignature_Error;
            }
        }

    }
}
