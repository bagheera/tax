using InstaTax.Core;
using InstaTax.Core.DomainObjects;
using NUnit.Framework;

namespace InstaTax.Tests
{
    [TestFixture]
    public class TestTaxStatement
    {
        [Test]
        public void MustHaveAnAnnualSalary()
        {
            AnnualSalary asal  = new AnnualSalary();
            TaxStatement stmt = new TaxStatement(asal);
            Assert.AreSame(asal, stmt.AnnualSalary);
        } 

        [Test]
        public void MustCalculateNetPayableTaxWithOnlyAnnualSalary()
        {
            var taxPayer = new User(50000, true, Gender.Female);
            TaxSlabs ts = TaxSlabs.GetInstance();
            AnnualSalary asal = new AnnualSalary
                                   {
                                       Basic = 200000.50,
                                       Hra = 1000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            TaxStatement stmt = new TaxStatement(asal);
            Assert.AreEqual(1901.05d, stmt.CalculateNetPayableTax(taxPayer),2);
        }
        
        [Test]
        public void MustCalculateNetPayableTaxWithAnnualSalaryAndOtherIncomes()
        {
            var taxPayer = new User(50000, true, Gender.Female);
            TaxSlabs ts = TaxSlabs.GetInstance();
            AnnualSalary asal = new AnnualSalary
                                   {
                                       Basic = 200000.50,
                                       Hra = 1000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            TaxStatement stmt = new TaxStatement(asal);
            
            OtherIncomes otherIncomes = new OtherIncomes();
            otherIncomes.Add(new OtherIncomeItem("Income from Interest", 4000.0));
            otherIncomes.Add(new OtherIncomeItem("Income from House Rent", 8000.0));
            stmt.OtherIncomes = otherIncomes;

            double totalIncome = asal.GetTaxableSalary() + otherIncomes.CalculateTotalAmount();

            Assert.AreEqual(3101, 
                stmt.CalculateNetPayableTax(taxPayer),2);
        }
        
        [Test]
        public void MustCalculateNetPayableTaxWithAnnualSalaryOtherIncomesAndChapter6Investments()
        {
            var taxPayer = new User(50000, true, Gender.Female);
            TaxSlabs ts = TaxSlabs.GetInstance();
            AnnualSalary asal = new AnnualSalary
                                   {
                                       Basic = 800000.50,
                                       Hra = 1000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            
            OtherIncomes otherIncomes = new OtherIncomes();
            otherIncomes.Add(new OtherIncomeItem("Income from Interest", 4000.0));
            otherIncomes.Add(new OtherIncomeItem("Income from House Rent", 8000.0));

            Chapter6Investment investments = new Chapter6Investment();
            investments.Add(new LifeInsurance(50000));
            investments.Add(new Elss(60000));

            TaxStatement stmt = new TaxStatement(asal);
            stmt.OtherIncomes = otherIncomes;
            stmt.Chapter6Investments = investments;

            double totalIncome = ((asal.GetTaxableSalary() + otherIncomes.CalculateTotalAmount())
                                  -
                                  (asal.CalculateHraExemption(taxPayer.FromMetro, taxPayer.RentPaid)));

            double totalInvestments = (asal.Epf + investments.GetTotal());

            totalIncome -= totalInvestments <= Chapter6Investment.Cap
                 ? totalInvestments
                 : Chapter6Investment.Cap;

            Assert.AreEqual(129803.17, 
                stmt.CalculateNetPayableTax(taxPayer),2);
        }
    }
}