using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMAP.Common.Consts
{
    /// <summary>
    /// 微信消息加解密错误码
    /// </summary>
    public enum MessageCryptErrorCode : int
    {
        /// <summary>
        /// OK
        /// </summary>
        WXMsgCrypt_OK = 0,

        /// <summary>
        /// 验证签名错误
        /// </summary>
        WXMsgCrypt_ValidateSignature_Error = -40001,

        /// <summary>
        /// 解析XML错误
        /// </summary>
        WXMsgCrypt_ParseXml_Error = -40002,

        /// <summary>
        /// 计算签名错误
        /// </summary>
        WXMsgCrypt_ComputeSignature_Error = -40003,

        /// <summary>
        /// 错误的AES KEY
        /// </summary>
        WXMsgCrypt_IllegalAesKey = -40004,

        /// <summary>
        /// 错误的APPID
        /// </summary>
        WXMsgCrypt_ValidateAppid_Error = -40005,

        /// <summary>
        /// 使用AES加密错误
        /// </summary>
        WXMsgCrypt_EncryptAES_Error = -40006,

        /// <summary>
        /// 使用AES解密错误
        /// </summary>
        WXMsgCrypt_DecryptAES_Error = -40007,

        /// <summary>
        /// 不正确的Buffer长度(block size/ AES)
        /// </summary>
        WXMsgCrypt_IllegalBuffer = -40008,

        /// <summary>
        /// 使用Base64加密错误
        /// </summary>
        WXMsgCrypt_EncodeBase64_Error = -40009,

        /// <summary>
        /// 使用Base64解密错误
        /// </summary>
        WXMsgCrypt_DecodeBase64_Error = -40010
    }
}
