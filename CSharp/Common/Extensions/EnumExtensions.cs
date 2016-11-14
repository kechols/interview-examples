using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Kevins.Examples.Common.Enums;

namespace Kevins.Examples.Common.Extensions
{
    public static class EnumExtentions
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }


        public static T GetEnumFromWords<T>(this string enumAsWords) where T : struct, IConvertible
        {
            var enums = GetValues<T>();
            foreach (var aEnum in enums)
            {
                if (aEnum.AsWords().Equals(enumAsWords))
                {
                    return (T)Enum.ToObject(typeof(T), aEnum.AsInt());
                }
            }
            return enums.FirstOrDefault();
        }


        public static T GetEnum<T>(this string enumAsWords) where T : struct, IConvertible
        {
            var enums = GetValues<T>();
            foreach (var aEnum in enums)
            {
                if (aEnum.Name().Equals(enumAsWords))
                {
                    return (T)Enum.ToObject(typeof(T), aEnum.AsInt());
                }
            }
            return enums.FirstOrDefault();
        }


        
        public static int AsInt<T>(this T anEnum) where T : struct, IConvertible
        {
            return ((int)Enum.ToObject(typeof(T), anEnum));
        }


        
        public static string AsWords<T>(this T anEnum) where T : struct, IConvertible
        {
            return anEnum.Name().CamelCaseAsWords();
        }


        
        public static string Name<T>(this T anEnum) where T : struct, IConvertible
        {
            return Enum.GetName(typeof(T), anEnum);
        }


        
        public static bool IsHardCopyDeliveryMethod(this DeliveryMethod deliveryMethod)
        {
            return !deliveryMethod.Equals(DeliveryMethod.Email);
        }


        
        public static bool IsFax(this DeliveryMethod deliveryMethod)
        {
            var methods = new List<DeliveryMethod> { DeliveryMethod.Fax };
            return IsMethodType(deliveryMethod, methods);
        }


        
        public static bool IsPrint(this DeliveryMethod deliveryMethod)
        {
            var methods = new List<DeliveryMethod> { DeliveryMethod.BatchPrint, DeliveryMethod.Print, DeliveryMethod.ServerRequest };
            return IsMethodType(deliveryMethod, methods);
        }


        
        private static bool IsMethodType(DeliveryMethod deliveryMethod, List<DeliveryMethod> methods)
        {
            return methods.Contains(deliveryMethod);
        }
    }
}
