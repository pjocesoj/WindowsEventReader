using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsEventReader
{
    public static class Guard
    {
        public static void NotNullOrEmpty(string value, string parameterName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Value cannot be null or empty.", parameterName);
            }
        }
        public static void NotNullOrEmpty(IEnumerable<string> values, string parameterName)
        {
            if (values == null || !values.Any())
            {
                throw new ArgumentException("Collection cannot be null or empty.", parameterName);
            }
        }
        public static void IsNumber(string value, string parameterName)
        {
            if (!int.TryParse(value, out _))
            {
                throw new ArgumentException("Value must be a valid number.", parameterName);
            }
        }
    }
}
