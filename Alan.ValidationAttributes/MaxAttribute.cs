using Alan.ValidationAttributes.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Alan.ValidationAttributes
{
    /// <summary>
    /// 最大值校验
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxAttribute : Attribute, IValidate
    {
        private double _max;
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 最大值校验
        /// </summary>
        /// <param name="maxValue">最大值</param>
        public MaxAttribute(double maxValue)
        {
            this._max = maxValue;
            this.ErrorMessage = "{0}最大值不能超过" + this._max;
        }

        public Tuple<bool, string> IsValid(object value, object instance, System.Reflection.PropertyInfo property)
        {
            if (value == null) return this.ReturnTrue();
            var validName = property.GetCustomAttribute<ValidNameAttribute>();
            if (validName != null && !String.IsNullOrWhiteSpace(this.ErrorMessage))
            {
                this.ErrorMessage = String.Format(this.ErrorMessage, validName.Name, this._max);
            }
            try
            {
                double dblValue = (double)Convert.ChangeType(value, typeof(double));
                if (dblValue > this._max) return this.ReturnFalse();
                return this.ReturnTrue();
            }
            catch { return this.ReturnFalse(); }
        }
    }
}