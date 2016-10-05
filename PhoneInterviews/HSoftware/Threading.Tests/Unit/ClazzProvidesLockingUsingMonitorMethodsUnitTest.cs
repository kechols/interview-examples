using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.HSoftware.Threading.Tests.Unit
{
    [TestClass]
    public class ClazzProvidesLockingUsingMonitorMethodsUnitTest
    {
        [TestMethod]
        public void ShouldHaveSafeMethodCallsUsingMonitorMethods()
        {
            var workerThreads = new List<Thread>();
            var numberFieldValue = 0;
            var incrementFiveTimes = 5;
            int functionExecuteCount = 0;

            // Start field value at 0
            ClazzProvidesLockingUsingMonitorMethods.ModifyNumberField(0);

            for (var i = 1; i <= incrementFiveTimes; i++)
            {
                var workerThread = new Thread(() =>
                {
                    Interlocked.Increment(ref functionExecuteCount);
                    // Wait for other threads to get inside the function
                    while (functionExecuteCount < incrementFiveTimes)
                    {
                        Thread.Yield();
                    }
                    ClazzProvidesLockingUsingMonitorMethods.IncrementNumberField();
                    numberFieldValue = ClazzProvidesLockingUsingMonitorMethods.GetNumberField();
                });
                workerThreads.Add(workerThread);
            }

            foreach (var workerThread in workerThreads)
            {
                workerThread.Start();
            }

            foreach (var workerThread in workerThreads)
            {
                workerThread.Join();
            }
            

            // Assert result
            Assert.AreEqual(incrementFiveTimes, numberFieldValue);
        }


        [TestMethod]
        public void ShouldNotHaveSafeMethodCallsWhenNotUsingMonitorMethods()
        {
            var workerThreads = new List<Thread>();
            var numberFieldValue = 0;
            var incrementFiveTimes = 5;
            int functionExecuteCount = 0;

            // Start field value at 0
            ClazzProvidesLockingUsingMonitorMethods.ModifyNumberField(0, false);

            for (var i = 1; i <= incrementFiveTimes; i++)
            {
                var workerThread = new Thread(() =>
                {
                    Interlocked.Increment(ref functionExecuteCount);
                    // Wait for other threads to get inside the function
                    while (functionExecuteCount < incrementFiveTimes)
                    {
                        Thread.Yield();
                    }
                    ClazzProvidesLockingUsingMonitorMethods.IncrementNumberField(false);
                    numberFieldValue = ClazzProvidesLockingUsingMonitorMethods.GetNumberField(false);
                });
                workerThreads.Add(workerThread);
            }

            foreach (var workerThread in workerThreads)
            {
                workerThread.Start();
            }


            // Assert result
            Assert.AreNotEqual(incrementFiveTimes, numberFieldValue);
        }
    }
}
