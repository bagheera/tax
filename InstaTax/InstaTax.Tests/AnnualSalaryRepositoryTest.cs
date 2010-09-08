using InstaTax.Core;
using InstaTax.Core.DataAccess;
using InstaTax.Core.DomainObjects;
using NUnit.Framework;

namespace InstaTax.Tests
{
    [TestFixture]
    public class AnnualSalaryRepositoryTest
    {
        [Test]
        public void ShouldBeAbleToSaveSalaryInformation()
        {
            IAnnualSalaryRepository rep = new AnnualSalaryRepository();

            var taxPayer = new User { FromMetro = false, Id = "abcd", RentPaid = 0 };
            var annualSalary = new AnnualSalary()
            {
                TaxPayer = taxPayer,
                Basic = 10000.50,
                Hra = 1000,
                ProfessionalTax = 100,
                SpecialAllowance = 10,
                Repository = rep
            };

            
            rep.SaveAnnualSalary(annualSalary);

            var fetchedSal = rep.GetAnnualSalary(taxPayer);

            Assert.AreEqual(annualSalary.UserId, fetchedSal.UserId);
        }
    }
}
