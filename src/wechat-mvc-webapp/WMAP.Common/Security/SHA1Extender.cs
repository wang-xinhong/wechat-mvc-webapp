using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WMAP.Common.Security
{
    /// <summary>
    /// SHA1 extender
    /// </summary>
    public static class SHA1Extender
    {
        /// <summary>
        /// Compute SHA1
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        public static String ComputeSHA1(this String originalString)
        {
            return ComputeSHA1(originalString, new ASCIIEncoding());
        }

        /// <summary>
        /// Compute SHA1 with Encoding
        /// </summary>
        /// <param name="originalString"></param>
        /// <param name="originalEncoding"></param>
        /// <returns></returns>
        public static String ComputeSHA1(this String originalString, Encoding originalEncoding)
        {
            try
            {
                byte[] dataToHash = originalEncoding.GetBytes(originalString);
                byte[] dataHashed = null;

                using (SHA1 sha = new SHA1CryptoServiceProvider())
                {
                    dataHashed = sha.ComputeHash(dataToHash);
                }

                if (dataHashed == null)
                    throw new ApplicationException();

                var hash = BitConverter.ToString(dataHashed).Replace("-", "");

                return hash.ToLower();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Verify SHA1 HashValue
        /// </summary>
        /// <param name="originalString"></param>
        /// <param name="expectedHashValue"></param>
        /// <returns></returns>
        public static Boolean VerifySHA1(this String originalString, String expectedHashValue)
        {
            return VerifySHA1(originalString, new ASCIIEncoding(), expectedHashValue);
        }

        /// <summary>
        /// Verify SHA1 HashValue With Encoding
        /// </summary>
        /// <param name="originalString"></param>
        /// <param name="originalEncoding"></param>
        /// <param name="expectedHashValue"></param>
        /// <returns></returns>
        public static Boolean VerifySHA1(String originalString, Encoding originalEncoding, String expectedHashValue)
        {
            try
            {
                return originalString.ComputeSHA1(originalEncoding).ToUpper().Equals(expectedHashValue);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
