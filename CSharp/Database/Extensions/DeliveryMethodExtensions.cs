using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Kevins.Examples.Common.Enums;

namespace Kevins.Examples.Database.Extensions
{
    public static class DeliveryMethodExtensions
    {
        
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
