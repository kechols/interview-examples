using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sunrise.Radiology.Messenger.Common.Utils.Patterns;

namespace Kevins.Examples.Common.Tests.Unit.Utils.Patterns
{
    [TestClass]
    public class RetryerTest
    {
        private static int ONCE = 1;
        private static int TWICE = 2;
        private TestRetryable operation;
        private Retryer retryer;

        [TestInitialize]
        public void SetUp()
        {
            operation = new TestRetryable();
            retryer = new Retryer(operation);
        }

        [TestMethod]
        public void ShouldExecuteOperation()
        {
            retryer.Perform(2);
            Assert.IsTrue(operation.didCompleteSuccessfully);
            Assert.IsFalse(operation.DidRecover());
        }


        [TestMethod]
        public void ShouldExecuteOperationWithPauseBetweenTries()
        {
            operation.WillFail(ONCE);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            // Even though we are saying pause for 1 millisec it will default to minimum pause of 1000 millisec
            retryer.Perform(2, 1);
            stopwatch.Stop();
            Assert.IsTrue(operation.didCompleteSuccessfully);
            Assert.IsTrue(operation.DidRecover());
            Assert.IsTrue((stopwatch.ElapsedMilliseconds >= 800), "Operation did not pause " + stopwatch.Elapsed);
        }


        [TestMethod]
        public void ShouldExecuteOperationUpToTheMaximumNumberOfRetries()
        {
            operation.WillFail(ONCE);
            retryer.Perform(2);
            Assert.IsTrue(operation.didCompleteSuccessfully);
            Assert.AreEqual(2, operation.numberOfAttempts);
            Assert.AreEqual(1, operation.numberOfRecoveries);
        }


        [TestMethod]
        public void ShouldThrowExceptionWhenMaximumNumberOfAttemptsHasBeenReached()
        {
            operation.WillFail(TWICE);
            try
            {
                retryer.Perform(2);
                Assert.Fail("should have thrown RetryFailedException when the maximum number of attempts was reached");
            }
            catch (RetryFailedException)
            {
            }
            Assert.IsFalse(operation.didCompleteSuccessfully);
        }


        [TestMethod]
        public void ShouldRequireMaximumNumberOfAttemptsGreaterThanOne()
        {
            operation.WillFail(ONCE);
            retryer.Perform(1);
            Assert.IsTrue(operation.didCompleteSuccessfully);
            Assert.AreEqual(2, operation.numberOfAttempts); // 1 is not valid so it is incremented to 2 by default
            Assert.AreEqual(1, operation.numberOfRecoveries);
        }
    }

    class TestRetryable : IRetryable
    {

        public int numberOfRecoveries;
        public bool didCompleteSuccessfully;
        private int numberOfFailures;
        public int numberOfAttempts;

        public bool Attempt()
        {
            numberOfAttempts++;
            if (numberOfAttempts <= numberOfFailures)
                return false;
            didCompleteSuccessfully = true;
            return true;
        }

        public void WillFail(int numberOfFailures)
        {
            this.numberOfFailures = numberOfFailures;
        }

        public void Recover()
        {
            numberOfRecoveries++;
        }

        public bool DidRecover()
        {
            return numberOfRecoveries > 0;
        }
    }
}
