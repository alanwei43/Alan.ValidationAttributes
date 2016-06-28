using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Alan.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ValidNameAttribute : Attribute
    {
        public string Name { get; set; }
        public ValidNameAttribute(string name) { this.Name = name; }

    }
}