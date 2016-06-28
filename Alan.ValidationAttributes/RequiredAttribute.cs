using Alan.ValidationAttributes.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Alan.ValidationAttributes
{
    /// <summary>
    /// 不为空校验
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RequiredAttribute : Attribute, IValidate
    {
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 不为空校验
        /// </summary>
        public RequiredAttribute()
        {
            this.ErrorMessage = "{0}不能为空";
        }

        public Tuple<bool, string> IsValid(object value, object instance, System.Reflection.PropertyInfo property)
        {


            var validName = property.GetCustomAttribute<ValidNameAttribute>();
            if (validName != null && !String.IsNullOrWhiteSpace(this.ErrorMessage))
            {
                this.ErrorMessage = String.Format(this.ErrorMessage, validName.Name);
            }


            if (value == null) return this.ReturnFalse();


            if (String.IsNullOrWhiteSpace(value.ToString())) return this.ReturnFalse();
            return this.ReturnTrue();
        }

    }
}