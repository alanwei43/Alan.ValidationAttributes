using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alan.ValidationAttributes.Extensions
{
    public static class TuplesExtensions
    {

        public static Tuple<bool, string> ReturnTrue(this IValidate valid)
        {
            return Tuple.Create(true, String.Empty);
        }
        public static Tuple<bool, string> ReturnFalse(this IValidate valid)
        {
            return Tuple.Create(false, valid.ErrorMessage);
        }
    }
}
