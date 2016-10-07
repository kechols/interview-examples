using System;

namespace Kevins.HSoftware.UsingSealed
{
    public static class SealedClazzFactory
    {
        public static ISealed SealedInstance(this bool implementsSealed, string value)
        {
            if (implementsSealed)
            {
                if (string.IsNullOrEmpty(value))
                {
                    // The below line of code is not allowed because the partially initialized contructor is not allowed by the compiler.
                    // So we are throwing an exception. But if you care to see the compiler error un comment the below line and see error in 
                    // Visual Studio IDE
                    // return new ClazzPreventsPartiallyInitializedObjectUsingSealed();
                    throw new ArgumentNullException(
                        @"Partially initialized contructor is not allowed by the compiler");
                }
                else
                {
                    return new ClazzPreventsPartiallyInitializedObjectUsingSealed(value);
                }
            }

            return new ClazzAllowsPartiallyInitializedObject();
        }
    }
}
