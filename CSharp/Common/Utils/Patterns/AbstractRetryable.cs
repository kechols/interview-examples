using System;
using Kevins.Examples.Common.Extensions;

namespace Kevins.Examples.Common.Utils.Patterns
{
    public abstract class AbstractRetryable : IRetryable
    {
        private Exception causeOfFailure;
        protected int MaxAttempts = "MaxRetryAttempts".RequiredSetting().AsInt();

        public bool Attempt()
        {
            try
            {
                return DoAttempt();
            }
            catch (Exception causeOfFailure)
            {
                this.causeOfFailure = causeOfFailure;
                return false;
            }
        }


        public Exception GetCauseOfFailure()
        {
            return causeOfFailure;
        }

        public void Recover() { }

        protected abstract bool DoAttempt();
    }
}
