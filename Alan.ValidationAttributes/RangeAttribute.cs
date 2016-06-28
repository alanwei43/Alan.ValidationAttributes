using Alan.ValidationAttributes.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Alan.ValidationAttributes
{
    /// <summary>
    /// 数值区间
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RangeAttribute : Attribute, IValidate
    {
        private double _min;
        private double _max;
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 数值区间校验
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        public RangeAttribute(double min, double max)
        {
            this._min = min;
            this._max = max;
            this.ErrorMessage = "{0}必须大于" + this._max + "小于" + this._min;
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
                if (dblValue < this._min || dblValue > this._max) return this.ReturnFalse();
                return this.ReturnTrue();
            }
            catch { return this.ReturnFalse(); }
        }
    }
}