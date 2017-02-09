using System;
using System.Text.RegularExpressions;

namespace clby.Core.Misc
{
    public sealed class ReflectionHelper
    {
        private static readonly Regex _rxGenericTypeNameFinder = new Regex("[^`]+", System.Text.RegularExpressions.RegexOptions.Compiled);
        public static string GetScrubbedGenericName(Type t)
        {
            String retval = t.Name;
            if (t.IsConstructedGenericType)//IsGenericType
            {
                retval = _rxGenericTypeNameFinder.Match(t.Name).Value;
                foreach (var a in t.GenericTypeArguments)//GetGenericArguments()
                {
                    retval += "_" + GetScrubbedGenericName(a);
                }
            }
            return retval;
        }
    }
}
