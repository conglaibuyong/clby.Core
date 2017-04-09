using System;

namespace clby.Core.Misc
{
    public class SecurityExtensions
    {
        public static string HMACSHA1(string input, string key)
        {
            byte[] Data = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] SecretKey = System.Text.Encoding.UTF8.GetBytes(key);
            System.Security.Cryptography.HMACSHA1 hmac = new System.Security.Cryptography.HMACSHA1(SecretKey);
            byte[] digest = hmac.ComputeHash(Data);
            string encodedStr = Convert.ToBase64String(digest);
            encodedStr = encodedStr.Replace('+', '-').Replace('/', '_');
            return encodedStr;
        }
    }
}
