using InstaTax.Core;
using InstaTax.Core.DomainObjects;
using NUnit.Framework;

namespace InstaTax.Tests
{
   [TestFixture, Category("UnitTest")]
    public class TestTaxStatement
    {
        [Test]
        public void MustHaveAnAnnualSalary()
        {
            AnnualSalary asal  = new AnnualSalary();
            TaxStatement stmt = new TaxStatement(asal,null);
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
            TaxStatement stmt = new TaxStatement(asal,taxPayer);
            Assert.AreEqual(1891.05d, stmt.CalculateNetPayableTax());
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
            TaxStatement stmt = new TaxStatement(asal,taxPayer);
            DonationsUnder80G donationsUnder80G = new DonationsUnder80G();
            donationsUnder80G.AddDonation(new FullyExemptDonation(1000));
            stmt.DonationsUnder80G = donationsUnder80G;
            Assert.AreEqual(116700.0, stmt.CalculateNetPayableTax(),0.01);

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
            TaxStatement stmt = new TaxStatement(asal,taxPayer);
            
            OtherIncomes otherIncomes = new OtherIncomes();
            otherIncomes.Add(new OtherIncomeItem("Income from Interest", 4000.0));
            otherIncomes.Add(new OtherIncomeItem("Income from House Rent", 8000.0));
            stmt.OtherIncomes = otherIncomes;

            double totalIncome = asal.GetTaxableSalary() + otherIncomes.CalculateTotalAmount();

            Assert.AreEqual(3091, 
                stmt.CalculateNetPayableTax(),2);
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

            Chapter6Investments investments = new Chapter6Investments();
            investments.Add(new LifeInsurance(50000));
            investments.Add(new Elss(60000));

            TaxStatement stmt = new TaxStatement(asal,taxPayer);
            stmt.OtherIncomes = otherIncomes;
            stmt.Chapter6Investments = investments;

            double totalIncome = ((asal.GetTaxableSalary() + otherIncomes.CalculateTotalAmount())
                                  -
                                  (asal.CalculateHraExemption(taxPayer.FromMetro, taxPayer.RentPaid)));

            double totalInvestments = (asal.Epf + investments.GetTotal());

            totalIncome -= totalInvestments <= Chapter6Investments.Cap
                 ? totalInvestments
                 : Chapter6Investments.Cap;

            Assert.AreEqual(120473, 
                stmt.CalculateNetPayableTax(),2);
        }
    }
}
