using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace WPFMonitor.Core.Uitl
{
    public class EmptyValidationRule : ValidationRule
    {
        public string Errmsg {get;set;}

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(false, Errmsg);
            }
            return new ValidationResult(true, null);
        }
    }  

}
