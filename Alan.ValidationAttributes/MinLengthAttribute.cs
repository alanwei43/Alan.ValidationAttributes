using Alan.ValidationAttributes.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Alan.ValidationAttributes
{
    /// <summary>
    /// 最小长度校验
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MinLengthAttribute : Attribute, IValidate
    {
        private int _minLength;

        public string ErrorMessage { get; set; }
        /// <summary>
        /// 最小长度校验
        /// </summary>
        /// <param name="minLength">最小长度</param>
        public MinLengthAttribute(int minLength)
        {
            this._minLength = minLength;
            this.ErrorMessage = "{0}最小长度为" + this._minLength;
        }
        public Tuple<bool, string> IsValid(object value, object instance, PropertyInfo property)
        {

            if (value == null) return this.ReturnTrue();



            var validName = property.GetCustomAttribute<ValidNameAttribute>();
            if (validName != null && !String.IsNullOrWhiteSpace(this.ErrorMessage))
            {
                this.ErrorMessage = String.Format(this.ErrorMessage, validName.Name);
            }



            var strValue = value.ToString();
            if (strValue.Length < this._minLength) return this.ReturnFalse();
            return this.ReturnTrue();
        }
    }
}
