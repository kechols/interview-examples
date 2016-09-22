using System;
using Kevins.Examples.Database.Common;
using Kevins.Examples.Database.Tests.Integration.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.Examples.Database.Tests.Integration
{
    [TestClass]
    public class FacilityHibernateTest : HibernateTestBase
    {

        [TestMethod]
        public void ShouldCorrectlyMapFacility()
        {
            WithSession(session => {
                var repository = new DatabaseRepository(session);
                Facility retrievedFacility = null;
                var expectedFacilityId = -1;
                try
                {
                    const string expectedDesc_F = "Kevin";
                    const string expectedDesc_E = "Echols";
                    const string expectedBillingFacilityNumber = "51515151";
                    const string expectedNotes = "With luck this test will pass!";
                    var facility = new Facility
                    {
                        EnglishDescription = expectedDesc_E,
                        FrenchDescription = expectedDesc_F,
                        BillingFacilityNumber = expectedBillingFacilityNumber,
                        Notes = expectedNotes,
                    };

                    // Very new unsaved facility has no Id
                    Assert.AreEqual(0, facility.Id);

                    // Save the facility and then verify that it has an Id
                    repository.SaveOrUpdate(facility);
                    Assert.IsTrue(facility.Id > 0);
                    expectedFacilityId = facility.Id;
                    repository.Clear();

                    retrievedFacility = repository.Get<Facility>(facility.Id);
                    Assert.IsTrue(retrievedFacility.Id > 0);
                    Assert.AreEqual(expectedFacilityId, retrievedFacility.Id);
                    Assert.AreEqual(expectedDesc_E, retrievedFacility.EnglishDescription);
                    Assert.AreEqual(expectedDesc_F, retrievedFacility.FrenchDescription);
                    Assert.AreEqual(expectedBillingFacilityNumber, retrievedFacility.BillingFacilityNumber);
                    Assert.AreEqual(expectedNotes, retrievedFacility.Notes);
                }
                catch (Exception e)
                {
                    // Here to debug exceptions if necessary
                    throw e;
                }
                finally
                {
                    retrievedFacility = repository.Get<Facility>(expectedFacilityId);
                    repository.Delete(retrievedFacility);
                    Assert.IsNull(repository.Get<Facility>(retrievedFacility.Id));
                }
            });
        }
    }
}
