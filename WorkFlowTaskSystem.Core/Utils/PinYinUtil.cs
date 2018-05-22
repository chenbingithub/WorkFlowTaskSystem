using Microsoft.International.Converters.PinYinConverter;

namespace WorkFlowTaskSystem.Core
{
    /// <summary>
    /// 将汉字转换为拼音
    /// </summary>
   public static class PinYinUtil
    {
        /// <summary>
        /// 返回字符串的简拼
        /// </summary>
        /// <param name="inputTxt"></param>
        /// <returns></returns>
        public static string GetSimplePinYin(string inputTxt)
        {
            string shortR = "";
            foreach (char c in inputTxt.Trim())
            {
                if (ChineseChar.IsValidChar(c))
                {
                    ChineseChar chineseChar = new ChineseChar(c);
                    shortR += chineseChar.Pinyins[0].Substring(0, 1).ToLower();
                }
                else
                {
                    shortR += c;
                }

            }
            return shortR;
        }

        /// <summary>
        /// 返回字符串全拼 
        /// </summary>
        /// <param name="inputTxt"></param>
        /// <returns></returns>
        public static string GetAllPinYin(string inputTxt)
        {
            string allR = "";
            foreach (char c in inputTxt.Trim())
            {
                if (ChineseChar.IsValidChar(c))
                {
                    ChineseChar chineseChar = new ChineseChar(c);
                    allR += chineseChar.Pinyins[0].Substring(0, chineseChar.Pinyins[0].Length - 1).ToLower();
                }
                else
                {
                    allR += c;
                }
                
            }
            return allR;
        }
    }
}
