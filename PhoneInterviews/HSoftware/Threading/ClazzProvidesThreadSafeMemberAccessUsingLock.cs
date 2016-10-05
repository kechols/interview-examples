namespace Kevins.HSoftware.Threading
{
    
    public static class ClazzProvidesThreadSafeMemberAccessUsingLock
    {
        private static int numberField = 1;
        private static object synchronizationObject = new object();

        public static int GetNumberField(bool makeThreadSafe = true)
        {
            if (makeThreadSafe)
            {
                lock (synchronizationObject)
                {
                    return numberField;
                }
            }
            return numberField;
        }


        public static void IncrementNumberField(bool makeThreadSafe = true)
        {
            if (makeThreadSafe)
            {
                lock (synchronizationObject)
                {
                    ++numberField;
                }
            }
            else
            {
                ++numberField;
            }
        }


        public static void ModifyNumberField(int newValue, bool makeThreadSafe = true)
        {
            if (makeThreadSafe)
            {
                lock (synchronizationObject)
                {
                    numberField = newValue;
                }
            }
            else
            {
                numberField = newValue;
            }
        }
    }
}
