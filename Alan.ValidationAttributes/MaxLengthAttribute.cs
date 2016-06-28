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
    /// 最大长度校验
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLengthAttribute : Attribute, IValidate
    {
        private int _maxLength;

        public string ErrorMessage { get; set; }

        /// <summary>
        /// 最大长度校验
        /// </summary>
        /// <param name="maxLength">最大长度</param>
        public MaxLengthAttribute(int maxLength)
        {
            this._maxLength = maxLength;
            this.ErrorMessage = "{0}长度不能超过" + this._maxLength;
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
            if (strValue.Length > this._maxLength) return this.ReturnFalse();
            return this.ReturnTrue();
        }
    }
}
