using Alan.ValidationAttributes.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Alan.ValidationAttributes
{
    /// <summary>
    /// 最小值校验
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class MinAttribute : Attribute, IValidate
    {
        private double _min;
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 最小值校验
        /// </summary>
        /// <param name="minValue">最小值</param>
        public MinAttribute(double minValue)
        {
            this._min = minValue;
            this.ErrorMessage = "{0}不能小于" + this._min;
        }

        public Tuple<bool, string> IsValid(object value, object instance, System.Reflection.PropertyInfo property)
        {
            if (value == null) return this.ReturnTrue();


            var validName = property.GetCustomAttribute<ValidNameAttribute>();
            if (validName != null && !String.IsNullOrWhiteSpace(this.ErrorMessage))
            {
                this.ErrorMessage = String.Format(this.ErrorMessage, validName.Name);
            }


            try
            {
                double dblValue = (double)Convert.ChangeType(value, typeof(double));
                if (dblValue < this._min) return this.ReturnFalse();
                return Tuple.Create(true, String.Empty);
            }
            catch { return this.ReturnFalse(); }
        }
    }
}