using System;
using System.Collections.Generic;
using System.IO;
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


        /// <summary>
        /// Tencent's Rijndael Encrypt
        /// </summary>
        /// <param name="input"></param>
        /// <param name="iv"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static String AES_encrypt(this byte[] input, byte[] iv, byte[] key)
        {
            using (var aes = new RijndaelManaged() { KeySize = KeySize, BlockSize = BlockSize, Padding = Padding, Mode = CiferMode })
            {
                var encrypt = aes.CreateEncryptor(key, iv);

                byte[] xBuff = null;

                #region 自己进行PKCS7补位，用系统自己带的不行
                byte[] msg = new byte[input.Length + 32 - input.Length % 32];
                Array.Copy(input, msg, input.Length);
                byte[] pad = TPKCS7Padding(input.Length);
                Array.Copy(pad, 0, msg, input.Length, pad.Length);
                #endregion

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                    {
                        cs.Write(msg, 0, msg.Length);
                    }
                    xBuff = ms.ToArray();
                }

                String Output = Convert.ToBase64String(xBuff);
                return Output;
            }

        }

        /// <summary>
        /// Tencent's Rijndael Decrypt
        /// </summary>
        /// <param name="input"></param>
        /// <param name="iv"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static byte[] AES_decrypt(this String input, byte[] iv, byte[] key)
        {
            using (var aes = new RijndaelManaged() { KeySize = KeySize, BlockSize = BlockSize, Padding = Padding, Mode = CiferMode })
            {
                var decrypt = aes.CreateDecryptor(iv, key);
                byte[] xBuff = null;
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                    {
                        byte[] xXml = Convert.FromBase64String(input);
                        byte[] msg = new byte[xXml.Length + 32 - xXml.Length % 32];
                        Array.Copy(xXml, msg, xXml.Length);
                        cs.Write(xXml, 0, xXml.Length);
                    }
                    xBuff = decode2(ms.ToArray());
                }
                return xBuff;

            }
        }

        /// <summary>
        /// decode
        /// </summary>
        /// <param name="decrypted"></param>
        /// <returns></returns>
        private static byte[] decode2(byte[] decrypted)
        {
            int pad = (int)decrypted[decrypted.Length - 1];
            if (pad < 1 || pad > 32)
            {
                pad = 0;
            }
            byte[] res = new byte[decrypted.Length - pad];
            Array.Copy(decrypted, 0, res, 0, decrypted.Length - pad);
            return res;
        }
    }
}
