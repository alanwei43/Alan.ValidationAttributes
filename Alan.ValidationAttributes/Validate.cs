using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Alan.ValidationAttributes
{
    public static class Validate
    {

        /// <summary>
        /// 校验是否通过
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Tuple<bool, string[]> IsValid(object obj)
        {
            var validResult = Valid(obj);

            var errorMessages = validResult.Where(v => v.Item1 == false).Select(v => v.Item2).ToArray();
            if (errorMessages.Length > 0) return Tuple.Create(false, errorMessages);

            return Tuple.Create(true, new string[0]);
        }
        /// <summary>
        /// 执行校验
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static List<Tuple<bool, string>> Valid(object obj)
        {
            var validSummary = new List<Tuple<bool, string>>();
            var properties = obj.GetType().GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var value = property.GetValue(obj, null);

                
                //var attributes = property.GetCustomAttributes(true);
                var attributes = property.CustomAttributes;
                foreach (var attribute in attributes)
                {
                    var instance = property.GetCustomAttribute(attribute.AttributeType);
                    var validate = instance as IValidate;
                    if (validate == null) continue;

                    var isValid = validate.IsValid(value, obj, property);
                    validSummary.Add(isValid);
                }
            }

            return validSummary;
        }

    }
}