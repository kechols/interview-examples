using System;
using Kevins.Examples.Database.Common;
using Kevins.Examples.Database.Tests.Integration.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.Examples.Database.Tests.Integration
{
    [TestClass]
    public class UserHibernateTest : HibernateTestBase
    {

        [TestMethod]
        public void ShouldCorrectlyMapUser()
        {
            WithSession(session => {
                var repository = new DatabaseRepository(session);
                User retrievedUser = null;
                var expectedId = -1;
                try
                {
                    const string expectedUserCode = "99999";
                    const string expectedUserPassword = "51515151";
                    const string expectedUsername = "Kevin";

                    var expectedDepartment = IntegrationTestHelper.GetRandom<Department>(session, IntegrationTestHelper.ColumnName<Department>(o => o.Id));
                    var expectedFacility = IntegrationTestHelper.GetRandom<Facility>(session, IntegrationTestHelper.ColumnName<Facility>(o => o.Id));
                    repository.Clear();

                    var user = new User
                    {
                        UserCode = expectedUserCode,
                        UserPassword = expectedUserPassword,
                        Name = expectedUsername,
                        Department = expectedDepartment,
                        Facility = expectedFacility,
                    };

                    // Very new unsaved user has no Id
                    Assert.AreEqual(0, user.Id);

                    // Save the user and then verify that it has an Id
                    repository.SaveOrUpdate(user);
                    Assert.IsTrue(user.Id > 0);
                    expectedId = user.Id;
                    repository.Clear();

                    retrievedUser = repository.Get<User>(user.Id);
                    Assert.IsTrue(retrievedUser.Id > 0);
                    Assert.AreEqual(expectedId, retrievedUser.Id);
                    Assert.AreEqual(expectedUserCode, retrievedUser.UserCode);
                    Assert.AreEqual(expectedUserPassword, retrievedUser.UserPassword);
                    Assert.AreEqual(expectedUsername, retrievedUser.Name);
                    Assert.AreEqual((object)expectedDepartment.Id, retrievedUser.Department.Id);
                    Assert.AreEqual((object)expectedFacility.Id, retrievedUser.Facility.Id);
                }
                catch (Exception e)
                {
                    // Here to debug exceptions if necessary
                    throw e;
                }
                finally
                {
                    retrievedUser = repository.Get<User>(expectedId);
                    repository.Clear();
                    repository.Delete(retrievedUser);
                    Assert.IsNull(repository.Get<User>(retrievedUser.Id));
                }
            });
        }


        [TestMethod]
        public void ShouldMapUserPreferences()
        {
            WithSession(session =>
            {
                var repository = new MessengerRepository(session);
                try
                {
                    var userWithManyPreferencesQuery = session.CreateSQLQuery(
                        @"select top 1 USER_No user_id, count(user_No) count 
                            from UserPrefs  group by USER_No order by count desc"
                    );
                    var result = (object[])userWithManyPreferencesQuery.UniqueResult();
                    var userWithManyPreferencesLookup = new { Id = (int)result[0], Count = (int)result[1] };

                    var userWithManyPreferences = repository.Get<User>(userWithManyPreferencesLookup.Id);
                    Assert.AreEqual(userWithManyPreferencesLookup.Count, userWithManyPreferences.Preferences.Count);
                }
                catch (Exception e)
                {
                    // Here to debug exceptions if necessary
                    throw e;
                }
            });
        }
    }
}
