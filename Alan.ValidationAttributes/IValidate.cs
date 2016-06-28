using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Alan.ValidationAttributes
{
    public interface IValidate
    {
        /// <summary>
        /// 错误消息
        /// </summary>
        string ErrorMessage { get; set; }

        /// <summary>
        /// 如果匹配上 校验通过, 否则校验失败.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        Tuple<bool, string> IsValid(object value, object instance, PropertyInfo property);
    }
}