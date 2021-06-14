using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class PrimitiveExtensions
    {
        /// <summary>
        /// Indicates whether the string is null.
        /// </summary>
        public static bool IsNull(this string str) => str is null;

        /// <summary>
        /// Indicates whether the string is null or an System.String.Empty string.
        /// </summary>
        public static bool IsEmpty(this string? str) => string.IsNullOrEmpty(str);
        
        /// <summary>
        /// Indicates whether the string is null, empty, or consists only of white-space characters.
        /// </summary>
        public static bool IsWhiteSpace(this string? str) => string.IsNullOrWhiteSpace(str);
    }
}
