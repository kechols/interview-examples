using System.Threading;

namespace Kevins.HSoftware.Threading
{
    
    public static class ClazzProvidesLockingUsingMonitorMethods
    {
        public static readonly int FAILED_TO_INCREMENT_NUMBER_FIELD = -1;
        private static int numberField = 1;
        private static object synchronizationObject = new object();
        

        public static object SynchronizationRoot => synchronizationObject;

        public static int GetNumberField(bool makeThreadSafe = true)
        {
            if (makeThreadSafe)
            {
                if (IsReadyToAccess())
                {
                    try
                    {
                        return numberField;
                    }
                    finally
                    {
                        QuitCheckingIfReadyToAccess();
                    }
                }
                return FAILED_TO_INCREMENT_NUMBER_FIELD;
            }
            return numberField;
        }



        public static void IncrementNumberField(bool makeThreadSafe = true)
        {
            if (makeThreadSafe)
            {
                if (IsReadyToAccess())
                {
                    try
                    {
                        ++numberField;
                    }
                    finally
                    {
                        QuitCheckingIfReadyToAccess();
                    }
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
                if (IsReadyToAccess())
                {
                    try
                    {
                        numberField = newValue;
                    }
                    finally
                    {
                        QuitCheckingIfReadyToAccess();
                    }
                }
            }
            else
            {
                numberField = newValue;
            }
        }


        private static bool IsReadyToAccess()
        {
            return Monitor.TryEnter(synchronizationObject, 250);
        }


        private static void QuitCheckingIfReadyToAccess()
        {
            Monitor.Exit(synchronizationObject);
        }
    }
}
