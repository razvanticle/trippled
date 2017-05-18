using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TrippleD.Core.Extensions
{
    public static class ReflectionExtensions
    {
        public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider attributeProvider)
        {
            return GetAttribute<TAttribute>(attributeProvider, false);
        }

        public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider attributeProvider, bool inherit)
        {
            return GetAttributes<TAttribute>(attributeProvider, inherit).FirstOrDefault();
        }
        
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this ICustomAttributeProvider attributeProvider)
        {
            return GetAttributes<TAttribute>(attributeProvider, false);
        }

        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(
            this ICustomAttributeProvider attributeProvider,
            bool inherit)
        {
            return attributeProvider.GetCustomAttributes(typeof(TAttribute), inherit).Cast<TAttribute>();
        }

        public static TValue GetAttributeValue<TAttribute, TValue>(
            this ICustomAttributeProvider attributeProvider,
            Func<TAttribute, TValue> accessor,
            TValue defaultValue = default(TValue)) where TAttribute : class
        {
            var attribute = attributeProvider.GetAttribute<TAttribute>();
            if (attribute != null)
            {
                return accessor(attribute);
            }

            return defaultValue;
        }

        /// <summary>
        ///     Gets all the properties which are editable.
        ///     A property is editable if it has public a getter and a public setter
        /// </summary>
        public static IEnumerable<PropertyInfo> GetEditableProperties(object instance)
        {
            var properties =
                instance.GetType()
                    .GetMembers(BindingFlags.Public | BindingFlags.Instance)
                    .Select(m => m as PropertyInfo)
                    .Where(p => p != null);

            foreach (var property in properties)
            {
                var getter = property.GetGetMethod();
                var setter = property.GetSetMethod();
                if (getter != null && setter != null)
                {
                    yield return property;
                }
            }
        }

        /// <summary>
        ///     Gets all the properties which are editable and are of simple type
        ///     A property is editable if it has public a getter and a public setter
        ///     A type is simple if it is primitive or value type, or enum or string
        /// </summary>
        public static IEnumerable<PropertyInfo> GetEditableSimpleProperties(object instance)
        {
            return GetEditableProperties(instance).Where(p => IsSimpleType(p.PropertyType));
        }

        /// <summary>
        ///     A type is simple if it is primitive or value type, or enum or string
        /// </summary>
        public static bool IsSimpleType(this Type type)
        {
            return type.GetTypeInfo().IsPrimitive || type.GetTypeInfo().IsValueType || type.GetTypeInfo().IsEnum ||
                   type == typeof(string);
        }
    }
}