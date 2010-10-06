using System;
using InstaTax.Core;
using InstaTax.Core.DomainObjects;
using NUnit.Framework;

namespace InstaTax.Tests{
    [TestFixture]
    public class ChapterSixDeductionTest{
        [Test]
        public void ShouldReturnZeroDeductionIfInvestmentIsNotAvailableAndEpfIsZero(){
            var salary = new AnnualSalary {Epf = 0.0};
            TaxStatement ts = new TaxStatement(salary);
            Assert.AreEqual(0.0, ts.GetChapter6Deductions(), 0.01);

            salary = new AnnualSalary();
            ts = new TaxStatement(salary) { Chapter6Investments = new Chapter6Investments() };
            Assert.AreEqual(0.0, ts.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnLifeInsuranceInvestmentAsDeductionIfNoOtherInvestmentIsMadeAndEpfIsZero(){
            var investments = new Chapter6Investments();
            investments.Add(new LifeInsurance(30000));
            var salary = new AnnualSalary {Epf = 0};
            TaxStatement ts = new TaxStatement(salary) { Chapter6Investments = investments };
            Assert.AreEqual(30000.0, ts.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnSumOfLifeInsuranceAndElssIfBothAreWithinCapAndOtherInvestmentAreZero(){
            var investments = new Chapter6Investments();
            investments.Add(new LifeInsurance(30000));
            investments.Add(new Elss(60000));

            var salary = new AnnualSalary {Epf = 0};
            TaxStatement ts = new TaxStatement(salary) { Chapter6Investments = investments };
            Assert.AreEqual(90000.0, ts.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void
            ShouldReturnCapValueAsDeductionIfLifeInsuranceAndElssInvestmentsExceedsTheCapAndOtherInvestmentAreZero(){
            var investments = new Chapter6Investments();
            investments.Add(new LifeInsurance(50000));
            investments.Add(new Elss(60000));

            var salary = new AnnualSalary {Epf = 0};
            TaxStatement ts = new TaxStatement(salary) { Chapter6Investments = investments };
            Assert.AreEqual(Chapter6Investments.Cap, ts.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnTotalPpfInvestmentAsDeductionIfPpfInvestmentIsWithinPpfCapAndNoOtherInvestmentIsMade(){
            var investments = new Chapter6Investments();
            const double ppfContribution = 69999.99;
            investments.Add(new PublicProvidentFund(ppfContribution));

            var salary = new AnnualSalary {Epf = 0};
            TaxStatement ts = new TaxStatement(salary) { Chapter6Investments = investments };
            Assert.AreEqual(ppfContribution, ts.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void
            ShouldReturnTotalInvestmentAsDeductionIfLicElssAndPpfInvestmentsAreWithinTheInvestmantCapAndEpfIsZero(){
            var investments = new Chapter6Investments();
            investments.Add(new LifeInsurance(50000.09));
            investments.Add(new Elss(30000));
            investments.Add(new PublicProvidentFund(19999.9));

            var salary = new AnnualSalary {Epf = 0};
            TaxStatement ts = new TaxStatement(salary) { Chapter6Investments = investments };
            Assert.AreEqual(99999.99, ts.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnTotalInvestmentCapAsDeductionIfLicElssAndPpfInvestmentsExceedsTheCapAndEpfIsZero(){
            var investments = new Chapter6Investments();
            investments.Add(new LifeInsurance(50000));
            investments.Add(new Elss(30000));
            investments.Add(new PublicProvidentFund(20000.01));

            var salary = new AnnualSalary {Epf = 0 };
            TaxStatement ts = new TaxStatement(salary) { Chapter6Investments = investments };
            Assert.AreEqual(Chapter6Investments.Cap, ts.GetChapter6Deductions(), 0.01);
        }


        [Test]
        public void ShouldReturnNoDeductionIfEpfIsNotAvailableAndNoOtherInvestmentIsMade(){
            var salary = new AnnualSalary {Epf = 0};
            TaxStatement ts = new TaxStatement(salary);
            Assert.AreEqual(0, ts.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnEpfAsDeductionIfNoOtherInvestmentIsMade(){
            const int epfContribution = 100;
            var salary = new AnnualSalary {Epf = epfContribution};
            TaxStatement ts = new TaxStatement(salary) { Chapter6Investments = null };
            Assert.AreEqual(epfContribution, ts.GetChapter6Deductions(), 0.01);
        }

       [Test]
        public void ShouldReturnTotalInvestmentAsDeductionWhenTotalOfAllInvestmetIsUnderDeductionCap(){
            var investments = new Chapter6Investments();
            investments.Add(new LifeInsurance(50000));
            investments.Add(new Elss(30000));
            investments.Add(new PublicProvidentFund(10000));

            var salary = new AnnualSalary {Epf = 9999.99};
            TaxStatement ts = new TaxStatement(salary) { Chapter6Investments = investments };
            Assert.AreEqual(99999.99, ts.GetChapter6Deductions(), 0.01);
        }


        [Test]
        public void ShouldReturnDeductionCapAsDeductionWhenTotalOfAllInvestmetIsAboveDeductionCap(){
            var investments = new Chapter6Investments();
            investments.Add(new LifeInsurance(50000));
            investments.Add(new Elss(30000));
            investments.Add(new PublicProvidentFund(10000));

            var salary = new AnnualSalary {Epf = 500000};
            TaxStatement ts = new TaxStatement(salary) { Chapter6Investments = investments };
            Assert.AreEqual(Chapter6Investments.Cap, ts.GetChapter6Deductions(), 0.01);
        }


        [Test]
        public void ShouldNotAcceptPpfInvestmentAbovePpfCap(){
            Assert.Throws<ArgumentException>(() => new PublicProvidentFund(70000.01));
        }

        [Test]
        public void ShouldReturnDeductionCapAsDeductionWhenOnlyHousingLoanPrincipalAsInvestementWithEpfAsZeroAndExceedsTheCap()
        {
            var investments = new Chapter6Investments();
            investments.Add(new HousingLoanPrincipal(100001));
            var salary = new AnnualSalary { Epf = 0 };

            TaxStatement ts = new TaxStatement(salary) { Chapter6Investments = investments };

            Assert.AreEqual(Chapter6Investments.Cap, ts.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnDeductionCapAsDeductionWhenOnlyHousingLoanPrincipalAsInvestementWithEpfAndExceedsTheCap()
        {
            var investments = new Chapter6Investments();
            investments.Add(new HousingLoanPrincipal(90001));
            var salary = new AnnualSalary { Epf = 10000 };

            TaxStatement ts = new TaxStatement(salary) { Chapter6Investments = investments };

            Assert.AreEqual(Chapter6Investments.Cap, ts.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnTotalDeductionsAsDeductionWhenOnlyHousingLoanPrincipalAsInvestementWithEpfAndUnderTheCap()
        {
            var investments = new Chapter6Investments();
            investments.Add(new HousingLoanPrincipal(60001));
            var salary = new AnnualSalary { Epf = 10000 };

            TaxStatement ts = new TaxStatement(salary) { Chapter6Investments = investments };

            Assert.AreEqual(70001, ts.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldReturnTotalDeductionsAsDeductionWhenOnlyHousingLoanPrincipalAsInvestementWithEpfAsZeroAndUnderTheCap()
        {
            var investments = new Chapter6Investments();
            investments.Add(new HousingLoanPrincipal(60001));
            var salary = new AnnualSalary { Epf = 0 };

            TaxStatement ts = new TaxStatement(salary) { Chapter6Investments = investments };

            Assert.AreEqual(60001, ts.GetChapter6Deductions(), 0.01);
        }

        [Test]
        public void ShouldSaveTheInvestment(){
            Chapter6InvestmentRepository  chapter6InvestmentRepo=new Chapter6InvestmentRepository();

            Investment LICInvestment = new LifeInsurance(10000);
            chapter6InvestmentRepo.Save(LICInvestment);
            Assert.AreEqual(LICInvestment.GetAmount(), chapter6InvestmentRepo.GetInvestmentDetails(LICInvestment.Id).GetAmount());

        }
    }

    
}