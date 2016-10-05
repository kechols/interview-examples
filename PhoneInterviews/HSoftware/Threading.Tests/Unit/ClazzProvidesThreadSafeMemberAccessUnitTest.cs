using System.Collections.Generic;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.HSoftware.Threading.Tests.Unit
{
    [TestClass]
    public class ClazzProvidesThreadSafeMemberAccessUnitTest
    {
        [TestMethod]
        public void ShouldHaveSafeMethodCallsInMultiThread()
        {
            var workerThreads = new List<Thread>();
            var numberFieldValue = 0;
            var incrementFiveTimes = 5;
            int functionExecuteCount = 0;

            // Start field value at 0
            ClazzProvidesThreadSafeMemberAccess.ModifyNumberField(0);

            for (var i = 1; i <= incrementFiveTimes; i++)
            {
                var workerThread = new Thread(() =>
                {
                    Interlocked.Increment(ref functionExecuteCount);
                    // Wait for other threads to get inside the function
                    while (functionExecuteCount < incrementFiveTimes)
                    {
                        Thread.Yield();
                        Thread.Sleep(10);
                    }
                    ClazzProvidesThreadSafeMemberAccess.IncrementNumberField();
                    numberFieldValue = ClazzProvidesThreadSafeMemberAccess.GetNumberField();
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
        public void ShouldNotHaveSafeMethodCallsInMultiThread()
        {
            var workerThreads = new List<Thread>();
            var numberFieldValue = 0;
            var incrementFiveTimes = 5;
            int functionExecuteCount = 0;

            // Start field value at 0
            ClazzProvidesThreadSafeMemberAccess.ModifyNumberField(0, false);

            for (var i = 1; i <= incrementFiveTimes; i++)
            {
                var workerThread = new Thread(() =>
                {
                    Interlocked.Increment(ref functionExecuteCount);
                    // Wait for other threads to get inside the function
                    while (functionExecuteCount < incrementFiveTimes)
                    {
                        Thread.Yield();
                        Thread.Sleep(10);
                    }
                    ClazzProvidesThreadSafeMemberAccess.IncrementNumberField(false);
                    numberFieldValue = ClazzProvidesThreadSafeMemberAccess.GetNumberField(false);
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
