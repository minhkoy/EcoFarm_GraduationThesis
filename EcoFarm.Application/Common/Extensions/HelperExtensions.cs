using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EcoFarm.Application.Common.Extensions
{
    public static class HelperExtensions
    {
        public static long ConvertDateTimeToLong(DateTime dateTime)
        {
            return Convert.ToInt64(dateTime.ToString("yyyyMMddHHmmss"));
        }

        /// <summary>
        /// Convert from long to DateTime. Pre-release
        /// </summary>
        /// <param name="longDatetime"></param>
        /// <returns></returns>
        public static DateTime ConvertLongToDateTime(long longDatetime)
        {
            return DateTime.ParseExact(longDatetime.ToString(), "yyyyMMddHHmmss", null);
        }

        public static string HmacSha256ToHexString(string dataToHash, string key)
        {
            HMACSHA256 hmacSha256 = new HMACSHA256();
            hmacSha256.Key = Encoding.UTF8.GetBytes(key);
            var hashResult = hmacSha256.ComputeHash(Encoding.UTF8.GetBytes(dataToHash));
            return Convert.ToHexString(hashResult);
        }

        /// <summary>
        /// Quick use for string.Format()
        /// </summary>
        /// <param name="originalText"></param>
        /// <param name="prms"></param>
        /// <returns></returns>
        public static string StringAfterFormatting(string originalText, params object[] prms)
        {
            return string.Format(originalText, prms);
        }

        public static int CalculateDayBetweenDates(DateTime startDate, DateTime endDate)
        {
            return (int)(endDate - startDate).TotalDays;
        }

        public static string GetRandomString(int length)
        {
            var seedStr = "0123456789abcdefghijklmnopqrstuvwxyz";
            string result = String.Empty;
            for (int i = 1; i <= length; i++)
            {
                result += seedStr[new Random().Next(seedStr.Length)];
            }

            return result;
        }
    }
}