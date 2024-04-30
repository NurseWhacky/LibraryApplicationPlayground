using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess
{
    public static class XmlExtensions
    {
        public static T ToEntity<T>(this XElement element) where T : new()
        {
            T entity = new T();
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                var attribute = element.Attribute(property.Name);
                if (attribute is not null)
                {
                    property.SetValue(entity, Convert.ChangeType(attribute.Value, property.PropertyType));
                    continue;
                }
                var childElement = element.Element(property.Name);
                if (childElement != null)
                {
                    if (property.PropertyType.IsPrimitive && (property.PropertyType == typeof(int) || property.PropertyType == typeof(double) || property.PropertyType == typeof(float) || property.PropertyType == typeof(decimal)))
                    {
                        object numericValue = TryParsePrimitiveNumericValue(childElement.Value, property.PropertyType);
                        if (numericValue != null)
                        {
                            property.SetValue(entity, numericValue);
                        }
                        else
                        {
                            throw new Exception();
                            // Handle case where child element value cannot be parsed
                        }
                    }
                    // Handle complex types or simple value conversion
                    //if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string))
                    if (property.PropertyType == typeof(string))
                    {
                        // TODO: handle conversion from string to numeric type
                        property.SetValue(entity, childElement.Value);
                    }
                }
            }
            return entity;
        }

        private static object TryParsePrimitiveNumericValue(string value, Type targetType)
        {
            if (targetType == typeof(int))
            {
                int intValue;
                if (int.TryParse(value, out intValue))
                {
                    return intValue;
                }
            }
            else if (targetType == typeof(double))
            {
                double doubleValue;
                if (double.TryParse(value, out doubleValue))
                {
                    return doubleValue;
                }
            }
            // ... Add similar logic for other primitive numeric types

            return null; // Indicate parsing failure
        }
    }
}
