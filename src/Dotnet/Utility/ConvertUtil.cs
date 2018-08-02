using Dotnet.Extensions;
using System;

namespace Dotnet.Utility
{
    /// <summary> 说明:强制转换
    /// </summary>
    public class ConvertUtil
    {

        /// <summary> 转换成Int32类型
        /// </summary>
        public static int ToInt32(object obj, int defaultValue)
        {
            if (obj != null)
            {
                int.TryParse(obj.ToString(), out defaultValue);
            }
            return defaultValue;
        }

        /// <summary> 转换成Int64类型
        /// </summary>
        public static long ToInt64(object obj, long defaultValue)
        {
	        if (obj != null)
	        {
		        long.TryParse(obj.ToString(), out defaultValue);
	        }
	        return defaultValue;
        }

        /// <summary>转换成Double类型
        /// </summary>
        public static double ToDouble(object obj, double defaultValue)
        {
            if (obj != null)
            {
                double.TryParse(obj.ToString(), out defaultValue);
            }
            return defaultValue;
        }
        /// <summary> 转换成double类型,并保留有效的位数
        /// </summary>
        public static double ToDouble(object obj, double defaultValue, int digit)
        {
            if (obj != null)
            {
                double.TryParse(obj.ToString(), out defaultValue);
                defaultValue = Math.Round(defaultValue, digit);
            }
            return defaultValue;
        }

        /// <summary>转换成Datetime
        /// </summary>
        public static DateTime ToDateTime(object obj, DateTime defaultValue)
        {
            var d1 = defaultValue ;
            if (obj != null)
            {
               var r= DateTime.TryParse(obj.ToString(), out d1);
               if (!r)
               {
                   return defaultValue;
               }
            }
            return d1;
        }

        /// <summary>转换成Bool类型
        /// </summary>
        public static bool ToBool(object obj, bool defaultValue)
        {
            if (obj != null)
            {
                bool.TryParse(obj.ToString(), out defaultValue);
            }
            return defaultValue;
        }

        /// <summary>
        /// 转换为64位整型
        /// </summary>
        /// <param name="input">输入值</param>
        public static long ToLong(object input)
        {
            return ToLongOrNull(input) ?? 0;
        }

        /// <summary>
        /// 转换为64位可空整型
        /// </summary>
        /// <param name="input">输入值</param>
        public static long? ToLongOrNull(object input)
        {
            var success = long.TryParse(input.SafeString(), out var result);
            if (success)
                return result;
            try
            {
                var temp = ToDecimalOrNull(input, 0);
                if (temp == null)
                    return null;
                return System.Convert.ToInt64(temp);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 转换为32位浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static float ToFloat(object input, int? digits = null)
        {
            return ToFloatOrNull(input, digits) ?? 0;
        }

        /// <summary>
        /// 转换为32位可空浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static float? ToFloatOrNull(object input, int? digits = null)
        {
            var success = float.TryParse(input.SafeString(), out var result);
            if (!success)
                return null;
            if (digits == null)
                return result;
            return (float)Math.Round(result, digits.Value);
        }

        /// <summary>
        /// 转换为64位浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static double ToDouble(object input, int? digits = null)
        {
            return ToDoubleOrNull(input, digits) ?? 0;
        }

        /// <summary>
        /// 转换为64位可空浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static double? ToDoubleOrNull(object input, int? digits = null)
        {
            var success = double.TryParse(input.SafeString(), out var result);
            if (!success)
                return null;
            if (digits == null)
                return result;
            return Math.Round(result, digits.Value);
        }

        /// <summary>
        /// 转换为128位浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static decimal ToDecimal(object input, int? digits = null)
        {
            return ToDecimalOrNull(input, digits) ?? 0;
        }

        /// <summary>
        /// 转换为128位可空浮点型,并按指定小数位舍入
        /// </summary>
        /// <param name="input">输入值</param>
        /// <param name="digits">小数位数</param>
        public static decimal? ToDecimalOrNull(object input, int? digits = null)
        {
            var success = decimal.TryParse(input.SafeString(), out var result);
            if (!success)
                return null;
            if (digits == null)
                return result;
            return Math.Round(result, digits.Value);
        }

        /// <summary>
        /// 转换为布尔值
        /// </summary>
        /// <param name="input">输入值</param>
        public static bool ToBool(object input)
        {
            return ToBoolOrNull(input) ?? false;
        }

        /// <summary>
        /// 转换为可空布尔值
        /// </summary>
        /// <param name="input">输入值</param>
        public static bool? ToBoolOrNull(object input)
        {
            bool? value = GetBool(input);
            if (value != null)
                return value.Value;
            return bool.TryParse(input.SafeString(), out var result) ? (bool?)result : null;
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        private static bool? GetBool(object input)
        {
            switch (input.SafeString().ToLower())
            {
                case "0":
                    return false;
                case "否":
                    return false;
                case "不":
                    return false;
                case "no":
                    return false;
                case "fail":
                    return false;
                case "1":
                    return true;
                case "是":
                    return true;
                case "ok":
                    return true;
                case "yes":
                    return true;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 转换为日期
        /// </summary>
        /// <param name="input">输入值</param>
        public static DateTime ToDate(object input)
        {
            return ToDateOrNull(input) ?? DateTime.MinValue;
        }

        /// <summary>
        /// 转换为可空日期
        /// </summary>
        /// <param name="input">输入值</param>
        public static DateTime? ToDateOrNull(object input)
        {
            return DateTime.TryParse(input.SafeString(), out var result) ? (DateTime?)result : null;
        }

        /// <summary>将string类型字符串转换成对应的guid
        /// </summary>
        public static Guid ToGuid(string str)
        {
            return new Guid(str);
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        public static Type GetType<T>()
        {
            return GetType(typeof(T));
        }

        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="type">类型</param>
        public static Type GetType(Type type)
        {
            return Nullable.GetUnderlyingType(type) ?? type;
        }

        /// <summary>
        /// 通用泛型转换
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="input">输入值</param>
        public static T To<T>(object input)
        {
            if (input == null)
                return default(T);
            if (input is string && string.IsNullOrWhiteSpace(input.ToString()))
                return default(T);
            Type type = GetType<T>();
            var typeName = type.Name.ToLower();
            try
            {
                if (typeName == "string")
                    return (T)(object)input.ToString();
                if (typeName == "guid")
                    return (T)(object)new Guid(input.ToString());
                if (type.IsEnum)
                    return EnumUtil.Parse<T>(input);
                if (input is IConvertible)
                    return (T)System.Convert.ChangeType(input, type);
                return (T)input;
            }
            catch
            {
                return default(T);
            }
        }
    }
}
