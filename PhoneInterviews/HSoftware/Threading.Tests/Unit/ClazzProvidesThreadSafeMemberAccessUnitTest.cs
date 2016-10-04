using System;
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
            var threadNumberFieldValue = 0;
            var valueAfterIncrementingFiveTimes = 5;

            // Start field value at 0
            ClazzProvidesThreadSafeMemberAccess.ModifyNumberField(0);

            for (var i = 1; i <= 5; i++)
            {
                var workerThread = new Thread(() =>
                {
                    RandomWait();
                    ClazzProvidesThreadSafeMemberAccess.IncrementNumberField();
                    threadNumberFieldValue = ClazzProvidesThreadSafeMemberAccess.GetNumberField();
                });
                workerThread.Start();
                workerThreads.Add(workerThread);
            }

            foreach (var workerThread in workerThreads)
            {
                workerThread.Join();
            }
            
            // Assert result
            Assert.AreEqual(valueAfterIncrementingFiveTimes, threadNumberFieldValue);
        }


        [TestMethod]
        public void ShouldNotHaveSafeMethodCallsInMultiThread()
        {
            var workerThreads = new List<Thread>();
            var threadNumberFieldValue = 0;
            var valueAfterIncrementingFiveTimes = 5;

            // Start field value at 0
            ClazzProvidesThreadSafeMemberAccess.ModifyNumberField(0, false);

            for (var i = 1; i <= 5; i++)
            {
                var workerThread = new Thread(() =>
                {
                    RandomWait();
                    ClazzProvidesThreadSafeMemberAccess.IncrementNumberField(false);
                    threadNumberFieldValue = ClazzProvidesThreadSafeMemberAccess.GetNumberField(false);
                });
                workerThread.Start();
                workerThreads.Add(workerThread);
            }


            foreach (var workerThread in workerThreads)
            {
                workerThread.Join();
            }

            // Assert result
            Assert.AreNotEqual(valueAfterIncrementingFiveTimes, threadNumberFieldValue);
        }


        private void RandomWait()
        {
            var randomizer = new Random();
            Thread.Sleep(randomizer.Next(50, 100));
        }
    }
}
