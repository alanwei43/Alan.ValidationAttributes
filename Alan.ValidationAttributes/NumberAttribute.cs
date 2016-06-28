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
    /// 数值型校验
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NumberAttribute : Attribute, IValidate
    {
        private int _decimalLength;
        private int _totalLength;
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 数值型校验
        /// </summary>
        /// <param name="decimalLength">小数位最大长度</param>
        /// <param name="totalLength">总共最大长度</param>
        public NumberAttribute(int decimalLength = int.MaxValue, int totalLength = int.MaxValue)
        {
            this._decimalLength = decimalLength;
            this._totalLength = totalLength;
            this.ErrorMessage = String.Format("{{0}}小数位不能超过{0}, 总长度不能超过{1}.", decimalLength, totalLength);
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
            if (String.IsNullOrWhiteSpace(strValue)) return this.ReturnTrue();
            if (!System.Text.RegularExpressions.Regex.IsMatch(strValue, @"^\d+(\.\d+)?$")) return this.ReturnFalse();

            var numbers = strValue.Split('.');
            if(numbers.Length == 2)
            {
                //拥有小数位
                //检验小数位长度
                if (numbers[1].Length > this._decimalLength) return this.ReturnFalse(); //小数位长度超限
            }
            if (String.Join("", numbers).Length > this._totalLength) return this.ReturnFalse();//总长度超限
            return this.ReturnTrue();
        }
    }
}
