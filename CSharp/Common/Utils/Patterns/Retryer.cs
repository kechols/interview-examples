using System;
using System.Threading;

namespace Sunrise.Radiology.Messenger.Common.Utils.Patterns
{
    public class RetryFailedException : Exception
    {
        public RetryFailedException(string message)
            : base(message)
        { }
    }

    // The purpose of this class is to retry to perfom a operation at least two or more times (default = 2)
    public class Retryer
    {
        private IRetryable operation;
        private int sleepBetweenRetries;

        public Retryer(IRetryable operation)
        {
            this.operation = operation;
        }


        // perfom a operation at least two or more times (maximumNumberOfAttempts default = 2) (default pauseBetweenRetries = 1000)
        public void Perform(int maximumNumberOfAttempts, int pauseBetweenRetries)
        {
            pauseBetweenRetries = (pauseBetweenRetries < 1000 ? 1000 : pauseBetweenRetries);
            sleepBetweenRetries = pauseBetweenRetries;
            Perform(maximumNumberOfAttempts);
        }


        // perfom a operation at least two or more times (default = 2)
        public void Perform(int maximumNumberOfAttempts)
        {
            maximumNumberOfAttempts = (maximumNumberOfAttempts <= 1 ? 2 : maximumNumberOfAttempts);
            for (int numberOfAttempts = 0; numberOfAttempts < maximumNumberOfAttempts; numberOfAttempts++)
            {
                if (operation.Attempt())
                    return;
                PauseIfRetrying();
                operation.Recover();
            }
            throw new RetryFailedException(string.Format("Failed to execute operation {0} after {1} attempt(s).", operation.GetType(),
                    maximumNumberOfAttempts));
        }


        private void PauseIfRetrying()
        {
            if (sleepBetweenRetries > 0)
            {
                try
                {
                    Thread.Sleep(sleepBetweenRetries);
                }
                catch (ThreadInterruptedException e)
                {
                    throw new Exception("There was a problem pausing between retries", e);
                }
            }
        }
    }
}
