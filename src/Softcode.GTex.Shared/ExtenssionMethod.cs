using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softcode.GTex
{
    public static class ExtenssionMethod
    {

        /// <summary>
        /// Returns first part of number.
        /// </summary>
        /// <param name="number">Initial number</param>
        /// <param name="N">Amount of digits required</param>
        /// <returns>First part of number</returns>
        public static int TakeNDigits(this int number, int N)
        {
            // this is for handling negative numbers, we are only insterested in postitve number
            number = Math.Abs(number);
            // special case for 0 as Log of 0 would be infinity
            if (number == 0)
                return number;
            // getting number of digits on this input number
            int numberOfDigits = (int)Math.Floor(Math.Log10(number) + 1);
            // check if input number has more digits than the required get first N digits
            if (numberOfDigits >= N)
                return (int)Math.Truncate((number / Math.Pow(10, numberOfDigits - N)));
            else
                return number;
        }

        /// <summary>
        /// Async create of a System.Collections.Generic.List<T> from an 
        /// System.Collections.Generic.IQueryable<T>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="list">The System.Collections.Generic.IEnumerable<T> 
        /// to create a System.Collections.Generic.List<T> from.</param>
        /// <returns> A System.Collections.Generic.List<T> that contains elements 
        /// from the input sequence.</returns>
        public static Task<List<T>> ToListAsync<T>(this IQueryable<T> list)
        {
            return Task.Run(() => list.ToList());
        }

        public static Guid ToGuid(this string aString)
        {
            Guid newGuid;

            if (string.IsNullOrWhiteSpace(aString))
            {
                return Guid.Empty;
            }

            if (Guid.TryParse(aString, out newGuid))
            {
                return newGuid;
            }

            return Guid.Empty;
        }

        public static DateTime FirstDayOfWeek(this DateTime dt)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return dt.AddDays(-diff).Date;
        }

        public static DateTime LastDayOfWeek(this DateTime dt)
        {
            return dt.FirstDayOfWeek().AddDays(6);
        }

        public static DateTime FirstDayOfMonth(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1).AddDays(-1);
        }

        public static DateTime FirstDayOfNextMonth(this DateTime dt)
        {
            return dt.FirstDayOfMonth().AddMonths(1);
        }
        public static string PadingWith(this string maxRef, int length, string padChar)
        {
            string newRef = "";
            if (String.IsNullOrEmpty(maxRef) && length > 0)
            {
                long numberValue = 1;
                newRef = padChar + (numberValue.ToString().PadLeft(length, '0'));
            }
            else
            {
                long numberValue = Convert.ToInt64(maxRef) + 1;
                newRef = padChar+(numberValue.ToString().PadLeft(length,'0'));

            }
            return newRef;
        }
        public static string PadingWithIncrement(this string maxRef, int length)
        {
            string newRef = "";
            if (String.IsNullOrEmpty(maxRef) && length > 0)
            {
                long numberValue = 1;
                newRef = Convert.ToString(numberValue).PadLeft(length, '0');
            }
            else
            {
                long numberValue = Convert.ToInt64(maxRef) + 1;
                newRef = Convert.ToString(numberValue).PadLeft(length, '0');
            }
            return newRef;
        }

        private static dynamic BindDynamic(DataRow dataRow)
        {
            dynamic result = null;
            if (dataRow != null)
            {
                result = new ExpandoObject();
                var resultDictionary = (IDictionary<string, object>)result;
                foreach (DataColumn column in dataRow.Table.Columns)
                {
                    var dataValue = dataRow[column.ColumnName];
                    resultDictionary.Add(column.ColumnName, DBNull.Value.Equals(dataValue) ? null : dataValue);
                }
            }
            return result;
        }
        public static IEnumerable<dynamic> ToJson(this DataTable dataTable)
        {
            foreach (DataRow row in dataTable.Rows)
            {
                yield return BindDynamic(row);
            }
        }
        public static string GenerateId(this Guid guid)
        {
            long i = 1;
            foreach (byte b in guid.ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }

        public static long GenerateLongId(this Guid guid)
        {
            byte[] buffer = guid.ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }


    }

}
