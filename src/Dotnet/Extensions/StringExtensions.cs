using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Dotnet.Extensions
{
    public static class StringExtensions
    {
        /// <summary>返回平台无关的Hashcode
        /// </summary>
        public static int GetStringHashcode(this string s)
        {
            if (string.IsNullOrEmpty(s)) return 0;

            unchecked
            {
                int hash = 23;
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (char c in s)
                {
                    hash = (hash << 5) - hash + c;
                }
                if (hash < 0)
                {
                    hash = Math.Abs(hash);
                }
                return hash;
            }
        }

        /// <summary>
        /// 检测对象是否为null,为null则抛出<see cref="ArgumentNullException"/>异常
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="parameterName">参数名</param>
        public static void CheckNull(this object obj, string parameterName)
        {
            if (obj == null)
                throw new ArgumentNullException(parameterName);
        }


        /// <summary>判断String类型是否为空
        /// </summary>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>判断String类型是否为空或者空格
        /// </summary>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }


        /// <summary>截取指定长度的字符串
        /// </summary>
        public static string Left(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(0, len);
        }

        /// <summary>
        /// </summary>
        public static string NormalizeLineEndings(this string str)
        {
            return str.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", Environment.NewLine);
        }

        /// <summary>
        /// </summary>
        public static int NthIndexOf(this string str, char c, int n)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            var count = 0;
            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] != c)
                {
                    continue;
                }

                if ((++count) == n)
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>移除字符串中指定结尾格式的字符
        /// </summary>
        public static string RemovePostFix(this string str, params string[] postFixes)
        {
            if (str.IsNullOrEmpty())
            {
                return null;
            }

            if (postFixes.IsNullOrEmpty())
            {
                return str;
            }

            foreach (var postFix in postFixes)
            {
                if (str.EndsWith(postFix))
                {
                    return str.Left(str.Length - postFix.Length);
                }
            }

            return str;
        }

        /// <summary>移除字符串中指定开始格式的字符
        /// </summary>
        public static string RemovePreFix(this string str, params string[] preFixes)
        {
            if (str.IsNullOrEmpty())
            {
                return null;
            }

            if (preFixes.IsNullOrEmpty())
            {
                return str;
            }

            foreach (var preFix in preFixes)
            {
                if (str.StartsWith(preFix))
                {
                    return str.Right(str.Length - preFix.Length);
                }
            }

            return str;
        }

        /// <summary>截取字符串右侧指定长度的字符串
        /// </summary>
        public static string Right(this string str, int len)
        {
            if (str == null)
            {
                throw new ArgumentNullException("str");
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(str.Length - len, len);
        }

        /// <summary>
        /// Uses string.Split method to split given string by given separator.
        /// </summary>
        public static string[] Split(this string str, string separator)
        {
            return str.Split(new[] { separator }, StringSplitOptions.None);
        }

        /// <summary>
        /// Uses string.Split method to split given string by given separator.
        /// </summary>
        public static string[] Split(this string str, string separator, StringSplitOptions options)
        {
            return str.Split(new[] { separator }, options);
        }

        /// <summary>
        /// Uses string.Split method to split given string by <see cref="Environment.NewLine"/>.
        /// </summary>
        public static string[] SplitToLines(this string str)
        {
            return str.Split(Environment.NewLine);
        }

        /// <summary>
        /// Uses string.Split method to split given string by <see cref="Environment.NewLine"/>.
        /// </summary>
        public static string[] SplitToLines(this string str, StringSplitOptions options)
        {
            return str.Split(Environment.NewLine, options);
        }

        /// <summary>
        /// Converts PascalCase string to camelCase string.
        /// </summary>
        /// <param name="str">String to convert</param>
        /// <returns>camelCase of the string</returns>
        public static string ToCamelCase(this string str) //TODO: Create an overload for invariant culture
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return str;
            }

            if (str.Length == 1)
            {
                return str.ToLowerInvariant();
            }

            return char.ToLowerInvariant(str[0]) + str.Substring(1);
        }

        /// <summary>转换成指定格式枚举
        /// </summary>
        public static T ToEnum<T>(this string value) where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (T)Enum.Parse(typeof(T), value);
        }

        /// <summary>转换成指定格式枚举
        /// </summary>
        public static T ToEnum<T>(this string value, bool ignoreCase) where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        /// <summary>
        /// 安全转换为字符串，去除两端空格，当值为null时返回""
        /// </summary>
        /// <param name="input">输入值</param>
        public static string SafeString(this object input)
        {
            return input == null ? string.Empty : input.ToString().Trim();
        }

        public static string ToFilePath(this string path)
        {
            return string.Join(Path.DirectorySeparatorChar.ToString(), path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar));
        }
        public static string CombinePath(this string p, string path)
        {
            return p + Path.DirectorySeparatorChar + path;
        }


        public static bool IsNotNullAndWhiteSpace(this string instance)
        {
            return !string.IsNullOrWhiteSpace(instance);
        }

        public static string FormatWith(this string instance, params object[] args)
        {
            return string.Format(instance, args);
        }

        public static string NoHTML(this string Htmlstring)
        {
            Htmlstring = Regex.Replace(Htmlstring, @"<script[\s\S]*?</script>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<noscript[\s\S]*?</noscript>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<style[\s\S]*?</style>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<.*?>", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<(.[^>]*)>", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"([\r\n])[\s]+", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"-->", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"<!--.*", " ", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(quot|#34);", "\"", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(amp|#38);", "&", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(lt|#60);", "<", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(gt|#62);", ">", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(nbsp|#160);", "", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(iexcl|#161);", "\xa1", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(cent|#162);", "\xa2", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(pound|#163);", "\xa3", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&(copy|#169);", "\xa9", RegexOptions.IgnoreCase);
            Htmlstring = Regex.Replace(Htmlstring, @"&#(\d+);", " ", RegexOptions.IgnoreCase);
            return Htmlstring;
        }


        public static byte[] ToByte(this string value)
        {
            return System.Text.Encoding.UTF8.GetBytes(value);
        }


        public static string HtmlDecode(this string value)
        {
            return System.Net.WebUtility.HtmlDecode(value);
        }

        public static string HtmlEncode(this string value)
        {
            return System.Net.WebUtility.HtmlEncode(value);
        }

        public static string UrlEncode(this string value)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(value);
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }
            return (sb.ToString());
        }


        public static string ToUnicode(this string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < value.Length; i++)
            {
                builder.Append("\\u" + ((int)value[i]).ToString("x"));
            }
            return builder.ToString();
        }


    }
}
