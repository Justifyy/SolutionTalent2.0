using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineTalent.Core.Common
{
    public static class HelperGeneric
    {
        public static T ToType<T>(this string value)
        {
            object parsedValue = default(T);
            Type type = typeof(T);

            try
            {
                parsedValue = Convert.ChangeType(value, type);
            }
            catch (ArgumentException)
            {
                parsedValue = null;
            }

            return (T)parsedValue;
        }
    }
}
