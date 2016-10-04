using System;

namespace Kevins.HSoftware.Threading
{
    
    public class ClazzWithThreadSafeStaticField
    {
        public static string NonThreadSafeStaticField = @"initialized non thread safe static field";


        [ThreadStaticAttribute()]
        public static string ThreadSafeStaticField = @"initialized thread safe static field";


        public static string GetStaticFieldValue(bool useThreadSafeField)
        {
            return useThreadSafeField ? ThreadSafeStaticField : NonThreadSafeStaticField;
        }
    }
}
