using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Kevins.Examples.Common.Enums;

namespace Kevins.Examples.Database.Extensions
{
    public static class DeliveryMethodExtensions
    {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool IsHardCopyDeliveryMethod(this DeliveryMethod deliveryMethod)
        {
            return !deliveryMethod.Equals(DeliveryMethod.Email);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool IsFax(this DeliveryMethod deliveryMethod)
        {
            var methods = new List<DeliveryMethod> { DeliveryMethod.Fax };
            return IsMethodType(deliveryMethod, methods);
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static bool IsPrint(this DeliveryMethod deliveryMethod)
        {
            var methods = new List<DeliveryMethod> { DeliveryMethod.BatchPrint, DeliveryMethod.Print, DeliveryMethod.ServerRequest };
            return IsMethodType(deliveryMethod, methods);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        private static bool IsMethodType(DeliveryMethod deliveryMethod, List<DeliveryMethod> methods)
        {
            return methods.Contains(deliveryMethod);
        }
    }
}
