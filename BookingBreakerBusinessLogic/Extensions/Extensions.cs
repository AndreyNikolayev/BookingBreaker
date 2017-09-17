using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingBreakerBusinessLogic.Extensions
{
    public static class Extensions
    {
        public static bool ToBoolean(this string value)
        {
            if(value == null)
            {
                return false;
            }
            return value.Equals("true", StringComparison.InvariantCultureIgnoreCase);
        }

        public static int ToInteger(this string value)
        {
            if (value == null)
            {
                return 0;
            }

            int integerValue = 0;
            int.TryParse(value, out integerValue);
            return integerValue;
        }
    }
}
