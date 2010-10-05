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
                                       Basic = 10000.50,
                                       Hra = 1000,
                                       ProfessionalTax = 100,
                                       SpecialAllowance = 10
                                   };
            TaxStatement stmt = new TaxStatement(asal);
            Assert.AreEqual(ts.ComputeTax(asal.GetTaxableSalary(), taxPayer), stmt.CalculateNetPayableTax(taxPayer));
        }

        [Test]
        public void MustCalculateNetPayableTaxWithAnnualSalaryAndDeductions()
        {
            var taxPayer = new User(0, true, Gender.Female);
            TaxSlabs ts = TaxSlabs.GetInstance();
            AnnualSalary asal = new AnnualSalary
                                   {
                                       Basic = 600000,
                                       Hra = 100000
                                   };
            TaxStatement stmt = new TaxStatement(asal);
            DonationsUnder80G donationsUnder80G = new DonationsUnder80G();
            donationsUnder80G.AddDonation(new FullyExemptDonation(1000));
            stmt.DonationsUnder80G = donationsUnder80G;
            Assert.AreEqual(116700.0, stmt.CalculateNetPayableTax(taxPayer),0.01);
        }
        
        [Test]
        public void MustCalculateNetPayableTaxWithAnnualSalaryAndOtherIncomes()
        {
            var taxPayer = new User(50000, true, Gender.Female);
            TaxSlabs ts = TaxSlabs.GetInstance();
            AnnualSalary asal = new AnnualSalary
                                   {
                                       Basic = 10000.50,
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

            Assert.AreEqual(ts.ComputeTax(totalIncome, taxPayer), 
                stmt.CalculateNetPayableTax(taxPayer));
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
                                  (asal.CalculateHraExemption(taxPayer.FromMetro, taxPayer.RentPaid) +
                                   asal.ProfessionalTax));

            double totalInvestments = (asal.Epf + investments.GetTotal());

            totalIncome -= totalInvestments <= Chapter6Investment.Cap
                 ? totalInvestments
                 : Chapter6Investment.Cap;

            Assert.AreEqual(ts.ComputeTax(totalIncome, taxPayer), 
                stmt.CalculateNetPayableTax(taxPayer));
        }
    }
}
