using System;

namespace clby.Core.Misc
{
    public class Base64Extensions
    {
        public static string ToUrlSafeBase64String(string input)
        {
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            string encodedStr = Convert.ToBase64String(bs);
            encodedStr = encodedStr.Replace('+', '-').Replace('/', '_');
            return encodedStr;
        }
        public static string FromUrlSafeBase64String(string input)
        {
            input = input.Replace('-', '+').Replace('_', '/');
            byte[] bs = Convert.FromBase64String(input);
            return System.Text.Encoding.UTF8.GetString(bs);
        }
    }
}
