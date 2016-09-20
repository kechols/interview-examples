using System;
using Kevins.Examples.Database;
using Kevins.Examples.Database.Common;
using Kevins.Examples.Database.Tests.Integration.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sunrise.Radiology.Messenger.Database.Common;

namespace Sunrise.Radiology.Messenger.Database.Tests.Integration
{
    [TestClass]
    public class UserPreferenceHibernateTest : HibernateTestBase
    {
        [TestMethod]
        public void ShouldCorrectlyMapUserPreference()
        {
            WithSession(session =>
            {
                var repository = new DatabaseRepository(session);
                UserPreference retrievedUserPreference = null;
                var expectedId = -1;
                try
                {
                    const string expectedKey = "Kevins.Test.Preference";
                    const string expectedValue = "To Be With Nice People";
                    var expectedUser = IntegrationTestHelper.RandomUser(session);
                    repository.Clear();

                    var userPreference = new UserPreference
                    {
                        Key = expectedKey,
                        Value = expectedValue,
                        User = expectedUser
                    };

                    // Very new unsaved userPreference has no Id
                    Assert.AreEqual(0, userPreference.Id);

                    // Save the userPreference and then verify that it has an Id
                    repository.SaveOrUpdate(userPreference);
                    Assert.IsTrue(userPreference.Id > 0);
                    expectedId = userPreference.Id;
                    repository.Clear();

                    retrievedUserPreference = repository.Get<UserPreference>(userPreference.Id);
                    Assert.IsTrue(retrievedUserPreference.Id > 0);
                    Assert.AreEqual(expectedId, retrievedUserPreference.Id);
                    Assert.AreEqual(expectedKey, retrievedUserPreference.Key);
                    Assert.AreEqual(expectedValue, retrievedUserPreference.Value);
                    Assert.AreEqual(expectedUser.Id, retrievedUserPreference.User.Id);
                }
                catch (Exception e)
                {
                    // Here to debug exceptions if necessary
                    throw e;
                }
                finally
                {
                    retrievedUserPreference = repository.Get<UserPreference>(expectedId);
                    repository.Clear();
                    repository.Delete(retrievedUserPreference);
                    Assert.IsNull(repository.Get<UserPreference>(retrievedUserPreference.Id));
                }
            });
        }
    }
}
