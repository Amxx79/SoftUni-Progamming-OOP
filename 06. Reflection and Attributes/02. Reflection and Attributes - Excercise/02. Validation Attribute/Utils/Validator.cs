using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ValidationAttributes.Attributes;

namespace ValidationAttributes.Utils
{
    public class Validator
    {
        public static bool IsValid(object obj)
        {
            Type objType = obj.GetType();
            var a = objType.GetProperties();

            PropertyInfo[] propertyInfo = objType
                .GetProperties()
                .Where(p => p.CustomAttributes
                .Any(ca => typeof(MyValidationAttribute)
                .IsAssignableFrom(ca.AttributeType))).ToArray();

            foreach ( PropertyInfo propInfo in propertyInfo) 
            {
                IEnumerable<MyValidationAttribute> attributes = propInfo
                    .GetCustomAttributes()
                    .Where(ca => typeof(MyValidationAttribute)
                    .IsAssignableFrom(ca.GetType()))
                    .Cast<MyValidationAttribute>();
                foreach (MyValidationAttribute attr in attributes)
                {
                    if (!attr.IsValid(propInfo.GetValue(obj)))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
