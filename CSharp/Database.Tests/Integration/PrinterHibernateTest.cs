using System;
using Kevins.Examples.Database.Common;
using Kevins.Examples.Database.Tests.Integration.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kevins.Examples.Database.Tests.Integration
{
    [TestClass]
    public class PrinterHibernateTest : HibernateTestBase
    {

        [TestMethod]
        public void ShouldCorrectlyMapPrinter()
        {
            WithSession(session => {
                var repository = new DatabaseRepository(session);
                Printer retrievedPrinter = null;
                var expectedId = -1;
                try
                {
                    const string expectedPrinterName = "KevinsPrinter";
                    const string expectedPrinterAlias = "Kevins";
                    const string expectedPrinterNotes = "Kevins test Printer";
                    var printer = new Printer
                    {
                        PrinterNickname = expectedPrinterName,
                        PrinterAlias = expectedPrinterAlias,
                        Notes = expectedPrinterNotes,
                    };
                    // Very new unsaved printer has no Id
                    Assert.AreEqual(0, printer.Id);

                    // Save the printer
                    repository.SaveOrUpdate(printer);
                    Assert.IsTrue(printer.Id > 0);
                    expectedId = printer.Id;
                    repository.Clear();

                    retrievedPrinter = repository.Get<Printer>(printer.Id);
                    Assert.IsTrue(retrievedPrinter.Id > 0);
                    Assert.AreEqual(expectedId, retrievedPrinter.Id);
                    Assert.AreEqual(expectedPrinterName, retrievedPrinter.PrinterNickname);
                    Assert.AreEqual(expectedPrinterAlias, retrievedPrinter.PrinterAlias);
                    Assert.AreEqual(expectedPrinterNotes, retrievedPrinter.Notes);
                }
                catch (Exception e)
                {
                    // Here to debug exceptions if necessary
                    throw e;
                }
                finally
                {
                    retrievedPrinter = repository.Get<Printer>(expectedId);
                    repository.Delete(retrievedPrinter);
                    Assert.IsNull(repository.Get<Printer>(retrievedPrinter.Id));
                }
            });
        }
    }
}
