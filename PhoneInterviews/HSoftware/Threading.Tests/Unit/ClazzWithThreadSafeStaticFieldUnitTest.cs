using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.HSoftware.Threading.Tests.Unit
{
    [TestClass]
    public class ClazzWithThreadSafeStaticFieldUnitTest
    {
        [TestMethod]
        public void ShouldHaveNonSafeFieldIntializedInMultiThread()
        {
            var evaluationOfMainThreadValue = IsNonSafeField(ClazzWithThreadSafeStaticField.GetStaticFieldValue(false));
            Assert.IsTrue(evaluationOfMainThreadValue);

            var spawnedThreadValue = string.Empty;
            var newStaticFieldThread = new Thread(() =>
                {
                    spawnedThreadValue = ClazzWithThreadSafeStaticField.GetStaticFieldValue(false);
                }
            );
            newStaticFieldThread.Start();
            Assert.IsTrue(IsNonSafeField(spawnedThreadValue));
        }


        [TestMethod]
        public void ShouldHaveNonSafeFieldIntializedInSingleThread()
        {
            Assert.IsTrue(IsNonSafeField(ClazzWithThreadSafeStaticField.GetStaticFieldValue(false)));
        }


        [TestMethod]
        public void ShouldHaveSafeFieldIntializedCorrectlyInMultiThread()
        {
            var evaluationOfMainThreadValue = IsSafeField(ClazzWithThreadSafeStaticField.GetStaticFieldValue());
            Assert.IsTrue(evaluationOfMainThreadValue);

            var spawnedThreadValue = string.Empty;
            var newStaticFieldThread = new Thread(() =>
            {
                spawnedThreadValue = ClazzWithThreadSafeStaticField.GetStaticFieldValue();
            }
            );
            newStaticFieldThread.Start();
            // The expected behaviour is that the new thread returns null for the thread safe static field.
            // The thread safe static field is only initialized in the main / first thread.
            Assert.IsNull(spawnedThreadValue);
        }



        private bool IsNonSafeField(string value)
        {
            return value.IndexOf(@"non") >= 0;
        }


        private bool IsSafeField(string value)
        {
            return !IsNonSafeField(value);
        }
    }
}
