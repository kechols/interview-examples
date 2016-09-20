using System;
using Kevins.Examples.Database.Common;
using Kevins.Examples.Database.Tests.Integration.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.Examples.Database.Tests.Integration
{
    [TestClass]
    public class DepartmentHibernateTest : HibernateTestBase
    {

        [TestMethod]
        public void ShouldCorrectlyMapDepartment()
        {
            WithSession(session => {
                var repository = new DatabaseRepository(session);
                Department retrievedDepartment = null;
                var expectedId = -1;
                try
                {
                    const string expectedDesc_F = "Kevin";
                    const string expectedDesc_E = "Echols";
                    var expectedPrinter = IntegrationTestHelper.RandomPrinter(session);
                    repository.Clear();

                    var department = new Department
                    {
                        EnglishDescription = expectedDesc_E,
                        FrenchDescription = expectedDesc_F,
                        Printer = expectedPrinter
                    };
                    /*

                    // Very new unsaved department has no Id
                    Assert.AreEqual(0, department.Id);

                    // Save the department and then verify that it has an Id
                    repository.SaveOrUpdate(department);
                    Assert.IsTrue(department.Id > 0);
                    expectedId = department.Id;
                    repository.Clear();

                    retrievedDepartment = repository.Get<Department>(department.Id);
                    Assert.IsTrue(retrievedDepartment.Id > 0);
                    Assert.AreEqual(expectedId,
                                    retrievedDepartment.Id);
                    Assert.AreEqual(expectedDesc_E, retrievedDepartment.EnglishDescription);
                    Assert.AreEqual(expectedDesc_F, retrievedDepartment.FrenchDescription);
                    Assert.AreEqual(expectedPrinter.Id, retrievedDepartment.Printer.Id);
                    */
                }
                catch (Exception e)
                {
                    // Here to debug exceptions if necessary
                    throw e;
                }
                finally
                {
                    retrievedDepartment = repository.Get<Department>(expectedId);
                    repository.Clear();
                    repository.Delete(retrievedDepartment);
                   //  Assert.IsNull(repository.Get<Department>(retrievedDepartment.Id));
                }
            });
        }
    }
}
