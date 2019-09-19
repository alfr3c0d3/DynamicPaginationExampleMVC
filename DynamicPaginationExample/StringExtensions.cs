using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace DynamicPaginationExample
{
    public static class StringExtensions
    {
        /// <summary>
        /// Creates a string sequence out of an expression applied on an <see cref="IEnumerable{TModel}"/>
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="list">The list of elements to look for</param>
        /// <param name="expression">The expression to apply the sequence</param>
        /// <param name="delimiter">The special character to delimit the sequence. "Space" by default</param>
        /// <returns></returns>
        public static string ToStringSequence<TModel>(this IEnumerable<TModel> list, Func<TModel, string> expression, string delimiter = ", ") => list != null || !list.Any() ? list.Select(expression).Aggregate((current, next) => $"{current}{delimiter}{next}") : "";

        /// <summary>
        /// Returns a formatted string with common numeric types. Based on <see cref="https://msdn.microsoft.com/en-us/library/dwhawy9k(v=vs.95).aspx"/> 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="numericFormat"></param>
        /// <param name="cultureInfoCode"></param>
        /// <returns></returns>
        public static string ToStandardNumericFormat<TValue>(this TValue value, string numericFormat, [Optional] CultureInfo cultureInfoCode)
        {
            if (value == null) return null;

            var fixedValue = Regex.Replace(value.ToString(), @"[^-?\d+\.]", "");
            var result = Regex.IsMatch(numericFormat, "P", RegexOptions.IgnoreCase) ? fixedValue.ToDecimal() / 100 : fixedValue.ToDecimal();

            return result.ToString(numericFormat, cultureInfoCode ?? new CultureInfo("en-US"));
        }

        /// <summary>
        /// Returns true if a specified substring occurs within this string, false otherwise.
        /// </summary>
        /// <param name="input">The main string value </param>
        /// <param name="substring">The substring to check</param>
        /// <param name="comp">The comparison rule</param>
        /// <returns>True or false</returns>
        public static bool Contains(this string input, string substring, StringComparison comp) => input?.IndexOf(substring, comp) > 0;

        /// <summary>
        /// Converts a string representation of a number to its 32-bit signed integer equivalent. 
        /// </summary>
        /// <param name="obj">The representation of a number</param>
        /// <returns>The equivalent Integer or 0 if the conversion was unsuccessful</returns>
        public static int ToInteger(this object obj)
        {
            if (obj == null) return 0;

            int.TryParse(obj.ToString(), out var retVal);

            return retVal;
        }

        /// <summary>
        /// Converts a string representation of a number to its 64-bit signed integer equivalent. 
        /// </summary>
        /// <param name="obj">The representation of a number</param>
        /// <returns>The equivalent Integer or 0 if the conversion was unsuccessful</returns>
        public static long ToLongInteger(this object obj)
        {
            if (obj == null) return 0;

            long.TryParse(obj.ToString(), out var retVal);

            return retVal;
        }

        /// <summary>
        /// Converts a string representation of a date and time to its DateTime equivalent. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>The equivalent DateTime or Null if the conversion was unsuccessful</returns>
        public static DateTime? ToDateTime(this object obj)
        {
            if (obj == null) return null;

            DateTime.TryParse(obj.ToString(), out var retVal);

            return retVal;
        }

        /// <summary>
        /// Converts a string representation of a decimal to its Decimal equivalent. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>The equivalent decimal or 0 if the conversion was unsuccessful</returns>
        public static decimal ToDecimal(this object obj)
        {
            if (obj == null) return 0;

            decimal.TryParse(obj.ToString(), out var retVal);

            return retVal;
        }

        /// <summary>
        /// Converts a string representation of a double to its Double equivalent. 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>The equivalent double or 0 if the conversion was unsuccessful</returns>
        public static double ToDouble(this object obj)
        {
            if (obj == null) return 0;

            double.TryParse(obj.ToString(), out var retVal);

            return retVal;
        }

        public static string GetValueFromJsonByKey(this string json, string key)
        {
            if (key == null) return string.Empty;
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            
            return !dictionary.ContainsKey(key) ? string.Empty : dictionary[key];
        }
    }
}