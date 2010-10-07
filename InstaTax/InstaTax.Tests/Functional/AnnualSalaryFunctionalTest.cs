using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InstaTax.Core;
using InstaTax.Core.DomainObjects;
using NUnit.Framework;

namespace InstaTax.Tests.Functional
{
    [TestFixture, Category("FunctionalTest")]
    public class AnnualSalaryFunctionalTest
    {
        [Test]
        public void MustCalculateNetPayableTaxWithAnnualSalaryOtherIncomesAndChapter6Investments()
        {
            var taxPayer = new User(50000, true, Gender.Female);
           

            TaxSlabs ts = TaxSlabs.GetInstance();
            AnnualSalary asal = new AnnualSalary
            {
                Basic = 810000.50,
                Hra = 1000,
                ProfessionalTax = 100,
                SpecialAllowance = 10
            };

            OtherIncomes otherIncomes = new OtherIncomes();
            otherIncomes.Add(new OtherIncomeItem("Income from Interest", 4000.0));
            otherIncomes.Add(new OtherIncomeItem("Income from House Rent", 8000.0));

            Chapter6Investments investments = new Chapter6Investments();
            investments.Add(new LifeInsurance(50000));
            investments.Add(new Elss(60000));

            TaxStatement stmt = new TaxStatement(asal, taxPayer);
            stmt.HousingLoanInterest = new HousingLoanInterest(10000);
            stmt.OtherIncomes = otherIncomes;
            stmt.Chapter6Investments = investments;

            Assert.AreEqual(120473,
                stmt.CalculateNetPayableTax(), 2);
        }
    }
}
