using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WMAP.Common.Security
{
    /// <summary>
    /// T spec Rijndael
    /// </summary>
    public static class TRijndaelExtender
    {
        #region Rijndael Settings

        /// <summary>
        /// keysize 256 * 8 = 2048
        /// </summary>
        private const int KeySize = 256;

        /// <summary>
        /// blocksize 128
        /// </summary>
        private const int BlockSize = 128;

        /// <summary>
        /// cipher mode is ChainBlockCipher
        /// </summary>
        private const CipherMode CiferMode = CipherMode.CBC;

        /// <summary>
        /// padding mode is none
        /// </summary>
        private const PaddingMode Padding = PaddingMode.None;

        #endregion

        /// <summary>
        /// Tencent's Pkcs7 Padding
        /// </summary>
        /// <param name="text_length"></param>
        /// <returns></returns>
        private static byte[] TPKCS7Padding(int text_length)
        {
            int block_size = 32;

            // 计算需要填充的位数
            int amount_to_pad = block_size - (text_length % block_size);
            if (amount_to_pad == 0)
            {
                amount_to_pad = block_size;
            }

            StringBuilder tmp = new StringBuilder();
            for (int index = 0; index < amount_to_pad; index++)
            {
                tmp.Append(amount_to_pad.ConvertToChar());
            }
            return Encoding.UTF8.GetBytes(tmp.ToString());
        }

        /// <summary>
        /// 将数字转化成ASCII码对应的字符，用于对明文进行补码
        /// </summary>
        /// <param name="a">需要转化的数字</param>
        /// <returns>转化得到的字符</returns>
        private static char ConvertToChar(this int a)
        {

            byte target = (byte)(a & 0xFF);
            return (char)target;
        }

    }
}
