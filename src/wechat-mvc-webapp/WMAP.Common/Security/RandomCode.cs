using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMAP.Common.Consts;

namespace WMAP.Common.Security
{
    public static class RandomCode
    {
        #region Random Setting

        private const int RandomDefaultLength = 16;

        #endregion

        /// <summary>
        /// Generate Random Code
        /// </summary>
        /// <param name="codeLen"></param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRange"/>
        public static string CreateRandCode(int codeLen = RandomDefaultLength)
        {
            if (codeLen <= 0)
            {
                codeLen = RandomDefaultLength;
            }

            int randValue = -1;
            Random rand = new Random(unchecked((int)(DateTime.Now.Ticks%int.MaxValue)));

            StringBuilder codeBuilder = new StringBuilder();

            for (int i = 0; i < codeLen; i++)
            {
                randValue = rand.Next(0, BaseT32Encoding.CodeBaseT32Dictionary.Length - 1);
                codeBuilder.Append(BaseT32Encoding.CodeBaseT32Dictionary[randValue]);
            }

            return codeBuilder.ToString();
        }

    }
}
