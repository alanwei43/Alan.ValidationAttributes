using Alan.ValidationAttributes.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Alan.ValidationAttributes
{
    /// <summary>
    /// 正则校验
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class RegexAttribute : Attribute, IValidate
    {
        private System.Text.RegularExpressions.Regex _regex;
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 正则表达式校验
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        public RegexAttribute(string pattern)
        {
            this.ErrorMessage = "{0}格式无效";
            this._regex = new System.Text.RegularExpressions.Regex(pattern);
        }

        /// <summary>
        /// 正则表达式校验
        /// </summary>
        /// <param name="pattern">正则表达式</param>
        /// <param name="errorMessage">错误消息</param>
        public RegexAttribute(System.Text.RegularExpressions.Regex pattern)
        {
            this.ErrorMessage = "{0}格式无效";
            this._regex = pattern;
        }

        public Tuple<bool, string> IsValid(object value, object instance, System.Reflection.PropertyInfo property)
        {

            string strValue = String.Empty;

            if (value != null) strValue = value.ToString();


            var validName = property.GetCustomAttribute<ValidNameAttribute>();
            if (validName != null && !String.IsNullOrWhiteSpace(this.ErrorMessage))
            {
                this.ErrorMessage = String.Format(this.ErrorMessage, validName.Name);
            }

            var isMatch = this._regex.IsMatch(strValue);
            if (isMatch) return this.ReturnTrue();
            return this.ReturnFalse();
        }
    }
}